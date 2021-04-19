using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.UI
{
  /// <summary>
  /// Nine Patch texture is a way to create rectangles from a single 3x3 texture that can draw any type of rectangle with a texture
  /// </summary>
  public class NinePatch
  {
    /// <summary>
    /// The texture that will be used in the nine patch
    /// </summary>
    public Texture2D Texture { get; private set; }

    /// <summary>
    /// The breaks for the nine patch texture so we can determine where to draw from
    /// </summary>
    public Point Break { get; private set; }

    /// <summary>
    /// Constructor to build a nine patch texture
    /// </summary>
    /// <param name="texture">The texture we are building from</param>
    public NinePatch(Texture2D texture)
    {
      Texture = texture;
      Break = new Point(Texture.Width / 3, Texture.Height / 3);
    }

    public Texture2D GenerateTexture(Rectangle rectangle)
    {
      return new Texture2D();
    }
  }
}
