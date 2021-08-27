using Antlr4.Runtime;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Keyboard;
using BlueJay.Events.Mouse;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Addons;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Language;
using BlueJay.UI.Component.Language.Antlr;
using BlueJay.UI.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.UI.Component
{
  public static class ServiceProviderExtension
  {
    /// <summary>
    /// Method is meant to add a UI component to the system
    /// </summary>
    /// <typeparam name="T">The current UI component we need to process for the system</typeparam>
    /// <param name="provider">The service provider we need to process the component with</param>
    /// <returns>Will return the generated root element for the component</returns>
    public static IEntity AddUIComponent<T>(this IServiceProvider provider)
      where T : UIComponent
    {
      return ProcessElementNode(provider, provider.GetRequiredService<EventQueue>(), provider.GetRequiredService<GraphicsDevice>(), provider.ParseUIComponet<T>(), null, null);
    }

    public static ElementNode ParseXML(this IServiceProvider serviceProvider, string xml, UIComponent instance, List<Type> components = null)
    {
      var stream = new AntlrInputStream(xml.Trim());
      ITokenSource lexer = new XMLLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new XMLParser(tokens);

      var expr = parser.document();

      var visitor = new XMLParserVisitor(serviceProvider, instance, components);
      visitor.Visit(expr);
      return visitor.Root;
    }

    internal static ExpressionResult ParseExpression(this IServiceProvider serviceProvider, string expression, UIComponent instance)
    {
      var stream = new AntlrInputStream(expression);
      ITokenSource lexer = new ExpressionLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new ExpressionParser(tokens);

      var expr = parser.expr();

      var visitor = new ExpressionVisitor(instance);
      return visitor.Visit(expr) as ExpressionResult;
    }

    internal static ExpressionResult ParseStyle(this IServiceProvider serviceProvider, string expression, UIComponent instance, string name)
    {
      var stream = new AntlrInputStream(expression);
      ITokenSource lexer = new StyleLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new StyleParser(tokens);

      var expr = parser.expr();

      var visitor = new StyleVisitor(serviceProvider, instance, name);
      return visitor.Visit(expr) as ExpressionResult;
    }

    internal static ElementFor ParseFor(this IServiceProvider serviceProvider, string expression, UIComponent instance)
    {
      var stream = new AntlrInputStream(expression);
      ITokenSource lexer = new ForLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new ForParser(tokens);

      var expr = parser.expr();

      var visitor = new ForVisitor(serviceProvider, instance);
      return visitor.Visit(expr) as ElementFor;
    }

    internal static ElementNode ParseUIComponet<T>(this IServiceProvider serviceProvider)
      where T : UIComponent
    {
      return ParseUIComponet(serviceProvider, typeof(T), out var instance);
    }

    internal static ElementNode ParseUIComponet(this IServiceProvider serviceProvider, Type type, out UIComponent instance)
    {
      instance = ActivatorUtilities.CreateInstance(serviceProvider, type) as UIComponent;
      var view = (ViewAttribute)Attribute.GetCustomAttribute(type, typeof(ViewAttribute));
      var components = (ComponentAttribute)Attribute.GetCustomAttribute(type, typeof(ComponentAttribute));

      return ParseXML(serviceProvider, view.XML, instance, components?.Components);
    }

    internal static IEntity ProcessElementNode(IServiceProvider provider, EventQueue eventQueue, GraphicsDevice graphics, ElementNode node, IEntity parent, Dictionary<string, object> scope)
    {
      if (node.For != null && !node.For.Processed)
      {
        node.For.Processed = true;
        foreach(var item in node.For.DataGetter(scope) as IEnumerable)
        {
          var newScope = scope != null ? new Dictionary<string, object>(scope) : new Dictionary<string, object>();
          newScope[node.For.ScopeName] = item;
          ProcessElementNode(provider, eventQueue, graphics, node, parent, newScope);
        }
        return null;
      }

      var styleProcessor = node.Props.FirstOrDefault(x => x.Name == PropNames.Style)?.DataGetter;
      var style = styleProcessor != null ? styleProcessor(scope) as Style : new Style();

      var hoverStyleProcessor = node.Props.FirstOrDefault(x => x.Name == PropNames.HoverStyle)?.DataGetter;
      var hoverStyle = hoverStyleProcessor != null ? hoverStyleProcessor(scope) as Style : new Style();

      IEntity entity = null;
      switch (node.Type)
      {
        case ElementType.Container:
          entity = provider.AddContainer(style, hoverStyle, parent);
          foreach(var evt in node.Events)
          {
            ProcessEvent(provider, evt, entity, scope);
          }
          break;
        case ElementType.Text:
          var txt = node.Props.FirstOrDefault(x => x.Name == PropNames.Text).DataGetter(scope) as string;
          entity = provider.AddText(txt, style, parent);
          if (node.Parent != null)
          {
            foreach (var evt in node.Parent.Events)
            {
              ProcessEvent(provider, evt, entity, scope);
            }
          }
          break;
      }

      if (entity != null)
      {
        foreach (var prop in node.Props)
        {
          if (prop.Name == PropNames.If)
            entity.Active = (bool)prop.DataGetter(scope);

          if (prop.ReactiveProps?.Count > 0)
          {
            foreach (var reactive in prop.ReactiveProps)
            {
              switch (prop.Name)
              {
                case PropNames.Text:
                  reactive.PropertyChanged += (sender, o) =>
                  {
                    var ta = entity.GetAddon<TextAddon>();
                    var txt = prop.DataGetter(scope) as string;
                    ta.Text = txt;
                    entity.Update(ta);
                    eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(graphics.Viewport.Width, graphics.Viewport.Height) });
                  };
                  break;
                case PropNames.Style:
                  reactive.PropertyChanged += (sender, o) =>
                  {
                    var newScope = scope != null ? new Dictionary<string, object>(scope) : new Dictionary<string, object>();
                    newScope[PropNames.Style] = style;
                    prop.DataGetter(newScope);
                    eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(graphics.Viewport.Width, graphics.Viewport.Height) });
                  };
                  break;
                case PropNames.HoverStyle:
                  reactive.PropertyChanged += (sender, o) =>
                  {
                    var newScope = scope != null ? new Dictionary<string, object>(scope) : new Dictionary<string, object>();
                    newScope[PropNames.Style] = style;
                    prop.DataGetter(newScope);
                    eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(graphics.Viewport.Width, graphics.Viewport.Height) });
                  };
                  break;
                case PropNames.If:
                  reactive.PropertyChanged += (sender, o) =>
                  {
                    SetActive(entity, (bool)prop.DataGetter(scope));
                    eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(graphics.Viewport.Width, graphics.Viewport.Height) });
                  };
                  break;
              }
            }
          }
        }
      }

      var isRoot = false;
      if (node.Instance.Root == null)
      {
        node.Instance.Root = entity;
        isRoot = true;
      }

      foreach (var child in node.Children)
        ProcessElementNode(provider, eventQueue, graphics, child, entity, scope);

      if (isRoot)
        node.Instance.Mounted();
      return entity;
    }

    internal static void SetActive(IEntity entity, bool active)
    {
      entity.Active = active;

      var la = entity.GetAddon<LineageAddon>();
      for (var i = 0; i < la.Children.Count; ++i)
        SetActive(la.Children[i], active);
    }

    internal static void ProcessEvent(IServiceProvider provider, ElementEvent evt, IEntity entity, Dictionary<string, object> scope)
    {
      switch (evt.Name)
      {
        case "Select":
          provider.AddEventListener<SelectEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
        case "Blur":
          provider.AddEventListener<BlurEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
        case "Focus":
          provider.AddEventListener<FocusEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
        case "KeyboardUp":
          provider.AddEventListener<KeyboardUpEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
        case "MouseDown":
          provider.AddEventListener<MouseDownEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
        case "MouseMove":
          provider.AddEventListener<MouseMoveEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
        case "MouseUp":
          provider.AddEventListener<MouseUpEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
      }
    }

    internal static bool InvokeEvent<T>(ElementEvent evt, Dictionary<string, object> scope, T obj)
    {
      var newScope = scope != null ? new Dictionary<string, object>(scope) : new Dictionary<string, object>();
      newScope[PropNames.Event] = obj;
      return (bool)evt.Callback(newScope);
    }
  }
}
