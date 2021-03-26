using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Addons
{
  public class ColorAddon : Addon<ColorAddon>
  {
    public Color Color;

    public ColorAddon(Color color)
    {
      Color = color;
    }
  }
}
