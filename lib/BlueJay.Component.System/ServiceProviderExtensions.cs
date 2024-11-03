using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Containers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.Component.System
{
  public static class ServiceProviderExtensions
  {
    /// <summary>
    /// Method is meant to add an entity to the entity collection and use DI to build out the object so that
    /// services and other DI components can be injected properly into the class
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="layer">The layer id that should be used when adding the entity</param>
    /// <param name="weight">The current weight of the layer being added</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static IEntity AddEntity(this IServiceProvider provider, string layer = "", int weight = 0)
    {
      return provider.AddEntity(ActivatorUtilities.CreateInstance<Entity>(provider), layer, weight);
    }

    /// <summary>
    /// Method is meant to add an entity to the entity collection and use DI to build out the object so that
    /// services and other DI components can be injected properly into the class
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="entity">The entity we are currently adding to the system</param>
    /// <param name="weight">The current weight of the layer being added</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static IEntity AddEntity(this IServiceProvider provider, IEntity entity, string layer = "", int weight = 0)
    {
      entity.Layer = layer;
      provider.GetRequiredService<ILayerCollection>()
        .Add(entity, layer, weight);
      return entity;
    }

    /// <summary>
    /// Add a global sprit font
    /// </summary>
    /// <param name="provider">The view provider we will use to find the collection and build out the object with</param>
    /// <param name="key">The key to use for this font lookup</param>
    /// <param name="font">The font we are anting to save globally</param>
    public static void AddSpriteFont(this IServiceProvider provider, string key, ISpriteFontContainer font)
    {
      provider.GetRequiredService<IFontCollection>()
        .SpriteFonts[key] = font;
    }

    /// <summary>
    /// Add a global texture font
    /// </summary>
    /// <param name="provider">The view provider we will use to find the collection and build out the object with</param>
    /// <param name="key">The key to use for this font lookup</param>
    /// <param name="font">The font we are wanting to save globally</param>
    public static void AddTextureFont(this IServiceProvider provider, string key, TextureFont font)
    {
      provider.GetRequiredService<IFontCollection>()
        .TextureFonts[key] = font;
    }
  }
}
