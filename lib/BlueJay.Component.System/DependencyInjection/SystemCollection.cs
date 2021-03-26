using System.Collections;
using System.Collections.Generic;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.DependencyInjection
{
  public class SystemCollection : List<IComponentSystem>, ISystemCollection
  {
  }
}