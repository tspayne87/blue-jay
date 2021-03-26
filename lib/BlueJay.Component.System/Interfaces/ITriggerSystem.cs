using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Component.System.Interfaces
{
  public interface ITriggerSystem
  {
    void Trigger(string type, object data);
    void AddTrigger(string type, Func<object, bool> callback);
  }
}
