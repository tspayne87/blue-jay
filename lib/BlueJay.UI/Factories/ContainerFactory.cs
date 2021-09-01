using BlueJay.Common.Addons;
using BlueJay.Component.System;
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
    /// <returns>Will return the entity so it can be used as a parent down the line</returns>
    public static T AddContainer<T>(this IServiceProvider provider)
      where T : IEntity
    {
      return provider.AddContainer<T>(new Style(), null);
    }

    /// <summary>
    /// Method is meant to create a container for a collection of nodes
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="style">The style that should be used for this container</param>
    /// <returns>Will return the entity so it can be used as a parent down the line</returns>
    public static T AddContainer<T>(this IServiceProvider provider, Style style)
      where T : IEntity
    {
      return provider.AddContainer<T>(style, null);
    }

    /// <summary>
    /// Method is meant to create a container for a collection of nodes
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="style">The style that should be used for this container</param>
    /// <param name="parent">The parent that should be attached to this parent</param>
    /// <returns>Will return the entity so it can be used as a parent down the line</returns>
    public static T AddContainer<T>(this IServiceProvider provider, Style style, IEntity parent)
      where T : IEntity
    {
      var entity = provider.AddUIEntity<T>(parent);
      entity.Active = parent?.Active ?? true;
      entity.Add(new StyleAddon(style));
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
    /// <param name="style">The style that should be used for this container</param>
    /// <param name="hoverStyle">The style that should be used when the style addon is in the hover state</param>
    /// <param name="parent">The parent that should be attached to this parent</param>
    /// <returns>Will return the entity so it can be used as a parent down the line</returns>
    public static T AddContainer<T>(this IServiceProvider provider, Style style, Style hoverStyle, IEntity parent)
      where T : IEntity
    {
      var entity = provider.AddUIEntity<T>(parent);
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
