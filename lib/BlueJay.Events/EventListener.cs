using BlueJay.Events.Interfaces;
using System;

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
    /// Property to determine if this event listener should process the event being triggered
    /// </summary>
    public object? ProcessTarget { get; protected set; }

    /// <summary>
    /// Helper method is meant to handle the internal event processing and pass it along to the abstracted process
    /// method
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    public void Process(IEvent evt)
    {
      Process((Event<T>)evt);
    }

    /// <summary>
    /// Helper method to determine if we should process this event listener
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    /// <returns>Will return a boolean determining if we should process the event listener</returns>
    public virtual bool ShouldProcess(IEvent evt)
    {
      return ProcessTarget == null || ProcessTarget == evt.Target;
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public abstract void Process(IEvent<T> evt);
  }
}
