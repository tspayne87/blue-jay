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
      var entity = ProcessElementNode(provider, provider.GetRequiredService<IEventQueue>(), provider.GetRequiredService<GraphicsDevice>(), provider.ParseUIComponet<T>(out var instance), null, new ReactiveScope());
      var collection = provider.GetRequiredService<UIComponentCollection>();
      collection.Add(instance);
      return entity;
    }

    /// <summary>
    /// This is used internally to do tests on the xml generator for element nodes
    /// </summary>
    /// <param name="serviceProvider">The service provider we need to process the component with</param>
    /// <param name="xml">The sudo-xml we want to process</param>
    /// <param name="instance">The instance of the root object</param>
    /// <param name="components">The components that can be used in this document</param>
    /// <returns>Will return an element node</returns>
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

    /// <summary>
    /// Helper method that will generate a function based on the expression given
    /// </summary>
    /// <typeparam name="T">The type of object being passed in</typeparam>
    /// <param name="serviceProvider">The service provider</param>
    /// <param name="expression">The expression that needs to be converted into a callback function</param>
    /// <param name="instance">The instance of the object being set</param>
    /// <returns>Will return the function to be called that will process the expression</returns>
    public static Func<ReactiveScope, object> GenerateExpression<T>(this IServiceProvider serviceProvider, string expression, T instance)
      where T : UIComponent
    {
      return serviceProvider.ParseExpression(expression, instance)
        .Callback;
    }


    /// <summary>
    /// Internal method is meant to generate a lambda function based on the expression
    /// </summary>
    /// <param name="serviceProvider">The service provider we need to process the component with</param>
    /// <param name="expression">The expression to build out the lambda function</param>
    /// <param name="instance">The root instance for this expression</param>
    /// <returns>Will return a lambda expression</returns>
    internal static ExpressionResult ParseExpression(this IServiceProvider serviceProvider, string expression, UIComponent instance)
    {
      var stream = new AntlrInputStream(expression);
      ITokenSource lexer = new ExpressionLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new ExpressionParser(tokens);

      var expr = parser.parse();

      var visitor = new ExpressionVisitor(instance);
      return visitor.Visit(expr) as ExpressionResult;
    }

    /// <summary>
    /// Internal method is meant to handle the style expression
    /// </summary>
    /// <param name="serviceProvider">The service provider we need to process the component with</param>
    /// <param name="expression">The style expression that needs to be processed</param>
    /// <returns>Will return the style lambda function</returns>
    internal static ExpressionResult ParseStyle(this IServiceProvider serviceProvider, string expression)
    {
      var stream = new AntlrInputStream(expression);
      ITokenSource lexer = new StyleLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new StyleParser(tokens);

      var expr = parser.expr();

      var visitor = new StyleVisitor(serviceProvider.GetRequiredService<ContentManager>());
      return visitor.Visit(expr) as ExpressionResult;
    }

    /// <summary>
    /// Internal method is meant to handle the for processor
    /// </summary>
    /// <param name="serviceProvider">The service provider we need to process the component with</param>
    /// <param name="expression">The for expression</param>
    /// <param name="instance">The current instance expression</param>
    /// <returns>Will return the for expression</returns>
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

    /// <summary>
    /// Internal method is meant to parse a UI Component
    /// </summary>
    /// <typeparam name="T">The UI Component being generated</typeparam>
    /// <param name="serviceProvider">The service provider we need to process the component with</param>
    /// <returns>The root node of the UI component</returns>
    internal static ElementNode ParseUIComponet<T>(this IServiceProvider serviceProvider, out UIComponent instance)
      where T : UIComponent
    {
      return ParseUIComponet(serviceProvider, typeof(T), out instance);
    }

    /// <summary>
    /// Internal method is meant to parse a UI Component
    /// </summary>
    /// <param name="serviceProvider">The service provider we need to process the component with</param>
    /// <param name="type">The UI Component type being generated</param>
    /// <param name="instance">The current instance expression</param>
    /// <returns>The root node of the UI component</returns>
    internal static ElementNode ParseUIComponet(this IServiceProvider serviceProvider, Type type, out UIComponent instance)
    {
      instance = ActivatorUtilities.CreateInstance(serviceProvider, type) as UIComponent;
      var view = (ViewAttribute)Attribute.GetCustomAttribute(type, typeof(ViewAttribute));
      var components = (ComponentAttribute)Attribute.GetCustomAttribute(type, typeof(ComponentAttribute));

      return ParseXML(serviceProvider, view.XML, instance, components?.Components);
    }

    /// <summary>
    /// Internal method is meant to convert the element node into an UI entity that will be rendered on the screen
    /// </summary>
    /// <param name="provider">The service provider we need to process the component with</param>
    /// <param name="eventQueue">The event queue so we can trigger UI updates when reactive elements change</param>
    /// <param name="graphics">The graphics device so we can get the width and height of the current screen</param>
    /// <param name="node">The current node we are working on</param>
    /// <param name="parent">The current parent element for the node being created so we can keep the same structure</param>
    /// <param name="scope">The current reactive scope for calling the lambda functions</param>
    /// <returns>Will return the generated reactive entity</returns>
    internal static ReactiveEntity ProcessElementNode(IServiceProvider provider, IEventQueue eventQueue, GraphicsDevice graphics, ElementNode node, ReactiveEntity parent, ReactiveScope scope)
    {
      if (!scope.ContainsKey(node.Instance.Identifier))
      {
        scope[node.Instance.Identifier] = node.Instance;
      }

      if (node.For != null && !node.For.Processed)
      {
        node.For.Processed = true;

        var entities = new List<ReactiveEntity>();
        parent.Subscriptions.AddRange(
          scope.Subscribe(x =>
            {
              var madeChange = false;
              var i = -1;
              var pla = parent.GetAddon<LineageAddon>();
              foreach (var item in node.For.DataGetter(scope) as IEnumerable)
              {
                i++;
                if (entities.Count <= i || item != entities[i].Data)
                {
                  madeChange = true;

                  var newScope = new ReactiveScope() { Parent = scope };
                  newScope[node.For.ScopeName] = item;
                  if (entities.Count > i)
                  {
                    var updatedNode = ProcessElementNode(provider, eventQueue, graphics, node, null, newScope);
                    updatedNode.Data = item;

                    var la = updatedNode.GetAddon<LineageAddon>();
                    var oldEntity = entities[i];

                    la.Parent = parent;
                    pla.Children[i] = updatedNode;
                    entities[i] = updatedNode;

                    updatedNode.Update(la);
                    RemoveEntity(provider.GetRequiredService<ILayerCollection>(), oldEntity);
                    continue;
                  }

                  var newNode = ProcessElementNode(provider, eventQueue, graphics, node, node.IsGlobal ? null : parent, newScope);
                  newNode.Data = item;
                  entities.Add(newNode);
                }
              }

              var start = ++i;
              for (; i < entities.Count; ++i)
              {
                madeChange = true;
                RemoveEntity(provider.GetRequiredService<ILayerCollection>(), entities[i]);
              }

              pla.Children.RemoveRange(start, entities.Count - start);
              entities.RemoveRange(start, entities.Count - start);
              parent.Update(pla);

              if (madeChange)
              {
                eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(graphics.Viewport.Width, graphics.Viewport.Height) });
              }
            }, node.For.ScopePaths)
        );
        return null;
      }

      ReactiveEntity entity = null;
      switch (node.Type)
      {
        case ElementType.Container:
          entity = provider.AddContainer(ActivatorUtilities.CreateInstance<ReactiveEntity>(provider), style: new Style(), parent: node.IsGlobal ? null : parent) as ReactiveEntity;
          foreach(var evt in node.Events)
          {
            ProcessEvent(provider, evt, entity, scope);
          }
          break;
        case ElementType.Text:
          entity = provider.AddText(ActivatorUtilities.CreateInstance<ReactiveEntity>(provider), string.Empty, parent: node.IsGlobal ? null : parent) as ReactiveEntity;
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
        entity.Node = node;
        entity.Scope = scope;
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
      {
        node.Instance.Mounted();
        node.Instance.ProcessWatch();
      }
      return entity;
    }

    /// <summary>
    /// Internal processor to add the event listeners to an entity for when events are triggered
    /// </summary>
    /// <param name="provider">The service provider we need to process the component with</param>
    /// <param name="evt">The event property we need to parse</param>
    /// <param name="entity">The current entity we need to watch on</param>
    /// <param name="scope">The current scope</param>
    internal static void ProcessEvent(IServiceProvider provider, ElementEvent evt, IEntity entity, ReactiveScope scope)
    {
      switch (evt.Name)
      {
        case "Select":
          provider.AddEventListener<SelectEvent>(x => ElementHelper.InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
        case "Blur":
          provider.AddEventListener<BlurEvent>(x => ElementHelper.InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
        case "Focus":
          provider.AddEventListener<FocusEvent>(x => ElementHelper.InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
        case "KeyboardUp":
          provider.AddEventListener<KeyboardUpEvent>(x => ElementHelper.InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
        case "MouseDown":
          provider.AddEventListener<MouseDownEvent>(x => ElementHelper.InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
        case "MouseMove":
          provider.AddEventListener<MouseMoveEvent>(x => ElementHelper.InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
        case "MouseUp":
          provider.AddEventListener<MouseUpEvent>(x => ElementHelper.InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity);
          break;
      }
    }

    /// <summary>
    /// Internal recursive method is meant to remove entities from the system
    /// </summary>
    /// <param name="layers">The layer collection to remove the entity from</param>
    /// <param name="entity">The current entity needing to be removed</param>
    internal static void RemoveEntity(ILayerCollection layers, IEntity entity)
    {
      // Remove the entity
      layers[entity.Layer].Entities.Remove(entity);
      entity.Dispose();

      // Remove all children
      var la = entity.GetAddon<LineageAddon>();
      foreach (var child in la.Children)
        RemoveEntity(layers, child);
    }
  }
}
