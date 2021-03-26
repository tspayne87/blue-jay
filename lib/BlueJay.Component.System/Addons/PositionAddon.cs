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
  public class PositionAddon : Addon<PositionAddon>
  {
    public Vector2 Position { get; set; }

    public PositionAddon(Vector2 position)
    {
      Position = position;
    }

    public override string ToString()
    {
      return $"Position | X: {Position.X}, Y: {Position.Y}";
    }
  }
}
