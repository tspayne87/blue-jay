﻿using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Events.Mouse;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.EventListeners
{
  public class UIMouseDownEventListener : EventListener<MouseDownEvent>
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
    public UIMouseDownEventListener(LayerCollection layers, EventQueue eventQueue)
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
    public override void Process(IEvent<MouseDownEvent> evt)
    {
      // Iterate over all entities so we can find the entity we need to fire the click event on
      var entities = _layers[UIStatic.LayerName].Entities;
      for (var i = entities.Count - 1; i >= 0; --i)
      {
        var entity = entities[i];
        if (Contains(entity, evt.Data.Position))
        {
          _eventQueue.DispatchEvent(new SelectEvent() { Position = evt.Data.Position }, entity);
          break;
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
