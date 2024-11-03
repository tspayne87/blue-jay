using BlueJay.Common.Addons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Test
{
  [Collection("KeyHelper Tests")]
  public class KeyHelperTests
  {
    public KeyHelperTests()
    {
      KeyHelper.SetNext(AddonKey.One);
    }

    [Fact]
    public void Create()
    {
      var one = KeyHelper.Create<PositionAddon>();
      Assert.Equal((AddonKey)1, one);

      var two = KeyHelper.Create<PositionAddon, BoundsAddon>();
      Assert.Equal((AddonKey)3, two);

      var three = KeyHelper.Create<PositionAddon, BoundsAddon, DebugAddon>();
      Assert.Equal((AddonKey)7, three);

      var four = KeyHelper.Create<PositionAddon, BoundsAddon, DebugAddon, TextureAddon>();
      Assert.Equal((AddonKey)15, four);

      var five = KeyHelper.Create<PositionAddon, BoundsAddon, DebugAddon, TextureAddon, ColorAddon>();
      Assert.Equal((AddonKey)31, five);

      var six = KeyHelper.Create<PositionAddon, BoundsAddon, DebugAddon, TextureAddon, ColorAddon, FrameAddon>();
      Assert.Equal((AddonKey)63, six);

      var seven = KeyHelper.Create<PositionAddon, BoundsAddon, DebugAddon, TextureAddon, ColorAddon, FrameAddon, SizeAddon>();
      Assert.Equal((AddonKey)127, seven);

      var eight = KeyHelper.Create<PositionAddon, BoundsAddon, DebugAddon, TextureAddon, ColorAddon, FrameAddon, SizeAddon, SpriteEffectsAddon>();
      Assert.Equal((AddonKey)255, eight);

      var nine = KeyHelper.Create<PositionAddon, BoundsAddon, DebugAddon, TextureAddon, ColorAddon, FrameAddon, SizeAddon, SpriteEffectsAddon, SpriteSheetAddon>();
      Assert.Equal((AddonKey)511, nine);

      var ten = KeyHelper.Create<PositionAddon, BoundsAddon, DebugAddon, TextureAddon, ColorAddon, FrameAddon, SizeAddon, SpriteEffectsAddon, SpriteSheetAddon, VelocityAddon>();
      Assert.Equal((AddonKey)1023, ten);
    }
  }
}