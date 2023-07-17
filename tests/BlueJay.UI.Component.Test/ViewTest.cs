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
  public class ViewTest : GameTest
  {
    [Fact]
    public void SingleElement()
    {
      var node = _game.Provider.ParseJayTML("Hello World {{Count}}", typeof(BaseComponent));
      node.GenerateUI();

      var basic = node.RootComponent as BaseComponent;
      Assert.NotNull(basic);
      Assert.Equal("-- Text: Hello World 0", _game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 2;
      Assert.Equal("-- Text: Hello World 2", _game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 10;
      Assert.Equal("-- Text: Hello World 10", _game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 0;
      Assert.Equal("-- Text: Hello World 0", _game.Provider.GetUIDebugStructureString());
    }

    [Fact]
    public void WithContainer()
    {
      var node = _game.Provider.ParseJayTML("<Container>Hello World {{Count}}</Container>", typeof(BaseComponent));
      node.GenerateUI();

      var basic = node.RootComponent as BaseComponent;
      Assert.NotNull(basic);
      AssertHelper.UIEqual("-- Container", "---- Text: Hello World 0", _game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 2;
      AssertHelper.UIEqual("-- Container", "---- Text: Hello World 2", _game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 10;
      AssertHelper.UIEqual("-- Container", "---- Text: Hello World 10", _game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 0;
      AssertHelper.UIEqual("-- Container", "---- Text: Hello World 0", _game.Provider.GetUIDebugStructureString());
    }

    [Fact]
    public void WithComponent()
    {
      var node = _game.Provider.ParseJayTML("<ChildComponent>Hello World {{Count}}</ChildComponent>", typeof(BaseComponent));
      node.GenerateUI();

      var basic = node.RootComponent as BaseComponent;
      Assert.NotNull(basic);
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: Hello From Child 1",
        "---- Text: Hello World 0",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 2;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: Hello From Child 1",
        "---- Text: Hello World 2",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 0;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: Hello From Child 1",
        "---- Text: Hello World 0",
        _game.Provider.GetUIDebugStructureString()
      );
    }

    [Fact]
    public void NoneBindingProp()
    {
      var node = _game.Provider.ParseJayTML("<PropComponent :None=\"2\">Hello World {{Count}}</PropComponent>", typeof(BaseComponent));
      node.GenerateUI();

      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 2",
        "---- Text: Hello World 0",
        _game.Provider.GetUIDebugStructureString()
      );
    }

    [Fact]
    public void OneWayBindingProp()
    {
      var node = _game.Provider.ParseJayTML("<PropComponent :OneWay=\"Count\" />", typeof(BaseComponent));
      node.GenerateUI();

      var basic = node.RootComponent as BaseComponent;
      Assert.NotNull(basic);
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 0",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 2;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 2 , None: 0",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 0;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 0",
        _game.Provider.GetUIDebugStructureString()
      );
    }

    [Fact]
    public void TwoWayBindingProp()
    {
      var node = _game.Provider.ParseJayTML("<PropComponent :TwoWay=\"Count\">Hello World {{Count}}</PropComponent>", typeof(BaseComponent));
      node.GenerateUI();

      var basic = node.RootComponent as BaseComponent;
      Assert.NotNull(basic);

      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 0",
        "---- Text: Hello World 0",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 2;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 2 , OneWay: 0 , None: 0",
        "---- Text: Hello World 2",
        _game.Provider.GetUIDebugStructureString()
      );

      Assert.Single(basic.Children);
      var prop = basic.Children[0] as PropComponent;
      Assert.NotNull(prop);

      prop.TwoWay.Value = 0;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: TwoWay: 0 , OneWay: 0 , None: 0",
        "---- Text: Hello World 0",
        _game.Provider.GetUIDebugStructureString()
      );
    }

    [Fact]
    public void ProvideAndInject()
    {
      var node = _game.Provider.ParseJayTML("<ProvideComponent><InjectComponent /></ProvideComponent>", typeof(BaseComponent));
      node.GenerateUI();

      var provide = node.RootComponent.Children[0] as ProvideComponent;
      Assert.NotNull(provide);

      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: Provide: 0",
        "---- Container",
        "------ Text: Inject: 0",
        _game.Provider.GetUIDebugStructureString()
      );

      provide.Provide.Value = 2;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: Provide: 2",
        "---- Container",
        "------ Text: Inject: 2",
        _game.Provider.GetUIDebugStructureString()
      );

      provide.Provide.Value = 0;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: Provide: 0",
        "---- Container",
        "------ Text: Inject: 0",
        _game.Provider.GetUIDebugStructureString()
      );
    }
  }
}
