namespace BlueJay.Events.Interfaces
{
  /// <summary>
  /// Interface to handle events when they come in
  /// </summary>
  /// <typeparam name="T">The type of event we are working with</typeparam>
  public interface IEventListener<T>
  {
    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    void Process(IEvent<T> evt);
  }
}
