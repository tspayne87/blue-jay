namespace BlueJay.Events.Interfaces
{
  /// <summary>
  /// The event processor to hide the internals so we can process the event queue
  /// </summary>
  public interface IEventProcessor
  {
    /// <summary>
    /// The process method is meant to process one tick of the game and all the events that should be processed at that time
    /// </summary>
    void Update();

    /// <summary>
    /// The process method that will handle the draw event
    /// </summary>
    void Draw();

    /// <summary>
    /// Handle the activate event for the event queue
    /// </summary>
    void Activate();

    /// <summary>
    /// Handle the deactivate event for the event queue
    /// </summary>
    void Deactivate();

    /// <summary>
    /// Handle the exit event for the event queue
    /// </summary>
    void Exit();
  }
}
