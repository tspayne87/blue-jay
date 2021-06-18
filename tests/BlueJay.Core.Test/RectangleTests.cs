using Microsoft.Xna.Framework;
using Xunit;

namespace BlueJay.Core.Test
{
  public class RectangleTests
  {
    [Fact]
    public void SideIntersection()
    {
      var wall = new Rectangle(5, 5, 50, 50);

      Assert.Equal(RectangleSide.Top, RectangleHelper.SideIntersection(new Rectangle(0, 0, 10, 10), wall));
      Assert.Equal(RectangleSide.Top, RectangleHelper.SideIntersection(new Rectangle(50, 0, 10, 10), wall));
      Assert.Equal(RectangleSide.Top, RectangleHelper.SideIntersection(new Rectangle(0, 5, 10, 10), wall));

      Assert.Equal(RectangleSide.Left, RectangleHelper.SideIntersection(new Rectangle(0, 6, 10, 10), wall));
      Assert.Equal(RectangleSide.Left, RectangleHelper.SideIntersection(new Rectangle(0, 44, 10, 10), wall));

      Assert.Equal(RectangleSide.Bottom, RectangleHelper.SideIntersection(new Rectangle(0, 45, 10, 10), wall));
      Assert.Equal(RectangleSide.Bottom, RectangleHelper.SideIntersection(new Rectangle(0, 50, 10, 10), wall));
      Assert.Equal(RectangleSide.Bottom, RectangleHelper.SideIntersection(new Rectangle(50, 45, 10, 10), wall));

      Assert.Equal(RectangleSide.Right, RectangleHelper.SideIntersection(new Rectangle(50, 6, 10, 10), wall));
      Assert.Equal(RectangleSide.Right, RectangleHelper.SideIntersection(new Rectangle(50, 44, 10, 10), wall));
    }

    [Fact]
    public void AddPosition()
    {
      var rectangle = new Rectangle(0, 0, 10, 10);

      Assert.Equal(new Rectangle(10, 0, 10, 10), rectangle.Add(new Vector2(10, 0)));
      Assert.Equal(new Rectangle(10, 0, 10, 10), rectangle.Add(new Point(10, 0)));
      Assert.Equal(new Rectangle(10, 0, 10, 10), rectangle.Add(10, 0));

      Assert.Equal(new Rectangle(10, 0, 10, 10), rectangle.AddX(10));
      Assert.Equal(new Rectangle(10, 0, 10, 10), rectangle.AddX(10f));

      Assert.Equal(new Rectangle(0, 10, 10, 10), rectangle.AddY(10));
      Assert.Equal(new Rectangle(0, 10, 10, 10), rectangle.AddY(10f));
    }
  }
}
