using BlueJay.Component.System.Enums;
using BlueJay.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Interfaces
{
  public interface IComponentSystem
  {
    long Identifier { get; }
    long Key { get; }
    SystemUpdateOrder UpdateOrder { get; }
    SystemDrawOrder DrawOrder { get; }

    void Initialize();
    void Update(int delta);
    void Update(int delta, IEntity entity);
    void Draw(int delta);
    void Draw(int delta, IEntity entity);
  }
}
