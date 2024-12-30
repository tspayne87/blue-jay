using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Test.Collections
{
  [Collection("KeyHelper Tests")]
  public class LayerCollectionTests
  {
    [Fact]
    public void AddEntity()
    {
      var serviceMock = new Mock<IServiceProvider>();
      var eventMock = new Mock<IEventQueue>();

      serviceMock.Setup(x => x.GetService(typeof(IServiceProvider)))
        .Returns(serviceMock.Object);

      var layers = new LayerCollection(serviceMock.Object);
      Assert.Empty(layers);

      layers.Add(new Entity(eventMock.Object));
      Assert.Single(layers);
      Assert.NotNull(layers[string.Empty]);
      Assert.Single(layers[string.Empty]!);
    }

    [Fact]
    public void AddLayer()
    {
      var layer = "Unit Layer";
      var serviceMock = new Mock<IServiceProvider>();
      var eventMock = new Mock<IEventQueue>();

      serviceMock.Setup(x => x.GetService(typeof(IServiceProvider)))
        .Returns(serviceMock.Object);

      var layers = new LayerCollection(serviceMock.Object);
      Assert.Empty(layers);

      layers.Add(layer);
      Assert.Single(layers);
      Assert.NotNull(layers[layer]);
      Assert.Empty(layers[layer]!);
    }

    [Fact]
    public void RemoveLayer()
    {
      var serviceMock = new Mock<IServiceProvider>();
      var eventMock = new Mock<IEventQueue>();

      serviceMock.Setup(x => x.GetService(typeof(IServiceProvider)))
        .Returns(serviceMock.Object);

      var layers = new LayerCollection(serviceMock.Object);
      var entity = new Entity(eventMock.Object);
      Assert.Empty(layers);

      layers.Add(entity);
      layers.Remove(entity);
      Assert.True(layers.Contains(string.Empty));
      Assert.Empty(layers[string.Empty]!);
    }

    [Fact]
    public void ImplementedMethods()
    {
      var serviceMock = new Mock<IServiceProvider>();
      var eventMock = new Mock<IEventQueue>();

      serviceMock.Setup(x => x.GetService(typeof(IServiceProvider)))
        .Returns(serviceMock.Object);

      var layer = new Layer("Unit Layer 1", 5);
      var layer2 = new Layer("Unit Layer 2", 10);
      var layer3 = new Layer("Unit Layer 3", 0);
      var items = new Layer[] { layer, layer2 };
      var layers = new LayerCollection(serviceMock.Object);
      Assert.Empty(layers);

      /// Add Layer
      layers.Add(layer);
      Assert.Single(layers);
      Assert.Equal(0, layers.IndexOf(layer));

      layers.Insert(0, layer3);
      Assert.Equal(0, layers.IndexOf(layer3));
      Assert.Equal(1, layers.IndexOf(layer));

      layers.RemoveAt(1);
      Assert.Single(layers);
      Assert.Equal(0, layers.IndexOf(layer3));

      layers.CopyTo(items, 0);
      Assert.Equal(layer3, items[0]);
      Assert.Equal(layer2, items[1]);

      layers.Remove(layer3);
      Assert.Empty(layers);

      layers.Add(layer2);
      layers.Add(layer);

      var enumerator = layers.GetEnumerator();
      enumerator.MoveNext();
      Assert.Equal(layer, enumerator.Current);
      enumerator.MoveNext();
      Assert.Equal(layer2, enumerator.Current);
    }
  }
}
