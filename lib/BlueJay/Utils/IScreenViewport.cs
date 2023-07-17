namespace BlueJay.Utils
{
  /// <summary>
  /// Interface is meant to interact with the current screen's viewport
  /// </summary>
  public interface IScreenViewport
  {
    /// <summary>
    /// The current width of the screen's viewport
    /// </summary>
    int Width { get; }

    /// <summary>
    /// The current height of the screen's viewport
    /// </summary>
    int Height { get; }
  }
}
