using BlueJay.Events;
using BlueJay.Events.Interfaces;
using System;

namespace BlueJay.App.Games.Breakout.EventListeners
{
  public class EndGameEventListener : EventListener<EndGameEvent>
  {
    public override void Process(IEvent<EndGameEvent> evt)
    {
      var i = 0;
    }
  }
}
