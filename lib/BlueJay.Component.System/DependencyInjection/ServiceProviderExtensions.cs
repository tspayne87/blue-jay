using System;
using BlueJay.Component.System.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;

namespace BlueJay.Component.System.DependencyInjection
{
  public static class ServiceProviderExtensions
  {
    public static T AddView<T>(this IServiceProvider provider, params object[] parameters)
      where T : IView
    {
      var item = ActivatorUtilities.CreateInstance<T>(provider, parameters);
      item.Initialize();
      item.LoadContent();

      provider.GetRequiredService<IViewCollection>()
        .Add(item);
      return item;
    }

    public static T AddEnginePlugin<T>(this IServiceProvider provider, params object[] parameters)
      where T : IEnginePlugin
    {
      var item = ActivatorUtilities.CreateInstance<T>(provider, parameters);
      item.Initialize();
      item.LoadContent();

      provider.GetRequiredService<IPluginCollection>()
        .Add(item);
      return item;
    }

    public static T AddEntity<T>(this IServiceProvider provider, params object[] parameters)
      where T : IEntity
    {
      var item = ActivatorUtilities.CreateInstance<T>(provider, parameters);
      item.LoadContent();

      provider.GetRequiredService<IEntityCollection>()
        .Add(item);
      return item;
    }

    public static T AddComponentSystem<T>(this IServiceProvider provider, params object[] parameters)
      where T : IComponentSystem
    {
      var item = ActivatorUtilities.CreateInstance<T>(provider, parameters);
      var manager = provider.GetRequiredService<ContentManager>();
      item.Initialize();

      provider.GetRequiredService<ISystemCollection>()
        .Add(item);
      return item;
    }
  }
}