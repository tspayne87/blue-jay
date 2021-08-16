using BlueJay.UI.Component.Language;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace BlueJay.UI.Component.Test
{
  public class XML
  {
    public IServiceProvider Provider => new ServiceCollection().BuildServiceProvider();

    [Fact]
    public void Single()
    {
      Provider.ParseXML("<Container />", new Component());
    }

    [Fact]
    public void InnerText()
    {
      Provider.ParseXML("<Container>Hello World</Container>", new Component());
    }

    [Fact]
    public void StringProp()
    {
      Provider.ParseXML("<Container prop1='Value 1'>Hello World</Container>", new Component());
    }

    [Fact]
    public void BindedLiteralProp()
    {
      Provider.ParseXML("<Container :prop1=\"'string'\">Hello World</Container>", new Component());
      Provider.ParseXML("<Container :prop1=\"15\">Hello World</Container>", new Component());
      Provider.ParseXML("<Container :prop1=\"15.0\">Hello World</Container>", new Component());
      Provider.ParseXML("<Container :prop1=\"true\">Hello World</Container>", new Component());
    }

    [Fact]
    public void BindedProp()
    {
      var component = new Component();

      var identifier = Provider.ParseXML("<Container :prop1=\"Integer\">Hello World</Container>", component);
      var func = Provider.ParseXML("<Container :prop1=\"OnSelect($event, Integer)\">Hello World</Container>", component);

      var identifierExpression = identifier.Props.FirstOrDefault(x => x.Name == "prop1");
      var funcExpression = func.Props.FirstOrDefault(x => x.Name == "prop1");

      Assert.Equal(5, identifierExpression.DataGetter(null));
      Assert.True((bool)funcExpression.DataGetter(null));

      component.Integer.Value = 10;
      Assert.Equal(10, identifierExpression.DataGetter(null));
      Assert.False((bool)funcExpression.DataGetter(null));
    }

    [Fact]
    public void BasicComponent()
    {
      var component = new Component();
      var tree = Provider.ParseXML("<Button :Int=\"Integer\" String='Value 1'>Hello World</Button>", component, new List<Type>() { typeof(Button) });

      Assert.NotNull(tree.Props.FirstOrDefault(x => x.Name == "Int"));
      Assert.NotNull(tree.Props.FirstOrDefault(x => x.Name == "String"));
      Assert.NotNull(tree.Props.FirstOrDefault(x => x.Name == "ButtonText"));

      Assert.Equal(5, tree.Props.First(x => x.Name == "Int").DataGetter(null));
      Assert.Equal("Value 1", tree.Props.First(x => x.Name == "String").Data);
      Assert.Equal("Hello World", tree.Props.First(x => x.Name == "ButtonText").DataGetter(null));
      Assert.Equal(ElementType.Container, tree.Type);
      Assert.Equal(2, tree.Children.Count);

      Assert.NotNull(tree.Children[0].Props.FirstOrDefault(x => x.Name == PropNames.Text));
      Assert.NotNull(tree.Children[1].Props.FirstOrDefault(x => x.Name == PropNames.Text));

      Assert.Equal(ElementType.Text, tree.Children[0].Type);
      Assert.Equal(ElementType.Text, tree.Children[1].Type);
      Assert.Equal("Button Hello World", tree.Children[0].Props.First(x => x.Name == PropNames.Text).Data);
      Assert.Equal("Hello World", tree.Children[1].Props.First(x => x.Name == PropNames.Text).Data);
    }

    [Fact]
    public void TwoWayBinding()
    {
      var component = new Component(5, "Testing This");
      var tree = Provider.ParseXML("<Button :Name=\"Str\">Hello World</Button>", component, new List<Type>() { typeof(Button) });

      Assert.NotNull(tree.Props.FirstOrDefault(x => x.Name == "Name"));
      Assert.NotNull(tree.Props.FirstOrDefault(x => x.Name == "ButtonText"));

      Assert.Equal("Testing This", tree.Props.First(x => x.Name == "Name").DataGetter(null));
      Assert.Equal("Testing This", tree.Props.First(x => x.Name == "ButtonText").DataGetter(null));

      component.Str.Value = "Hello World";
      Assert.Equal("Hello World", tree.Props.First(x => x.Name == "Name").DataGetter(null));
      Assert.Equal("Hello World", tree.Props.First(x => x.Name == "ButtonText").DataGetter(null));
    }

    [Fact]
    public void EventProp()
    {
      Provider.ParseXML("<Container @select=\"OnSelect($event, Integer)\">Hello World</Container>", new Component());
      Provider.ParseXML("<Container @select.global=\"OnSelect($event, Integer)\">Hello World</Container>", new Component());
    }

    public class Component : UIComponent
    {
      public readonly ReactiveProperty<int> Integer;
      public readonly ReactiveProperty<string> Str;

      public Component(int integer = 5, string str = "Test")
      {
        Integer = new ReactiveProperty<int>(integer);
        Str = new ReactiveProperty<string>(str);
      }

      public bool OnSelect(SelectEvent evt, int integer)
      {
        return integer % 2 == 1;
      }
    }

    [View("<Container :ButtonText=\"Name\">Button Hello World <Slot /></Container>")]
    public class Button : UIComponent
    {
      [Prop(PropBinding.TwoWay)]
      public readonly ReactiveProperty<string> Name;

      public Button()
      {
        Name = new ReactiveProperty<string>("Hello World");
      }
    }
  }
}
