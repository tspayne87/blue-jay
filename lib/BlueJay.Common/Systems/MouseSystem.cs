using BlueJay.Component.System.Interfaces;
using BlueJay.Events.Interfaces;
using BlueJay.Common.Events.Mouse;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using static BlueJay.Common.Events.Mouse.MouseEvent;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// The mouse system that is meant to trigger events out to the systems
  /// </summary>
  public class MouseSystem : IUpdateSystem
  {
    /// <summary>
    /// The collection of button event presses that we should keep track of
    /// </summary>
    private readonly Dictionary<ButtonType, bool> _pressed;

    /// <summary>
    /// The current event queue that should process events
    /// </summary>
    private readonly IEventQueue _queue;

    /// <summary>
    /// The previous position that was processed the last frame
    /// </summary>
    private Point PreviousPosition { get; set; }

    /// <summary>
    /// The previous scroll wheel value to determine if an event needs to fire
    /// </summary>
    private int PreviousScrollWheelValue { get; set; }

    /// <inheritdoc />
    public long Key => 0;

    /// <inheritdoc />
    public List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor is meant to initialize the mouse system so we can start processing the mouse events to the system
    /// </summary>
    /// <param name="queue">The event queue we will be dispatching events too</param>
    public MouseSystem(IEventQueue queue)
    {
      _queue = queue;
      _pressed = EnumHelper.GenerateEnumDictionary<ButtonType, bool>(false);

      PreviousPosition = Point.Zero;
      PreviousScrollWheelValue = 0;
    }

    /// <inheritdoc />
    public void OnUpdate()
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
        ButtonType? key = null;
        if (state.LeftButton == ButtonState.Pressed) key = ButtonType.Left;
        else if (state.RightButton == ButtonState.Pressed) key = ButtonType.Right;
        else if (state.MiddleButton == ButtonState.Pressed) key = ButtonType.Middle;
        else if (state.XButton1 == ButtonState.Pressed) key = ButtonType.XButton1;
        else if (state.XButton2 == ButtonState.Pressed) key = ButtonType.XButton2;
        _queue.DispatchEvent(new MouseMoveEvent() { Position = state.Position, PreviousPosition = PreviousPosition, Button = key });
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
    private ButtonState GetState(MouseState state, ButtonType type)
    {
      switch(type)
      {
        case ButtonType.Left: return state.LeftButton;
        case ButtonType.Middle: return state.MiddleButton;
        case ButtonType.Right: return state.RightButton;
        case ButtonType.XButton1: return state.XButton1;
        case ButtonType.XButton2: return state.XButton2;
      }
      throw new ArgumentException("The enum type was not defined in the switch", nameof(type));
    }
  }
}
