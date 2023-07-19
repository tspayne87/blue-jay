using Microsoft.Xna.Framework;

namespace BlueJay.UI.Events
{
  /// <summary>
  /// Focus event that is triggered when the element is focused or selected by the user
  /// </summary>
  public class FocusEvent
  {
    /// <summary>
    /// The current position of the mouse when we focus on the element
    /// </summary>
    public Point Position { get; set; }
  }
}
