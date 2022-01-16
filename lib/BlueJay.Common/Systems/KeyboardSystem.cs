using BlueJay.Component.System.Interfaces;
using BlueJay.Events.Interfaces;
using BlueJay.Common.Events.Keyboard;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// The keyboard system that is meant to send events out to the system based on what is happening in the keyboard state
  /// </summary>
  public class KeyboardSystem : IUpdateSystem
  {
    /// <summary>
    /// The current event queue that should process events
    /// </summary>
    private readonly IEventQueue _queue;

    /// <summary>
    /// The collection of button event presses that we should keep track of
    /// </summary>
    private readonly Dictionary<Keys, bool> _pressed;

    /// <inheritdoc />
    public long Key => 0;

    /// <inheritdoc />
    public List<string> Layers => new List<string>() { string.Empty };

    /// <summary>
    /// Constructor is meant to initialize the mouse system so we can start processing the mouse events to the system
    /// </summary>
    /// <param name="queue">The event queue we will be dispatching events too</param>
    public KeyboardSystem(IEventQueue queue)
    {
      _queue = queue;
      _pressed = EnumHelper.GenerateEnumDictionary<Keys, bool>(false);
    }

    /// <inheritdoc />
    public void OnUpdate()
    {
      var state = Keyboard.GetState();

      var items = _pressed.ToArray();
      var shift = state.IsKeyDown(Keys.LeftShift) || state.IsKeyDown(Keys.RightShift);
      var ctrl = state.IsKeyDown(Keys.LeftControl) || state.IsKeyDown(Keys.RightControl);
      var alt = state.IsKeyDown(Keys.LeftAlt) || state.IsKeyDown(Keys.RightAlt);
      for(var i = 0; i < items.Length; ++i)
      {
        var pair = items[i];
        var keyState = state[pair.Key];
        if (keyState == KeyState.Down && !pair.Value)
        {
          _queue.DispatchEvent(new KeyboardDownEvent() { Key = pair.Key, CapsLock = state.CapsLock, NumLock = state.NumLock, Shift = shift, Ctrl = ctrl, Alt = alt });
          _queue.DispatchEvent(new KeyboardPressEvent() { Key = pair.Key, CapsLock = state.CapsLock, NumLock = state.NumLock, Shift = shift, Ctrl = ctrl, Alt = alt });
          _pressed[pair.Key] = true;
        }
        else if (keyState == KeyState.Up && pair.Value)
        {
          _queue.DispatchEvent(new KeyboardUpEvent() { Key = pair.Key, CapsLock = state.CapsLock, NumLock = state.NumLock, Shift = shift, Ctrl = ctrl, Alt = alt });
          _pressed[pair.Key] = false;
        }
        else if (keyState == KeyState.Down && pair.Value)
        {
          _queue.DispatchEvent(new KeyboardPressEvent() { Key = pair.Key, CapsLock = state.CapsLock, NumLock = state.NumLock, Shift = shift, Ctrl = ctrl, Alt = alt });
        }
      }
    }
  }
}
