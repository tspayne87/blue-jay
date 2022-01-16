using Microsoft.Xna.Framework;

namespace BlueJay.Common.Events.Mouse
{
  /// <summary>
  /// Mouse move event
  /// </summary>
  public sealed class MouseMoveEvent : MouseEvent
  {
    /// <summary>
    /// The previous position that was recorded last frame
    /// </summary>
    public Point? PreviousPosition { get; set; }
  }
}
