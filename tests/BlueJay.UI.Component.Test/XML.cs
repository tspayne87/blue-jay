using BlueJay.UI.Component.Language;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BlueJay.UI.Component.Test
{
  public class XML
  {
    [Fact]
    public void Single()
    {
      Language.Language.ParseXML("<Container />", new Component());
    }

    [Fact]
    public void InnerText()
    {
      Language.Language.ParseXML("<Container>Hello World</Container>", new Component());
    }

    [Fact]
    public void StringProp()
    {
      Language.Language.ParseXML("<Container prop1='Value 1'>Hello World</Container>", new Component());
    }

    [Fact]
    public void BindedLiteralProp()
    {
      Language.Language.ParseXML("<Container :prop1=\"'string'\">Hello World</Container>", new Component());
      Language.Language.ParseXML("<Container :prop1=\"15\">Hello World</Container>", new Component());
      Language.Language.ParseXML("<Container :prop1=\"15.0\">Hello World</Container>", new Component());
      Language.Language.ParseXML("<Container :prop1=\"true\">Hello World</Container>", new Component());
    }

    [Fact]
    public void BindedProp()
    {
      Language.Language.ParseXML("<Container :prop1=\"Integer\">Hello World</Container>", new Component());
    }

    [Fact]
    public void EventProp()
    {
      Language.Language.ParseXML("<Container @select=\"OnSelect($event)\">Hello World</Container>", new Component());
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
    }
  }
}
