using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.Core
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
    /// The top left render source for the ninepatch
    /// </summary>
    public Rectangle TopLeft => new Rectangle(Point.Zero, Break);

    /// <summary>
    /// The top render source for the ninepatch
    /// </summary>
    public Rectangle Top => new Rectangle(new Point(Break.X, 0), Break);

    /// <summary>
    /// The top right render source for the ninepatch
    /// </summary>
    public Rectangle TopRight => new Rectangle(new Point(Break.X * 2, 0), Break);

    /// <summary>
    /// The middle left render source for the ninepatch
    /// </summary>
    public Rectangle MiddleLeft => new Rectangle(new Point(0, Break.Y), Break);

    /// <summary>
    /// The middle render source for the ninepatch
    /// </summary>
    public Rectangle Middle => new Rectangle(Break, Break);

    /// <summary>
    /// The middle right render source for the ninepatch
    /// </summary>
    public Rectangle MiddleRight => new Rectangle(new Point(Break.X * 2, Break.Y), Break);

    /// <summary>
    /// The bottom left render source for the ninepatch
    /// </summary>
    public Rectangle BottomLeft => new Rectangle(new Point(0, Break.Y * 2), Break);

    /// <summary>
    /// The bottom render source for the ninepatch
    /// </summary>
    public Rectangle Bottom => new Rectangle(new Point(Break.X, Break.Y * 2), Break);

    /// <summary>
    /// The bottom right render source for the ninepatch
    /// </summary>
    public Rectangle BottomRight => new Rectangle(new Point(Break.X * 2, Break.Y * 2), Break);

    /// <summary>
    /// Constructor to build a nine patch texture
    /// </summary>
    /// <param name="texture">The texture we are building from</param>
    public NinePatch(Texture2D texture)
    {
      if (texture.Width % 3 != 0) throw new ArgumentException("Width value needs to be a multiple of 3", nameof(texture));
      if (texture.Height % 3 != 0) throw new ArgumentException("Height value needs to be a multiple of 3", nameof(texture));
      Texture = texture;
      Break = new Point(Texture.Width / 3, Texture.Height / 3);
    }
  }
}
