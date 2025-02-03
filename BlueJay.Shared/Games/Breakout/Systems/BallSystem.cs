using BlueJay.Shared.Games.Breakout.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using BlueJay.Component.System;
using BlueJay.Common.Addons;
using BlueJay.Events.Interfaces;
using System.Linq;

namespace BlueJay.Shared.Games.Breakout.Systems
{
  /// <summary>
  /// The heart of the game that handles the ball and how it behaves
  /// </summary>
  public class BallSystem : IUpdateSystem
  {
    /// <summary>
    /// The current layer collection that has all the entities in the game at the moment
    /// </summary>
    private readonly IQuery _paddleQuery;

    /// <summary>
    /// The current layer collection that has all the entities in the game at the moment
    /// </summary>
    private readonly IQuery _blockQuery;

    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The event queue we want to dispatch events too
    /// </summary>
    private readonly IEventQueue _eventQueue;

    /// <summary>
    /// The game service that is meant to process the different states of the game
    /// </summary>
    private readonly BreakoutGameService _service;

    /// <summary>
    /// The entities that we want to process in this system
    /// </summary>
    private readonly IQuery _entities;

    /// <summary>
    /// The current service provider
    /// </summary>
    private readonly IServiceProvider _provider;

    /// <summary>
    /// Constructor is meant to inject various items to be used in this system
    /// </summary>
    /// <param name="layerCollection">The layer collection that has all the entities in the game at the moment</param>
    /// <param name="graphics">The current graphic device we are working with</param>
    /// <param name="eventQueue">The event queue we want to dispatch events too</param>
    /// <param name="service">The current service that keeps track of the game</param>
    /// <param name="entities">The entities that we want to process in this system</param>
    public BallSystem(GraphicsDevice graphics, IEventQueue eventQueue, BreakoutGameService service,
    IQuery<BoundsAddon, VelocityAddon, BallActiveAddon> entities, IServiceProvider provider, IQuery query)
    {
      _entities = entities.WhereLayer(LayerNames.BallLayer);
      _blockQuery = query.WhereLayer(LayerNames.BlockLayer);
      _paddleQuery = query.WhereLayer(LayerNames.PaddleLayer);
      _graphics = graphics;
      _eventQueue = eventQueue;
      _service = service;
      _provider = provider;
    }

    /// <inheritdoc />
    public void OnUpdate()
    {
      foreach (var entity in _entities)
      {
        var baa = entity.GetAddon<BallActiveAddon>();
        if (!baa.IsActive) BeforeStart(entity);
        else DuringGame(entity);
      }
    }

    /// <summary>
    /// Helper method that is meant to lock the ball to the paddle before the game has started
    /// </summary>
    /// <param name="entity">The entity we are working with (aka: The ball)</param>
    private void BeforeStart(IEntity entity)
    {
      var paddle = _paddleQuery.FirstOrDefault();
      if (paddle != null)
      {
        var ba = entity.GetAddon<BoundsAddon>();
        var pba = paddle.GetAddon<BoundsAddon>();

        // Position the ball in the middle of the paddle
        ba.Bounds.X = pba.Bounds.X + ((pba.Bounds.Width - ba.Bounds.Width) / 2);
        ba.Bounds.Y = pba.Bounds.Y - ba.Bounds.Height;
        entity.Update(ba);
      }
    }

    /// <summary>
    /// Helper method is meant to handle the ball during gameplay
    /// </summary>
    /// <param name="entity">The entity we are working with (aka: The ball)</param>
    private void DuringGame(IEntity entity)
    {
      var paddle = _paddleQuery.FirstOrDefault();
      if (paddle != null)
      {
        var ba = entity.GetAddon<BoundsAddon>();
        var va = entity.GetAddon<VelocityAddon>();

        // Add the velocity to the bounds to move it around
        ba.Bounds = ba.Bounds.Add(va.Velocity);
        var pba = paddle.GetAddon<BoundsAddon>();

        var side = RectangleHelper.SideIntersection(ba.Bounds, pba.Bounds);
        if (side != RectangleSide.None)
        { // If the paddle has a side intersection we want to process it
          if (side == RectangleSide.Top)
          {
            var offset = ((ba.Bounds.X - pba.Bounds.X) + (ba.Bounds.Width / 2f)) / pba.Bounds.Width;
            var velX = (int)Math.Floor(offset * 11 - 5);
            va.Velocity = new Vector2(Core.MathHelper.Clamp(velX + va.Velocity.X, -5, 5), -Core.MathHelper.Clamp(va.Velocity.Y + 1, -8, 8));
          }
        }
        else if (ba.Bounds.X <= 0)
        { // We change the x direction if we hit the left side of the screen
          ba.Bounds.X = 0;
          va.Velocity = new Vector2(-va.Velocity.X, va.Velocity.Y);
        }
        else if ((ba.Bounds.Y - BlockConsts.TopOffset) <= 0)
        { // We change the y direction if we hit the top of the screen
          ba.Bounds.Y = BlockConsts.TopOffset;
          va.Velocity = new Vector2(va.Velocity.X, -va.Velocity.Y);
        }
        else if (ba.Bounds.X >= _graphics.Viewport.Width - ba.Bounds.Width)
        { // We change the x direction if we hit the right of the screen
          ba.Bounds.X = _graphics.Viewport.Width - ba.Bounds.Width;
          va.Velocity = new Vector2(-va.Velocity.X, va.Velocity.Y);
        }
        else if (ba.Bounds.Y >= _graphics.Viewport.Height - ba.Bounds.Height)
        { // Ball Lost Remove ball from calculation systems and trigger event
          _eventQueue.DispatchEvent(new LostBallEvent());
        }
        else
        { // Check if the ball has intersected with any blocks on this frame
          var blocks = _blockQuery.ToList();
          foreach (var block in blocks)
          {
            var bba = block.GetAddon<BoundsAddon>();
            side = RectangleHelper.SideIntersection(ba.Bounds, bba.Bounds);
            if (side != RectangleSide.None)
            { // If we are dealing with a collection we need to alter the direction based on what side was hit
              var bi = block.GetAddon<BlockIndexAddon>();
              if (side == RectangleSide.Left || side == RectangleSide.Right)
              { // Change the x direction if we hit left or right side
                va.Velocity = new Vector2(-va.Velocity.X, va.Velocity.Y);
              }
              else
              { // Change the y direction if we hit the top or bottom
                va.Velocity = new Vector2(va.Velocity.X, -va.Velocity.Y);
              }

              // Remove the block from the screen since the ball has hit it
              _provider.RemoveEntity(block);
              _service.Score += bi.Score;
              if (blocks.Count == 1)
              {
                _eventQueue.DispatchEvent(new NextRoundEvent());
              }
            }
          }
        }

        entity.Update(ba);
        entity.Update(va);
      }
    }
  }
}
