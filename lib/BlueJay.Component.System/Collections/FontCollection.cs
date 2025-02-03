using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Containers;

namespace BlueJay.Component.System.Collections
{
  /// <summary>
  /// The collections of fonts that can be used in the system
  /// </summary>
  internal class FontCollection : IFontCollection
  {
    /// <inheritdoc />
    public Dictionary<string, ISpriteFontContainer> SpriteFonts { get; set; }

    /// <inheritdoc />
    public Dictionary<string, TextureFont> TextureFonts { get; set; }

    /// <summary>
    /// Constructor to build the placeholders for all the fonts
    /// </summary>
    public FontCollection()
    {
      SpriteFonts = new Dictionary<string, ISpriteFontContainer>();
      TextureFonts = new Dictionary<string, TextureFont>();
    }
  }
}
