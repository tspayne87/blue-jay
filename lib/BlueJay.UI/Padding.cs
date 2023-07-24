using Microsoft.Xna.Framework;

namespace BlueJay.UI
{
  /// <summary>
  /// The padding that should surround the current element
  /// </summary>
  public struct Padding
  {
    /// <summary>
    /// The top amount of padding for an element
    /// </summary>
    public int Top { get; set; }

    /// <summary>
    /// The right amount of padding for an element
    /// </summary>
    public int Right { get; set; }

    /// <summary>
    /// The bottom amount of padding for an element
    /// </summary>
    public int Bottom { get; set; }

    /// <summary>
    /// The left amount of padding for an element
    /// </summary>
    public int Left { get; set; }

    /// <summary>
    /// The combination of top and bottom together
    /// </summary>
    public int TopBottom => Top + Bottom;

    /// <summary>
    /// The combination of left and right together
    /// </summary>
    public int LeftRight => Left + Right;

    /// <summary>
    /// Constructor to create padding that is the same for all directions
    /// </summary>
    /// <param name="all">All the directions for the padding</param>
    public Padding(int all)
    {
      Top = all;
      Right = all;
      Bottom = all;
      Left = all;
    }

    /// <summary>
    /// Constructor to create padding that will change based on the x and y directions of the padding
    /// </summary>
    /// <param name="y">The y direction or top and bottom of the padding</param>
    /// <param name="x">The x direction or right and left of the padding</param>
    public Padding(int y, int x)
    {
      Top = y;
      Right = x;
      Bottom = y;
      Left = x;
    }

    /// <summary>
    /// Constructor to create padding that will change based on the top/bottom and the x value
    /// </summary>
    /// <param name="top">The top value for the padding</param>
    /// <param name="x">The x direction or right and left of the padding</param>
    /// <param name="bottom">The bottom value for the padding</param>
    public Padding(int top, int x, int bottom)
    {
      Top = top;
      Right = x;
      Bottom = bottom;
      Left = x;
    }

    /// <summary>
    /// Constructor to create padding that will change based on the top/right/bottom and the left
    /// </summary>
    /// <param name="top">The top value for the padding</param>
    /// <param name="right">The right value for the padding</param>
    /// <param name="bottom">The bottom value for the padding</param>
    /// <param name="left">The left value for the padding</param>
    public Padding(int top, int right, int bottom, int left)
    {
      Top = top;
      Right = right;
      Bottom = bottom;
      Left = left;
    }

    /// <summary>
    /// Constructor is meant to build out a padding based on the vector position
    /// </summary>
    /// <param name="pos">The vector position</param>
    public Padding(Vector2 pos)
    {
      Top = (int)pos.X;
      Right = (int)pos.Y;
      Bottom = (int)pos.X;
      Left = (int)pos.Y;
    }

    /// <summary>
    /// Constructor is meant to build out a padding based on the vector position
    /// </summary>
    /// <param name="pos">The vector position</param>
    public Padding(Vector3 pos)
    {
      Top = (int)pos.X;
      Right = (int)pos.Y;
      Bottom = (int)pos.Z;
      Left = (int)pos.Y;
    }
    /// <summary>
    /// Constructor is meant to build out a padding based on the vector position
    /// </summary>
    /// <param name="pos">The vector position</param>
    public Padding(Vector4 pos)
    {
      Top = (int)pos.X;
      Right = (int)pos.Y;
      Bottom = (int)pos.Z;
      Left = (int)pos.W;
    }

    /// <summary>
    /// Constructor is meant to build out a padding based on the point position
    /// </summary>
    /// <param name="pos">The current point position</param>
    public Padding(Point pos)
    {
      Top = pos.X;
      Right = pos.Y;
      Bottom = pos.X;
      Left = pos.Y;
    }
  }
}
