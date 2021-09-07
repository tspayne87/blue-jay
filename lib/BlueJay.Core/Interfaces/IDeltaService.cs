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

    /// <summary>
    /// The current delta in seconds for each frame
    /// </summary>
    double DeltaSeconds { get; }
  }
}
