namespace BlueJay.Core.Interfaces
{
  /// <summary>
  /// Delta service to keep track of the current ticks delta value
  /// </summary>
  public interface IDeltaService
  {
    /// <summary>
    /// The current delta for each frame
    /// </summary>
    int Delta { get; }
  }
}
