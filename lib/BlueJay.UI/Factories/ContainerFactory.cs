using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using System;

namespace BlueJay.UI.Factories
{
  public static class ContainerFactory
  {
    /// <summary>
    /// Method is meant to create a container for a collection of nodes
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="style">The style that should be used for this container</param>
    /// <param name="hoverStyle">The style that should be used when the style addon is in the hover state</param>
    /// <param name="parent">The parent that should be attached to this parent</param>
    /// <returns>Will return the entity so it can be used as a parent down the line</returns>
    public static IEntity AddContainer(this IServiceProvider provider, Style style = default, Style hoverStyle = default, IEntity parent = null)
    {
      var entity = provider.AddUIEntity(parent);
      entity.Active = parent?.Active ?? true;
      entity.Add(new StyleAddon(style, hoverStyle));
      entity.Add(new TextureAddon());
      entity.Add(new PositionAddon());
      entity.Add(new ColorAddon());
      entity.Add(new BoundsAddon());
      return entity;
    }

    /// <summary>
    /// Method is meant to create a container for a collection of nodes
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="entity">The entity that was created and should be used</param>
    /// <param name="style">The style that should be used for this container</param>
    /// <param name="hoverStyle">The style that should be used when the style addon is in the hover state</param>
    /// <param name="parent">The parent that should be attached to this parent</param>
    /// <returns>Will return the entity so it can be used as a parent down the line</returns>
    public static IEntity AddContainer(this IServiceProvider provider, IEntity entity, Style style = default, Style hoverStyle = default, IEntity parent = null)
    {
      provider.AddUIEntity(entity, parent);
      entity.Active = parent?.Active ?? true;
      entity.Add(new StyleAddon(style, hoverStyle));
      entity.Add(new TextureAddon());
      entity.Add(new PositionAddon());
      entity.Add(new ColorAddon());
      entity.Add(new BoundsAddon());
      return entity;
    }
  }
}
