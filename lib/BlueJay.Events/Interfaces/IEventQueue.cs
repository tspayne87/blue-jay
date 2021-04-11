namespace BlueJay.Events.Interfaces
{
  /// <summary>
  /// The event queue interface that will allow developers to dispatch and add event listeners
  /// </summary>
  public interface IEventQueue
  {
    /// <summary>
    /// Helper method is meant to dispatch events, this will defer them to the next frame for the event queue and will not be processed
    /// in the same frame it is triggered
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="evt">The event that is being triggered</param>
    /// <param name="target">The current object that this event is targeting</param>
    void DispatchEvent<T>(T evt, object target = null);

    /// <summary>
    /// Helper method is meant to add on event listeners into the system so they can interact with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="handler">The handler when the event is fired</param>
    void AddEventListener<T>(IEventListener<T> handler);
  }
}
