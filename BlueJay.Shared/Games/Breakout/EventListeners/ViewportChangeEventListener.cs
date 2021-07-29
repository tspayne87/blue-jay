using BlueJay.Events;
using BlueJay.Events.Interfaces;

namespace BlueJay.Shared.Games.Breakout.EventListeners
{
  public class ViewportChangeEventListener : EventListener<ViewportChangeEvent>
  {
    /// <summary>
    /// The current event queue that is being processed
    /// </summary>
    private readonly EventQueue _eventQueue;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    public ViewportChangeEventListener(EventQueue eventQueue)
    {
      _eventQueue = eventQueue;
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<ViewportChangeEvent> evt)
    {
      _eventQueue.DispatchEvent(new UpdateBoundsEvent() { Size = evt.Data.Current });
    }
  }
}
