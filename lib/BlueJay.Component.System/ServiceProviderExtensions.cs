﻿using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Containers;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.Component.System
{
  public static class ServiceProviderExtensions
  {
    /// <summary>
    /// The next id to be used for the entity
    /// </summary>
    private static long _nextId = 0;

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
      var entity = ActivatorUtilities.CreateInstance<Entity>(provider);
      entity.Id = Interlocked.Increment(ref _nextId);
      return provider.AddEntity(entity, layer, weight);
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
      provider.GetRequiredService<ILayers>()
        .Add(entity, layer, weight);
      return entity;
    }

    /// <summary>
    /// Method is meant to remove the entity from the layer collection
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="entity">The entity we would like to remove</param>
    public static void RemoveEntity(this IServiceProvider provider, IEntity entity)
    {
      provider.GetRequiredService<ILayers>()
        .Remove(entity);
      entity.Dispose();
    }

    /// <summary>
    /// Method is meant to remove all entities from the system or a specific layer
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="layer">The layer to remove data from</param>
    public static void ClearEntities(this IServiceProvider provider, string? layer = null)
    {
      var collection = provider.GetRequiredService<ILayers>();
      if (string.IsNullOrWhiteSpace(layer))
      {
        collection.Clear();
        return;
      }
      if (collection.Contains(layer))
        collection[layer]!.Clear();
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
