using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Events.Mouse;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.EventListeners
{
  /// <summary>
  /// Event listener to watch the mouse movements to interact with the UI entities
  /// </summary>
  public class UIMouseMoveListener : EventListener<MouseMoveEvent>
  {
    /// <summary>
    /// The layer collection to grab the UI elements from the screen
    /// </summary>
    private readonly LayerCollection _layers;

    /// <summary>
    /// The event queue that will trigger style update events to rerender the UI entity
    /// </summary>
    private readonly EventQueue _eventQueue;

    /// <summary>
    /// Constructor to build out the mouse move listener to interact with UI entities
    /// </summary>
    /// <param name="layers">The layer collection we are working under</param>
    /// <param name="eventQueue">The current event queue that will be used to update the texture of the bounds if needed</param>
    public UIMouseMoveListener(LayerCollection layers, EventQueue eventQueue)
    {
      _layers = layers;
      _eventQueue = eventQueue;
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
    public override void Process(IEvent<MouseMoveEvent> evt)
    {
      IEntity hoverEntity = null; // The current hover entity that was found in the system

      // Iterate over all entities so we can find the hover entity and reset hovering if needed
      var entities = _layers[UIStatic.LayerName].Entities;
      for (var i = entities.Count - 1; i >= 0; --i)
      {
        var entity = entities[i];
        var sa = entity.GetAddon<StyleAddon>();
        if (sa.Hovering)
          _eventQueue.DispatchEvent(new StyleUpdateEvent(entity));
        sa.Hovering = false;
        if (hoverEntity == null && Contains(entity, evt.Data.Position))
        {
          hoverEntity = entity;
        }
      }

      // If we found a hover entity we want to loop through the lineage and update the hovering flag
      if (hoverEntity != null)
      {
        _eventQueue.DispatchEvent(evt.Data, hoverEntity);

        var sa = hoverEntity.GetAddon<StyleAddon>();
        var la = hoverEntity.GetAddon<LineageAddon>();
        while (sa != null && la != null && hoverEntity != null)
        {
          if (!sa.Hovering)
            _eventQueue.DispatchEvent(new StyleUpdateEvent(hoverEntity));
          sa.Hovering = true;

          hoverEntity = la.Parent;
          la = hoverEntity?.GetAddon<LineageAddon>();
          sa = hoverEntity?.GetAddon<StyleAddon>();
        }
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
      var ba = entity.GetAddon<BoundsAddon>();
      var pa = entity.GetAddon<PositionAddon>();

      var bounds = new Rectangle((int)pa.Position.X, (int)pa.Position.Y, ba.Bounds.Width, ba.Bounds.Height);
      return ba != null && pa != null && bounds.Contains(position);
    }
  }
}
