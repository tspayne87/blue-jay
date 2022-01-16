using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.Component.System
{
  public static class ServiceCollectionExtensions
  {
    /// <summary>
    /// Add BlueJay services used to process the internals of the component system
    /// </summary>
    /// <param name="collection">The service collection to configure scoped and singletons</param>
    /// <returns>Will return the collection for chaining</returns>
    public static IServiceCollection AddBlueJay(this IServiceCollection collection)
    {

      return collection
        // Add Singletons for the project
        .AddSingleton<IFontCollection, FontCollection>()

        // Add Scoped 
        .AddScoped<ILayerCollection, LayerCollection>();
    }
  }
}
