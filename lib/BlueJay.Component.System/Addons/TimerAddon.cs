using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Component.System.Addons
{
  public class TimerAddon : Addon<TimerAddon>
  {
    public int Timer { get; set; }
    public string TriggerId { get; set; }

    public TimerAddon(int timer, string triggerId)
    {
      Timer = timer;
      TriggerId = triggerId;
    }

    public override void OnRemove()
    {
      TriggerSystem.Trigger(TriggerId, null);
    }
  }
}
