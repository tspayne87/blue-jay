using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlueJay.Core.Container;
using Moq;
using Xunit;

namespace BlueJay.Core.Test
{
  public class TextureFontTests
  {
    [Fact]
    public void Constructor_WithValidParameters_ShouldSetProperties()
    {
      // Arrange
      var container = new Mock<ITexture2DContainer>();
      container.SetupGet(c => c.Width).Returns(100);
      container.SetupGet(c => c.Height).Returns(200);

      var rows = 5;
      var cols = 5;
      var alphabet = "abc";

      // Act
      var font = new TextureFont(container.Object, rows, cols, alphabet);

      // Assert
      Assert.Equal(container.Object, font.Texture);
      Assert.Equal(40, font.Height);
      Assert.Equal(20, font.Width);
    }

    [Fact]
    public void GetBounds()
    {
      // Arrange
      var container = new Mock<ITexture2DContainer>();
      container.SetupGet(c => c.Width).Returns(60);
      container.SetupGet(c => c.Height).Returns(40);

      var rows = 1;
      var cols = 3;
      var alphabet = "abc";

      var font = new TextureFont(container.Object, rows, cols, alphabet);

      // Act
      var bounds = font.GetBounds('b');

      // Assert
      Assert.Equal(20, bounds.X);
      Assert.Equal(0, bounds.Y);
      Assert.Equal(20, bounds.Width);
      Assert.Equal(40, bounds.Height);
    }

    [Fact]
    public void MeasureString()
    {
      // Arrange
      var container = new Mock<ITexture2DContainer>();
      container.SetupGet(c => c.Width).Returns(60);
      container.SetupGet(c => c.Height).Returns(40);

      var rows = 1;
      var cols = 3;
      var alphabet = "abc";

      var font = new TextureFont(container.Object, rows, cols, alphabet);

      // Act
      var size = font.MeasureString("abcabc");

      // Assert
      Assert.Equal(120, size.X);
      Assert.Equal(40, size.Y);
    }

    [Fact]
    public void FitString()
    {
      // Arrange
      var container = new Mock<ITexture2DContainer>();
      container.SetupGet(c => c.Width).Returns(60);
      container.SetupGet(c => c.Height).Returns(40);

      var rows = 1;
      var cols = 3;
      var alphabet = "abc";

      var font = new TextureFont(container.Object, rows, cols, alphabet);

      // Act
      var result = font.FitString("abc abc", 60, 1);

      // Assert
      Assert.Equal("abc\nabc", result);
    }

    [Fact]
    public void FitWithSpaces()
    {
      // Arrange
      var container = new Mock<ITexture2DContainer>();
      container.SetupGet(c => c.Width).Returns(60);
      container.SetupGet(c => c.Height).Returns(40);

      var rows = 1;
      var cols = 3;
      var alphabet = "abc";

      var font = new TextureFont(container.Object, rows, cols, alphabet);

      // Act
      var result = font.FitString("abc abc", 140, 1);

      // Assert
      Assert.Equal("abc abc", result);
    }
  }
}
