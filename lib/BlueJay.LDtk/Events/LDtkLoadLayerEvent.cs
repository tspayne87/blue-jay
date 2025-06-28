using System;
using BlueJay.LDtk.Data;

namespace BlueJay.LDtk.Events;

public class LDtkLoadLayerEvent
{
  public ILDtkWorld World { get; }
  public ILDtkLevel Level { get; }
  public ILDtkLayerInstance Layer { get; }

  public LDtkLoadLayerEvent(ILDtkWorld world, ILDtkLevel level, ILDtkLayerInstance layer)
  {
    World = world ?? throw new ArgumentNullException(nameof(world), "World cannot be null");
    Level = level ?? throw new ArgumentNullException(nameof(level), "Level cannot be null");
    Layer = layer ?? throw new ArgumentNullException(nameof(layer), "Layer cannot be null");
  }
}
