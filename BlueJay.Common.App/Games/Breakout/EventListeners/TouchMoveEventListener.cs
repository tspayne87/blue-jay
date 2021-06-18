using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Events.Touch;

namespace BlueJay.Common.App.Games.Breakout.EventListeners
{
  /// <summary>
  /// Touch move event listener to move the paddle based on the location of the touch
  /// </summary>
  public class TouchMoveEventListener : EventListener<TouchMoveEvent>
  {
    /// <summary>
    /// The layer collection that has all the entities in the game at the moment
    /// </summary>
    private readonly LayerCollection _layerCollection;

    /// <summary>
    /// Constructor to inject the scoped items into the listener to handle different process
    /// </summary>
    /// <param name="layerCollection">The layer colllection we are working with</param>
    public TouchMoveEventListener(LayerCollection layerCollection)
    {
      _layerCollection = layerCollection;
    }

    /// <summary>
    /// Helper method is meant to handle the internal event processing and pass it along to the abstracted process
    /// method
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    public override void Process(IEvent<TouchMoveEvent> evt)
    {
      if (_layerCollection[LayerNames.PaddleLayer].Entities.Count == 1)
      {
        var paddle = _layerCollection[LayerNames.PaddleLayer].Entities[0];
        var ba = paddle.GetAddon<BoundsAddon>();

        ba.Bounds.X = (int)evt.Data.Position.X - (ba.Bounds.Width / 2);
      }
    }
  }
}
