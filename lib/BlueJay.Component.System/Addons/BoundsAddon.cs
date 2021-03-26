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
  public class BoundsAddon : Addon<BoundsAddon>
  {
    public Rectangle Bounds;

    public BoundsAddon(int x, int y, int width, int height)
      : this(new Rectangle(x, y, width, height)) { }

    public BoundsAddon(Rectangle bounds)
    {
      Bounds = bounds;
    }

    public override string ToString()
    {
      return $"Bounds | X: {Bounds.X}, Y: {Bounds.Y}, Width: {Bounds.Width}, Height: {Bounds.Height}";
    }
  }
}
