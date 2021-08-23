using Antlr4.Runtime;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events.Keyboard;
using BlueJay.Events.Mouse;
using BlueJay.UI.Component.Language;
using BlueJay.UI.Component.Language.Antlr;
using BlueJay.UI.Factories;
using Microsoft.Extensions.DependencyInjection;
using System;
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
      return ProcessElementNode(provider, provider.ParseUIComponet<T>(), null);
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

    internal static ExpressionResult ParseStyle(this IServiceProvider serviceProvider, string expression, UIComponent instance)
    {
      var stream = new AntlrInputStream(expression);
      ITokenSource lexer = new StyleLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new StyleParser(tokens);

      var expr = parser.expr();

      var visitor = new StyleVisitor(serviceProvider, instance);
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

    internal static IEntity ProcessElementNode(IServiceProvider provider, ElementNode node, IEntity parent)
    {
      var style = new Style();
      var processor = node.Props.FirstOrDefault(x => x.Name == PropNames.Style)?.DataGetter;
      style = processor != null ? processor(null) as Style : new Style();

      IEntity entity = null;
      switch (node.Type)
      {
        case ElementType.Container:
          entity = provider.AddContainer(style, parent);
          provider.AddEventListener<SelectEvent>(x => InvokeEvent("Select", x, node.Events), entity);
          provider.AddEventListener<BlurEvent>(x => InvokeEvent("Blur", x, node.Events), entity);
          provider.AddEventListener<FocusEvent>(x => InvokeEvent("Focus", x, node.Events), entity);
          provider.AddEventListener<KeyboardUpEvent>(x => InvokeEvent("KeyboardUp", x, node.Events), entity);
          provider.AddEventListener<MouseDownEvent>(x => InvokeEvent("MouseDown", x, node.Events), entity);
          provider.AddEventListener<MouseMoveEvent>(x => InvokeEvent("MouseMove", x, node.Events), entity);
          provider.AddEventListener<MouseUpEvent>(x => InvokeEvent("MouseUp", x, node.Events), entity);
          break;
        case ElementType.Text:
          var txt = node.Props.FirstOrDefault(x => x.Name == PropNames.Text).DataGetter(null) as string;
          entity = provider.AddText(txt, style, parent);
          if (node.Parent != null)
          {
            provider.AddEventListener<SelectEvent>(x => InvokeEvent("Select", x, node.Parent.Events), entity);
            provider.AddEventListener<BlurEvent>(x => InvokeEvent("Blur", x, node.Parent.Events), entity);
            provider.AddEventListener<FocusEvent>(x => InvokeEvent("Focus", x, node.Parent.Events), entity);
            provider.AddEventListener<KeyboardUpEvent>(x => InvokeEvent("KeyboardUp", x, node.Parent.Events), entity);
            provider.AddEventListener<MouseDownEvent>(x => InvokeEvent("MouseDown", x, node.Parent.Events), entity);
            provider.AddEventListener<MouseMoveEvent>(x => InvokeEvent("MouseMove", x, node.Parent.Events), entity);
            provider.AddEventListener<MouseUpEvent>(x => InvokeEvent("MouseUp", x, node.Parent.Events), entity);
          }
          break;
      }

      foreach (var child in node.Children)
        ProcessElementNode(provider, child, entity);
      return entity;
    }

    internal static bool InvokeEvent<T>(string eventName, T data, List<ElementEvent> events)
    {
      var evt = events.FirstOrDefault(x => x.Name == eventName);
      if (evt != null) return (bool)evt.Callback(data);
      return true;
    }
  }
}
