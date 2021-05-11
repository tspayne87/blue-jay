using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Core
{
  public static class RectangleHelper
  {
    public static RectangleSide SideIntersection(Rectangle self, Rectangle target)
    {
      var check = Rectangle.Intersect(self, target);

      if (check == Rectangle.Empty) return RectangleSide.None;
      if (check.Y == target.Y) return RectangleSide.Top;
      else if (check.Y + check.Height == target.Y + target.Height) return RectangleSide.Bottom;
      else if (check.X == target.X) return RectangleSide.Left;
      else if (check.X + check.Width == target.X + target.Width) return RectangleSide.Right;
      return RectangleSide.None;
    }
  }

  public enum RectangleSide
  {
    Top, Right, Bottom, Left, None
  }
}
