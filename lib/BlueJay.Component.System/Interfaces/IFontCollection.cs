using BlueJay.Core;
using BlueJay.Core.Containers;

namespace BlueJay.Component.System.Interfaces
{
  public interface IFontCollection
  {
    /// <summary>
    /// The global list of sprite fonts
    /// </summary>
    Dictionary<string, ISpriteFontContainer> SpriteFonts { get; }

    /// <summary>
    /// The global list of texture fonts
    /// </summary>
    Dictionary<string, TextureFont> TextureFonts { get; }
  }
}
