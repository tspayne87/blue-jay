using BlueJay.Component.System.Systems;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Input.Touch;
using BlueJay.Events;
using BlueJay.Events.Touch;

namespace BlueJay.Systems
{
  /// <summary>
  /// The touch system that is meant to trigger events out of the touch state
  /// </summary>
  public class TouchSystem : ComponentSystem
  {
    /// <summary>
    /// The current event queue that should process events
    /// </summary>
    private readonly EventQueue _queue;

    /// <summary>
    /// The Identifier for this system 0 is used if we do not care about the entities
    /// </summary>
    public override long Key => 0;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor is meant to initialize the mouse system so we can start processing the mouse events to the system
    /// </summary>
    /// <param name="queue">The event queue we will be dispatching events too</param>
    public TouchSystem(EventQueue queue)
    {
      _queue = queue;
    }

    /// <summary>
    /// The update event that is called before all entity update events for this system
    /// </summary>
    public override void OnUpdate()
    {
      var touches = TouchPanel.GetState();

      if (touches.Count == 1)
      {
        var touch = touches[0];
        switch(touch.State)
        {
          case TouchLocationState.Pressed:
            _queue.DispatchEvent(new TouchDownEvent() { Position = touch.Position, Pressure = touch.Pressure });
            break;
          case TouchLocationState.Released:
            _queue.DispatchEvent(new TouchUpEvent() { Position = touch.Position, Pressure = touch.Pressure });
            break;
          case TouchLocationState.Moved:
            _queue.DispatchEvent(new TouchMoveEvent() { Position = touch.Position, Pressure = touch.Pressure });
            break;
        }
      }
    }
  }
}
