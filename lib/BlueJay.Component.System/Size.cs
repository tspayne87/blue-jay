using Microsoft.Xna.Framework;
using System;

namespace BlueJay.Component.System
{
  /// <summary>
  /// Struct to keep track of a size idea
  /// </summary>
  public struct Size
  {
    /// <summary>
    /// The width of the size
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// The height of the size
    /// </summary>
    public int Height { get; set; }

    /// <summary>
    /// Constructor to build out a square with the same width/height
    /// </summary>
    /// <param name="size">The size of this Size</param>
    public Size(int size)
      : this(size, size) { }

    /// <summary>
    /// Constructor to build out a rectangle based on the width and height
    /// </summary>
    /// <param name="width">The width of the Size</param>
    /// <param name="height">The height of the Size</param>
    public Size(int width, int height)
    {
      Width = width;
      Height = height;
    }

    /// <summary>
    /// Converts the size to a point
    /// </summary>
    /// <returns>Will return a point version of this size</returns>
    public Point ToPoint()
    {
      return new Point(Width, Height);
    }

    /// <summary>
    /// Operator is meant to add two sizes together
    /// </summary>
    /// <param name="ls">The left hand side of the operator</param>
    /// <param name="rs">The right hand side of the operator</param>
    /// <returns>The added size</returns>
    public static Size operator +(Size ls, Size rs) => new Size(ls.Width + rs.Width, ls.Height + rs.Height);

    /// <summary>
    /// Operator is meant to subtract two sizes together
    /// </summary>
    /// <param name="ls">The left hand side of the operator</param>
    /// <param name="rs">The right hand side of the operator</param>
    /// <returns>The subtracted size</returns>
    public static Size operator -(Size ls, Size rs) => new Size(ls.Width - rs.Width, ls.Height - rs.Height);

    /// <summary>
    /// Operator is meant to multiple two sizes together
    /// </summary>
    /// <param name="ls">The left hand side of the operator</param>
    /// <param name="rs">The right hand side of the operator</param>
    /// <returns>The multipled size</returns>
    public static Size operator *(Size ls, Size rs) => new Size(ls.Width * rs.Width, ls.Width * rs.Width);

    /// <summary>
    /// Operator is meant to divide two sizes together
    /// </summary>
    /// <param name="ls">The left hand side of the operator</param>
    /// <param name="rs">The right hand side of the operator</param>
    /// <returns>The divided size</returns>
    public static Size operator /(Size ls, Size rs)
    {
      if (rs.Width == 0 || rs.Height == 0) throw new DivideByZeroException();
      return new Size(ls.Width / rs.Width, ls.Height / rs.Height);
    }
  }
}
