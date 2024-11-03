using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Core.Container
{
  /// <summary>
  /// Container meant to wrap a texture 2d
  /// </summary>
  public interface ITexture2DContainer
  {
    /// <summary>
    /// The containing texture
    /// </summary>
    Texture2D? Current { get; set; }

    /// <summary>
    /// Wrapper getter for <see cref="Texture2D.Width" />
    /// </summary>
    int Width { get; }

    /// <summary>
    /// Wrapper getter for <see cref="Texture2D.Height" />
    /// </summary>
    int Height { get; }

    /// <summary>
    /// Wrapper method for <see cref="Texture2D.SetData{T}(T[])"/>
    /// </summary>
    void SetData<T>(T[] data) where T : struct;

    /// <summary>
    /// Wrapper method for <see cref="IDisposable.Dispose"/>
    /// </summary>
    void Dispose();
  }
}
