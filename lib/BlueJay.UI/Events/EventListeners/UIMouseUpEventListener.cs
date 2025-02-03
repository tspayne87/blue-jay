using BlueJay.Common.Addons;
using BlueJay.Common.Events.Mouse;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using BlueJay.UI.Services;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Events.EventListeners
{
  internal class UIMouseUpEventListener : EventListener<MouseUpEvent>
  {
    /// <summary>
    /// The event queue that will trigger style update events to rerender the UI entity
    /// </summary>
    private readonly IEventQueue _eventQueue;

    /// <summary>
    /// The current layer of entities that we are working with
    /// </summary>
    private readonly IQuery _query;

    /// <summary>
    /// The UI Service for keeping track of globals
    /// </summary>
    private readonly UIService _service;

    /// <summary>
    /// Constructor to build out the mouse move listener to interact with UI entities
    /// </summary>
    /// <param name="eventQueue">The current event queue that will be used to update the texture of the bounds if needed</param>
    /// <param name="service">The UI Service for keeping track of globals</param>
    /// <param name="query">The current layer of entities that we are working with</param>
    public UIMouseUpEventListener(IEventQueue eventQueue, UIService service, IQuery query)
    {
      _eventQueue = eventQueue;
      _service = service;
      _query = query.WhereLayer(UIStatic.LayerName);
    }

    /// <summary>
    /// Helper method to determine if we should process this event listener
    /// </summary>
    /// <param name="evt">The event that is being processed</param>
    /// <returns>Will return a boolean determining if we should process the event listener</returns>
    public override bool ShouldProcess(IEvent evt)
    {
      return evt.Target == null;
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<MouseUpEvent> evt)
    {
      // Iterate over all entities so we can find the entity we need to fire the click event on
      IEntity? foundEntity = null;
      foreach (var entity in _query.Reverse())
      {
        if (entity.Active && Contains(entity, evt.Data.Position))
        {
          foundEntity = entity;
          break;
        }
      }

      if (_service.FocusedEntity != null && _service.FocusedEntity != foundEntity)
      {
        _eventQueue.DispatchEvent(new BlurEvent(), _service.FocusedEntity);
        _service.FocusedEntity = null;
      }
      if (foundEntity != null)
      {
        _eventQueue.DispatchEvent(evt.Data, foundEntity);

        _eventQueue.DispatchEvent(new SelectEvent() { Position = evt.Data.Position }, foundEntity);
        if (_service.FocusedEntity != foundEntity)
          _eventQueue.DispatchEvent(new FocusEvent(), foundEntity);
        _service.FocusedEntity = foundEntity;
      }
    }

    /// <summary>
    /// Helper method is meant to determine if the position contains inside the bounds of the entity based on the bounds and position
    /// </summary>
    /// <param name="entity">The entity we are checking against</param>
    /// <param name="position">The position we are working with</param>
    /// <returns>Will return true if the position is in the bounds of the entity</returns>
    private bool Contains(IEntity entity, Point position)
    {
      if (entity.Contains<TextAddon>())
        return false;

      var ba = entity.GetAddon<BoundsAddon>();
      var pa = entity.GetAddon<PositionAddon>();

      var bounds = new Rectangle((int)pa.Position.X, (int)pa.Position.Y, ba.Bounds.Width, ba.Bounds.Height);
      return bounds.Contains(position);
    }
  }
}
