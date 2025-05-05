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

      var layers = new Layers(serviceMock.Object);
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

      var layers = new Layers(serviceMock.Object);
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

      var layers = new Layers(serviceMock.Object);
      var entity = new Entity(eventMock.Object);
      Assert.Empty(layers);

      layers.Add(entity);
      layers.Remove(entity);
      Assert.True(layers.Contains(string.Empty));
      Assert.Empty(layers[string.Empty]!);
    }
  }
}
