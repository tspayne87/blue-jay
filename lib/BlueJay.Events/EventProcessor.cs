using BlueJay.Events.Interfaces;

namespace BlueJay.Events
{
  /// <summary>
  /// The event processor to hide the internals so we can process the event queue
  /// </summary>
  internal class EventProcessor : IEventProcessor
  {
    /// <summary>
    /// The current queue processor we should be using
    /// </summary>
    private readonly IEventQueue _queue;

    /// <summary>
    /// Constructor to add in the event queue processor that should be used with the event processor
    /// </summary>
    /// <param name="queue">The queue processor</param>
    public EventProcessor(IEventQueue queue)
    {
      _queue = queue;
    }

    /// <summary>
    /// The process method is meant to process one tick of the game and all the events that should be processed at that time
    /// </summary>
    /// <returns>Will return true if it should continue processing</returns>
    public void Update()
    {
      _queue.Tick();
      _queue.Update();
    }

    /// <summary>
    /// The process method that is meant to handle the draw event
    /// </summary>
    public void Draw()
    {
      _queue.Draw();
    }

    /// <summary>
    /// Handle the activate event for the event queue
    /// </summary>
    public void Activate()
    {
      _queue.Activate();
    }

    /// <summary>
    /// Handle the deactivate event for the event queue
    /// </summary>
    public void Deactivate()
    {
      _queue.Deactivate();
    }

    /// <summary>
    /// Handle the exit event for the event queue
    /// </summary>
    public void Exit()
    {
      _queue.Exit();
    }
  }
}
