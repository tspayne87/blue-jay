using BlueJay.Events;
using BlueJay.Events.Interfaces;

namespace BlueJay.App.Games.Breakout.EventListeners
{
  /// <summary>
  /// Listener is meant to do something once the game has ended
  /// </summary>
  public class EndGameEventListener : EventListener<EndGameEvent>
  {
    /// <summary>
    /// Helper method is meant to handle the internal event processing and pass it along to the abstracted process
    /// method
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    public override void Process(IEvent<EndGameEvent> evt)
    {
      // TODO: Add UI elements to show gameover as text and give buttons to restart or go back to title screen
    }
  }
}
