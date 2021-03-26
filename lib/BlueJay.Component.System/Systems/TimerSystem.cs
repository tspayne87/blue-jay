using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Component.System.Systems
{
  public class TimerSystem : ComponentSystem
  {
    public override long Key => TimerAddon.Identifier;

    public override void Update(int delta, IEntity entity)
    {
      var ta = entity.GetAddon<TimerAddon>();
      ta.Timer -= delta;
      if (ta.Timer <= 0)
      {
        entity.Remove(ta);
      }
    }
  }
}
