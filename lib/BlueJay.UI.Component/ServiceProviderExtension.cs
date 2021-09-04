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
using BlueJay.UI.Component.Reactivity;
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

    internal static ReactiveEntity ProcessElementNode(IServiceProvider provider, EventQueue eventQueue, GraphicsDevice graphics, ElementNode node, ReactiveEntity parent, ReactiveScope scope)
    {
      scope = scope?.NewScope() ?? new ReactiveScope();
      scope[PropNames.Identifier] = node.Instance;

      if (node.For != null && !node.For.Processed)
      {
        node.For.Processed = true;

        var entities = new List<ReactiveEntity>();
        parent.DisposableEvents.AddRange(
          scope.Subscribe(x =>
            {
              var madeChange = false;
              var i = 0;
              foreach (var item in node.For.DataGetter(scope) as IEnumerable)
              {
                if (entities.Count <= i || item != entities[i].Data)
                {
                  madeChange = true;

                  var newScope = scope.NewScope();
                  newScope[node.For.ScopeName] = item;
                  if (entities.Count > i)
                  {
                    entities[i].Scope = newScope;
                    entities[i].Data = item;
                    continue;
                  }

                  var newNode = ProcessElementNode(provider, eventQueue, graphics, node, node.IsGlobal ? null : parent, newScope);
                  newNode.Data = item;
                  entities.Add(newNode);
                }
                i++;
              }
              // TODO: Need to add function to destory an entity

              if (madeChange)
              {
                eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(graphics.Viewport.Width, graphics.Viewport.Height) });
              }
            }, node.For.ScopePaths)
          );
        return null;
      }

      var styleProcessor = node.Props.FirstOrDefault(x => x.Name == PropNames.Style)?.DataGetter;
      var style = styleProcessor != null ? styleProcessor(scope) as Style : new Style();

      var hoverStyleProcessor = node.Props.FirstOrDefault(x => x.Name == PropNames.HoverStyle)?.DataGetter;
      var hoverStyle = hoverStyleProcessor != null ? hoverStyleProcessor(scope) as Style : new Style();

      ReactiveEntity entity = null;
      switch (node.Type)
      {
        case ElementType.Container:
          entity = provider.AddContainer<ReactiveEntity>(style, hoverStyle, node.IsGlobal ? null : parent);
          foreach(var evt in node.Events)
          {
            ProcessEvent(provider, evt, entity, scope);
          }
          break;
        case ElementType.Text:
          var txt = node.Props.FirstOrDefault(x => x.Name == PropNames.Text).DataGetter(scope) as string;
          entity = provider.AddText<ReactiveEntity>(txt, style, node.IsGlobal ? null : parent);
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
        entity.Scope = scope;
        entity.Node = node;
        entity.ProcessProperties();
      }

      foreach(var reference in node.Refs)
      {
        var fieldProp = node.Instance.GetType().GetField(reference.PropName);
        if (fieldProp != null) fieldProp.SetValue(node.Instance, entity);

        var prop = node.Instance.GetType().GetProperty(reference.PropName);
        if (prop != null) prop.SetValue(node.Instance, entity);
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

    internal static void ProcessEvent(IServiceProvider provider, ElementEvent evt, IEntity entity, ReactiveScope scope)
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

    internal static bool InvokeEvent<T>(ElementEvent evt, ReactiveScope scope, T obj)
    {
      var newScope = scope != null ? scope.NewScope() : new ReactiveScope();
      newScope[PropNames.Event] = obj;
      return (bool)evt.Callback(newScope);
    }
  }
}
