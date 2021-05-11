using BlueJay.Component.System.Addons;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using Microsoft.Xna.Framework;

namespace BlueJay.App.Games.Breakout.EventListeners
{
  public class StartBallEventListener : EventListener<StartBallEvent>
  {
    public override void Process(IEvent<StartBallEvent> evt)
    {
      var va = evt.Data.Ball.GetAddon<VelocityAddon>();
      va.Velocity = new Vector2(3, -3);
    }
  }
}
