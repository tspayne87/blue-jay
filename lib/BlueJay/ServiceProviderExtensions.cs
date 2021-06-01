using System;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Interfaces;
using Microsoft.Extensions.DependencyInjection;

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
      item.LoadContent();

      provider.GetRequiredService<LayerCollection>()
        .AddEntity(item, layer, weight);
      return item;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="provider"></param>
    /// <param name="key"></param>
    /// <param name="parameters"></param>
    /// <returns></returns>
    public static T AddRenderer<T>(this IServiceProvider provider, string key, params object[] parameters)
      where T : IRenderer
    {
      var item = ActivatorUtilities.CreateInstance<T>(provider, parameters);

      provider.GetRequiredService<RendererCollection>()
        .Add(key, item);
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
    public static T AddComponentSystem<T>(this IServiceProvider provider, params object[] parameters)
      where T : IComponentSystem
    {
      var item = ActivatorUtilities.CreateInstance<T>(provider, parameters);
      item.OnInitialize();

      provider.GetRequiredService<SystemCollection>()
        .Add(item);
      return item;
    }

    /// <summary>
    /// Method is meant to add a view to the view collection and use DI to build out the object so that
    /// services and other DI components can be injected properly into the class
    /// </summary>
    /// <typeparam name="T">The current object we are adding to the view collection</typeparam>
    /// <param name="provider">The view provider we will use to find the collection and build out the object with</param>
    /// <param name="parameters">The constructor parameters that do not exists in D</param>
    /// <returns>Will return the system that was created and added to the collection</returns>
    public static T AddView<T>(this IServiceProvider provider, params object[] parameters)
      where T : IView
    {
      var item = ActivatorUtilities.CreateInstance<T>(provider, parameters);
      item.Initialize(provider.GetRequiredService<IServiceProvider>());

      provider.GetRequiredService<IViewCollection>()
        .Add(item);
      return item;
    }

    /// <summary>
    /// Method is meant to add an event listener based on the event to the event queue for processing
    /// </summary>
    /// <typeparam name="T">The event Listener implementation that should be used</typeparam>
    /// <typeparam name="K">The event we are wanting to add the queue to</typeparam>
    /// <param name="provider">The view provider we will use to find the collection and build out the object with</param>
    /// <param name="parameters">The constructor parameters that do not exists in D</param>
    public static void AddEventListener<T, K>(this IServiceProvider provider, params object[] parameters)
      where T : IEventListener<K>
    {
      var eventQueue = provider.GetRequiredService<EventQueue>();
      eventQueue.AddEventListener(ActivatorUtilities.CreateInstance<T>(provider));
    }

    /// <summary>
    /// Method is meant to add an event listener based on the event to the event queue for processing
    /// </summary>
    /// <typeparam name="T">The event Listener implementation that should be used</typeparam>
    /// <typeparam name="K">The event we are wanting to add the queue to</typeparam>
    /// <param name="provider">The view provider we will use to find the collection and build out the object with</param>
    /// <param name="parameters">The constructor parameters that do not exists in D</param>
    public static void AddEventListener<T>(this IServiceProvider provider, Func<T, bool> callback, object target = null)
    {
      var eventQueue = provider.GetRequiredService<EventQueue>();
      eventQueue.AddEventListener(callback, target);
    }
  }
}