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

    /// <summary>
    /// If the shift is pressed at the moment
    /// </summary>
    public bool Shift { get; set; }

    /// <summary>
    /// If ths ctrl is pressed at the moment
    /// </summary>
    public bool Ctrl { get; set; }

    /// <summary>
    /// If the alt is pressed at the moment
    /// </summary>
    public bool Alt { get; set; }
  }
}
