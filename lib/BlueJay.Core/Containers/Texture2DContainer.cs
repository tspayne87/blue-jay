using BlueJay.Core.Container;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Core.Containers
{
  /// <summary>
  /// Implementation of <see cref="ITexture2DContainer" />
  /// </summary>
  internal class Texture2DContainer : ITexture2DContainer
  {
    /// <inheritdoc />
    public Texture2D? Current { get; set; }

    /// <inheritdoc />
    public int Width => Current?.Width ?? 0;

    /// <inheritdoc />
    public int Height => Current?.Height ?? 0;

    public Texture2DContainer()
    {
      Current = null;
    }

    /// <inheritdoc />
    public void SetData<T>(T[] data) where T : struct
    {
      Current?.SetData(data);
    }

    /// <inheritdoc />
    public void Dispose()
    {
      Current?.Dispose();
    }
  }
}
