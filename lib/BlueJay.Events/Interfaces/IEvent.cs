namespace BlueJay.Events.Interfaces
{
  /// <summary>
  /// The event object that will be sent when an event is triggered
  /// </summary>
  /// <typeparam name="TData">The data that is sent from the triggered event</typeparam>
  public interface IEvent<TData>
  {
    /// <summary>
    /// The current target the event came from
    /// </summary>
    object Target { get; }

    /// <summary>
    /// The data that comes along with the event
    /// </summary>
    TData Data { get; }

    /// <summary>
    /// The current name of the event
    /// </summary>
    string Name { get; }
  }
}
