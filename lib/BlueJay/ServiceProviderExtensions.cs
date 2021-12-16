using System;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using BlueJay.Events.Lifecycle;

namespace BlueJay
{
  public static class ServiceProviderExtensions
  {
    /// <summary>
    /// Method is meant to add an entity to the entity collection and use DI to build out the object so that
    /// services and other DI components can be injected properly into the class
    /// </summary>
    /// <typeparam name="T">The current object we are adding to the entity collection</typeparam>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="parameters">The constructor parameters that do not exists in DI</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static T AddEntity<T>(this IServiceProvider provider, params object[] parameters)
      where T : IEntity
    {
      return provider.AddEntity<T>(string.Empty, 0, parameters);
    }

    /// <summary>
    /// Method is meant to add an entity to the entity collection and use DI to build out the object so that
    /// services and other DI components can be injected properly into the class
    /// </summary>
    /// <typeparam name="T">The current object we are adding to the entity collection</typeparam>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="layer">The layer id that should be used when adding the entity</param>
    /// <param name="parameters">The constructor parameters that do not exists in DI</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static T AddEntity<T>(this IServiceProvider provider, string layer, params object[] parameters)
      where T : IEntity
    {
      return provider.AddEntity<T>(layer, 0, parameters);
    }

    /// <summary>
    /// Method is meant to add an entity to the entity collection and use DI to build out the object so that
    /// services and other DI components can be injected properly into the class
    /// </summary>
    /// <typeparam name="T">The current object we are adding to the entity collection</typeparam>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="layer">The layer id that should be used when adding the entity</param>
    /// <param name="weight">The current weight of the layer being added</param>
    /// <param name="parameters">The constructor parameters that do not exists in DI</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static T AddEntity<T>(this IServiceProvider provider, string layer, int weight, params object[] parameters)
      where T : IEntity
    {
      var item = ActivatorUtilities.CreateInstance<T>(provider, parameters);
      item.Layer = layer;

      provider.GetRequiredService<LayerCollection>()
        .AddEntity(item, layer, weight);
      return item;
    }

    /// <summary>
    /// Method is meant to add a system to the system collection and use DI to build out the object so that
    /// services and other DI components can be injected properly into the class
    /// </summary>
    /// <typeparam name="T">The current object we are adding to the service collection</typeparam>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="parameters">The constructor parameters that do not exists in D</param>
    /// <returns>Will return the system that was created and added to the collection</returns>
    public static T AddSystem<T>(this IServiceProvider provider, params object[] parameters)
      where T : ISystem
    {
      var eventQueue = provider.GetRequiredService<EventQueue>();
      var system = ActivatorUtilities.CreateInstance<T>(provider, parameters);

      // If this is an update system we need to add an event listener to the queue
      if (system is IUpdateSystem || system is IUpdateEntitySystem || system is IUpdateEndSystem)
        eventQueue.AddEventListener(ActivatorUtilities.CreateInstance<UpdateEventListener>(provider, new object[] { system }));

      // If this is a draw system we need to add an event listener to the queue
      if (system is IDrawSystem || system is IDrawEntitySystem || system is IDrawEndSystem)
        eventQueue.AddEventListener(ActivatorUtilities.CreateInstance<DrawEventListener>(provider, new object[] { system }));

      return system;
    }

    /// <summary>
    /// Method is meant to add a view to the view collection and use DI to build out the object so that
    /// services and other DI components can be injected properly into the class
    /// </summary>
    /// <typeparam name="T">The current object we are adding to the view collection</typeparam>
    /// <param name="provider">The view provider we will use to find the collection and build out the object with</param>
    /// <returns>Will return the system that was created and added to the collection</returns>
    public static T SetStartView<T>(this IServiceProvider provider)
      where T : IView
    {
      return provider.GetRequiredService<IViewCollection>()
        .SetCurrent<T>();
    }

    /// <summary>
    /// Add a global sprit font
    /// </summary>
    /// <param name="provider">The view provider we will use to find the collection and build out the object with</param>
    /// <param name="key">The key to use for this font lookup</param>
    /// <param name="font">The font we are anting to save globally</param>
    public static void AddSpriteFont(this IServiceProvider provider, string key, SpriteFont font)
    {
      provider.GetRequiredService<FontCollection>()
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
      provider.GetRequiredService<FontCollection>()
        .TextureFonts[key] = font;
    }

    /// <summary>
    /// Method is meant to add an event listener based on the event to the event queue for processing
    /// </summary>
    /// <typeparam name="T">The event Listener implementation that should be used</typeparam>
    /// <typeparam name="K">The event we are wanting to add the queue to</typeparam>
    /// <param name="provider">The view provider we will use to find the collection and build out the object with</param>
    /// <param name="parameters">The constructor parameters that do not exists in D</param>
    public static IDisposable AddEventListener<T, K>(this IServiceProvider provider, params object[] parameters)
      where T : IEventListener<K>
    {
      var eventQueue = provider.GetRequiredService<EventQueue>();
      return eventQueue.AddEventListener(ActivatorUtilities.CreateInstance<T>(provider, parameters));
    }

    /// <summary>
    /// Method is meant to add an event listener based on the event to the event queue for processing
    /// </summary>
    /// <typeparam name="T">The event Listener implementation that should be used</typeparam>
    /// <typeparam name="K">The event we are wanting to add the queue to</typeparam>
    /// <param name="provider">The view provider we will use to find the collection and build out the object with</param>
    /// <param name="parameters">The constructor parameters that do not exists in D</param>
    public static IDisposable AddEventListener<T>(this IServiceProvider provider, Func<T, bool> callback, object target = null)
    {
      var eventQueue = provider.GetRequiredService<EventQueue>();
      return eventQueue.AddEventListener(callback, target);
    }
  }
}