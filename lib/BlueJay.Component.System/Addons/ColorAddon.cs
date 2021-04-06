using Microsoft.Xna.Framework;

namespace BlueJay.Component.System.Addons
{
  /// <summary>
  /// The color addon is for attaching color to an entity
  /// </summary>
  public class ColorAddon : Addon<ColorAddon>
  {
    /// <summary>
    /// The current color that should be used for the entity
    /// </summary>
    public Color Color;


    /// <summary>
    /// Constructor to build out the color addon
    /// </summary>
    /// <param name="color">The current color that should be assigned</param>
    public ColorAddon(Color color)
    {
      Color = color;
    }

    /// <summary>
    /// Overridden to string method is meant to print out a nice version of the
    /// addon for debugging purposes
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"Color | R: {Color.R}, G: {Color.G}, B: {Color.B}, A: {Color.A}";
    }
  }
}
