using BlueJay.Core.Container;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Core.Containers
{
  /// <summary>
  /// Implementation of the <see cref="IGraphicsDeviceContainer" />
  /// </summary>
  internal class GraphicsDeviceContainer : IGraphicsDeviceContainer
  {
    /// <summary>
    /// The Service provider to create instances from
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// The graphics device meant to build textures from
    /// </summary>
    private readonly GraphicsDevice _graphicsDevice;

    /// <summary>
    /// Constructor meant to inject various services into the graphics device container
    /// </summary>
    /// <param name="serviceProvider">The Service provider to create instances from</param>
    /// <param name="graphicsDevice">The graphics device meant to build textures from</param>
    public GraphicsDeviceContainer(IServiceProvider serviceProvider, GraphicsDevice graphicsDevice)
    {
      _serviceProvider = serviceProvider;
      _graphicsDevice = graphicsDevice;
    }

    /// <inheritdoc />
    public ITexture2DContainer CreateTexture2D(int width, int height)
    {
      return new Texture2D(_graphicsDevice, width, height).AsContainer();
    }
  }
}
