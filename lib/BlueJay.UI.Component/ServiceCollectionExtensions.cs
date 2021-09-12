using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// Set of methods that are meant to add in items to the service collection
  /// </summary>
  public static class ServiceCollectionExtensions
  {
    /// <summary>
    /// Helper method is meant to add extra UI elements to DI
    /// </summary>
    /// <param name="serviceCollection">The service collection we are wanting to add some items to</param>
    /// <returns>Will return the service collection given with the added items</returns>
    public static IServiceCollection AddComponentUI(this IServiceCollection serviceCollection)
    {
      return serviceCollection
        .AddScoped<UIComponentCollection>();
    }
  }
}
