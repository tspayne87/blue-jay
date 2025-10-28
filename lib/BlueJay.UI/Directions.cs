using System;

namespace BlueJay.UI;

public struct Directions : IEquatable<Directions>
{
  /// <summary>
  /// The distance from the top edge.
  /// </summary>
  public int Top;

  /// <summary>
  /// The distance from the bottom edge.
  /// </summary>
  public int Bottom;

  /// <summary>
  /// The distance from the left edge.
  /// </summary>
  public int Left;

  /// <summary>
  /// The distance from the right edge.
  /// </summary>
  public int Right;

  /// <summary>
  /// Initializes a new instance of the <see cref="Directions"/> struct.
  /// </summary>
  /// <param name="all">The distance from all edges.</param>
  public Directions(int all)
    : this(all, all, all, all) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="Directions"/> struct.
  /// </summary>
  /// <param name="vertical">The distance from the top and bottom edges.</param>
  /// <param name="horizontal">The distance from the left and right edges.</param>
  public Directions(int vertical, int horizontal)
    : this(vertical, vertical, horizontal, horizontal) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="Directions"/> struct.
  /// </summary>
  /// <param name="top">The distance from the top edge.</param>
  /// <param name="bottom">The distance from the bottom edge.</param>
  /// <param name="left">The distance from the left edge.</param>
  /// <param name="right">The distance from the right edge.</param>
  public Directions(int top, int bottom, int left, int right)
  {
    Top = top;
    Bottom = bottom;
    Left = left;
    Right = right;
  }

  public bool Equals(Directions other)
  {
    return Top == other.Top &&
           Bottom == other.Bottom &&
           Left == other.Left &&
           Right == other.Right;
  }

  /// <inheritdoc />
  public override bool Equals(object? obj)
  {
    return obj is Directions other && Equals(other);
  }

  /// <inheritdoc />
  public override int GetHashCode()
  {
    return HashCode.Combine(Top, Bottom, Left, Right);
  }

  /// <summary>
  /// Determines whether the specified <see cref="Directions"/> is equal to the current <see cref="Directions"/>.
  /// </summary>
  /// <param name="a">The first directions to compare.</param>
  /// <param name="b">The second directions to compare.</param>
  /// <returns>Will return true if the directions are equal, otherwise false.</returns>
  public static bool operator ==(Directions a, Directions b) => a.Equals(b);

  /// <summary>
  /// Determines whether the specified <see cref="Directions"/> is not equal to the current <see cref="Directions"/>.
  /// </summary>
  /// <param name="a">The first directions to compare.</param>
  /// <param name="b">The second directions to compare.</param>
  /// <returns>Will return true if the directions are not equal, otherwise false.</returns>
  public static bool operator !=(Directions a, Directions b) => !a.Equals(b);
}
