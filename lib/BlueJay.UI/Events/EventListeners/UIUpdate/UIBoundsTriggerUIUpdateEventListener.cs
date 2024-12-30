using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Events.EventListeners.UIUpdate
{
  /// <summary>
  /// Clear trigger is meant to send out event messages and clean up the bounds before rendering
  /// </summary>
  internal class UIBoundsTriggerUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The current event queue that is being processed
    /// </summary>
    private readonly IEventQueue _eventQueue;

    /// <summary>
    /// The current layer of entities that we are working with
    /// </summary>
    private readonly IQuery _query;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="eventQueue">The current event queue that will be used to update the texture of the bounds if needed</param>
    /// <param name="query">The current layer of entities that we are working with</param>
    public UIBoundsTriggerUIUpdateEventListener(IEventQueue eventQueue, IQuery query)
    {
      _eventQueue = eventQueue;
      _query = query.WhereLayer(UIStatic.LayerName);
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<UIUpdateEvent> evt)
    {
      foreach (var entity in _query)
        ProcessEntity(entity);
    }

    /// <summary>
    /// Helper method is meant to send on an event if the bounds changed for this entity
    /// </summary>
    /// <param name="entity">The entity we are checking</param>
    private void ProcessEntity(IEntity entity)
    {
      var ba = entity.GetAddon<BoundsAddon>();
      var sa = entity.GetAddon<StyleAddon>();

      if (ba.Bounds != sa.CalculatedBounds)
      {
        ba.Bounds = sa.CalculatedBounds;
        entity.Update(ba);
        _eventQueue.DispatchEvent(new StyleUpdateEvent(entity));
      }
      sa.CalculatedBounds = Rectangle.Empty;
      entity.Update(sa);
    }
  }
}
