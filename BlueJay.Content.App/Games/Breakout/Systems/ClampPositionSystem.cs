using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.Content.App.Games.Breakout.Systems
{
  /// <summary>
  /// System is meant to clamp the position of the paddle so it does not go outside the bounds
  /// of the screen
  /// </summary>
  public class ClampPositionSystem : IUpdateEntitySystem
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <inheritdoc />
    public long Key => AddonHelper.Identifier<BoundsAddon>();

    /// <inheritdoc />
    public List<string> Layers => new List<string>() { LayerNames.PaddleLayer };

    /// <summary>
    /// Constructor is meant to give defaults and inject the global graphics device
    /// </summary>
    /// <param name="graphics">The global graphics device we will use to find out how big the screen is</param>
    public ClampPositionSystem(GraphicsDevice graphics)
    {
      _graphics = graphics;
    }

    /// <inheritdoc />
    public void OnUpdate(IEntity entity)
    {
      // We do not want to paddle to go outside of the bounds of the screen so we clamp the X coord
      var ba = entity.GetAddon<BoundsAddon>();
      ba.Bounds.X = MathHelper.Clamp(ba.Bounds.X, 0, _graphics.Viewport.Width - ba.Bounds.Width);
      entity.Update(ba);
    }
  }
}
