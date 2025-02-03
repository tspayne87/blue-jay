using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.Shared.Games.Breakout.Systems
{
  /// <summary>
  /// System is meant to clamp the position of the paddle so it does not go outside the bounds
  /// of the screen
  /// </summary>
  public class ClampPositionSystem : IUpdateSystem
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The entities that we want to process in this system
    /// </summary>
    private readonly IQuery _entities;

    /// <summary>
    /// Constructor is meant to give defaults and inject the global graphics device
    /// </summary>
    /// <param name="graphics">The global graphics device we will use to find out how big the screen is</param>
    /// <param name="entities">The entities that we want to process in this system</param>
    public ClampPositionSystem(GraphicsDevice graphics, IQuery<BoundsAddon> entities)
    {
      _graphics = graphics;
      _entities = entities.WhereLayer(LayerNames.PaddleLayer);
    }

    /// <inheritdoc />
    public void OnUpdate()
    {
      foreach (var entity in _entities)
      {
        // We do not want to paddle to go outside of the bounds of the screen so we clamp the X coord
        var ba = entity.GetAddon<BoundsAddon>();
        ba.Bounds.X = MathHelper.Clamp(ba.Bounds.X, 0, _graphics.Viewport.Width - ba.Bounds.Width);
        entity.Update(ba);
      }
    }
  }
}
