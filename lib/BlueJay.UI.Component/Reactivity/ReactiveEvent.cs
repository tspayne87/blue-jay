namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// Reactive update event that triggers when something updates
  /// </summary>
  public class ReactiveEvent
  {
    /// <summary>
    /// The data that was updated for this event
    /// </summary>
    public object? Data { get; set; }

    /// <summary>
    /// The type of event that is being triggered
    /// </summary>
    public EventType Type { get; set; }

    /// <summary>
    /// The event type enumeration
    /// </summary>
    [Flags]
    public enum EventType
    {
      /// <summary>
      /// Add event
      /// </summary>
      Add = 1,

      /// <summary>
      /// Update event
      /// </summary>
      Update = 2,

      /// <summary>
      /// Remove event
      /// </summary>
      Remove = 4
    }
  }
}
