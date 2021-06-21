using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Components
{
  public abstract class UIComponent
  {
    public void Initialize(UIComponent parent)
    {

    }

    public void Emit<T>(string eventName, T data)
    {

    }
  }
}
