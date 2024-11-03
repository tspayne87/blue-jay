using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace BlueJay.Core.Containers
{
  /// <summary>
  /// Implementation of <see cref="ISpriteFontContainer" />
  /// </summary>
  internal class SpriteFontContainer : ISpriteFontContainer
  {
    /// <inheritdoc />
    public SpriteFont? Current { get; set; }

    /// <summary>
    /// The default constructor for this container
    /// </summary>
    public SpriteFontContainer()
    {
      Current = null;
    }

    /// <inheritdoc />
    public Vector2 MeasureString(string text)
    {
      return Current?.MeasureString(text) ?? Vector2.Zero;
    }

    /// <inheritdoc />
    public Vector2 MeasureString(StringBuilder text)
    {
      return Current?.MeasureString(text) ?? Vector2.Zero;
    }
  }
}
