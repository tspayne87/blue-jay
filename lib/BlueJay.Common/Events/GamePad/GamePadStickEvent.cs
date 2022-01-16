using Microsoft.Xna.Framework;

namespace BlueJay.Common.Events.GamePad
{
  /// <summary>
  /// The game pad stick event
  /// </summary>
  public sealed class GamePadStickEvent
  {
    /// <summary>
    /// The current value this stick has for this frame
    /// </summary>
    public Vector2 Value { get; set; }

    /// <summary>
    /// The previous frames stick value
    /// </summary>
    public Vector2 PreviousValue { get; set; }

    /// <summary>
    /// The type
    /// </summary>
    public ThumbStickType Type { get; set; }

    /// <summary>
    /// The current player index that triggered this event
    /// </summary>
    public PlayerIndex Index { get; set; }

    /// <summary>
    /// The type of thumb sticks that exist on the gamepad
    /// </summary>
    public enum ThumbStickType
    {
      Left, Right
    }
  }
}
