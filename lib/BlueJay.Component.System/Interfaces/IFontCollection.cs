using BlueJay.Core;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.Component.System.Interfaces
{
  public interface IFontCollection
  {
    /// <summary>
    /// The global list of sprite fonts
    /// </summary>
    Dictionary<string, SpriteFont> SpriteFonts { get; }

    /// <summary>
    /// The global list of texture fonts
    /// </summary>
    Dictionary<string, TextureFont> TextureFonts { get; }
  }
}
