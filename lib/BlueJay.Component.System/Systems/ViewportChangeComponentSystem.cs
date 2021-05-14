using BlueJay.Core;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Component.System.Systems
{
  /// <summary>
  /// Abstract base class meant to be a wrapper for component systems that only need to update 
  /// </summary>
  public abstract class ViewportChangeComponentSystem : ComponentSystem
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The previous screen Size
    /// </summary>
    private Size _previous;

    /// <summary>
    /// If we have a change we want to process entities in the system
    /// </summary>
    private bool _hasChange;

    /// <summary>
    /// The key we want to store since it is what we need to use to find entities in the game
    /// </summary>
    protected abstract long _key { get; }

    /// <summary>
    /// The current addon key that is meant to act as a selector for the Draw/Update
    /// methods with entities
    /// </summary>
    public override long Key => _hasChange ? _key : 0;

    /// <summary>
    /// Constructor is meant to assign defaults and inject the graphic device
    /// </summary>
    /// <param name="graphics"></param>
    public ViewportChangeComponentSystem(GraphicsDevice graphics)
    {
      _graphics = graphics;
      _hasChange = false;
      _previous = Size.Empty;
    }

    /// <summary>
    /// The update event that is called before all entity update events for this system
    /// </summary>
    public override void OnUpdate()
    {
      var current = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height);

      // Track changes to the viewport so that on update for entities are only done when the viewport changes
      _hasChange = false;
      if (_previous != current)
      {
        _hasChange = true;
        _previous = current;
      }
    }
  }
}
