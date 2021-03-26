using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Addons
{
  public class DebugAddon : Addon<DebugAddon>
  {
    public long Key;

    public DebugAddon(Type type)
    {
      Key = IdentifierHelper.Addon(type);
    }
  }
}
