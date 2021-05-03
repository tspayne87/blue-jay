using BlueJay.Events.Interfaces;

namespace BlueJay.Events
{
  /// <summary>
  /// The event object that will be sent when an event is triggered
  /// </summary>
  /// <typeparam name="TData">The data that is sent from the triggered event</typeparam>
  internal class Event<T> : IEvent<T>
  {
    /// <summary>
    /// The data that comes along with the event
    /// </summary>
    public T Data { get; private set; }

    /// <summary>
    /// The current name of the event
    /// </summary>
    public string Name { get; private set; }

    /// <summary>
    /// If this event is concidered complete and should stop processing
    /// </summary>
    public bool IsComplete { get; private set; }

    /// <summary>
    /// The current target we are working with
    /// </summary>
    public object Target { get; private set; }

    /// <summary>
    /// Constructor method to build out the event object with a easy way of doing it
    /// </summary>
    /// <param name="data">The data we are working with this event</param>
    /// <param name="target">The current target this event was triggered on</param>
    public Event(T data, object target = null)
    {
      Data = data;
      Name = data.GetType().Name;
      Target = target;
      IsComplete = false;
    }

    /// <summary>
    /// Method is meant to stop the event cycle from moving forward and make sure that no other event listeners past the
    /// one that called this method gets ran
    /// </summary>
    public void StopPropagation()
    {
      IsComplete = true;
    }
  }
}
