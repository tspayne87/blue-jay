using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BlueJay.App.Games.Breakout.Systems
{
  public class ClampPositionSystem : ComponentSystem
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    public override long Key => BoundsAddon.Identifier;

    public override List<string> Layers => new List<string>() { LayerNames.PaddleLayer };

    public ClampPositionSystem(GraphicsDevice graphics)
    {
      _graphics = graphics;
    }

    public override void OnUpdate(IEntity entity)
    {
      var ba = entity.GetAddon<BoundsAddon>();
      ba.Bounds.X = Math.Clamp(ba.Bounds.X, 0, _graphics.Viewport.Width - ba.Bounds.Width);
    }
  }
}
