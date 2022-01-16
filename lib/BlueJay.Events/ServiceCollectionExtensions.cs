using BlueJay.Events.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.Events
{
  public static class ServiceCollectionExtensions
  {
    /// <summary>
    /// Adds BlueJay event processor and queue into the service collections so that they can be used
    /// in DI
    /// </summary>
    /// <param name="collection">The service collection to include various services</param>
    /// <returns>Will return a collecton to enable chaining</returns>
    public static IServiceCollection AddBlueJayEvents(this IServiceCollection collection)
    {
      return collection
        .AddScoped<IEventQueue, EventQueue>()
        .AddScoped<IEventProcessor, EventProcessor>();
    }
  }
}
