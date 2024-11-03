using Microsoft.Xna.Framework;

namespace BlueJay.Core
{
  /// <summary>
  /// Set of rectangle helper functions
  /// </summary>
  public static class RectangleHelper
  {
    /// <summary>
    /// Helper method is meant to be a basic way of determining what side was hit if an intersection occured
    /// </summary>
    /// <param name="self">The entity we want to check against the target</param>
    /// <param name="target">The target we need to check which side was hit</param>
    /// <returns>Will return a side that was hit or none if nothing was hit or if we are inside the rectangle</returns>
    public static RectangleSide SideIntersection(Rectangle self, Rectangle target)
    {
      return SideIntersection(self, target, out var _);
    }

    /// <summary>
    /// Helper method is meant to be a basic way of determining what side was hit if an intersection occured
    /// </summary>
    /// <param name="self">The entity we want to check against the target</param>
    /// <param name="target">The target we need to check which side was hit</param>
    /// <returns>Will return a side that was hit or none if nothing was hit or if we are inside the rectangle</returns>
    public static RectangleSide SideIntersection(Rectangle self, Rectangle target, out Rectangle intersection)
    {
      intersection = Rectangle.Intersect(self, target);

      if (intersection == Rectangle.Empty) return RectangleSide.None;
      if (intersection.Y == target.Y) return RectangleSide.Top;
      else if (intersection.Y + intersection.Height == target.Y + target.Height) return RectangleSide.Bottom;
      else if (intersection.X == target.X) return RectangleSide.Left;
      else if (intersection.X + intersection.Width == target.X + target.Width) return RectangleSide.Right;
      return RectangleSide.None;
    }
  }

  /// <summary>
  /// The side that was hit if an intersection occurs
  /// </summary>
  public enum RectangleSide
  {
    Top, Right, Bottom, Left, None
  }
}
