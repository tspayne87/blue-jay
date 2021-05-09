using Microsoft.Xna.Framework.Input;

namespace BlueJay.Events.Keyboard
{
  /// <summary>
  /// The basic keyboard event
  /// </summary>
  public abstract class KeyboardEvent
  {
    /// <summary>
    /// The current key that triggered an event
    /// </summary>
    public Keys Key { get; set; }

    /// <summary>
    /// If the caps lock is on
    /// </summary>
    public bool CapsLock { get; set; }

    /// <summary>
    /// If the num lock is on
    /// </summary>
    public bool NumLock { get; set; }
  }
}
