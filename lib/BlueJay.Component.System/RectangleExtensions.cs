using Microsoft.Xna.Framework;
using System;

namespace BlueJay.Component.System
{
  public static class RectangleExtensions
  {
    /// <summary>
    /// Helper method to add a position to the rectangle
    /// </summary>
    /// <param name="rect">The current rectangle we are working with</param>
    /// <param name="pos">The position that should be added to the current rectangle</param>
    /// <returns>Will return a new rectangle that has the pos added to its position</returns>
    public static Rectangle Add(this Rectangle rect, Vector2 pos)
    {
      int x = pos.X < 0 ? (int)Math.Floor(pos.X) : (int)Math.Ceiling(pos.X);
      int y = pos.Y < 0 ? (int)Math.Floor(pos.Y) : (int)Math.Ceiling(pos.Y);
      return new Rectangle(rect.X + x, rect.Y + y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Helper method to add a point to the rectangle
    /// </summary>
    /// <param name="rect">The current rectangle we are working with</param>
    /// <param name="pos">The point that should be added to the current rectangle</param>
    /// <returns>Will return a new rectangle that has the point added to its position</returns>
    public static Rectangle Add(this Rectangle rect, Point pos)
    {
      return new Rectangle(rect.X + pos.X, rect.Y + pos.Y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Helper method to add a x and y coord to the rectangle
    /// </summary>
    /// <param name="rect">The current rectangle we are working with</param>
    /// <param name="x">The x position we want to add by</param>
    /// <param name="y">The y position we want to add by</param>
    /// <returns>Will return a new rectangle that has the x and y coords added to its position</returns>
    public static Rectangle Add(this Rectangle rect, int x, int y)
    {
      return new Rectangle(rect.X + x, rect.Y + y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Helper method to add the x coord to the rectangle
    /// </summary>
    /// <param name="rect">The current rectangle we are working with</param>
    /// <param name="x">The x position we want to add by</param>
    /// <returns>Will return a new rectangle that has the x coord added to its position</returns>
    public static Rectangle AddX(this Rectangle rect, int x)
    {
      return new Rectangle(rect.X + x, rect.Y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Helper method to add the y coord to the rectangle
    /// </summary>
    /// <param name="rect">The current rectangle we are working with</param>
    /// <param name="y">The y position we want to add by</param>
    /// <returns>Will return a new rectangle that has the y coord added to its position</returns>
    public static Rectangle AddY(this Rectangle rect, int y)
    {
      return new Rectangle(rect.X, rect.Y + y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Helper method to add the x coord to the rectangle
    /// </summary>
    /// <param name="rect">The current rectangle we are working with</param>
    /// <param name="x">The x position we want to add by</param>
    /// <returns>Will return a new rectangle that has the x coord added to its position</returns>
    public static Rectangle AddX(this Rectangle rect, float x)
    {
      int newX = x < 0 ? (int)Math.Floor(x) : (int)Math.Ceiling(x);
      return new Rectangle(rect.X + newX, rect.Y, rect.Width, rect.Height);
    }

    /// <summary>
    /// Helper method to add the y coord to the rectangle
    /// </summary>
    /// <param name="rect">The current rectangle we are working with</param>
    /// <param name="y">The y position we want to add by</param>
    /// <returns>Will return a new rectangle that has the y coord added to its position</returns>
    public static Rectangle AddY(this Rectangle rect, float y)
    {
      int newY = y < 0 ? (int)Math.Floor(y) : (int)Math.Ceiling(y);
      return new Rectangle(rect.X, rect.Y + newY, rect.Width, rect.Height);
    }

    /// <summary>
    /// Helper method to determine if two rectangles are overlapping
    /// </summary>
    /// <param name="rect">The current rectangle we are working with</param>
    /// <param name="other">The other rectangle to determine if they are overlapping</param>
    /// <returns>Will return true or false based on if the rectangles are overlapping</returns>
    public static bool isOverlapping(this Rectangle rect, Rectangle other)
    {
      var rec1 = new int[] { rect.X, rect.Y, rect.X + rect.Width, rect.Y + rect.Height };
      var rec2 = new int[] { other.X, other.Y, other.X + other.Width, other.Y + other.Height };

      if (rec1[2] <= rec2[0] || rec1[0] >= rec2[2]) return false; // Horizontal
      if (rec1[3] <= rec2[1] || rec1[1] >= rec2[3]) return false; // Vertical
      return true;
    }
  }
}
