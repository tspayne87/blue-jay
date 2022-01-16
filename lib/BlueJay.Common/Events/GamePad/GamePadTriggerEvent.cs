using Microsoft.Xna.Framework;

namespace BlueJay.Common.Events.GamePad
{
  /// <summary>
  /// The game pad trigger event
  /// </summary>
  public sealed class GamePadTriggerEvent
  {
    /// <summary>
    /// The current frame pressure of the game pad trigger
    /// </summary>
    public float Value { get; set; }

    /// <summary>
    /// The previous frame pressure of the game pad trigger
    /// </summary>
    public float PreviousValue { get; set; }

    /// <summary>
    /// The type of trigger that was pressed
    /// </summary>
    public TriggerType Type { get; set; }

    /// <summary>
    /// The player index that triggered this event
    /// </summary>
    public PlayerIndex Index { get; set; }

    /// <summary>
    /// The type of trigger that was pressed
    /// </summary>
    public enum TriggerType
    {
      Left, Right
    }
  }
}
