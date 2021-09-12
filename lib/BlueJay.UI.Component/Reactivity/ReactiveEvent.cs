namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// Reactive update event that triggers when something updates
  /// </summary>
  public class ReactiveEvent
  {
    /// <summary>
    /// The current path that was updated for this event
    /// </summary>
    public string Path { get; set; }

    /// <summary>
    /// The data that was updated for this event
    /// </summary>
    public object Data { get; set; }

    /// <summary>
    /// The type of event that is being triggered
    /// </summary>
    public EventType Type { get; set; }

    /// <summary>
    /// The event type enumeration
    /// </summary>
    public enum EventType
    {
      /// <summary>
      /// Update event
      /// </summary>
      Update,
      
      /// <summary>
      /// Add event
      /// </summary>
      Add,
      
      /// <summary>
      /// Remove event
      /// </summary>
      Remove
    }
  }
}
