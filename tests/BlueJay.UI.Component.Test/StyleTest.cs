using BlueJay.UI.Component.Test.Components;
using System;
using BlueJay.Moq;

namespace BlueJay.UI.Component.Test
{
  public class StyleTest : GameTest
  {
    [Fact]
    public void PaddingOneNumber()
    {
      var padding = AssetAndReturnStyle("<Container Style=\"Padding: 15\" />", x => x.Padding);
      Assert.Equal(15, padding.Value.Top);
      Assert.Equal(15, padding.Value.Right);
      Assert.Equal(15, padding.Value.Bottom);
      Assert.Equal(15, padding.Value.Left);
    }

    [Fact]
    public void PaddingTwoNumbers()
    {
      var padding = AssetAndReturnStyle("<Container Style=\"Padding: 15, 20\" />", x => x.Padding);
      Assert.Equal(15, padding.Value.Top);
      Assert.Equal(20, padding.Value.Right);
      Assert.Equal(15, padding.Value.Bottom);
      Assert.Equal(20, padding.Value.Left);
    }

    [Fact]
    public void PaddingThreeNumbers()
    {
      var padding = AssetAndReturnStyle("<Container Style=\"Padding: 15, 20, 10\" />", x => x.Padding);
      Assert.Equal(15, padding.Value.Top);
      Assert.Equal(20, padding.Value.Right);
      Assert.Equal(10, padding.Value.Bottom);
      Assert.Equal(20, padding.Value.Left);
    }

    [Fact]
    public void PaddingFourNumbers()
    {
      var padding = AssetAndReturnStyle("<Container Style=\"Padding: 15, 20, 10, 5\" />", x => x.Padding);
      Assert.Equal(15, padding.Value.Top);
      Assert.Equal(20, padding.Value.Right);
      Assert.Equal(10, padding.Value.Bottom);
      Assert.Equal(5, padding.Value.Left);
    }

    [Fact]
    public void BackgroundColorThreeNumbers()
    {
      var backgroundColor = AssetAndReturnStyle("<Container Style=\"BackgroundColor: 60, 60, 60\" />", x => x.BackgroundColor);
      Assert.Equal(60, backgroundColor.Value.R);
      Assert.Equal(60, backgroundColor.Value.G);
      Assert.Equal(60, backgroundColor.Value.B);
      Assert.Equal(255, backgroundColor.Value.A);
    }

    [Fact]
    public void BackgroundColorFourNumbers()
    {
      var backgroundColor = AssetAndReturnStyle("<Container Style=\"BackgroundColor: 60, 60, 60, 60\" />", x => x.BackgroundColor);
      Assert.Equal(60, backgroundColor.Value.R);
      Assert.Equal(60, backgroundColor.Value.G);
      Assert.Equal(60, backgroundColor.Value.B);
      Assert.Equal(60, backgroundColor.Value.A);
    }

    public T AssetAndReturnStyle<T>(string template, Func<Style, T> func)
    {
      var node = _game.Provider.ParseJayTML(template, typeof(BaseComponent));
      node.GenerateUI();

      var entity = _game.Provider.GetFirstUIRootEntity();
      Assert.NotNull(entity);

      var style = entity.GetStyle(func);
      Assert.NotNull(style);

      return style;
    }
  }
}
