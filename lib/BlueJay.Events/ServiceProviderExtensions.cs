using BlueJay.Events.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlueJay.Events
{
  public static class ServiceProviderExtensions
  {
    /// <summary>
    /// Method is meant to add an event listener based on the event to the event queue for processing
    /// </summary>
    /// <typeparam name="T">The event Listener implementation that should be used</typeparam>
    /// <typeparam name="K">The event we are wanting to add the queue to</typeparam>
    /// <param name="provider">The view provider we will use to find the collection and build out the object with</param>
    /// <param name="parameters">The constructor parameters that do not exists in D</param>
    /// <returns>Will return a disposable that can be disposed to remove this event listener</returns>
    public static IDisposable AddEventListener<T, K>(this IServiceProvider provider, params object[] parameters)
      where T : IEventListener<K>
    {
      var eventQueue = provider.GetRequiredService<IEventQueue>();
      return eventQueue.AddEventListener(ActivatorUtilities.CreateInstance<T>(provider, parameters));
    }

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    public static IDisposable AddEventListener<T>(this IServiceProvider provider, Func<T, bool> callback, int? weight = null)
    {
      var eventQueue = provider.GetRequiredService<IEventQueue>();
      return eventQueue.AddEventListener(callback, weight);
    }

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    public static IDisposable AddEventListener<T>(this IServiceProvider provider, Func<T, object?, bool> callback, int? weight = null)
    {
      var eventQueue = provider.GetRequiredService<IEventQueue>();
      return eventQueue.AddEventListener(callback, weight);
    }

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    /// <param name="target">The target this callback should be attached to</param>
    public static IDisposable AddEventListener<T>(this IServiceProvider provider, Func<T, bool> callback, object? target, int? weight = null)
    {
      var eventQueue = provider.GetRequiredService<IEventQueue>();
      return eventQueue.AddEventListener(callback, target, weight);
    }

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    /// <param name="target">The target this callback should be attached to</param>
    public static IDisposable AddEventListener<T>(this IServiceProvider provider, Func<T, object?, bool> callback, object? target, int? weight = null)
    {
      var eventQueue = provider.GetRequiredService<IEventQueue>();
      return eventQueue.AddEventListener(callback, target, weight);
    }
  }
}
