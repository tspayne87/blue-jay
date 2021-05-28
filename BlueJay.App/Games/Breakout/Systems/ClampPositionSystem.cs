using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BlueJay.App.Games.Breakout.Systems
{
  /// <summary>
  /// System is meant to clamp the position of the paddle so it does not go outside the bounds
  /// of the screen
  /// </summary>
  public class ClampPositionSystem : ComponentSystem
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The current addon key that is meant to act as a selector for the Draw/Update
    /// methods with entities
    /// </summary>
    public override long Key => BoundsAddon.Identifier;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>() { LayerNames.PaddleLayer };

    /// <summary>
    /// Constructor is meant to give defaults and inject the global graphics device
    /// </summary>
    /// <param name="graphics">The global graphics device we will use to find out how big the screen is</param>
    public ClampPositionSystem(GraphicsDevice graphics)
    {
      _graphics = graphics;
    }

    /// <summary>
    /// The update event that is called for eeach entity that was selected by the key
    /// for this system.
    /// </summary>
    /// <param name="entity">The current entity that should be updated</param>
    public override void OnUpdate(IEntity entity)
    {
      // We do not want to paddle to go outside of the bounds of the screen so we clamp the X coord
      var ba = entity.GetAddon<BoundsAddon>();
      ba.Bounds.X = Math.Clamp(ba.Bounds.X, 0, _graphics.Viewport.Width - ba.Bounds.Width);
    }
  }
}
