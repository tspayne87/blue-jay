using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.Component.System.DependencyInjection
{
  public static class ServiceCollectionServiceExtensions
  {
    public static IServiceCollection AddComponentSystem(this IServiceCollection services)
    {
      return services
        .AddSingleton<IViewCollection, ViewCollection>()
        .AddScoped<IPluginCollection, PluginCollection>()
        .AddScoped<IEntityCollection, EntityCollection>()
        .AddScoped<ISystemCollection, SystemCollection>()
        .AddTransient<ITriggerSystem, TriggerSystem>()
        .AddScoped<MouseService>()
        .AddScoped<IEngine, Engine>();
    }
  }
}