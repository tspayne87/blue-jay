using BlueJay.Core.Interfaces;
using BlueJay.Component.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueJay.Component.System.Enums;

namespace BlueJay.Component.System.Systems
{
  public abstract class ComponentSystem : IComponentSystem
  {
    public long Identifier { get; internal set; }
    public abstract long Key { get; }

    public virtual SystemUpdateOrder UpdateOrder => SystemUpdateOrder.None;
    public virtual SystemDrawOrder DrawOrder => SystemDrawOrder.None;

    public virtual void Draw(int delta)
      { }

    public virtual void Draw(int delta, IEntity entity)
      { }

    public virtual void Initialize()
      { }

    public virtual void Update(int delta)
      { }

    public virtual void Update(int delta, IEntity entity)
      { }
  }
}
