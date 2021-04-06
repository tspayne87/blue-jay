using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Component.System.Addons
{
  /// <summary>
  /// Addon is meant to handle storing the correct sprit effect for rendering
  /// </summary>
  public class SpriteEffectsAddon : Addon<SpriteEffectsAddon>
  {
    /// <summary>
    /// The current sprite effects for the texture on render
    /// </summary>
    public SpriteEffects Effects { get; set; }

    /// <summary>
    /// Constructor to assign a starting sprite effect for this addon
    /// </summary>
    /// <param name="effects"></param>
    public SpriteEffectsAddon(SpriteEffects effects)
    {
      Effects = effects;
    }

    /// <summary>
    /// Overriden to string function is meant to give a quick and easy way to see
    /// how this object looks while debugging
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"Sprite Effect | {Effects.ToString("G")}";
    }
  }
}
