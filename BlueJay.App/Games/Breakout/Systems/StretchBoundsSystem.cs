using BlueJay.App.Games.Breakout.Addons;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using BlueJay.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.App.Games.Breakout.Systems
{
  public class StretchBoundsSystem : ComponentSystem
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    private int _previousWidth;
    private bool _hasChange;
    private long _key;

    public override long Key => _hasChange ? _key : 0;

    public override List<string> Layers => new List<string>() { LayerNames.BlockLayer, LayerNames.PaddleLayer };

    public StretchBoundsSystem(GraphicsDevice graphics)
    {
      _graphics = graphics;
      _previousWidth = 0;
      _hasChange = false;
      _key = TypeAddon.Identifier | BoundsAddon.Identifier;
    }

    public override void OnUpdate()
    {
      _hasChange = false;
      if (_previousWidth != _graphics.Viewport.Width)
      {
        _hasChange = true;
        _previousWidth = _graphics.Viewport.Width;
      }
    }

    public override void OnUpdate(IEntity entity)
    {
      var ta = entity.GetAddon<TypeAddon>();
      var ba = entity.GetAddon<BoundsAddon>();

      switch (ta.Type)
      {
        case EntityType.Block:
          {
            var bia = entity.GetAddon<BlockIndexAddon>();
            var size = new Size((_graphics.Viewport.Width - (BlockConsts.Padding * (BlockConsts.Amount + 1))) / BlockConsts.Amount, _graphics.Viewport.Height / 15);
            var position = new Point((bia.Index % BlockConsts.Amount) * (size.Width + BlockConsts.Padding) + BlockConsts.Padding, (bia.Index / BlockConsts.Amount) * (size.Height + BlockConsts.Padding) + 30);
            ba.Bounds = new Rectangle(position, size.ToPoint());
          }
          break;
        case EntityType.Paddle:
          {
            var size = new Size(_graphics.Viewport.Width / 7, 20);
            var position = new Point(ba.Bounds.X, _graphics.Viewport.Height - (_graphics.Viewport.Height / 10));
            ba.Bounds = new Rectangle(position, size.ToPoint());
          }
          break;
      }
    }
  }
}
