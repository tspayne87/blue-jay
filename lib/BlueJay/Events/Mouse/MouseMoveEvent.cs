using Microsoft.Xna.Framework;

namespace BlueJay.Events.Mouse
{
  /// <summary>
  /// Mouse move event
  /// </summary>
  public class MouseMoveEvent : MouseEvent
  {
    /// <summary>
    /// The previous position that was recorded last frame
    /// </summary>
    public Point? PreviousPosition { get; set; }
  }
}
