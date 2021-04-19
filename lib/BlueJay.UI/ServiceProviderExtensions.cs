using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlueJay.UI
{
  public static class ServiceProviderExtensions
  {
    /// <summary>
    /// Method is meant to add a ui entity to the entity collection and use DI to build out the object so that
    /// services and other DI components can be injected properly into the class
    /// </summary>
    /// <typeparam name="T">The current object we are adding to the entity collection</typeparam>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="parameters">The constructor parameters that do not exists in DI</param>
    /// <returns>Will return the entity that was created and added to the collection</returns>
    public static T AddUIEntity<T>(this IServiceProvider provider, IEntity parent = null, params object[] parameters)
      where T : IEntity
    {
      var item = ActivatorUtilities.CreateInstance<T>(provider, parameters);
      item.LoadContent();

      // Add this item as a child to the parent
      if (parent != null)
      {
        var la = parent.GetAddon<LineageAddon>();
        la?.Children.Add(item);
      }

      item.Add<LineageAddon>(parent);
      provider.GetRequiredService<LayerCollection>()
        .AddEntity(item, UIStatic.LayerName, 15);
      return item;
    }
  }
}
