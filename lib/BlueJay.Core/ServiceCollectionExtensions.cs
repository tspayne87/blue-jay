using BlueJay.Core.Container;
using BlueJay.Core.Containers;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.Core
{
  /// <summary>
  /// Extension methods meant to add containers to the dependency services
  /// </summary>
  public static class ServiceCollectionExtensions
  {
    /// <summary>
    /// Adds various containers into the dependency services
    /// </summary>
    /// <param name="collection">The service collection for dependency injection</param>
    /// <returns>Will return the service collection for chaining</returns>
    public static IServiceCollection AddBlueJayCore(this IServiceCollection collection)
    {
      return collection
        .AddSingleton<IGraphicsDeviceContainer, GraphicsDeviceContainer>()
        .AddSingleton<ISpriteBatchContainer, SpriteBatchContainer>();
    }
  }
}
