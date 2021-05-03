using Microsoft.Xna.Framework;

namespace BlueJay.Component.System.Addons
{
  /// <summary>
  /// Bounds Addon to determine the rectangle that will represent where the entity
  /// is in the world
  /// </summary>
  public class BoundsAddon : Addon<BoundsAddon>
  {
    /// <summary>
    /// The current bounds of the entity
    /// </summary>
    public Rectangle Bounds;

    /// <summary>
    /// Basic bounds addon to create a basic bounds addon
    /// </summary>
    public BoundsAddon()
      : this(new Rectangle()) { }

    /// <summary>
    /// Constructor method is meant to help with the creation of the bounds addon
    /// </summary>
    /// <param name="x">The current x coordinate</param>
    /// <param name="y">The current y coordinate</param>
    /// <param name="width">The current width of the entity</param>
    /// <param name="height">The current height of the entity</param>
    public BoundsAddon(int x, int y, int width, int height)
      : this(new Rectangle(x, y, width, height)) { }

    /// <summary>
    /// Constructor method is meant to assign the bounds property for the addon
    /// </summary>
    /// <param name="bounds">The bounds property</param>
    public BoundsAddon(Rectangle bounds)
    {
      Bounds = bounds;
    }

    /// <summary>
    /// Overriden to string function is meant to give a quick and easy way to see
    /// how this object looks while debugging
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"Bounds | X: {Bounds.X}, Y: {Bounds.Y}, Width: {Bounds.Width}, Height: {Bounds.Height}";
    }
  }
}
