using BlueJay.Component.System.Systems;
using BlueJay.Core;
using BlueJay.Events;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.Systems
{
  /// <summary>
  /// System is meant to trigger a viewport change event when the viewport changes
  /// </summary>
  public class ViewportSystem : ComponentSystem
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The current event queue that should process events
    /// </summary>
    private readonly EventQueue _queue;

    /// <summary>
    /// The previous screen Size
    /// </summary>
    private Size _previous;

    /// <summary>
    /// The current addon key that is meant to act as a selector for the Draw/Update
    /// methods with entities
    /// </summary>
    public override long Key => 0;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor is meant to assign defaults and inject the graphic device
    /// </summary>
    /// <param name="graphics"></param>
    /// <param name="queue">The event queue we will be dispatching events too</param>
    public ViewportSystem(GraphicsDevice graphics, EventQueue queue)
    {
      _graphics = graphics;
      _queue = queue;
      _previous = Size.Empty;
    }

    /// <summary>
    /// The update event that is called before all entity update events for this system
    /// </summary>
    public override void OnUpdate()
    {
      var current = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height);
      if (_previous != current)
      { // If the viewport has changed trigger and event in the system
        _queue.DispatchEvent(new ViewportChangeEvent() { Current = current, Previous = _previous });
        _previous = current;
      }
    }
  }
}
