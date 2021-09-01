using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Interactivity;
using BlueJay.UI.Component.Language;
using BlueJay.UI.Component.Reactivity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
      var tree = Provider.ParseXML("<Container />", new Component());

      Assert.Empty(tree.Children);
      Assert.Equal(ElementType.Container, tree.Type);
    }

    [Fact]
    public void InnerText()
    {
      var tree = Provider.ParseXML("<Container>Hello World</Container>", new Component());

      Assert.Single(tree.Children);
      Assert.NotNull(tree.Children[0].Props.FirstOrDefault(x => x.Name == PropNames.Text));
      Assert.Equal("Hello World", tree.Children[0].Props.First(x => x.Name == PropNames.Text).DataGetter(null));
    }

    [Fact]
    public void StringProp()
    {
      var tree = Provider.ParseXML("<Container Prop1='Value 1' />", new Component());

      Assert.Empty(tree.Children);
      Assert.NotNull(tree.Props.FirstOrDefault(x => x.Name == "Prop1"));
      Assert.Equal("Value 1", tree.Props.First(x => x.Name == "Prop1").DataGetter(null));
    }

    [Fact]
    public void BindedLiteralProp()
    {
      var tree = Provider.ParseXML("<Container :Prop1=\" 'Hello World'\" />", new Component());
      Assert.Empty(tree.Children);
      Assert.NotNull(tree.Props.FirstOrDefault(x => x.Name == "Prop1"));
      Assert.Equal("Hello World", tree.Props.First(x => x.Name == "Prop1").DataGetter(null));

      tree = Provider.ParseXML("<Container :Prop1=\"15\" />", new Component());
      Assert.Empty(tree.Children);
      Assert.NotNull(tree.Props.FirstOrDefault(x => x.Name == "Prop1"));
      Assert.Equal(15, tree.Props.First(x => x.Name == "Prop1").DataGetter(null));

      tree = Provider.ParseXML("<Container :Prop1=\"15.0\" />", new Component());
      Assert.Empty(tree.Children);
      Assert.NotNull(tree.Props.FirstOrDefault(x => x.Name == "Prop1"));
      Assert.Equal(15.0f, tree.Props.First(x => x.Name == "Prop1").DataGetter(null));

      tree = Provider.ParseXML("<Container :Prop1='true' />", new Component());
      Assert.Empty(tree.Children);
      Assert.NotNull(tree.Props.FirstOrDefault(x => x.Name == "Prop1"));
      Assert.Equal(true, tree.Props.First(x => x.Name == "Prop1").DataGetter(null));
    }

    [Fact]
    public void BindedProp()
    {
      var component = new Component();
      var scope = new ReactiveScope(new Dictionary<string, object>() { { "event", new SelectEvent() } });

      var identifier = Provider.ParseXML("<Container :Prop1=\"Integer\">Hello World</Container>", component);
      var func = Provider.ParseXML("<Container :Prop1=\"OnSelect($event, Integer)\">Hello World</Container>", component);

      var identifierExpression = identifier.Props.FirstOrDefault(x => x.Name == "Prop1");
      var funcExpression = func.Props.FirstOrDefault(x => x.Name == "Prop1");

      Assert.Equal(5, identifierExpression.DataGetter(null));
      Assert.True((bool)funcExpression.DataGetter(scope));

      component.Integer.Value = 10;
      Assert.Equal(10, identifierExpression.DataGetter(null));
      Assert.False((bool)funcExpression.DataGetter(scope));
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
      Assert.Equal("Value 1", tree.Props.First(x => x.Name == "String").DataGetter(null));
      Assert.Equal("Hello World", tree.Props.First(x => x.Name == "ButtonText").DataGetter(null));
      Assert.Equal(ElementType.Container, tree.Type);
      Assert.Equal(2, tree.Children.Count);

      Assert.NotNull(tree.Children[0].Props.FirstOrDefault(x => x.Name == PropNames.Text));
      Assert.NotNull(tree.Children[1].Props.FirstOrDefault(x => x.Name == PropNames.Text));

      Assert.Equal(ElementType.Text, tree.Children[0].Type);
      Assert.Equal(ElementType.Text, tree.Children[1].Type);
      Assert.Equal("Button Hello World", tree.Children[0].Props.First(x => x.Name == PropNames.Text).DataGetter(null));
      Assert.Equal("Hello World", tree.Children[1].Props.First(x => x.Name == PropNames.Text).DataGetter(null));
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
    }

    [Fact]
    public void EventProp()
    {
      var scope = new ReactiveScope(new Dictionary<string, object>() { { "event", new SelectEvent() } });
      var tree = Provider.ParseXML("<Container @Select=\"OnSelect($event, Integer)\" />", new Component());
      Assert.Empty(tree.Children);
      Assert.NotNull(tree.Events.FirstOrDefault(x => x.Name == "Select"));
      Assert.False(tree.Events.First(x => x.Name == "Select").IsGlobal);
      Assert.True((bool)tree.Events.Find(x => x.Name == "Select").Callback(scope));

      var treeGlobal = Provider.ParseXML("<Container @Select.Global=\"OnSelect($event, Integer)\" />", new Component());
      Assert.Empty(treeGlobal.Children);
      Assert.NotNull(treeGlobal.Events.FirstOrDefault(x => x.Name == "Select"));
      Assert.True(treeGlobal.Events.First(x => x.Name == "Select").IsGlobal);
      Assert.True((bool)treeGlobal.Events.Find(x => x.Name == "Select").Callback(scope));
    }

    [Fact]
    public void StyleProp()
    {
      var component = Provider.ParseXML("<Container Style=\"Position: Absolute; WidthPercentage: 1; Height: 4; VerticalAlign: Center; BackgroundColor: 200, 200, 200\">Hello World</Container>", new Component());
      Assert.NotNull(component.Props.FirstOrDefault(x => x.Name == PropNames.Style));

      var test = new Color(200, 200, 200);

      var style = component.Props.First(x => x.Name == PropNames.Style).DataGetter(null) as Style;
      Assert.Equal(Position.Absolute, style.Position);
      Assert.Equal(1f, style.WidthPercentage);
      Assert.Equal(4, style.Height);
      Assert.Equal(VerticalAlign.Center, style.VerticalAlign);
      Assert.Equal(new Color(200, 200, 200), style.BackgroundColor);
    }

    [Fact]
    public void TextExpression()
    {
      var instance = new Component();
      var tree = Provider.ParseXML("<Container>Score: {{Integer}}</Container>", instance);

      Assert.Single(tree.Children);
      Assert.NotNull(tree.Children[0].Props.FirstOrDefault(x => x.Name == PropNames.Text));

      Assert.Equal("Score: 5", tree.Children[0].Props.First(x => x.Name == PropNames.Text).DataGetter(null));

      instance.Integer.Value = 10;
      Assert.Equal("Score: 10", tree.Children[0].Props.First(x => x.Name == PropNames.Text).DataGetter(null));
    }

    [Fact]
    public void ReactiveStyleProp()
    {
      var instance = new Component();
      var component = Provider.ParseXML("<Container Style=\"Position: Absolute; WidthPercentage: 1; Height: {{Integer}}; VerticalAlign: Center; BackgroundColor: 200, 200, 200\">Hello World</Container>", instance);
      Assert.NotNull(component.Props.FirstOrDefault(x => x.Name == PropNames.Style));

      var style = component.Props.First(x => x.Name == PropNames.Style).DataGetter(null) as Style;
      Assert.Equal(Position.Absolute, style.Position);
      Assert.Equal(1f, style.WidthPercentage);
      Assert.Equal(5, style.Height);
      Assert.Equal(VerticalAlign.Center, style.VerticalAlign);
      Assert.Equal(new Color(200, 200, 200), style.BackgroundColor);

      // Re-apply style changes
      instance.Integer.Value = 10;
      component.Props.First(x => x.Name == PropNames.Style).DataGetter(new ReactiveScope(new Dictionary<string, object>() { { PropNames.Style, style } }));
      Assert.Equal(Position.Absolute, style.Position);
      Assert.Equal(1f, style.WidthPercentage);
      Assert.Equal(10, style.Height);
      Assert.Equal(VerticalAlign.Center, style.VerticalAlign);
      Assert.Equal(new Color(200, 200, 200), style.BackgroundColor);
    }

    [Fact]
    public void ScopeVariable()
    {
      var tree = Provider.ParseXML("<Container :HelloWorld='AppendWorld($Hello)' />", new Component());

      Assert.NotNull(tree.Props.FirstOrDefault(x => x.Name == "HelloWorld"));
      Assert.Equal("Hello World", tree.Props.First(x => x.Name == "HelloWorld").DataGetter(new ReactiveScope(new Dictionary<string, object>() { { "Hello", "Hello" } })));
    }

    [Fact]
    public void ForProp()
    {
      var instance = new Component();
      var tree = Provider.ParseXML("<Container for='var $item in Items' />", instance);

      Assert.NotNull(tree.For);
      Assert.True((tree.For.DataGetter(null) as List<string>).SequenceEqual(new List<string>() { "Hello World" }));

      instance.Items.Add("Add One More");
      Assert.True((tree.For.DataGetter(null) as List<string>).SequenceEqual(new List<string>() { "Hello World", "Add One More" }));
    }

    [Fact]
    public void RefProp()
    {
      var tree = Provider.ParseXML("<Container ref='HelloWorld' />", new Component());

      Assert.NotEmpty(tree.Refs);
      Assert.NotNull(tree.Refs.FirstOrDefault(x => x.PropName == "HelloWorld"));
    }

    [Fact]
    public void GlobalCheck()
    {
      var tree = Provider.ParseXML("<Container Global />", new Component());

      Assert.True(tree.IsGlobal);
    }

    public class Component : UIComponent
    {
      public readonly ReactiveProperty<int> Integer;
      public readonly ReactiveProperty<string> Str;
      public readonly ReactiveCollection<string> Items;

      public Component(int integer = 5, string str = "Test")
      {
        Integer = new ReactiveProperty<int>(integer);
        Str = new ReactiveProperty<string>(str);
        Items = new ReactiveCollection<string>("Hello World");
      }

      public bool OnSelect(SelectEvent evt, int integer)
      {
        return integer % 2 == 1;
      }

      public string AppendWorld(string prefix)
      {
        return $"{prefix} World";
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
