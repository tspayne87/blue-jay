using System.Collections.Generic;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.Collections
{
  public class SystemCollection : List<IComponentSystem>, ISystemCollection
  {
  }
}