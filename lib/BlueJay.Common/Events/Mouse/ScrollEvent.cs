using Microsoft.Xna.Framework;

namespace BlueJay.Common.Events.Mouse
{
  /// <summary>
  /// The mouse scroll event
  /// </summary>
  public sealed class ScrollEvent
  {
    /// <summary>
    /// The current scroll wheel value that was tracked
    /// </summary>
    public int ScrollWheelValue { get; set; }

    /// <summary>
    /// Last frames scroll wheel value that was tracked
    /// </summary>
    public int PreviousScrollWheelValue { get; set; }

    /// <summary>
    /// The current position of the mouse cursor
    /// </summary>
    public Point Position { get; set; }
  }
}
