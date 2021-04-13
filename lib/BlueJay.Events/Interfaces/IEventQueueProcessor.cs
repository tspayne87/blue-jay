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
    void Update();

    /// <summary>
    /// Helper method to handle the draw event
    /// </summary>
    void Draw();

    /// <summary>
    /// Helper method to push whatever is in the defered queue into the current queue
    /// </summary>
    void Tick();
  }
}
