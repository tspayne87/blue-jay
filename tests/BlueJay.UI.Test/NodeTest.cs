using BlueJay.Core;
using BlueJay.UI.Interfaces;
using BlueJay.Utils;
using Moq;

namespace BlueJay.UI.Test;

public class NodeTest
{
  [Fact]
  public void WidthPercentageTest()
  {
    var service = CreateServiceProvider(new Size(800, 600));
    var root = service.CreateUINode("unit-test");
    root.Style = new Style() { Width = "50%" };

    Assert.Equal(400, root.CalculateWidth());
    Assert.Equal(0, root.CalculateHeight());
  }

  [Fact]
  public void WidthPixelTest()
  {
    var service = CreateServiceProvider(new Size(800, 600));
    var root = service.CreateUINode("unit-test");
    root.Style = new Style() { Width = "200" };

    Assert.Equal(200, root.CalculateWidth());
    Assert.Equal(0, root.CalculateHeight());
  }

  [Fact]
  public void HeightPercentageTest()
  {
    var service = CreateServiceProvider(new Size(800, 600));
    var root = service.CreateUINode("unit-test");
    root.Style = new Style() { Height = "25%" };

    Assert.Equal(800, root.CalculateWidth());
    Assert.Equal(150, root.CalculateHeight());
  }

  [Fact]
  public void HeightPixelTest()
  {
    var service = CreateServiceProvider(new Size(800, 600));
    var root = service.CreateUINode("unit-test");
    root.Style = new Style() { Height = "150" };

    Assert.Equal(800, root.CalculateWidth());
    Assert.Equal(150, root.CalculateHeight());
  }

  [Fact]
  public void HeightChildrenTest()
  {
    var service = CreateServiceProvider(new Size(800, 600));
    var root = service.CreateUINode("unit-test");
    var child = service.CreateUINode("child");
    child.Style = new Style() { Height = "100" };
    root.AddChild(child);

    Assert.Equal(800, root.CalculateWidth());
    Assert.Equal(100, root.CalculateHeight());
  }

  [Fact]
  public void HeightChildrenWithGapTest()
  {
    var service = CreateServiceProvider(new Size(800, 600));
    var root = service.CreateUINode("unit-test");
    root.Style = new Style() { Gap = 10 };

    var child1 = service.CreateUINode("child");
    child1.Style = new Style() { Height = "100" };
    root.AddChild(child1);

    var child2 = service.CreateUINode("child2");
    child2.Style = new Style() { Height = "200" };
    root.AddChild(child2);

    Assert.Equal(800, root.CalculateWidth());
    Assert.Equal(200, root.CalculateHeight());

    Assert.Equal(395, child1.CalculateWidth());
    Assert.Equal(395, child2.CalculateWidth());
  }

  [Fact]
  public void HeightChildrenWithGapAndVerticalDirectionTest()
  {
    var service = CreateServiceProvider(new Size(800, 600));
    var root = service.CreateUINode("unit-test");
    root.Style = new Style() { Gap = 10, Direction = Direction.Vertical };

    var child1 = service.CreateUINode("child");
    child1.Style = new Style() { Height = "100" };
    root.AddChild(child1);

    var child2 = service.CreateUINode("child2");
    child2.Style = new Style() { Height = "200" };
    root.AddChild(child2);

    Assert.Equal(800, root.CalculateWidth());
    Assert.Equal(310, root.CalculateHeight());

    Assert.Equal(800, child1.CalculateWidth());
    Assert.Equal(800, child2.CalculateWidth());
  }

  [Fact]
  public void ChildrenPositionTest()
  {
    var service = CreateServiceProvider(new Size(800, 600));
    var root = service.CreateUINode("unit-test");
    root.Style = new Style() { Gap = 10 };

    var child1 = service.CreateUINode("child1");
    child1.Style = new Style() { Height = "100" };

    var child2 = service.CreateUINode("child2");
    child2.Style = new Style() { Height = "200" };

    root.AddChild(child1);
    root.AddChild(child2);

    Assert.Equal(0, child1.CalculateLeftPosition());
    Assert.Equal(0, child1.CalculateTopPosition());
    Assert.Equal(405, child2.CalculateLeftPosition());
    Assert.Equal(0, child2.CalculateTopPosition());
  }

  [Fact]
  public void ChildrenPositionVerticalTest()
  {
    var service = CreateServiceProvider(new Size(800, 600));
    var root = service.CreateUINode("unit-test");
    root.Style = new Style() { Gap = 10, Direction = Direction.Vertical };

    var child1 = service.CreateUINode("child1");
    child1.Style = new Style() { Height = "100" };

    var child2 = service.CreateUINode("child2");
    child2.Style = new Style() { Height = "200" };

    root.AddChild(child1);
    root.AddChild(child2);

    Assert.Equal(0, child1.CalculateLeftPosition());
    Assert.Equal(0, child1.CalculateTopPosition());
    Assert.Equal(0, child2.CalculateLeftPosition());
    Assert.Equal(110, child2.CalculateTopPosition());
  }

  [Fact]
  public void OffsetTopLeftTest()
  {
    var service = CreateServiceProvider(new Size(800, 600));
    var root = service.CreateUINode("unit-test");
    root.Style = new Style() { Offset = new Directions(10, 0, 20, 0) };

    var child1 = service.CreateUINode("child1");
    child1.Style = new Style() { Height = "100" };

    var child2 = service.CreateUINode("child2");
    child2.Style = new Style() { Height = "200" };

    root.AddChild(child1);
    root.AddChild(child2);

    Assert.Equal(20, child1.CalculateLeftPosition());
    Assert.Equal(10, child1.CalculateTopPosition());
    Assert.Equal(20, child2.CalculateLeftPosition());
    Assert.Equal(120, child2.CalculateTopPosition());
  }

  [Fact]
  public void OffsetBottomRightTest()
  {
    var service = CreateServiceProvider(new Size(800, 600));
    var root = service.CreateUINode("unit-test");
    root.Style = new Style() { Offset = new Directions(0, 10, 0, 20), Gap = 10, Direction = Direction.Vertical };

    var child1 = service.CreateUINode("child1");
    child1.Style = new Style() { Height = "100" };

    var child2 = service.CreateUINode("child2");
    child2.Style = new Style() { Height = "200" };

    root.AddChild(child1);
    root.AddChild(child2);

    Assert.Equal(-20, child1.CalculateLeftPosition());
    Assert.Equal(-10, child1.CalculateTopPosition());
    Assert.Equal(-20, child2.CalculateLeftPosition());
    Assert.Equal(90, child2.CalculateTopPosition());
  }

  /// <summary>
  /// Creates a service provider for the specified screen size and global style.
  /// </summary>
  /// <param name="screenSize">The current screen size for this current test</param>
  /// <param name="globalStyle">The global style to apply</param>
  /// <returns>Will return a service provider</returns>
  private IServiceProvider CreateServiceProvider(Size screenSize, Style? globalStyle = null)
  {
    var mockService = new Mock<IServiceProvider>();
    var mockScreen = new Mock<IScreenViewport>();
    var mockStyleService = new Mock<IStyleService>();

    mockScreen.SetupGet(s => s.Width).Returns(screenSize.Width);
    mockScreen.SetupGet(s => s.Height).Returns(screenSize.Height);

    mockStyleService.Setup(s => s.GetStyle(It.IsAny<string>(), It.IsAny<StyleDecorator>()))
      .Returns<string, StyleDecorator>((tag, decorator) => globalStyle ?? new Style());

    mockService.Setup(s => s.GetService(typeof(IScreenViewport))).Returns(mockScreen.Object);
    mockService.Setup(s => s.GetService(typeof(IStyleService))).Returns(mockStyleService.Object);

    return mockService.Object;
  }
}