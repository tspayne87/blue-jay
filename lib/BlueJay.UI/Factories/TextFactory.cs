using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using System;

namespace BlueJay.UI.Factories
{
  public static class TextFactory
  {
    /// <summary>
    /// Method is meant to add a text node to the ui elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="text">The text that should be used to render for this node</param>
    public static T AddText<T>(this IServiceProvider provider, string text)
      where T : IEntity
    {
      return provider.AddText<T>(text, new Style(), null);
    }

    /// <summary>
    /// Method is meant to add a text node to the ui elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="text">The text that should be used to render for this node</param>
    /// <param name="style">The styles that should be included on this text node</param>
    public static T AddText<T>(this IServiceProvider provider, string text, Style style)
      where T : IEntity
    {
      return provider.AddText<T>(text, style, null);
    }

    /// <summary>
    /// Method is meant to add a text node to the ui elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="text">The text that should be used to render for this node</param>
    /// <param name="parent">The parent this text node should have</param>
    public static T AddText<T>(this IServiceProvider provider, string text, IEntity parent)
      where T : IEntity
    {
      return provider.AddText<T>(text, new Style(), parent);
    }

    /// <summary>
    /// Method is meant to add a text node to the ui elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="text">The text that should be used to render for this node</param>
    /// <param name="style">The styles that should be included on this text node</param>
    /// <param name="parent">The parent this text node should have</param>
    public static T AddText<T>(this IServiceProvider provider, string text, Style style, IEntity parent)
      where T : IEntity
    {
      var entity = provider.AddUIEntity<T>(parent);
      entity.Active = parent?.Active ?? true;
      entity.Add(new StyleAddon(style));
      entity.Add(new TextAddon(text));
      entity.Add(new TextureAddon());
      entity.Add(new PositionAddon());
      entity.Add(new ColorAddon(Color.Black));
      entity.Add(new BoundsAddon());

      return entity;
    }
  }
}
