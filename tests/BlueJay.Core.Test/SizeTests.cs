using Microsoft.Xna.Framework;
using System;
using Xunit;

namespace BlueJay.Core.Test
{
  public class SizeTests
  {
    [Fact]
    public void Constructor()
    {
      var square = new Size(10);
      Assert.Equal(10, square.Width);
      Assert.Equal(10, square.Height);

      var rectangle = new Size(10, 5);
      Assert.Equal(10, rectangle.Width);
      Assert.Equal(5, rectangle.Height);

      var squarePoint = new Size(new Point(10));
      Assert.Equal(10, squarePoint.Width);
      Assert.Equal(10, squarePoint.Height);

      var rectangleVector = new Size(new Vector2(10, 5));
      Assert.Equal(10, rectangle.Width);
      Assert.Equal(5, rectangle.Height);
    }

    [Fact]
    public void ToPoint()
    {
      var square = new Size(10);
      Assert.Equal(new Point(10, 10), square.ToPoint());

      var rectangle = new Size(10, 5);
      Assert.Equal(new Point(10, 5), rectangle.ToPoint());
    }

    [Fact]
    public void ToVector2()
    {
      var square = new Size(10);
      Assert.Equal(new Vector2(10, 10), square.ToVector2());

      var rectangle = new Size(10, 5);
      Assert.Equal(new Vector2(10, 5), rectangle.ToVector2());
    }

    [Fact]
    public void Equality()
    {
      var square = new Size(10);
      Assert.Equal(new Size(10), square);
      Assert.NotEqual(new Size(10, 5), square);

      Assert.True(new Size(10) == square);
      Assert.True(new Size(10, 5) != square);

      Assert.True(square.Equals(new Size(10)));
      Assert.True(square.Equals((object)new Size(10)));
      Assert.False(square.Equals(5));

      Assert.Equal(square.Width.GetHashCode() + square.Height.GetHashCode(), square.GetHashCode());
    }

    [Fact]
    public void Operators()
    {
      var square = new Size(10);

      Assert.Equal(new Size(20), square + new Size(10));
      Assert.Equal(new Size(5), square - new Size(5));
      Assert.Equal(new Size(100), square * new Size(10));
      Assert.Equal(new Size(1), square / new Size(10));
      Assert.Equal(new Size(1), square % new Size(3));

      Assert.Equal(new Size(20, 15), square + new Size(10, 5));
      Assert.Equal(new Size(0, 5), square - new Size(10, 5));
      Assert.Equal(new Size(100, 50), square * new Size(10, 5));
      Assert.Equal(new Size(1, 2), square / new Size(10, 5));
      Assert.Equal(new Size(1, 2), square % new Size(3, 4));

      Assert.Throws<DivideByZeroException>(() => square / Size.Empty);
      Assert.Throws<DivideByZeroException>(() => square % Size.Empty);
    }

    [Fact]
    public void Empty()
    {
      Assert.Equal(new Size(0), Size.Empty);
      Assert.NotEqual(new Size(10), Size.Empty);
    }
  }
}
