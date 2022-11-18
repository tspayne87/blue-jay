using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Events.Lifecycle;

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
    /// The system we need to draw on
    /// </summary>
    private readonly ISystem _system;

    /// <summary>
    /// Constructor to build out the update event
    /// </summary>
    /// <param name="layerCollection">The entity collection we are working with</param>
    /// <param name="system">The current system being processed</param>
    public DrawEventListener(ILayerCollection layerCollection, ISystem system)
    {
      _layerCollection = layerCollection;
      _system = system;
    }

    /// <summary>
    /// Process method is meant to handle the update event and send that update to all systems in the system
    /// </summary>
    /// <param name="evt">The current update event we are working with</param>
    public override void Process(IEvent<DrawEvent> evt)
    {
      if (_system is IDrawSystem)
        ((IDrawSystem)_system).OnDraw();

      if (_system.Key != 0 && _system is IDrawEntitySystem)
      {
        for (var j = 0; j < _layerCollection.Count; ++j)
        {
          if (_system.Layers.Count == 0 || _system.Layers.Contains(_layerCollection[j].Id))
          {
            foreach(var entity in _layerCollection[j].GetByKey(_system.Key))
            {
              if (entity.Active)
              {
                ((IDrawEntitySystem)_system).OnDraw(entity);
              }
            }
          }
        }
      }

      if (_system is IDrawEndSystem)
        ((IDrawEndSystem)_system).OnDrawEnd();
    }
  }
}
