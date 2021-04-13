using System;
using BlueJay.Component.System.Interfaces;
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
      var item = ActivatorUtilities.CreateInstance<T>(provider, parameters);
      item.LoadContent();

      provider.GetRequiredService<IEntityCollection>()
        .Add(item);
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

      provider.GetRequiredService<ISystemCollection>()
        .Add(item);
      return item;
    }
  }
}