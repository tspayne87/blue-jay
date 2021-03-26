using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Component.System.Addons
{
  public class SpriteEffectsAddon : Addon<SpriteEffectsAddon>
  {
    public SpriteEffects Effects { get; set; }

    public SpriteEffectsAddon(SpriteEffects effects)
    {
      Effects = effects;
    }
  }
}
