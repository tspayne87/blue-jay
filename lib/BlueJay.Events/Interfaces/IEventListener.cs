namespace BlueJay.Events.Interfaces
{
  /// <summary>
  /// Basic event listener that does not require a type mainly used internally to store
  /// the event triggers
  /// </summary>
  public interface IEventListener
  {
    /// <summary>
    /// Helper method is meant to handle the internal event processing and pass it along to the abstracted process
    /// method
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    internal void Process(IEvent evt);

    /// <summary>
    /// Helper method to determine if we should process this event listener
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    /// <returns>Will return a boolean determining if we should process the event listener</returns>
    internal bool ShouldProcess(IEvent evt);
  }

  /// <summary>
  /// Interface to handle events when they come in
  /// </summary>
  /// <typeparam name="T">The type of event we are working with</typeparam>
  public interface IEventListener<T> : IEventListener
  {
    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    void Process(IEvent<T> evt);

    /// <summary>
    /// Helper method to determine if we should process this event listener
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    /// <returns>Will return a boolean determining if we should process the event listener</returns>
    bool ShouldProcess(IEvent<T> evt);
  }
}
