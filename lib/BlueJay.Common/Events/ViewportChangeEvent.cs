using BlueJay.Core;

namespace BlueJay.Common.Events
{
  /// <summary>
  /// Viewport change event that happens when the size of the viewport has changed
  /// </summary>
  public sealed class ViewportChangeEvent
  {
    /// <summary>
    /// The current size of the viewport
    /// </summary>
    public Size Current { get; set; }

    /// <summary>
    /// The previous size of the viewport
    /// </summary>
    public Size Previous { get; set; }
  }
}
