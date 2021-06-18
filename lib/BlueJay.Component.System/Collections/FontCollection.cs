using System.Collections.Generic;
using BlueJay.Core;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Component.System.Collections
{
  /// <summary>
  /// The collections of fonts that can be used in the system
  /// </summary>
  public class FontCollection
  {
    /// <summary>
    /// The global list of sprite fonts
    /// </summary>
    public Dictionary<string, SpriteFont> SpriteFonts { get; set; }

    /// <summary>
    /// The global list of texture fonts
    /// </summary>
    public Dictionary<string, TextureFont> TextureFonts { get; set; }

    /// <summary>
    /// Constructor to build the placeholders for all the fonts
    /// </summary>
    public FontCollection()
    {
      SpriteFonts = new Dictionary<string, SpriteFont>();
      TextureFonts = new Dictionary<string, TextureFont>();
    }
  }
}
