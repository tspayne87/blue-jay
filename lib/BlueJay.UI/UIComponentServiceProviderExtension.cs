using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.UI.Addons;
using BlueJay.UI.Components;
using BlueJay.UI.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace BlueJay.UI
{
  public static class UIComponentServiceProviderExtension
  {
    private static Regex ExpressionRegex = new Regex(@"{{([^}]+)}}");

    public static IServiceProvider AddUIComponent<T>(this IServiceProvider provider, params object[] parameters)
    {
      var view = (ViewAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(ViewAttribute));
      var collection = provider.GetRequiredService<UIComponentCollection>();
      if (view != null && view.View.ChildNodes.Count == 1)
      {
        var component = ActivatorUtilities.CreateInstance<T>(provider, parameters);
        GenerateItem(view.View.ChildNodes[0], provider, component);
        collection.Add(component);
      }

      return provider;
    }

    private static IServiceProvider AddUIComponent(IServiceProvider provider, Type type, params object[] parameters)
    {
      var view = (ViewAttribute)Attribute.GetCustomAttribute(type, typeof(ViewAttribute));
      var collection = provider.GetRequiredService<UIComponentCollection>();
      if (view != null && view.View.ChildNodes.Count == 1)
      {
        var component = ActivatorUtilities.CreateInstance(provider, type, parameters);
        GenerateItem(view.View.ChildNodes[0], provider, component);
        collection.Add(component);
      }

      return provider;
    }

    private static void GenerateItem<T>(XmlNode node, IServiceProvider provider, T component, IEntity parent = null)
    {
      var contentManager = provider.GetRequiredService<ContentManager>();
      var style = node.Attributes?["style"]?.GenerateStyle(contentManager);
      var hoverStyle = node.Attributes?["hoverStyle"]?.GenerateStyle(contentManager);
      IEntity entity = null;
      switch(node.Name)
      {
        case "container":
          entity = GenerateContainer(provider, style, hoverStyle, parent);
          break;
        case "text":
          entity = GenerateText(node, provider, style, component, parent);
          break;
        default:
          break;
      }

      if (entity != null)
      {
        var onClick = node.Attributes == null ? null : node.Attributes["onSelect"]?.InnerText;
        if (!string.IsNullOrEmpty(onClick) && entity != null)
        {
          var method = typeof(T).GetMethod(onClick);
          provider.AddEventListener<SelectEvent>(x => (bool)method.Invoke(component, new object[] { x }), entity);
        }

        for (var i = 0; i < node.ChildNodes.Count; ++i)
        {
          GenerateItem(node.ChildNodes[i], provider, component, entity);
        }
      }
    }

    private static IEntity GenerateContainer(IServiceProvider provider, Style style, Style hoverStyle, IEntity parent)
    {
      if (style == null && hoverStyle == null) return provider.AddContainer(new Style(), parent);
      else if (hoverStyle != null) return provider.AddContainer(style, hoverStyle, parent);
      return provider.AddContainer(style, parent);
    }

    private static IEntity GenerateText(XmlNode node, IServiceProvider provider, Style style, object component, IEntity parent)
    {
      var txt = node.InnerText;
      var field = component.GetType().GetField(node.InnerText.Replace("{{", string.Empty).Replace("}}", string.Empty));
      if (ExpressionRegex.IsMatch(txt))
      {
        var eventQueue = provider.GetRequiredService<EventQueue>();
        var graphics = provider.GetRequiredService<GraphicsDevice>();

        var entity = provider.AddText(ExpressionRegex.TranslateText(txt, component), style, parent);
        foreach (var prop in ExpressionRegex.GetReactiveProps(txt, component))
        {
          prop.PropertyChanged += (sender, o) =>
          {
            var ta = entity.GetAddon<TextAddon>();
            if (ta != null)
            {
              ta.Text = ExpressionRegex.TranslateText(txt, component);
              eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(graphics.Viewport.Width, graphics.Viewport.Height) });
            }
          };
        }
        return entity;
      }
      return provider.AddText(txt, style, parent);
    }
  }
}
