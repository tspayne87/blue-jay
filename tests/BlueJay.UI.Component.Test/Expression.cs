using BlueJay.UI.Component.Language;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;

namespace BlueJay.UI.Component.Test
{
  public class Expression
  {
    [Fact]
    public void Basic()
    {
      var scopes = new List<ExpressionScope>() { GetScope() };

      Assert.Equal(30, ExpressionReader.Parse("Method(20)", scopes));
      Assert.Equal("Hello-World-5", ExpressionReader.Parse("Method2('Hello-World', Integer)", scopes));
    }

    /// <summary>
    /// Helper method is meant to create a basic scope based on the fake UIComponent below
    /// </summary>
    /// <returns>A generated scope to work with the expression reader</returns>
    private ExpressionScope GetScope()
    {
      var data = new Component();
      var props = new Dictionary<string, IReactiveProperty>()
      {
        { "String", data.String },
        { "Integer", data.Integer }
      };
      var functions = new Dictionary<string, MethodInfo>()
      {
        { "Method", data.GetType().GetMethod("Method") },
        { "Method2", data.GetType().GetMethod("Method2") }
      };

      return new ExpressionScope(data, props, functions);
    }

    [View("<container>Hello World</container>")]
    public class Component : UIComponent
    {
      public readonly ReactiveProperty<string> String;
      public readonly ReactiveProperty<int> Integer;

      public Component()
      {
        String = new ReactiveProperty<string>("Test");
        Integer = new ReactiveProperty<int>(5);
      }

      public int Method(int integer)
      {
        return integer + 10;
      }

      public string Method2(string str, int integer)
      {
        return $"{str}-{integer}";
      }
    }
  }
}
