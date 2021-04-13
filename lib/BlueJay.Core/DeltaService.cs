using BlueJay.Core.Interfaces;

namespace BlueJay.Core
{
  /// <summary>
  /// Delta service to keep track of the current ticks delta value
  /// </summary>
  public class DeltaService : IDeltaService
  {
    /// <summary>
    /// The current delta for each frame
    /// </summary>
    public int Delta { get; set; }
  }
}
