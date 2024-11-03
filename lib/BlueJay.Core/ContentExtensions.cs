using BlueJay.Core.Container;
using BlueJay.Core.Containers;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Core
{
  /// <summary>
  /// List of extension methods meant to add extra functionallity to the texture2d object
  /// </summary>
  public static class ContentExtensions
  {
    /// <summary>
    /// Helper method meant to add a wrapper for a texture2d object
    /// </summary>
    /// <param name="texture">The texture we want to wrap in a container</param>
    /// <returns>Will return the wrapped container</returns>
    public static ITexture2DContainer AsContainer(this Texture2D texture)
    {
      var container = new Texture2DContainer();
      container.Current = texture;
      return container;
    }

    /// <summary>
    /// Helper method meant to add a wrapper for the spritefont object
    /// </summary>
    /// <param name="spriteFont">The sprite font we want to wrap in a container</param>
    /// <returns>Will return the wrapped container</returns>
    public static ISpriteFontContainer AsContainer(this SpriteFont spriteFont)
    {
      var container = new SpriteFontContainer();
      container.Current = spriteFont;
      return container;
    }
  }
}
