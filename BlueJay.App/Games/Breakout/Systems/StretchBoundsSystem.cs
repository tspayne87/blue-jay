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
  /// <summary>
  /// The stretch bounds system is meant to move and stretch bounds in the system to make it look
  /// like the screen is the whole game
  /// </summary>
  public class StretchBoundsSystem : ComponentSystem
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The previous screen width
    /// </summary>
    private int _previousWidth;

    /// <summary>
    /// If we have a change we want to process entities in the system
    /// </summary>
    private bool _hasChange;

    /// <summary>
    /// The key we want to store since it is what we need to use to find entities in the game
    /// </summary>
    private readonly long _key;

    /// <summary>
    /// The current addon key that is meant to act as a selector for the Draw/Update
    /// methods with entities
    /// </summary>
    public override long Key => _hasChange ? _key : 0;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>() { LayerNames.BlockLayer, LayerNames.PaddleLayer };

    /// <summary>
    /// Constructor is meant to give defaults and inject the global graphics device
    /// </summary>
    /// <param name="graphics">The global graphics device we will use to find out how big the screen is</param>
    public StretchBoundsSystem(GraphicsDevice graphics)
    {
      _graphics = graphics;
      _previousWidth = 0;
      _hasChange = false;
      _key = TypeAddon.Identifier | BoundsAddon.Identifier;
    }

    /// <summary>
    /// The update event that is called before all entity update events for this system
    /// </summary>
    public override void OnUpdate()
    {
      _hasChange = false;
      if (_previousWidth != _graphics.Viewport.Width)
      { // We want to process the change for all the entities in the system
        _hasChange = true;
        _previousWidth = _graphics.Viewport.Width;
      }
    }

    /// <summary>
    /// The update event that is called for eeach entity that was selected by the key
    /// for this system.
    /// </summary>
    /// <param name="entity">The current entity that should be updated</param>
    public override void OnUpdate(IEntity entity)
    {
      var ta = entity.GetAddon<TypeAddon>();
      var ba = entity.GetAddon<BoundsAddon>();

      switch (ta.Type)
      {
        case EntityType.Block:
          { // We want to reshape the blocks to fit the screen
            var bia = entity.GetAddon<BlockIndexAddon>();
            var size = new Size((_graphics.Viewport.Width - (BlockConsts.Padding * (BlockConsts.Amount + 1))) / BlockConsts.Amount, _graphics.Viewport.Height / 15);
            var position = new Point((bia.Index % BlockConsts.Amount) * (size.Width + BlockConsts.Padding) + BlockConsts.Padding, (bia.Index / BlockConsts.Amount) * (size.Height + BlockConsts.Padding) + 30);
            ba.Bounds = new Rectangle(position, size.ToPoint());
          }
          break;
        case EntityType.Paddle:
          { // We want to reshape the paddle to fit the screen
            var size = new Size(_graphics.Viewport.Width / 7, 20);
            var position = new Point(ba.Bounds.X, _graphics.Viewport.Height - (_graphics.Viewport.Height / 10));
            ba.Bounds = new Rectangle(position, size.ToPoint());
          }
          break;
      }
    }
  }
}
