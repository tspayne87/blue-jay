using BlueJay.Events.Interfaces;

namespace BlueJay.Events
{
  /// <summary>
  /// The event processor to hide the internals so we can process the event queue
  /// </summary>
  public class EventProcessor : IEventProcessor
  {
    /// <summary>
    /// The current queue processor we should be using
    /// </summary>
    private readonly IEventQueueProcessor _queue;

    /// <summary>
    /// Constructor to add in the event queue processor that should be used with the event processor
    /// </summary>
    /// <param name="queue">The queue processor</param>
    public EventProcessor(IEventQueueProcessor queue)
    {
      _queue = queue;
    }

    /// <summary>
    /// The process method is meant to process one tick of the game and all the events that should be processed at that time
    /// </summary>
    /// <returns>Will return true if it should continue processing</returns>
    public bool Process()
    {
      _queue.Tick();
      return _queue.ProcessCurrent();
    }
  }
}
