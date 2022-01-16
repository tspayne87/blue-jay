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
    /// Method is meant to add an event listener based on the event to the event queue for processing
    /// </summary>
    /// <typeparam name="T">The event Listener implementation that should be used</typeparam>
    /// <param name="provider">The view provider we will use to find the collection and build out the object with</param>
    /// <returns>Will return a disposable that can be disposed to remove this event listener</returns>
    public static IDisposable AddEventListener<T>(this IServiceProvider provider, Func<T, bool> callback, object target = null)
    {
      var eventQueue = provider.GetRequiredService<IEventQueue>();
      return eventQueue.AddEventListener(callback, target);
    }
  }
}
