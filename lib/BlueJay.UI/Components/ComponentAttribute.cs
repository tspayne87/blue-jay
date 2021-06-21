using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Components
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
  public class ComponentAttribute : Attribute
  {
    public Type[] Components { get; private set; }

    public ComponentAttribute(params Type[] components)
    {
      Components = components;
    }
  }
}
