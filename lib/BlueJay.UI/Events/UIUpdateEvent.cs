using BlueJay.Core;

namespace BlueJay.UI.Events
{
  /// <summary>
  /// UI Update event that is meant to determine the size of the screen
  /// </summary>
  public class UIUpdateEvent
  {
    /// <summary>
    /// The current size of the UI screen
    /// </summary>
    public Size Size { get; set; }
  }
}
