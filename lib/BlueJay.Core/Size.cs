using Microsoft.Xna.Framework;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BlueJay.Core
{
  /// <summary>
  /// Struct to keep track of a size idea
  /// </summary>
  public struct Size : IEquatable<Size>
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
    /// Method is meant to check if two sizes are equal or not
    /// </summary>
    /// <param name="other">The other object</param>
    /// <returns>Will return true if the to sizes are equal</returns>
    public bool Equals([AllowNull] Size other)
    {
      if (other == null) return false;
      return Width == other.Width && Height == other.Height;
    }

    /// <summary>
    /// Gets the hashcode for the width and height together
    /// </summary>
    /// <returns>Returns the hash code</returns>
    public override int GetHashCode()
    {
      return Width.GetHashCode() + Height.GetHashCode();
    }

    /// <summary>
    /// Method is meant to check if two sizes are equal or not
    /// </summary>
    /// <param name="other">The other object</param>
    /// <returns>Will return true if the to sizes are equal</returns>
    public override bool Equals(object obj)
    {
      return obj is Size ? Equals((Size)obj) : false;
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
    /// Operator is meant to determine if two sizes are equal
    /// </summary>
    /// <param name="ls">The left hand side of the operator</param>
    /// <param name="rs">The right hand side of the operator</param>
    /// <returns>Will return if the two sizes are equal</returns>
    public static bool operator ==(Size ls, Size rs) => ls.Equals(rs);

    /// <summary>
    /// Operator is meant to determine if two sizes are not equal
    /// </summary>
    /// <param name="ls">The left hand side of the operator</param>
    /// <param name="rs">The right hand side of the operator</param>
    /// <returns>Will return if the two sizes are not equal</returns>
    public static bool operator !=(Size ls, Size rs) => !ls.Equals(rs);

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

    /// <summary>
    /// An empty version of the size
    /// </summary>
    public static Size Empty => new Size(0);
  }
}
