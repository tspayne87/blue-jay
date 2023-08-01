using Microsoft.Xna.Framework;

namespace BlueJay.UI.Events
{
  /// <summary>
  /// The hover event that is trigger when the mouse is hovering over an element
  /// </summary>
  public class HoverEvent
  {
    /// <summary>
    /// The current position of the mouse when we hover on the element
    /// </summary>
    public Point Position { get; set; }

    /// <summary>
    /// Constructor to build out the hover event
    /// </summary>
    /// <param name="position">The current position of the mouse when we hover on the element</param>
    public HoverEvent(Point position)
    {
      Position = position;
    }
  }
}
