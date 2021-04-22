namespace BlueJay.Events.Interfaces
{
  /// <summary>
  /// The event object that will be sent when an event is triggered
  /// </summary>
  public interface IEvent
  {
    /// <summary>
    /// The current target the event came from
    /// </summary>
    object Target { get; }

    /// <summary>
    /// The current name of the event
    /// </summary>
    string Name { get; }

    /// <summary>
    /// If this event is complete and needs to stop propagating
    /// </summary>
    bool IsComplete { get; }

    /// <summary>
    /// Method is meant to stop the event cycle from moving forward and make sure that no other event listeners past the
    /// one that called this method gets ran
    /// </summary>
    void StopPropagation();
  }

  /// <summary>
  /// The event object that will be sent when an event is triggered
  /// </summary>
  /// <typeparam name="TData">The data that is sent from the triggered event</typeparam>
  public interface IEvent<TData> : IEvent
  {
    /// <summary>
    /// The data that comes along with the event
    /// </summary>
    TData Data { get; }
  }
}
