﻿using BlueJay.Shared.Games.Breakout.Addons;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Common.Events.Keyboard;

namespace BlueJay.Shared.Games.Breakout.EventListeners
{
  /// <summary>
  /// Keyboard press listener is meant to watch for when keyboard buttons are pressed so we can act on them
  /// </summary>
  public class KeyboardPressEventListener : EventListener<KeyboardPressEvent>
  {
    /// <summary>
    /// The layer collection that has all the entities in the game at the moment
    /// </summary>
    private readonly ILayerCollection _layerCollection;

    /// <summary>
    /// The event queue we want to dispatch events too
    /// </summary>
    private readonly IEventQueue _eventQueue;

    /// <summary>
    /// Constructor to inject the scoped items into the listener to handle different process
    /// </summary>
    /// <param name="layerCollection">The layer colllection we are working with</param>
    /// <param name="eventQueue">The entity queue to dispatch events to the game</param>
    public KeyboardPressEventListener(ILayerCollection layerCollection, IEventQueue eventQueue)
    {
      _layerCollection = layerCollection;
      _eventQueue = eventQueue;
    }

    /// <summary>
    /// Helper method is meant to handle the internal event processing and pass it along to the abstracted process
    /// method
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    public override void Process(IEvent<KeyboardPressEvent> evt)
    {
      if (_layerCollection[LayerNames.PaddleLayer].Count == 1)
      {
        var paddle = _layerCollection[LayerNames.PaddleLayer][0];
        var ba = paddle.GetAddon<BoundsAddon>();

        switch (evt.Data.Key)
        {
          case Keys.A: // Move left if A is pressed
          case Keys.Left:
            ba.Bounds = ba.Bounds.Add(new Vector2(-10, 0));
            paddle.Update(ba);
            break;
          case Keys.D: // Move right if D is pressed
          case Keys.Right:
            ba.Bounds = ba.Bounds.Add(new Vector2(10, 0));
            paddle.Update(ba);
            break;
          case Keys.Space:
            if (_layerCollection[LayerNames.BallLayer].Count == 1)
            { // Trigger an event to start the game if the ball is not active 
              var ball = _layerCollection[LayerNames.BallLayer][0];
              var baa = ball.GetAddon<BallActiveAddon>();
              if (!baa.IsActive)
              {
                baa.IsActive = true;
                ball.Update(baa);
                _eventQueue.DispatchEvent(new StartBallEvent() { Ball = ball });
              }
            }
            break;
        }
      }
    }
  }
}
