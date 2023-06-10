using Antlr4.Runtime;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Common.Events.Keyboard;
using BlueJay.Common.Events.Mouse;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Language;
using BlueJay.UI.Component.Language.Antlr;
using BlueJay.UI.Component.Reactivity;
using BlueJay.UI.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections;
using System.Collections.Generic;
using BlueJay.UI.Events;
using BlueJay.UI.Component.Nodes;
using BlueJay.UI.Component.Interactivity.Dropdown;

namespace BlueJay.UI.Component
{
  public static class ServiceProviderExtension
  {
    public static void AttachComponent<T>(this IServiceProvider provider, Style? globalStyle = null)
      where T: UIComponent
    {
      var collection = provider.GetRequiredService<UIComponentCollection>();

      var node = provider.ParseJayTML<T>(null);
      if (node != null)
      {
        collection.Add(node.UIComponent);
        node.Initialize();
        node.GenerateUI(globalStyle);
      }
    }

    public static INode? ParseJayTML<T>(this IServiceProvider provider, UIComponent? parentComponent)
      where T : UIComponent
    {
      return ParseJayTML(provider, typeof(T), parentComponent);
    }

    public static INode? ParseJayTML(this IServiceProvider provider, Type type, UIComponent? parentComponent)
    {
      var view = Attribute.GetCustomAttribute(type, typeof(ViewAttribute)) as ViewAttribute;
      return ParseJayTML(provider, view?.XML ?? String.Empty, type, parentComponent);
    }

    public static INode? ParseJayTML<T>(this IServiceProvider provider, string xml, UIComponent? parentComponent)
      where T : UIComponent
    {
      return ParseJayTML(provider, xml, typeof(T), parentComponent);
    }

    public static INode? ParseJayTML(this IServiceProvider provider, string xml, Type type, UIComponent? parentComponent)
    {
      var components = Attribute.GetCustomAttribute(type, typeof(ComponentAttribute)) as ComponentAttribute;
      var stream = new AntlrInputStream(xml.Trim());
      ITokenSource lexer = new JayTMLLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new JayTMLParser(tokens);

      var expr = parser.document();

      var instance = ActivatorUtilities.CreateInstance(provider, type) as UIComponent;
      if (instance == null)
        throw new ArgumentNullException("Could not create component instance");

      var visitor = new JayTMLVisitor(provider, instance, components?.Components ?? new List<Type>());
      var result = visitor.Visit(expr);

      return result as INode;
    }
  }
}
