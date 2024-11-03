using BlueJay.Component.System.Interfaces;
using BlueJay.Events.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Test
{
  [Collection("KeyHelper Tests")]
  public class LayerTests
  {
    [Fact]
    public void ReadOnly()
    {
      var layer = new Layer(string.Empty, 0);
      Assert.False(layer.IsReadOnly);
    }

    [Fact]
    public void SquareBracket()
    {
      var mockLayers = new Mock<ILayerCollection>();
      var mockEvents = new Mock<IEventQueue>();

      var entity1 = new Entity(mockLayers.Object, mockEvents.Object);
      var entity2 = new Entity(mockLayers.Object, mockEvents.Object);

      var layer = new Layer(string.Empty, 0);
      layer.Add(entity1);

      Assert.Single(layer);
      Assert.Equal(entity1, layer[0]);

      layer[0] = entity2;
      Assert.Single(layer);
      Assert.Equal(entity2, layer[0]);
    }

    [Fact]
    public void ImplementedMethods()
    {
      var mockLayers = new Mock<ILayerCollection>();
      var mockEvents = new Mock<IEventQueue>();

      var entity1 = new Entity(mockLayers.Object, mockEvents.Object);
      var entity2 = new Entity(mockLayers.Object, mockEvents.Object);
      var entity3 = new Entity(mockLayers.Object, mockEvents.Object);

      var items = new IEntity[] { entity3, entity2 };

      var layer = new Layer(string.Empty, 0);
      layer.Add(entity1);

      Assert.Single(layer);

      layer.Insert(0, entity2);
      Assert.Equal(2, layer.Count);
      Assert.Equal(entity2, layer[0]);
      Assert.Equal(1, layer.IndexOf(entity1));

      var span = layer.AsSpan();
      Assert.Equal(2, span.Length);
      Assert.Equal(entity2, span[0]);
      Assert.Equal(entity1, span[1]);

      layer.RemoveAt(0);
      Assert.Single(layer);
      Assert.Equal(entity1, layer[0]);

      Assert.DoesNotContain(entity2, layer);

      layer.CopyTo(items, 0);
      Assert.Equal(2, items.Length);
      Assert.Equal(entity1, items[0]);
      Assert.Equal(entity2, items[1]);

      var enumerator = layer.GetEnumerator();
      enumerator.MoveNext();
      Assert.Equal(entity1, enumerator.Current);

      layer.Clear();
      Assert.Empty(layer);
    }
  }
}
