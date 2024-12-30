using System.Linq;
using BlueJay.Common.Addons;
using BlueJay.Common.Events.Touch;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;

namespace BlueJay.Shared.Games.Breakout.EventListeners
{
  /// <summary>
  /// Touch move event listener to move the paddle based on the location of the touch
  /// </summary>
  public class TouchMoveEventListener : EventListener<TouchMoveEvent>
  {
    /// <summary>
    /// The current paddles that exist on the system
    /// </summary>
    private readonly IQuery _query;

    /// <summary>
    /// Constructor to inject the scoped items into the listener to handle different process
    /// </summary>
    /// <param name="layerCollection">The layer colllection we are working with</param>
    public TouchMoveEventListener(IQuery query)
    {
      _query = query.WhereLayer(LayerNames.PaddleLayer);
    }

    /// <summary>
    /// Helper method is meant to handle the internal event processing and pass it along to the abstracted process
    /// method
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    public override void Process(IEvent<TouchMoveEvent> evt)
    {
      var paddle = _query.FirstOrDefault();
      if (paddle != null)
      {
        var ba = paddle.GetAddon<BoundsAddon>();

        ba.Bounds.X = (int)evt.Data.Position.X - (ba.Bounds.Width / 2);
        paddle.Update(ba);
      }
    }
  }
}
