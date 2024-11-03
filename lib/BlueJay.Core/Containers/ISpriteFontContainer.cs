using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Text;

namespace BlueJay.Core.Containers
{
  /// <summary>
  /// Container meant to wrap a sprite font
  /// </summary>
  public interface ISpriteFontContainer
  {
    /// <summary>
    /// The containing sprite font
    /// </summary>
    SpriteFont? Current { get; set; }

    /// <summary>
    /// Wrapper around <see cref="SpriteFont.MeasureString(string)" />
    /// </summary>
    Vector2 MeasureString(string text);

    /// <summary>
    /// Wrapper around <see cref="SpriteFont.MeasureString(StringBuilder)" />
    /// </summary>
    Vector2 MeasureString(StringBuilder text);
  }
}
