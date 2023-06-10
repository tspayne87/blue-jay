using BlueJay.Common.Events.Keyboard;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Services;

namespace BlueJay.UI.Events.EventListeners
{
  internal class UIKeyboardUpEventListener : EventListener<KeyboardUpEvent>
  {
    /// <summary>
    /// The event queue that will trigger style update events to rerender the UI entity
    /// </summary>
    private readonly IEventQueue _eventQueue;

    /// <summary>
    /// The UI Service for keeping track of globals
    /// </summary>
    private readonly UIService _service;

    /// <summary>
    /// Constructor to build out the keyboard up listener to interact with UI entities
    /// </summary>
    /// <param name="service">The UI Service for keeping track of globals</param>
    /// <param name="eventQueue">The event queue that will trigger style update events to rerender the UI entity</param>
    public UIKeyboardUpEventListener(UIService service, IEventQueue eventQueue)
    {
      _service = service;
      _eventQueue = eventQueue;
    }

    /// <summary>
    /// Helper method to determine if we should process this event listener
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    /// <returns>Will return a boolean determining if we should process the event listener</returns>
    public override bool ShouldProcess(IEvent evt)
    {
      return _service.FocusedEntity != null;
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<KeyboardUpEvent> evt)
    {
      _eventQueue.DispatchEvent(evt.Data, _service.FocusedEntity);
    }
  }
}
