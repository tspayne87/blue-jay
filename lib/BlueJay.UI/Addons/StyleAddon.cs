using BlueJay.Component.System.Addons;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.UI.Addons
{
  public class StyleAddon : Addon<StyleAddon>
  {
    /// <summary>
    /// The basic nine patch style for the UI element
    /// </summary>
    public NinePatch NinePatch { get; set; }

    /// <summary>
    /// The texture based on the nine patch
    /// </summary>
    public Texture2D CachedTexture { get; set; }
  }
}
