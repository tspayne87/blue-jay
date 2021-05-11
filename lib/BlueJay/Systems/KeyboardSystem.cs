using BlueJay.Component.System.Systems;
using BlueJay.Events;
using BlueJay.Events.Keyboard;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.Systems
{
  /// <summary>
  /// The keyboard system that is meant to send events out to the system based on what is happening in the keyboard state
  /// </summary>
  public class KeyboardSystem : ComponentSystem
  {
    /// <summary>
    /// The current event queue that should process events
    /// </summary>
    private readonly EventQueue _queue;

    /// <summary>
    /// The collection of button event presses that we should keep track of
    /// </summary>
    private readonly Dictionary<Keys, bool> _pressed;

    /// <summary>
    /// The Identifier for this system 0 is used if we do not care about the entities
    /// </summary>
    public override long Key => 0;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>() { string.Empty };

    /// <summary>
    /// Constructor is meant to initialize the mouse system so we can start processing the mouse events to the system
    /// </summary>
    /// <param name="queue">The event queue we will be dispatching events too</param>
    public KeyboardSystem(EventQueue queue)
    {
      _queue = queue;
      _pressed = EnumHelper.GenerateEnumDictionary<Keys, bool>(false);
    }

    /// <summary>
    /// The update event that is called before all entity update events for this system
    /// </summary>
    public override void OnUpdate()
    {
      var state = Keyboard.GetState();

      var items = _pressed.ToArray();
      for(var i = 0; i < items.Length; ++i)
      {
        var pair = items[i];
        var keyState = state[pair.Key];
        if (keyState == KeyState.Down && !pair.Value)
        {
          _queue.DispatchEvent(new KeyboardDownEvent() { Key = pair.Key, CapsLock = state.CapsLock, NumLock = state.NumLock });
          _queue.DispatchEvent(new KeyboardPressEvent() { Key = pair.Key, CapsLock = state.CapsLock, NumLock = state.NumLock });
          _pressed[pair.Key] = true;
        }
        else if (keyState == KeyState.Up && pair.Value)
        {
          _queue.DispatchEvent(new KeyboardUpEvent() { Key = pair.Key, CapsLock = state.CapsLock, NumLock = state.NumLock });
          _pressed[pair.Key] = false;
        }
        else if (keyState == KeyState.Down && pair.Value)
        {
          _queue.DispatchEvent(new KeyboardPressEvent() { Key = pair.Key, CapsLock = state.CapsLock, NumLock = state.NumLock });
        }
      }
    }
  }
}
