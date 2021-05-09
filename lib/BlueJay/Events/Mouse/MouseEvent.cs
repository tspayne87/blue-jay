using Microsoft.Xna.Framework;

namespace BlueJay.Events.Mouse
{
  /// <summary>
  /// Basic mouse event
  /// </summary>
  public abstract class MouseEvent
  {
    /// <summary>
    /// The current position of the mouse relative to the screen
    /// </summary>
    public Point Position { get; set; }

    /// <summary>
    /// The current button being pressed if null no button is being pressed
    /// </summary>
    public ButtonType? Button { get; set; }

    /// <summary>
    /// The different type of button types that could exist
    /// </summary>
    public enum ButtonType
    {
      Right, Middle, Left, XButton1, XButton2
    }
  }
}
