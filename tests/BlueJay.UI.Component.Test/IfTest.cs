using BlueJay.Moq;
using BlueJay.UI.Component.Test.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.UI.Component.Test
{
  public class IfTest : GameTest
  {
    [Fact]
    public void Basic()
    {
      var node = _game.Provider.ParseJayTML("<Container if=\"Count > 0\">Hello World {{Count}}</Container>", typeof(BaseComponent));
      node.GenerateUI();

      var basic = node.RootComponent as BaseComponent;
      Assert.NotNull(basic);
      Assert.Empty(_game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 2;
      AssertHelper.UIEqual("-- Container", "---- Text: Hello World 2", _game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 0;
      Assert.Empty(_game.Provider.GetUIDebugStructureString());
    }

    [Fact]
    public void WithComponent()
    {
      var node = _game.Provider.ParseJayTML("<ChildComponent if=\"Count > 0\">Hello World {{Count}}</ChildComponent>", typeof(BaseComponent));
      node.GenerateUI();

      var basic = node.RootComponent as BaseComponent;
      Assert.NotNull(basic);
      Assert.Empty(_game.Provider.GetUIDebugStructureString());

      basic.Count.Value = 2;
      AssertHelper.UIEqual(
        "-- Container",
        "---- Text: Hello From Child 1",
        "---- Text: Hello World 2",
        _game.Provider.GetUIDebugStructureString()
      );

      basic.Count.Value = 0;
      Assert.Empty(_game.Provider.GetUIDebugStructureString());
    }
  }
}
