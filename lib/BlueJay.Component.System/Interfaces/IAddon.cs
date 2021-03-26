using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Interfaces
{
  public interface IAddon
  {
    long Identifier { get; }

    void LoadContent(ContentManager manager);
    void OnRemove();
    void SetTriggerSystem(ITriggerSystem triggerSystem);
  }
}
