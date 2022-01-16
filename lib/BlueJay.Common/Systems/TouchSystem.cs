using System.Collections.Generic;
using Microsoft.Xna.Framework.Input.Touch;
using BlueJay.Common.Events.Touch;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events.Interfaces;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// The touch system that is meant to trigger events out of the touch state
  /// </summary>
  public class TouchSystem : IUpdateSystem
  {
    /// <summary>
    /// The current event queue that should process events
    /// </summary>
    private readonly IEventQueue _queue;

    /// <inheritdoc />
    public long Key => 0;

    /// <inheritdoc />
    public List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor is meant to initialize the mouse system so we can start processing the mouse events to the system
    /// </summary>
    /// <param name="queue">The event queue we will be dispatching events too</param>
    public TouchSystem(IEventQueue queue)
    {
      _queue = queue;
    }

    /// <inheritdoc />
    public void OnUpdate()
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
