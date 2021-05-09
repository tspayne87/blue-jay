using Microsoft.Xna.Framework;

namespace BlueJay.Events.GamePad
{
  /// <summary>
  /// The basic game pad button event
  /// </summary>
  public abstract class GamePadButtonEvent
  {
    /// <summary>
    /// The type of button we are dealing with from the gamepad
    /// </summary>
    public ButtonType Type { get; set; }

    /// <summary>
    /// The player index we are working with
    /// </summary>
    public PlayerIndex Index { get; set; }

    /// <summary>
    /// The different button types we are working with on the gamepad
    /// </summary>
    public enum ButtonType
    {
      DPadDown, DPadLeft, DPadRight, DPadUp, RightShoulder, LeftStick, LeftShoulder, Start, X, Y, RightStick, Back, A, B, BigButton
    }
  }
}
