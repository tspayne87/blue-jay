using BlueJay.Common.Events;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// System is meant to trigger a viewport change event when the viewport changes
  /// </summary>
  public class ViewportSystem : IUpdateSystem
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The current event queue that should process events
    /// </summary>
    private readonly IEventQueue _queue;

    /// <summary>
    /// The previous screen Size
    /// </summary>
    private Size _previous;

    /// <inheritdoc />
    public long Key => 0;

    /// <inheritdoc />
    public List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor is meant to assign defaults and inject the graphic device
    /// </summary>
    /// <param name="graphics"></param>
    /// <param name="queue">The event queue we will be dispatching events too</param>
    public ViewportSystem(GraphicsDevice graphics, IEventQueue queue)
    {
      _graphics = graphics;
      _queue = queue;
      _previous = Size.Empty;
    }

    /// <inheritdoc />
    public void OnUpdate()
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
