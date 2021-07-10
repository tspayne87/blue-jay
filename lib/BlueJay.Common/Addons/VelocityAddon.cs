using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;

namespace BlueJay.Common.Addons
{
  /// <summary>
  /// Velocity addon to track the current velocity of the entity
  /// </summary>
  public struct VelocityAddon : IAddon
  {
    /// <summary>
    /// The current velocity of the addon
    /// </summary>
    public Vector2 Velocity;

    /// <summary>
    /// Constuctor to build out the velocity based on the x and y positions
    /// </summary>
    /// <param name="x">The x velocity vector</param>
    /// <param name="y">The y velocity vector</param>
    public VelocityAddon(int x, int y)
      : this(new Vector2(x, y)) { }

    /// <summary>
    /// Constructor to build out the velocity addon based on a vecotr
    /// </summary>
    /// <param name="velocity">The starting velocity for this entity</param>
    public VelocityAddon(Vector2 velocity)
    {
      Velocity = velocity;
    }

    /// <summary>
    /// Overriden to string function is meant to give a quick and easy way to see
    /// how this object looks while debugging
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"Velocity | X: {Velocity.X}, Y: {Velocity.Y}";
    }
  }
}
