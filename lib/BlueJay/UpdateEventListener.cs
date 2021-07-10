﻿using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Events.Lifecycle;

namespace BlueJay
{
  /// <summary>
  /// Main lifecycle listener to hook in the component system into the event system
  /// </summary>
  public class UpdateEventListener : EventListener<UpdateEvent>
  {
    /// <summary>
    /// The current layer collection
    /// </summary>
    private readonly LayerCollection _layerCollection;

    /// <summary>
    /// The current system being processed
    /// </summary>
    private readonly ISystem _system;

    /// <summary>
    /// Constructor to build out the update event
    /// </summary>
    /// <param name="layerCollection">The layer collection we are working with</param>
    /// <param name="system">The current system being processed</param>
    public UpdateEventListener(LayerCollection layerCollection, ISystem system)
    {
      _layerCollection = layerCollection;
      _system = system;
    }

    /// <summary>
    /// Process method is meant to handle the update event and send that update to all systems in the system
    /// </summary>
    /// <param name="evt">The current update event we are working with</param>
    public override void Process(IEvent<UpdateEvent> evt)
    {
      // Call On update if system has an on update event
      if (_system is IUpdateSystem)
        ((IUpdateSystem)_system).OnUpdate();

      // If we are dealing with a system that needs to update the entities
      if (_system.Key != 0 && _system is IUpdateEntitySystem)
      {
        for (var j = 0; j < _layerCollection.Count; ++j)
        {
          if (_system.Layers.Count == 0 || _system.Layers.Contains(_layerCollection[j].Id))
          {
            var entities = _layerCollection[j].Entities.GetByKey(_system.Key);
            for (var k = 0; k < entities.Count; ++k)
            {
              if (entities[k].Active)
              {
                ((IUpdateEntitySystem)_system).OnUpdate(entities[k]);
              }
            }
          }
        }
      }

      // Call on update end if the system has the update end event
      if (_system is IUpdateEndSystem)
        ((IUpdateEndSystem)_system).OnUpdateEnd();
    }
  }
}
