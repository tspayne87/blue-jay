namespace BlueJay.Events.Interfaces
{
  /// <summary>
  /// Event processor is meant to handle the current queue and tick it to the next frame
  /// </summary>
  public interface IEventQueueProcessor
  {
    /// <summary>
    /// Helper method to process the current queue
    /// </summary>
    /// <returns>Will return if we need to continue processing</returns>
    bool ProcessCurrent();

    /// <summary>
    /// Helper method to push whatever is in the defered queue into the current queue
    /// </summary>
    void Tick();
  }
}
