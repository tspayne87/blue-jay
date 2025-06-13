using System;
using BlueJay.LDtk.Data;

namespace BlueJay.LDtk.Events;

public class LDtkLoadEntityEvent
{
  public ILDtkWorld World { get; }
  public ILDtkLevel Level { get; }
  public ILDtkLayerInstance Layer { get; }
  public ILDtkEntityInstance Entity { get; }

  public LDtkLoadEntityEvent(ILDtkWorld world, ILDtkLevel level, ILDtkLayerInstance layer, ILDtkEntityInstance entity)
  {
    World = world ?? throw new ArgumentNullException(nameof(world), "World cannot be null");
    Level = level ?? throw new ArgumentNullException(nameof(level), "Level cannot be null");
    Layer = layer ?? throw new ArgumentNullException(nameof(layer), "Layer cannot be null");
    Entity = entity ?? throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
  }
}
