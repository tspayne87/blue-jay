using BlueJay.Core;

namespace BlueJay.Shared.Games.Breakout
{
  /// <summary>
  /// Event is meant to update the bounds for all the entities in the system
  /// </summary>
  public class UpdateBoundsEvent
  {
    /// <summary>
    /// The current size of the viewport
    /// </summary>
    public Size Size { get; set; }
  }
}
