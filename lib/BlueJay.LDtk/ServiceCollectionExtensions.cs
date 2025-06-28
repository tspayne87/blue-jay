using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.LDtk;

public static class ServiceCollectionExtensions
{
  /// <summary>
    /// Add Bluejay LDtk services to the service collection.
    /// </summary>
    /// <param name="collection">The service collection to configure scoped and singletons</param>
    /// <returns>Will return the collection for chaining</returns>
    public static IServiceCollection AddBlueJaySystem(this IServiceCollection collection)
    {
      return collection
        // Add Singletons for the project
        .AddSingleton<ILDtkService, LDtkService>();
    }
}
