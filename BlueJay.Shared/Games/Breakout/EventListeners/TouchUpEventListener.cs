using BlueJay.Shared.Games.Breakout.Addons;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Component.System.Interfaces;
using BlueJay.Common.Events.Touch;
using System.Linq;

namespace BlueJay.Shared.Games.Breakout.EventListeners
{
  public class TouchUpEventListener : EventListener<TouchUpEvent>
  {
    /// <summary>
    /// The current ball query to get the ball in the layer
    /// </summary>
    private readonly IQuery _query;

    /// <summary>
    /// The event queue we want to dispatch events too
    /// </summary>
    private readonly IEventQueue _eventQueue;

    /// <summary>
    /// Constructor to inject the scoped items into the listener to handle different process
    /// </summary>
    /// <param name="layerCollection">The layer colllection we are working with</param>
    /// <param name="eventQueue">The entity queue to dispatch events to the game</param>
    public TouchUpEventListener(IEventQueue eventQueue, IQuery query)
    {
      _eventQueue = eventQueue;
      _query = query.WhereLayer(LayerNames.BallLayer);
    }

    /// <summary>
    /// Helper method is meant to handle the internal event processing and pass it along to the abstracted process
    /// method
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    public override void Process(IEvent<TouchUpEvent> evt)
    {
      var ball = _query.FirstOrDefault();
      if (ball != null)
      { // Trigger an event to start the game if the ball is not active
        var baa = ball.GetAddon<BallActiveAddon>();
        if (!baa.IsActive)
        {
          baa.IsActive = true;
          ball.Update(baa);
          _eventQueue.DispatchEvent(new StartBallEvent() { Ball = ball });
        }
      }
    }
  }
}
