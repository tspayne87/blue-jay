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
  public class VelocityAddon : Addon<VelocityAddon>
  {
    public Vector2 Velocity;

    public VelocityAddon(int x, int y)
      : this(new Vector2(x, y)) { }

    public VelocityAddon(Vector2 position)
    {
      Velocity = position;
    }

    public override string ToString()
    {
      return $"Velocity | X: {Velocity.X}, Y: {Velocity.Y}";
    }
  }
}
