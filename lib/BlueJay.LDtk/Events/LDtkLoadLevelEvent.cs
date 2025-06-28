using System;
using BlueJay.LDtk.Data;

namespace BlueJay.LDtk.Events;

public class LDtkLoadLevelEvent
{
  public ILDtkWorld World { get; }
  public ILDtkLevel Level { get; }

  public LDtkLoadLevelEvent(ILDtkWorld world, ILDtkLevel level)
  {
    World = world ?? throw new ArgumentNullException(nameof(world), "World cannot be null");
    Level = level ?? throw new ArgumentNullException(nameof(level), "Level cannot be null");
  }
}
