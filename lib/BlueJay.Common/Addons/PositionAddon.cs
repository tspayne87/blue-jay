using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace BlueJay.Common.Addons
{
  /// <summary>
  /// The position addon the determine where the entity is in the world
  /// </summary>
  public struct PositionAddon : IAddon
  {
    /// <summary>
    /// The position of the entity
    /// </summary>
    public Vector2 Position { get; set; }

    public PositionAddon(int x, int y)
	 {
      Position = new Vector2(x, y);
	 }

    /// <summary>
    /// Constructor to build out position for the addon
    /// </summary>
    /// <param name="position">The position that should be assigned to the entity</param>
    public PositionAddon(Vector2 position)
    {
      Position = position;
    }

    /// <summary>
    /// Overriden string method to give a nice string to print out for debugging
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"Position | X: {Position.X}, Y: {Position.Y}";
    }
  }
}
