using System.Collections.Generic;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Component.System.Collections
{
  /// <summary>
  /// The collections of fonts that can be used in the system
  /// </summary>
  internal class FontCollection : IFontCollection
  {
    /// <inheritdoc />
    public Dictionary<string, SpriteFont> SpriteFonts { get; set; }

    /// <inheritdoc />
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
