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
    /// <param name="style">The styles that should be included on this text node</param>
    /// <param name="parent">The parent this text node should have</param>
    public static IEntity AddText(this IServiceProvider provider, string text, Style style = default, IEntity parent = default)
    {
      var entity = provider.AddUIEntity(parent);
      entity.Active = parent?.Active ?? true;
      entity.Add(new StyleAddon(style));
      entity.Add(new TextAddon(text));
      entity.Add(new TextureAddon());
      entity.Add(new PositionAddon());
      entity.Add(new ColorAddon(Color.Black));
      entity.Add(new BoundsAddon());

      return entity;
    }

    /// <summary>
    /// Method is meant to add a text node to the ui elements
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="entity">The entity being added into the system</param>
    /// <param name="text">The text that should be used to render for this node</param>
    /// <param name="style">The styles that should be included on this text node</param>
    /// <param name="parent">The parent this text node should have</param>
    public static IEntity AddText(this IServiceProvider provider, IEntity entity, string text, Style style = default, IEntity parent = default)
    {
      provider.AddUIEntity(entity, parent);
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
