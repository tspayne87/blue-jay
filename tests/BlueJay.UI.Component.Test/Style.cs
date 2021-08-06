using BlueJay.UI.Component.Language;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BlueJay.UI.Component.Test
{
  public class Style
  {
    [Fact]
    public void Basic()
    {
      var scopes = new List<LanguageScope>() { new Component().GenerateScope() };

      var style = Language.Language.ParseStyle("padding: 5; textColor: 0, 0, 0; font: Default; columnGap: 3", scopes);

      Assert.Equal(5, style.Padding);
      Assert.Equal(new Color(0, 0, 0), style.TextColor);
      Assert.Equal("Default", style.Font);
      Assert.Equal(new Point(3), style.ColumnGap);
    }

    [Fact]
    public void ReactiveProp()
    {
      var scopes = new List<LanguageScope>() { new Component(5, new Point(3)).GenerateScope() };

      var style = Language.Language.ParseStyle("padding: {{Padding}}; textColor: 0, 0, 0; font: Default; columnGap: {{ColumnGap}}", scopes);

      Assert.Equal(5, style.Padding);
      Assert.Equal(new Color(0, 0, 0), style.TextColor);
      Assert.Equal("Default", style.Font);
      Assert.Equal(new Point(3), style.ColumnGap);
    }

    [View("<container>Hello World</container>")]
    public class Component : UIComponent
    {
      public readonly ReactiveProperty<int> Padding;
      public readonly ReactiveProperty<Point> ColumnGap;

      public Component(int padding = 0, Point columnGap = default)
      {
        Padding = new ReactiveProperty<int>(padding);
        ColumnGap = new ReactiveProperty<Point>(columnGap);
      }
    }
  }
}
