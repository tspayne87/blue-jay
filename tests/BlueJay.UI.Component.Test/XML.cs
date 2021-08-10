using BlueJay.UI.Component.Language;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Reactive.Subjects;

namespace BlueJay.UI.Component.Test
{
  public class XML
  {
    [Fact]
    public void Single()
    {
      Language.Language.ParseXML("<container />", new Component());
    }

    [Fact]
    public void InnerText()
    {
      Language.Language.ParseXML("<container>Hello World</container>", new Component());
    }

    [Fact]
    public void StringProp()
    {
      Language.Language.ParseXML("<container    prop1='Value 1'>Hello World</container>", new Component());
    }

    [Fact]
    public void BindedProp()
    {
      Language.Language.ParseXML("<container :prop1=\"Prop\">Hello World</container>", new Component());
    }

    [Fact]
    public void EventProp()
    {
      Language.Language.ParseXML("<container @select=\"OnSelect($event)\">Hello World</container>", new Component());
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
