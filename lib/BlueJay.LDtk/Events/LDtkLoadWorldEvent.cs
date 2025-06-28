using System;
using BlueJay.LDtk.Data;

namespace BlueJay.LDtk.Events;

public class LDtkLoadWorldEvent
{
  public ILDtkWorld World { get; }

  public LDtkLoadWorldEvent(ILDtkWorld world)
  {
    World = world ?? throw new ArgumentNullException(nameof(world), "World cannot be null");
  }
}
