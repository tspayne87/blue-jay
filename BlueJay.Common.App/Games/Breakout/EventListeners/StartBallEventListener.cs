using BlueJay.Component.System.Addons;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using Microsoft.Xna.Framework;

namespace BlueJay.Common.App.Games.Breakout.EventListeners
{
  /// <summary>
  /// Event listener is meant to process once the start ball is triggered by the keyboard listener
  /// </summary>
  public class StartBallEventListener : EventListener<StartBallEvent>
  {
    /// <summary>
    /// Helper method is meant to handle the internal event processing and pass it along to the abstracted process
    /// method
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    public override void Process(IEvent<StartBallEvent> evt)
    {
      // Get the ball and add basic velocity to it
      var va = evt.Data.Ball.GetAddon<VelocityAddon>();
      va.Velocity = new Vector2(3, -3);
    }
  }
}
