using System;
using BlueJay.Component.System.Interfaces;
using BlueJay.EventListeners;
using BlueJay.Events.Interfaces;
using BlueJay.Interfaces;
using BlueJay.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay
{
  public static class ServiceProviderExtensions
  {
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
      var eventQueue = provider.GetRequiredService<IEventQueue>();
      var system = ActivatorUtilities.CreateInstance<T>(provider, parameters);

      // If this is an update system we need to add an event listener to the queue
      if (system is IUpdateSystem || system is IUpdateEntitySystem || system is IUpdateEndSystem)
        eventQueue.AddEventListener(ActivatorUtilities.CreateInstance<UpdateEventListener>(provider, new object[] { system }));

      // If this is a draw system we need to add an event listener to the queue
      if (system is IDrawSystem || system is IDrawEntitySystem || system is IDrawEndSystem)
        provider.GetRequiredService<DrawableSystemCollection>().Add(system);

      return system;
    }

    /// <summary>
    /// Method meant to include all the blue jay dependencies to the service collection
    /// </summary>
    /// <param name="collection">The collection we are adding dependencies too</param>
    /// <returns>Will return the collection for chaining</returns>
    public static IServiceCollection AddBlueJay(this IServiceCollection collection)
    {
      return collection
        .AddSingleton<IViewCollection, ViewCollection>()
        .AddScoped<DrawableSystemCollection>();
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
  }
}