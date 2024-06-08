using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Events.Lifecycle;
using System.Runtime.InteropServices;

namespace BlueJay.EventListeners
{
  /// <summary>
  /// Main lifecycle listener to hook into the draw event
  /// </summary>
  internal class DrawEventListener : EventListener<DrawEvent>
  {
    /// <summary>
    /// The current layer collection
    /// </summary>
    private readonly ILayerCollection _layerCollection;

    /// <summary>
    /// The list of systems that have been set to be drawable
    /// </summary>
    private readonly DrawableSystemCollection _systems;

    /// <summary>
    /// Constructor to build out the update event
    /// </summary>
    /// <param name="layerCollection">The entity collection we are working with</param>
    /// <param name="systems">The list of systems that have been set to be drawable</param>
    public DrawEventListener(ILayerCollection layerCollection, DrawableSystemCollection systems)
    {
      _layerCollection = layerCollection;
      _systems = systems;
    }

    /// <summary>
    /// Process method is meant to handle the update event and send that update to all systems in the system
    /// </summary>
    /// <param name="evt">The current update event we are working with</param>
    public override void Process(IEvent<DrawEvent> evt)
    {
      foreach (var system in CollectionsMarshal.AsSpan(_systems))
      {
        if (!(system is IDrawEntitySystem))
        {
          if (system is IDrawSystem)
            ((IDrawSystem)system).OnDraw();

          if (system is IDrawEndSystem)
            ((IDrawEndSystem)system).OnDrawEnd();
        }
      }

      foreach (var layer in _layerCollection.AsSpan())
      {
        foreach (var system in CollectionsMarshal.AsSpan(_systems))
        {
          if (system is IDrawEntitySystem)
          {
            if (system is IDrawSystem)
              ((IDrawSystem)system).OnDraw();
            if (system.Key != AddonKey.None && (system.Layers.Count == 0 || system.Layers.Contains(layer.Id)))
            {
              foreach (var entity in layer.GetByKey(system.Key))
              {
                if (entity.Active)
                {
                  ((IDrawEntitySystem)system).OnDraw(entity);
                }
              }
            }
            if (system is IDrawEndSystem)
              ((IDrawEndSystem)system).OnDrawEnd();
          }
        }
      }
    }
  }
}
