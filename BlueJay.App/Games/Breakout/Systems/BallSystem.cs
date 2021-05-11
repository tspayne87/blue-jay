using BlueJay.App.Games.Breakout.Addons;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using BlueJay.Core;
using BlueJay.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BlueJay.App.Games.Breakout.Systems
{
  public class BallSystem : ComponentSystem
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;
    private readonly LayerCollection _layerCollection;
    private readonly EventQueue _eventQueue;

    public override long Key => BoundsAddon.Identifier | VelocityAddon.Identifier | BallActiveAddon.Identifier;

    public override List<string> Layers => new List<string>() { LayerNames.BallLayer };

    public BallSystem(LayerCollection layerCollection, GraphicsDevice graphics, EventQueue eventQueue)
    {
      _layerCollection = layerCollection;
      _graphics = graphics;
      _eventQueue = eventQueue;
    }

    public override void OnUpdate(IEntity entity)
    {
      var baa = entity.GetAddon<BallActiveAddon>();
      if (!baa.IsActive) BeforeStart(entity);
      else DuringGame(entity);
    }

    private void BeforeStart(IEntity entity)
    {
      var paddle = _layerCollection[LayerNames.PaddleLayer].Entities[0];
      var ba = entity.GetAddon<BoundsAddon>();

      var pba = paddle.GetAddon<BoundsAddon>();

      ba.Bounds.X = pba.Bounds.X + ((pba.Bounds.Width - ba.Bounds.Width) / 2);
      ba.Bounds.Y = pba.Bounds.Y - ba.Bounds.Height;
    }

    private void DuringGame(IEntity entity)
    {
      var ba = entity.GetAddon<BoundsAddon>();
      var va = entity.GetAddon<VelocityAddon>();

      ba.Bounds = ba.Bounds.Add(va.Velocity);


      var paddle = _layerCollection[LayerNames.PaddleLayer].Entities[0];
      var pba = paddle.GetAddon<BoundsAddon>();

      var side = RectangleHelper.SideIntersection(ba.Bounds, pba.Bounds);
      if (side != RectangleSide.None)
      {
        if (side == RectangleSide.Top)
        {
          var offset = ((ba.Bounds.X - pba.Bounds.X) + (ba.Bounds.Width / 2f)) / pba.Bounds.Width;
          var velX = (int)Math.Floor(offset * 11 - 5);
          va.Velocity = new Vector2(Math.Clamp(velX + va.Velocity.X, -5, 5), -Math.Clamp(va.Velocity.Y + 1, -8, 8));
        }
      }
      else if (ba.Bounds.X <= 0)
      {
        ba.Bounds.X = 0;
        va.Velocity = new Vector2(-va.Velocity.X, va.Velocity.Y);
      }
      else if (ba.Bounds.Y <= 0)
      {
        ba.Bounds.Y = 0;
        va.Velocity = new Vector2(va.Velocity.X, -va.Velocity.Y);
      }
      else if (ba.Bounds.X >= _graphics.Viewport.Width - ba.Bounds.Width)
      {
        ba.Bounds.X = _graphics.Viewport.Width - ba.Bounds.Width;
        va.Velocity = new Vector2(-va.Velocity.X, va.Velocity.Y);
      }
      else if (ba.Bounds.Y >= _graphics.Viewport.Height - ba.Bounds.Height)
      {
        // Ball Lost Remove ball from calculation systems and trigger event
        _layerCollection[LayerNames.BallLayer].Entities.Remove(entity);
        _eventQueue.DispatchEvent(new EndGameEvent());
      }
      else
      {
        var blocks = _layerCollection[LayerNames.BlockLayer].Entities;
        for (var i = 0; i < blocks.Count; ++i)
        {
          var bba = blocks[i].GetAddon<BoundsAddon>();
          side = RectangleHelper.SideIntersection(ba.Bounds, bba.Bounds);
          if (side != RectangleSide.None)
          {
            if (side == RectangleSide.Left || side == RectangleSide.Right)
            {
              va.Velocity = new Vector2(-va.Velocity.X, va.Velocity.Y);
            }
            else
            {
              va.Velocity = new Vector2(va.Velocity.X, -va.Velocity.Y);
            }

            _layerCollection[LayerNames.BlockLayer].Entities.Remove(blocks[i]);
          }
        }
      }
    }
  }
}
