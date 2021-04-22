using BlueJay.Events.Interfaces;

namespace BlueJay.Events
{
  /// <summary>
  /// Event listener class that will implement the basic process method that will call the abstracted process
  /// method on the inherted object so that we can keep type consistancy
  /// </summary>
  /// <typeparam name="T">The event that has been triggered</typeparam>
  public abstract class EventListener<T> : IEventListener<T>
  {
    /// <summary>
    /// Helper method is meant to handle the internal event processing and pass it along to the abstracted process
    /// method
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    public void Process(IEvent evt)
    {
      Process(evt as IEvent<T>);
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public abstract void Process(IEvent<T> evt);
  }
}
