using System;
using BlueJay.Events.Interfaces;
using BlueJay.LDtk.Data;
using BlueJay.LDtk.Events;
using BlueJay.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.LDtk;

public class LDtkService : ILDtkService
{
  private readonly IEventQueue _queue;
  private readonly IContentManagerContainer _content;
  private readonly IServiceProvider _service;

  public LDtkService(IEventQueue queue, IContentManagerContainer content, IServiceProvider service)
  {
    _queue = queue ?? throw new ArgumentNullException(nameof(queue), "Event queue cannot be null");
    _content = content ?? throw new ArgumentNullException(nameof(content), "Content manager cannot be null");
    _service = service ?? throw new ArgumentNullException(nameof(service), "Service provider cannot be null");
  }

  public void LoadFile(string assetName)
  {
    var ldtk = _content.Load<LDtkObject>(assetName);

    foreach (var worldInstance in ldtk.Worlds)
    {
      if (worldInstance == null)
        continue;

      var world = ActivatorUtilities.CreateInstance<ILDtkWorld>(_service, worldInstance);

      // Trigger LDtkLoadWorldEvent for each world
      _queue.DispatchEvent(new LDtkLoadWorldEvent(world));

      foreach (var levelInstance in worldInstance.Levels)
      {
        if (levelInstance == null)
          continue;

        var level = ActivatorUtilities.CreateInstance<ILDtkLevel>(_service, worldInstance, levelInstance);

        // Trigger LDtkLoadLevelEvent for each level in the world
        _queue.DispatchEvent(new LDtkLoadLevelEvent(world, level));


        foreach (var layerInstance in levelInstance.LayerInstances.Reverse())
        {
          if (layerInstance == null)
            continue;
          var layer = ActivatorUtilities.CreateInstance<ILDtkLayerInstance>(_service, worldInstance, levelInstance, layerInstance);

          // Trigger LDtkLoadLayerEvent for each layer in the level
          _queue.DispatchEvent(new LDtkLoadLayerEvent(world, level, layer));

          if (layer.InstanceType == LayerType.Entities)
          {
            foreach (var entityInstance in layerInstance.EntityInstances)
            {
              if (entityInstance == null)
                continue;
              var entity = ActivatorUtilities.CreateInstance<ILDtkEntityInstance>(_service, worldInstance, levelInstance, layerInstance, entityInstance);

              // Trigger LDtkLoadEntityEvent for each entity in the layer
              _queue.DispatchEvent(new LDtkLoadEntityEvent(world, level, layer, entity));
            }
          }
        }
      }
    }
  }
}
