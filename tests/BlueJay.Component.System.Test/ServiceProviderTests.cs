using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Containers;
using BlueJay.Events.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Test
{
  [Collection("KeyHelper Tests")]
  public class ServiceProviderTests
  {
    [Fact]
    public void AddEntity()
    {
      var layer = "Unit Tests";
      var weight = 0;
      var fonts = new Dictionary<string, SpriteFont>();
      var mockService = new Mock<IServiceProvider>();
      var mockLayers = new Mock<ILayerCollection>();
      var mockEvents = new Mock<IEventQueue>();
      var entity = new Entity(mockLayers.Object, mockEvents.Object);

      mockService.Setup(x => x.GetService(typeof(ILayerCollection)))
        .Returns(mockLayers.Object);
      mockService.Setup(x => x.GetService(typeof(IEventQueue)))
        .Returns(mockEvents.Object);

      var newEntity = mockService.Object.AddEntity(layer, weight);
      Assert.Equal(layer, newEntity.Layer);
      mockLayers.Verify(x => x.Add(newEntity, layer, weight));

      Assert.Empty(entity.Layer);
      mockService.Object.AddEntity(entity, layer, weight);
      Assert.Equal(layer, entity.Layer);
      mockLayers.Verify(x => x.Add(entity, layer, weight));
    }

    [Fact]
    public void AddSpriteFont()
    {
      var fonts = new Dictionary<string, ISpriteFontContainer>();
      var mockService = new Mock<IServiceProvider>();
      var mockSpriteFont = new Mock<IFontCollection>();

      mockService.Setup(x => x.GetService(typeof(IFontCollection)))
        .Returns(mockSpriteFont.Object);
      mockSpriteFont.Setup(x => x.SpriteFonts)
        .Returns(fonts);

      mockService.Object.AddSpriteFont("Unit Test", null!);

      Assert.Contains("Unit Test", fonts);
      Assert.Null(fonts["Unit Test"]);
    }

    [Fact]
    public void AddTextureFont()
    {
      var fonts = new Dictionary<string, TextureFont>();
      var mockService = new Mock<IServiceProvider>();
      var mockSpriteFont = new Mock<IFontCollection>();

      mockService.Setup(x => x.GetService(typeof(IFontCollection)))
        .Returns(mockSpriteFont.Object);
      mockSpriteFont.Setup(x => x.TextureFonts)
        .Returns(fonts);

      mockService.Object.AddTextureFont("Unit Test", null!);

      Assert.Contains("Unit Test", fonts);
      Assert.Null(fonts["Unit Test"]);
    }
  }
}
