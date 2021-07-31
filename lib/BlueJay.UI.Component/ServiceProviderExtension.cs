﻿using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace BlueJay.UI.Component
{
  public static class ServiceProviderExtension
  {
    /// <summary>
    /// The set of global objects defined by the project
    /// </summary>
    internal static List<Type> Globals = new List<Type>() { typeof(Container), typeof(Slot) };

    /// <summary>
    /// The expression regex to process the text and convert the expressions into data from the component
    /// </summary>
    internal static Regex ExpressionRegex = new Regex(@"{{([^}]+)}}");

    /// <summary>
    /// Method is meant to add a UI component to the system
    /// </summary>
    /// <typeparam name="T">The current UI component we need to process for the system</typeparam>
    /// <param name="provider">The service provider we need to process the component with</param>
    /// <returns>Will return the generated root element for the component</returns>
    public static IEntity AddUIComponent<T>(this IServiceProvider provider)
    {
      return AddUIComponent(provider, typeof(T));
    }

    /// <summary>
    /// Internal add ui component that is meant to take a basic type on translate that type to a component
    /// </summary>
    /// <param name="provider">The service provider we need to process the component with</param>
    /// <param name="type">The current UI component we need to process for the system</param>
    /// <returns>Will return the generated root element for the component</returns>
    internal static IEntity AddUIComponent(IServiceProvider provider, Type type)
    {
      var view = (ViewAttribute)Attribute.GetCustomAttribute(type, typeof(ViewAttribute));
      var components = (ComponentAttribute)Attribute.GetCustomAttribute(type, typeof(ComponentAttribute));
      var collection = provider.GetRequiredService<UIComponentCollection>();
      IEntity entity = null;
      if (view != null && view.View.ChildNodes.Count == 1)
      {
        var instance = (UIComponent)ActivatorUtilities.CreateInstance(provider, type);
        instance.Initialize(view.View.ChildNodes[0], null, null);
        entity = GenerateItem(view.View.ChildNodes[0], provider, Globals.Concat(components?.Components ?? new List<Type>()), instance, null);
        collection.Add(instance);
      }

      return entity;
    }

    /// <summary>
    /// Internal add ui component that is meant to take a basic type on translate that type to a component
    /// </summary>
    /// <param name="provider">The service provider we need to process the component with</param>
    /// <param name="type">The current UI component we need to process for the system</param>
    /// <param name="parentInstance">The parent instance we are processing</param>
    /// <param name="node">The current node we are processing for the component type</param>
    /// <returns>Will return the generated root element for the component</returns>
    internal static IEntity AddUIComponent(IServiceProvider provider, Type type, UIComponent parentInstance, XmlNode node, IEntity parent = null)
    {
      var view = (ViewAttribute)Attribute.GetCustomAttribute(type, typeof(ViewAttribute));
      var components = (ComponentAttribute)Attribute.GetCustomAttribute(type, typeof(ComponentAttribute));
      var collection = provider.GetRequiredService<UIComponentCollection>();
      IEntity entity = null;
      if (view != null && view.View.ChildNodes.Count == 1)
      {
        var instance = (UIComponent)ActivatorUtilities.CreateInstance(provider, type);
        instance.Initialize(node, parentInstance, null);
        entity = GenerateItem(view.View.ChildNodes[0], provider, Globals.Concat(components?.Components ?? new List<Type>()), instance, parentInstance, parent);
        collection.Add(instance);
      }

      return entity;
    }

    /// <summary>
    /// Helper method is meant to generate an item based on the xml node and the instance of the component
    /// </summary>
    /// <param name="node">The xml node representing the UI component being generated</param>
    /// <param name="provider">The service provider we need to process the component with</param>
    /// <param name="components">The components that are used in the xml</param>
    /// <param name="instance">The current UI component instance we need to process</param>
    /// <param name="parentInstance">The parent instance of the UI component</param>
    /// <param name="parent">The current parent we are processing</param>
    /// <returns>Will return the generated root entity based on the xml</returns>
    internal static IEntity GenerateItem(XmlNode node, IServiceProvider provider, IEnumerable<Type> components, UIComponent instance, UIComponent parentInstance, IEntity parent = null)
    {
      IEntity entity = null;
      var componentType = components.FirstOrDefault(x => x.Name.Equals(node.Name.Replace("-", ""), StringComparison.OrdinalIgnoreCase));
      if (node.Name == "#text") componentType = typeof(Text);
      if (componentType != null)
      {
        var processChildren = false;
        if (Attribute.GetCustomAttribute(componentType, typeof(ViewAttribute)) != null)
        {
          entity = AddUIComponent(provider, componentType, instance, node, parent);
        }
        else
        {
          var component = (UIComponent)ActivatorUtilities.CreateInstance(provider, componentType);
          component.Initialize(node, instance, parentInstance);
          entity = component.Render(parent);
          processChildren = true;
        }

        if (instance.Root == null)
        {
          instance.Root = entity;
        }
        
        // Handle Style updates
        if (entity != null)
        {
          HandleStyle(node, instance, entity, provider, "style");
          HandleStyle(node, instance, entity, provider, "hoverStyle");
        }

        // Add this ui component as a system if it needs to be added
        var eventQueue = provider.GetRequiredService<EventQueue>();

        // If this is an update system we need to add an event listener to the queue
        if (instance is IUpdateSystem || instance is IUpdateEntitySystem || instance is IUpdateEndSystem)
          eventQueue.AddEventListener(ActivatorUtilities.CreateInstance<UpdateEventListener>(provider, new object[] { instance }));

        // If this is a draw system we need to add an event listener to the queue
        if (instance is IDrawSystem || instance is IDrawEntitySystem || instance is IDrawEndSystem)
          eventQueue.AddEventListener(ActivatorUtilities.CreateInstance<DrawEventListener>(provider, new object[] { instance }));

        if (processChildren)
        {
          for (var i = 0; i < node.ChildNodes.Count; ++i)
          {
            GenerateItem(node.ChildNodes[i], provider, components, instance, parentInstance, entity);
          }
        }
      }
      return entity;
    }

    private static void HandleStyle(XmlNode node, UIComponent current, IEntity entity, IServiceProvider provider, string attrType)
    {
      if (node.Attributes != null && node.Attributes[attrType] != null) {
        var contentManager = provider.GetRequiredService<ContentManager>();
        Style style = null;

        var text = node.Attributes[attrType].InnerText;
        if (ExpressionRegex.IsMatch(text))
        {
          style = ExpressionRegex.TranslateText(text, current).GenerateStyle(contentManager);

          var eventQueue = provider.GetRequiredService<EventQueue>();
          var graphics = provider.GetRequiredService<GraphicsDevice>();
          foreach(var prop in ExpressionRegex.GetReactiveProps(text, current))
          {
            prop.PropertyChanged += (sender, o) =>
            {
              var updateStyle = ExpressionRegex.TranslateText(text, current).GenerateStyle(contentManager);
              eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(graphics.Viewport.Width, graphics.Viewport.Height) });
            };
          }
        }
        else
        {
          style = text.GenerateStyle(contentManager);
        }

        if (style != null)
        {
          var sa = entity.GetAddon<StyleAddon>();

          if (attrType == "style")
          {
            style.Parent = sa.Style;
            sa.Style = style;
          }

          if (attrType == "hoverStyle")
          {
            style.Parent = sa.HoverStyle;
            sa.HoverStyle = style;
          }

          entity.Update(sa);
        }
      }
    }
  }
}
