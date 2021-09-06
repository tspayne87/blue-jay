using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Reactivity
{
  public class ReactiveParentProperty : IReactiveParentProperty
  {
    public IReactiveProperty Value { get; set; }
    public string Name { get; set; }

    public ReactiveParentProperty(IReactiveProperty value, string name)
    {
      Value = value;
      Name = name;
    }
  }
}
