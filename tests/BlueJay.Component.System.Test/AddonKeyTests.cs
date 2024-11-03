using BlueJay.Common.Addons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Test
{
  [Collection("KeyHelper Tests")]
  public class AddonKeyTests
  {
    public AddonKeyTests()
    {
      KeyHelper.SetNext(AddonKey.One);
    }

    [Fact]
    public void EqualTests()
    {
      var left = KeyHelper.Create<TextureAddon, BoundsAddon>();
      var right = KeyHelper.Create<BoundsAddon, TextureAddon>();

      Assert.True(left == right);
      Assert.True(3L == right);
      Assert.True(3u == left);
    }

    [Fact]
    public void ObjectEqualTests()
    {
      var left = KeyHelper.Create<TextureAddon, BoundsAddon>();
      object right = KeyHelper.Create<BoundsAddon, TextureAddon>();

      Assert.True(left.Equals(right));
      Assert.False(left.Equals("Unit Test"));
    }

    [Fact]
    public void NotEqualTests()
    {
      var left = KeyHelper.Create<TextureAddon>();
      var right = KeyHelper.Create<BoundsAddon>();

      Assert.True(left != right);
    }

    [Fact]
    public void OverflowEqualTest()
    {
      KeyHelper.SetNext(1 << 62);
      var left = KeyHelper.Create<TextureAddon, BoundsAddon>();
      var right = KeyHelper.Create<BoundsAddon, TextureAddon>();

      Assert.True(left == right);
    }
  }
}
