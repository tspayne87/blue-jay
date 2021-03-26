using BlueJay.Component.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Component.System
{
  public class TriggerSystem : ITriggerSystem
  {
    /// <summary>
    /// The triggers that need to be called 
    /// </summary>
    private Dictionary<string, List<Func<object, bool>>> _triggers;

    public TriggerSystem()
    {
      _triggers = new Dictionary<string, List<Func<object, bool>>>();
    }

    /// <summary>
    /// Add a trigger to the event system
    /// </summary>
    /// <param name="type">The type of event we are wanting to watch</param>
    /// <param name="callback">The callback that should be called</param>
    public void AddTrigger(string type, Func<object, bool> callback)
    {
      if (!_triggers.ContainsKey(type)) _triggers[type] = new List<Func<object, bool>>();
      _triggers[type].Add(callback);
    }

    /// <summary>
    /// Trigger the event and call all the callbacks for this function
    /// </summary>
    /// <param name="type">The type of event that is being triggered</param>
    /// <param name="data">The options that should be attached to this event</param>
    public void Trigger(string type, object data)
    {
      if (_triggers.ContainsKey(type))
      {
        foreach(var callback in _triggers[type])
        {
          if (!callback(data))
          {
            break;
          }
        }
      }
    }
  }
}
