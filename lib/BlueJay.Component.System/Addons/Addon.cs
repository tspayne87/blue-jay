using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Addons
{
  public abstract class Addon<TAddon> : IAddon
    where TAddon: Addon<TAddon>
  {
    public static readonly long Identifier = IdentifierHelper.Addon<TAddon>();
    long IAddon.Identifier => Identifier;

    protected ITriggerSystem TriggerSystem { get; private set; }

    public virtual void LoadContent(ContentManager manager) { }

    public virtual void OnRemove() { }

    public void SetTriggerSystem(ITriggerSystem triggerSystem)
    {
      TriggerSystem = triggerSystem;
    }
  }
}
