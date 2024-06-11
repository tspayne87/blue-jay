using BlueJay.Common.Addons;
using BlueJay.Component.System.Events;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events.Interfaces;
using Microsoft.Xna.Framework;
using Moq;

namespace BlueJay.Component.System.Test
{
  public class EntityTests
  {
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
  }
}