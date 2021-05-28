using BlueJay.Component.System.Systems;
using BlueJay.Events;
using BlueJay.Events.GamePad;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.Systems
{
  /// <summary>
  /// System is meant to handle gamepad inputs and send an event out to the system that can be listened to
  /// </summary>
  public class GamepadSystem : ComponentSystem
  {
    /// <summary>
    /// The list of all the player indexes in the system
    /// </summary>
    private readonly Dictionary<PlayerIndex, GamePadHandler> _handlers;

    /// <summary>
    /// The Identifier for this system 0 is used if we do not care about the entities
    /// </summary>
    public override long Key => 0;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>() { string.Empty };

    /// <summary>
    /// Constructor is meant to initialize the gamepad system so we can start processing the mouse events to the system
    /// </summary>
    /// <param name="queue">The event queue we will be dispatching events too</param>
    public GamepadSystem(EventQueue queue)
    {
      _handlers = new Dictionary<PlayerIndex, GamePadHandler>()
      {
        { PlayerIndex.One, new GamePadHandler(queue) },
        { PlayerIndex.Two, new GamePadHandler(queue) },
        { PlayerIndex.Three, new GamePadHandler(queue) },
        { PlayerIndex.Four, new GamePadHandler(queue) }
      };
    }

    /// <summary>
    /// The update event that is called before all entity update events for this system
    /// </summary>
    public override void OnUpdate()
    {
      var items = _handlers.ToArray();
      for (var i = 0; i < items.Length; ++i)
      {
        var pair = items[i];
        var capabilities = GamePad.GetCapabilities(pair.Key);
        if (capabilities.IsConnected)
        {
          var state = GamePad.GetState(pair.Key);
          pair.Value.HandleButtons(state, pair.Key);
          pair.Value.HandleTriggerEvents(state, pair.Key);
          pair.Value.HandleStickEvents(state, pair.Key);
        }
      }
    }

    /// <summary>
    /// Helper class is meant to handle the pressed events and hold previous states to pass with events
    /// </summary>
    private class GamePadHandler
    {
      /// <summary>
      /// The collection of button event presses that we should keep track of
      /// </summary>
      private readonly Dictionary<GamePadButtonEvent.ButtonType, bool> _pressed;

      /// <summary>
      /// The previous trigger from last frame
      /// </summary>
      private readonly Dictionary<GamePadTriggerEvent.TriggerType, float> _triggers;

      /// <summary>
      /// The previous stick from last frame
      /// </summary>
      private readonly Dictionary<GamePadStickEvent.ThumbStickType, Vector2> _sticks;

      /// <summary>
      /// The current event queue that should process events
      /// </summary>
      private readonly EventQueue _queue;

      /// <summary>
      /// Constructor is meant to build out the handler and setup all the data points needed to be listened on
      /// </summary>
      /// <param name="queue">The current event queue we need to work with</param>
      public GamePadHandler(EventQueue queue)
      {
        _queue = queue;
        _pressed = EnumHelper.GenerateEnumDictionary<GamePadButtonEvent.ButtonType, bool>(false);
        _triggers = EnumHelper.GenerateEnumDictionary<GamePadTriggerEvent.TriggerType, float>(0f);
        _sticks = EnumHelper.GenerateEnumDictionary<GamePadStickEvent.ThumbStickType, Vector2>(Vector2.Zero);
      }

      /// <summary>
      /// Helper method is meant to handle buttons that changed on this frame based on the last
      /// </summary>
      /// <param name="state">The current game pad state we are working with</param>
      /// <param name="index">The player index we are working with</param>
      public void HandleButtons(GamePadState state, PlayerIndex index)
      {
        var list = _pressed.ToArray();
        for (var i = 0; i < list.Length; ++i)
        {
          var pair = list[i];
          var btnState = GetButtonState(state, pair.Key);
          if (btnState == ButtonState.Pressed && !pair.Value)
          {
            _queue.DispatchEvent(new GamePadButtonDownEvent() { Type = pair.Key, Index = index });
            _pressed[pair.Key] = true;
          }
          else if (btnState == ButtonState.Released && pair.Value)
          {
            _queue.DispatchEvent(new GamePadButtonUpEvent() { Type = pair.Key, Index = index });
            _pressed[pair.Key] = false;
          }
        }
      }

      /// <summary>
      /// Handler is meant to work with stick events and process them based on the last frame
      /// </summary>
      /// <param name="state">The current game pad state we are working with</param>
      /// <param name="index">The player index we are working with</param>
      public void HandleStickEvents(GamePadState state, PlayerIndex index)
      {
        var list = _sticks.ToArray();
        for (var i = 0; i < list.Length; ++i)
        {
          var pair = list[i];
          var stickState = GetStickState(state, pair.Key);
          if (stickState != pair.Value)
          {
            _queue.DispatchEvent(new GamePadStickEvent() { Value = stickState, PreviousValue = pair.Value, Type = pair.Key, Index = index });
            _sticks[pair.Key] = stickState;
          }
        }
      }

      /// <summary>
      /// Handler is meant to work with the trigger events and process them based on the last frame
      /// </summary>
      /// <param name="state">The current game pad state we are working with</param>
      /// <param name="index">The player index we are working with</param>
      public void HandleTriggerEvents(GamePadState state, PlayerIndex index)
      {
        var list = _triggers.ToArray();
        for (var i = 0; i < list.Length; ++i)
        {
          var pair = list[i];
          var stickState = GetTriggerState(state, pair.Key);
          if (stickState != pair.Value)
          {
            _queue.DispatchEvent(new GamePadTriggerEvent() { Value = stickState, PreviousValue = pair.Value, Type = pair.Key, Index = index });
            _triggers[pair.Key] = stickState;
          }
        }
      }

      /// <summary>
      /// Getter to give one location to load all the buttons in the gamepad state
      /// </summary>
      /// <param name="state">The current game pad state we are working with</param>
      /// <param name="type">The enumeration type that is meant to lookup the data point</param>
      /// <returns>Will return the current button state for the given type</returns>
      private ButtonState GetButtonState(GamePadState state, GamePadButtonEvent.ButtonType type)
      {
        switch (type)
        {
          case GamePadButtonEvent.ButtonType.DPadDown: return state.DPad.Down;
          case GamePadButtonEvent.ButtonType.DPadLeft: return state.DPad.Left;
          case GamePadButtonEvent.ButtonType.DPadRight: return state.DPad.Right;
          case GamePadButtonEvent.ButtonType.DPadUp: return state.DPad.Up;
          case GamePadButtonEvent.ButtonType.RightShoulder: return state.Buttons.RightShoulder;
          case GamePadButtonEvent.ButtonType.LeftStick: return state.Buttons.LeftStick;
          case GamePadButtonEvent.ButtonType.LeftShoulder: return state.Buttons.LeftShoulder;
          case GamePadButtonEvent.ButtonType.Start: return state.Buttons.Start;
          case GamePadButtonEvent.ButtonType.Y: return state.Buttons.Y;
          case GamePadButtonEvent.ButtonType.X: return state.Buttons.X;
          case GamePadButtonEvent.ButtonType.RightStick: return state.Buttons.RightStick;
          case GamePadButtonEvent.ButtonType.Back: return state.Buttons.Back;
          case GamePadButtonEvent.ButtonType.A: return state.Buttons.A;
          case GamePadButtonEvent.ButtonType.B: return state.Buttons.B;
          case GamePadButtonEvent.ButtonType.BigButton: return state.Buttons.BigButton;
        }
        throw new ArgumentException("The enum type was not defined in the switch", nameof(type));
      }

      /// <summary>
      /// Getter for grabbing the trigger state based on the type given
      /// </summary>
      /// <param name="state">The current game pad state we are working with</param>
      /// <param name="type">The enumeration type that is meant to lookup the data point</param>
      /// <returns>Will give the current trigger float value for this frame</returns>
      private float GetTriggerState(GamePadState state, GamePadTriggerEvent.TriggerType type)
      {
        switch (type)
        {
          case GamePadTriggerEvent.TriggerType.Left: return state.Triggers.Left;
          case GamePadTriggerEvent.TriggerType.Right: return state.Triggers.Right;
        }
        throw new ArgumentException("The enum type was not defined in the switch", nameof(type));
      }

      /// <summary>
      /// Getter is meant to lookup the stick location based on the type given
      /// </summary>
      /// <param name="state">The current game pad state we are working with</param>
      /// <param name="type">The enumeration type that is meant to lookup the data point</param>
      /// <returns>Will return the current stick state based on the enumeration type</returns>
      private Vector2 GetStickState(GamePadState state, GamePadStickEvent.ThumbStickType type)
      {
        switch (type)
        {
          case GamePadStickEvent.ThumbStickType.Left: return state.ThumbSticks.Left;
          case GamePadStickEvent.ThumbStickType.Right: return state.ThumbSticks.Right;
        }
        throw new ArgumentException("The enum type was not defined in the switch", nameof(type));
      }
    }
  }
}
