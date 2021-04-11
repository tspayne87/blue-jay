namespace BlueJay.Events.Interfaces
{
  /// <summary>
  /// The event processor to hide the internals so we can process the event queue
  /// </summary>
  public interface IEventProcessor
  {
    /// <summary>
    /// The process method is meant to process one tick of the game and all the events that should be processed at that time
    /// </summary>
    /// <returns>Will return true if it should continue processing</returns>
    bool Process();
  }
}
