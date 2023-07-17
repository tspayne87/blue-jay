using BlueJay.Component.System.Interfaces;
using BlueJay.Moq;
using BlueJay.UI.Component.Test.Components;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.UI.Component.Test
{
  public class ForTest : GameTest
  {
    [Fact]
    public void Basic()
    {
      var node = _game.Provider.ParseJayTML("<Container for=\"#index in 0..{{Count}}\">Hello World {{#index}}</Container>", typeof(BaseComponent));
      node.GenerateUI();

      var basic = node.RootComponent as BaseComponent;
      Assert.NotNull(basic);
      Assert.Empty(_game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 2;
      AssertHelper.UIEqual(
        "-- Container", "---- Text: Hello World 0",
        "-- Container", "---- Text: Hello World 1",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 5;
      AssertHelper.UIEqual(
        "-- Container", "---- Text: Hello World 0",
        "-- Container", "---- Text: Hello World 1",
        "-- Container", "---- Text: Hello World 2",
        "-- Container", "---- Text: Hello World 3",
        "-- Container", "---- Text: Hello World 4",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 0;
      Assert.Empty(_game.Provider.GetUIDebugStructureString());
    }

    [Fact]
    public void WithComponent()
    {
      var node = _game.Provider.ParseJayTML("<ChildComponent for=\"#item in 0..{{Count}}\">Hello World {{#item}}</ChildComponent>", typeof(BaseComponent));
      node.GenerateUI();

      var basic = node.RootComponent as BaseComponent;
      Assert.NotNull(basic);
      Assert.Empty(_game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 2;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: Hello From Child 1",
        "---- Text: Hello World 0",
        "-- Container",
        "---- Text: Hello From Child 1",
        "---- Text: Hello World 1",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 0;
      Assert.Empty(_game.Provider.GetUIDebugStructureString());
    }

    [Fact]
    public void NoneBindingProp()
    {
      var node = _game.Provider.ParseJayTML("<PropComponent for=\"#item in 0..{{Count}}\" :None=\"#item\" />", typeof(BaseComponent));
      node.GenerateUI();

      var basic = node.RootComponent as BaseComponent;
      Assert.NotNull(basic);
      Assert.Empty(_game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 2;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 0",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 1",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 10;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 0",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 1",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 2",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 3",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 4",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 5",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 6",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 7",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 8",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 9",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 0;
      Assert.Empty(_game.Provider.GetUIDebugStructureString());
    }

    [Fact]
    public void OneWayBindingProp()
    {
      var node = _game.Provider.ParseJayTML("<PropComponent for=\"#item in 0..{{Count}}\" :OneWay=\"Count\" />", typeof(BaseComponent));
      node.GenerateUI();

      var basic = node.RootComponent as BaseComponent;
      Assert.NotNull(basic);
      Assert.Empty(_game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 2;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 2 , None: 0",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 2 , None: 0",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 3;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 3 , None: 0",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 3 , None: 0",
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 3 , None: 0",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 0;
      Assert.Empty(_game.Provider.GetUIDebugStructureString());
    }

    [Fact]
    public void TwoWayBindingProp()
    {
      var node = _game.Provider.ParseJayTML("<PropComponent for=\"#item in 0..{{Count}}\" :TwoWay=\"Count\" />", typeof(BaseComponent));
      node.GenerateUI();

      var basic = node.RootComponent as BaseComponent;
      Assert.NotNull(basic);
      Assert.Empty(_game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 2;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 2 , OneWay: 0 , None: 0",
        "-- Container",
        "---- Text: TwoWay: 2 , OneWay: 0 , None: 0",
        _game.Provider.GetUIDebugStructureString()
      );

      Assert.Equal(2, basic.Children.Count);
      var prop = basic.Children[0] as PropComponent;
      Assert.NotNull(prop);

      prop.TwoWay.Value = 3;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 3 , OneWay: 0 , None: 0",
        "-- Container",
        "---- Text: TwoWay: 3 , OneWay: 0 , None: 0",
        "-- Container",
        "---- Text: TwoWay: 3 , OneWay: 0 , None: 0",
        _game.Provider.GetUIDebugStructureString()
      );
      Assert.Equal(3, basic.Children.Count);

      basic.Count.Value = 0;
      Assert.Empty(_game.Provider.GetUIDebugStructureString());
    }
  }
}
