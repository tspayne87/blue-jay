﻿using BlueJay.Component.System.Systems;
using BlueJay.Events;
using BlueJay.Events.Mouse;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.Systems
{
  /// <summary>
  /// The mouse system that is meant to trigger events out to the systems
  /// </summary>
  public class MouseSystem : ComponentSystem
  {
    /// <summary>
    /// The collection of button event presses that we should keep track of
    /// </summary>
    private readonly Dictionary<MouseEvent.ButtonType, bool> _pressed;

    /// <summary>
    /// The current event queue that should process events
    /// </summary>
    private readonly EventQueue _queue;

    /// <summary>
    /// The previous position that was processed the last frame
    /// </summary>
    private Point PreviousPosition { get; set; }

    /// <summary>
    /// The previous scroll wheel value to determine if an event needs to fire
    /// </summary>
    private int PreviousScrollWheelValue { get; set; }

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
    public MouseSystem(EventQueue queue)
    {
      _queue = queue;
      _pressed = EnumHelper.GenerateEnumDictionary<MouseEvent.ButtonType, bool>(false);

      PreviousPosition = Point.Zero;
      PreviousScrollWheelValue = 0;
    }

    /// <summary>
    /// The update event that is called before all entity update events for this system
    /// </summary>
    public override void OnUpdate()
    {
      // Get the current moust state to process
      var state = Mouse.GetState();

      // Handle all the mouse up and down events based on the button pressed
      var list = _pressed.ToArray();
      for(var i = 0; i < list.Length; ++i)
      {
        var pair = list[i];
        var btnState = GetState(state, pair.Key);
        if (btnState == ButtonState.Pressed && !pair.Value)
        {
          _queue.DispatchEvent(new MouseDownEvent() { Position = state.Position, Button = pair.Key });
          _pressed[pair.Key] = true;
        }
        else if (btnState == ButtonState.Released && pair.Value)
        {
          _queue.DispatchEvent(new MouseUpEvent() { Position = state.Position, Button = pair.Key });
          _pressed[pair.Key] = false;
        }
      }

      // Trigger event for when the mouse moves
      if (PreviousPosition != state.Position)
      {
        _queue.DispatchEvent(new MouseMoveEvent() { Position = state.Position, PreviousPosition = PreviousPosition });
      }

      // Trigger event for scroll wheel
      if (PreviousScrollWheelValue != state.ScrollWheelValue)
      {
        _queue.DispatchEvent(new ScrollEvent() { PreviousScrollWheelValue = PreviousScrollWheelValue, ScrollWheelValue = state.ScrollWheelValue });
      }

      // Set all the previous states
      PreviousPosition = state.Position;
      PreviousScrollWheelValue = state.ScrollWheelValue;
    }

    /// <summary>
    /// Get the current button state based on the enum type given
    /// </summary>
    /// <param name="state">The current mouse state we are processing</param>
    /// <param name="type">The enum button type we need to look up</param>
    /// <returns>Will return the button state</returns>
    private ButtonState GetState(MouseState state, MouseEvent.ButtonType type)
    {
      switch(type)
      {
        case MouseEvent.ButtonType.Left: return state.LeftButton;
        case MouseEvent.ButtonType.Middle: return state.MiddleButton;
        case MouseEvent.ButtonType.Right: return state.RightButton;
        case MouseEvent.ButtonType.XButton1: return state.XButton1;
        case MouseEvent.ButtonType.XButton2: return state.XButton2;
      }
      throw new ArgumentException("The enum type was not defined in the switch", nameof(type));
    }
  }
}
