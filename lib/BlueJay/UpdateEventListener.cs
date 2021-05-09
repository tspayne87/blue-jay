using BlueJay.Component.System.Collections;
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
    /// The current system collection
    /// </summary>
    private readonly SystemCollection _systemCollection;

    /// <summary>
    /// Constructor to build out the update event
    /// </summary>
    /// <param name="layerCollection">The layer collection we are working with</param>
    /// <param name="systemCollection">The current system collection we are working with</param>
    public UpdateEventListener(LayerCollection layerCollection, SystemCollection systemCollection)
    {
      _layerCollection = layerCollection;
      _systemCollection = systemCollection;
    }

    /// <summary>
    /// Process method is meant to handle the update event and send that update to all systems in the system
    /// </summary>
    /// <param name="evt">The current update event we are working with</param>
    public override void Process(IEvent<UpdateEvent> evt)
    {
      for (var i = 0; i < _systemCollection.Count; ++i)
      {
        _systemCollection[i].OnUpdate();

        if (_systemCollection[i].Key != 0)
        {
          for (var j = 0; j < _layerCollection.Count; ++j)
          {
            if (_systemCollection[i].Layers.Count == 0 || _systemCollection[i].Layers.Contains(_layerCollection[j].Id))
            {
              var entities = _layerCollection[j].Entities.GetByKey(_systemCollection[i].Key);
              for (var k = 0; k < entities.Count; ++k)
              {
                if (entities[k].Active)
                {
                  _systemCollection[i].OnUpdate(entities[k]);
                }
              }
            }
          }
        }
      }
    }
  }
}
