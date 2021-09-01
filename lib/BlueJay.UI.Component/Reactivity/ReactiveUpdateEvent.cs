using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Reactivity
{
  public class ReactiveUpdateEvent
  {
    public string Path { get; set; }
    public object Data { get; set; }
    public EventType Type { get; set; }

    public enum EventType
    {
      Update, Add, Remove
    }
  }
}
