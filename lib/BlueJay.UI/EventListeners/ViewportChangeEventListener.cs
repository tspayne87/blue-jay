using BlueJay.Common.Events;
using BlueJay.Events;
using BlueJay.Events.Interfaces;

namespace BlueJay.UI.EventListeners
{
  /// <summary>
  /// Listener is meant to trigger an event that we need to update the UI
  /// </summary>
  public class ViewportChangeEventListener : EventListener<ViewportChangeEvent>
  {
    /// <summary>
    /// The current event queue that is being processed
    /// </summary>
    private readonly IEventQueue _eventQueue;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="eventQueue">The event queue to trigger the UI update event</param>
    public ViewportChangeEventListener(IEventQueue eventQueue)
    {
      _eventQueue = eventQueue;
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<ViewportChangeEvent> evt)
    {
      _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = evt.Data.Current });
    }
  }
}
