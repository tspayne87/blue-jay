using BlueJay.Common.Addons;
using BlueJay.Component.System.Events;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Moq;

namespace BlueJay.Component.System.Test
{
  [Collection("KeyHelper Tests")]
  public class EntityTests
  {
    public EntityTests()
    {
      KeyHelper.SetNext(AddonKey.One);
    }

    [Fact]
    public void AddAddon()
    {
      var vector = new Vector2(15, 10);
      var layers = new Mock<ILayerCollection>();
      var layer = new Mock<ILayer>();
      var events = new Mock<IEventQueue>();

      layers.Setup(x => x[It.IsAny<string>()])
        .Returns(layer.Object);

      var entity = new Entity(layers.Object, events.Object);

      Assert.True(entity.Add(new PositionAddon(vector)));
      layer.Verify(x => x.UpdateAddonTree(entity));
      events.Verify(x => x.DispatchEvent(It.IsAny<AddAddonEvent>(), entity));
      Assert.True(entity.MatchKey(KeyHelper.Create<PositionAddon>()));
      Assert.False(entity.MatchKey(KeyHelper.Create<DebugAddon>()));

      var pa = entity.GetAddon<PositionAddon>();
      Assert.Equal(vector, pa.Position);

      Assert.False(entity.Add(new PositionAddon(vector)));
    }

    [Fact]
    public void RemoveAddon()
    {
      var vector = new Vector2(15, 10);
      var layers = new Mock<ILayerCollection>();
      var layer = new Mock<ILayer>();
      var events = new Mock<IEventQueue>();

      layers.Setup(x => x[It.IsAny<string>()])
        .Returns(layer.Object);

      var entity = new Entity(layers.Object, events.Object);

      Assert.True(entity.Add(new PositionAddon(vector)));
      Assert.True(entity.MatchKey(KeyHelper.Create<PositionAddon>()));

      Assert.True(entity.Remove<PositionAddon>());
      layer.Verify(x => x.UpdateAddonTree(entity));
      events.Verify(x => x.DispatchEvent(It.IsAny<RemoveAddonEvent>(), entity));
      Assert.False(entity.MatchKey(KeyHelper.Create<PositionAddon>()));

      var pa = new PositionAddon(vector);
      Assert.True(entity.Add(pa));
      Assert.True(entity.Add(new SizeAddon(10, 10)));

      Assert.True(entity.Remove(pa));
      Assert.False(entity.MatchKey(KeyHelper.Create<PositionAddon>()));
      Assert.True(entity.MatchKey(KeyHelper.Create<SizeAddon>()));

      /// Try to remove it again
      Assert.False(entity.Remove(pa));
    }

    [Fact]
    public void UpdateAddon()
    {
      var vector = new Vector2(15, 10);
      var layers = new Mock<ILayerCollection>();
      var layer = new Mock<ILayer>();
      var events = new Mock<IEventQueue>();

      layers.Setup(x => x[It.IsAny<string>()])
        .Returns(layer.Object);

      var entity = new Entity(layers.Object, events.Object);

      Assert.True(entity.Add(new PositionAddon(vector)));
      Assert.True(entity.MatchKey(KeyHelper.Create<PositionAddon>()));

      var pa = entity.GetAddon<PositionAddon>();
      pa.Position = vector * 2;
      Assert.True(entity.Update(pa));
      events.Verify(x => x.DispatchEvent(It.IsAny<UpdateAddonEvent>(), entity));

      pa = entity.GetAddon<PositionAddon>();
      Assert.Equal(vector * 2, pa.Position);

      var sa = new SizeAddon(10, 10);
      Assert.False(entity.Update(sa));
    }

    [Fact]
    public void UpsertAddon()
    {
      var vector = new Vector2(15, 10);
      var layers = new Mock<ILayerCollection>();
      var layer = new Mock<ILayer>();
      var events = new Mock<IEventQueue>();

      layers.Setup(x => x[It.IsAny<string>()])
        .Returns(layer.Object);

      var entity = new Entity(layers.Object, events.Object);

      Assert.True(entity.Add(new PositionAddon(vector)));
      layer.Verify(x => x.UpdateAddonTree(entity));
      events.Verify(x => x.DispatchEvent(It.IsAny<AddAddonEvent>(), entity));
      Assert.True(entity.MatchKey(KeyHelper.Create<PositionAddon>()));
      Assert.False(entity.MatchKey(KeyHelper.Create<DebugAddon>()));

      var pa = entity.GetAddon<PositionAddon>();
      Assert.Equal(vector, pa.Position);

      pa.Position = vector * 2;
      Assert.True(entity.Upsert(pa));
      events.Verify(x => x.DispatchEvent(It.IsAny<UpdateAddonEvent>(), entity));

      pa = entity.GetAddon<PositionAddon>();
      Assert.Equal(vector * 2, pa.Position);

      Assert.True(entity.Upsert(new SizeAddon(10, 5)));
      var sa = entity.GetAddon<SizeAddon>();
      Assert.Equal(10, sa.Size.Width);
      Assert.Equal(5, sa.Size.Height);
    }

    [Fact]
    public void GetAddons()
    {
      var vector = new Vector2(15, 10);
      var layers = new Mock<ILayerCollection>();
      var events = new Mock<IEventQueue>();

      var entity = new Entity(layers.Object, events.Object);

      Assert.True(entity.Add(new PositionAddon(vector)));
      Assert.True(entity.Add(new DebugAddon(KeyHelper.Create<PositionAddon>())));
      Assert.True(entity.Add(new VelocityAddon()));

      var count = 0;
      var addons = entity.GetAddons(KeyHelper.Create<PositionAddon, DebugAddon>());
      foreach (var addon in addons)
      {
        if (addon is PositionAddon pa)
        {
          Assert.Equal(vector, pa.Position);
          count++;
        }
        if (addon is DebugAddon da)
        {
          Assert.Equal(KeyHelper.Create<PositionAddon>(), da.KeyIdentifier);
          count++;
        }
      }
      Assert.Equal(2, count);
    }

    [Fact]
    public void TryGetAddon()
    {
      var vector = new Vector2(15, 10);
      var layers = new Mock<ILayerCollection>();
      var events = new Mock<IEventQueue>();

      var entity = new Entity(layers.Object, events.Object);

      Assert.True(entity.Add(new PositionAddon(vector)));

      Assert.True(entity.TryGetAddon<PositionAddon>(out var pa));
      Assert.Equal(vector, pa.Position);
      Assert.False(entity.TryGetAddon<DebugAddon>(out var da));
    }

    [Fact]
    public void Contains()
    {
      var layers = new Mock<ILayerCollection>();
      var events = new Mock<IEventQueue>();

      var entity = new Entity(layers.Object, events.Object);

      Assert.True(entity.Add(new BoundsAddon(10, 10, 10, 10)));
      Assert.True(entity.Add(new ColorAddon(Color.Red)));
      Assert.True(entity.Add(new DebugAddon(KeyHelper.Create<BoundsAddon>())));
      Assert.True(entity.Add(new FrameAddon(1, 1, 1)));
      Assert.True(entity.Add(new PositionAddon(Vector2.Zero)));
      Assert.True(entity.Add(new SizeAddon(10, 10)));
      Assert.True(entity.Add(new SpriteEffectsAddon(SpriteEffects.None)));
      Assert.True(entity.Add(new SpriteSheetAddon(10, 10)));
      Assert.True(entity.Add(new VelocityAddon(Vector2.Zero)));

      Assert.True(entity.Contains<BoundsAddon>());
      Assert.True(entity.Contains<BoundsAddon, ColorAddon>());
      Assert.True(entity.Contains<BoundsAddon, ColorAddon, DebugAddon>());
      Assert.True(entity.Contains<BoundsAddon, ColorAddon, DebugAddon, FrameAddon>());
      Assert.True(entity.Contains<BoundsAddon, ColorAddon, DebugAddon, FrameAddon, PositionAddon>());
      Assert.True(entity.Contains<BoundsAddon, ColorAddon, DebugAddon, FrameAddon, PositionAddon, SizeAddon>());
      Assert.True(entity.Contains<BoundsAddon, ColorAddon, DebugAddon, FrameAddon, PositionAddon, SizeAddon, SpriteEffectsAddon>());
      Assert.True(entity.Contains<BoundsAddon, ColorAddon, DebugAddon, FrameAddon, PositionAddon, SizeAddon, SpriteEffectsAddon, SpriteSheetAddon>());
      Assert.True(entity.Contains<BoundsAddon, ColorAddon, DebugAddon, FrameAddon, PositionAddon, SizeAddon, SpriteEffectsAddon, SpriteSheetAddon, VelocityAddon>());
      Assert.False(entity.Contains<BoundsAddon, ColorAddon, DebugAddon, FrameAddon, PositionAddon, SizeAddon, SpriteEffectsAddon, SpriteSheetAddon, VelocityAddon, TextureAddon>());
    }
  }
}