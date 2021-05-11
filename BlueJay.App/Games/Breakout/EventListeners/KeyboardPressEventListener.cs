using BlueJay.App.Games.Breakout.Addons;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Events.Keyboard;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace BlueJay.App.Games.Breakout.EventListeners
{
  public class KeyboardPressEventListener : EventListener<KeyboardPressEvent>
  {
    private readonly LayerCollection _layerCollection;
    private readonly EventQueue _eventQueue;

    public KeyboardPressEventListener(LayerCollection layerCollection, EventQueue eventQueue)
    {
      _layerCollection = layerCollection;
      _eventQueue = eventQueue;
    }

    public override void Process(IEvent<KeyboardPressEvent> evt)
    {
      var paddle = _layerCollection[LayerNames.PaddleLayer].Entities[0];
      var ba = paddle.GetAddon<BoundsAddon>();

      switch(evt.Data.Key)
      {
        case Keys.A:
          ba.Bounds = ba.Bounds.Add(new Vector2(-10, 0));
          break;
        case Keys.D:
          ba.Bounds = ba.Bounds.Add(new Vector2(10, 0));
          break;
        case Keys.Space:
          {
            var ball = _layerCollection[LayerNames.BallLayer].Entities[0];
            var baa = ball.GetAddon<BallActiveAddon>();
            if (!baa.IsActive)
            {
              baa.IsActive = true;
              _eventQueue.DispatchEvent(new StartBallEvent() { Ball = ball });
            }
          }
          break;
      }
    }
  }
}
