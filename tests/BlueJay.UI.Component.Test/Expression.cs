using BlueJay.UI.Component.Reactivity;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace BlueJay.UI.Component.Test
{
  public class Expression
  {
    public IServiceProvider Provider => new ServiceCollection().BuildServiceProvider();

    [Fact]
    public void StaticPropertyExpression()
    {
      var component = new Component(staticInt: 15);
      var staticInteger = Provider.GenerateExpression("StaticInteger", component);
      Assert.Equal(15, staticInteger(component.GenerateScope()));
    }

    [Fact]
    public void ReactivePropertyExpression()
    {
      var component = new Component(integer: 15);
      var integer = Provider.GenerateExpression("Integer", component);
      Assert.Equal(15, integer(component.GenerateScope()));

      component.Integer.Value = 25;
      Assert.Equal(25, integer(component.GenerateScope()));
    }

    [Fact]
    public void ScopePropertyExpression()
    {
      var scope = Provider.GenerateExpression("Hello", new Component());
      Assert.Equal("World", scope(new ReactiveScope() { { "Hello", "World" } }));
    }

    [Fact]
    public void BasicExpression()
    {
      var component = new Component(integer: 15, str: "Hello World");
      var basic = Provider.GenerateExpression("'{Str}.{Integer}'", component);
      Assert.Equal("Hello World.15", basic(component.GenerateScope()));

      component.Integer.Value = 25;
      Assert.Equal("Hello World.25", basic(component.GenerateScope()));

      component.Str.Value = "Test Hello World";
      Assert.Equal("Test Hello World.25", basic(component.GenerateScope()));
    }

    [Fact]
    public void NestedExpression()
    {
      var component = new Component(integer: 15, staticInt: 25);
      var nested = Provider.GenerateExpression("Integer + Nested.SecondInteger", component);
      Assert.Equal(20, nested(component.GenerateScope()));

      nested = Provider.GenerateExpression("StaticInteger + Nested.SecondInteger", component);
      Assert.Equal(30, nested(component.GenerateScope()));
    }

    [Fact]
    public void ScopeExpression()
    {
      var component = new Component(str: "Hello World");
      component.Scope = new ReactiveScope() { { "Hello", "World" } };
      var scope = Provider.GenerateExpression("'{Str}.{Hello}'", component);
      Assert.Equal("Hello World.World", scope(component.GenerateScope()));

      component.Str.Value = "Test Hello World";
      Assert.Equal("Test Hello World.World", scope(component.GenerateScope()));
    }

    [Fact]
    public void NumericExpression()
    {
      var method = Provider.GenerateExpression("-12", new Component());
      Assert.Equal(-12, method(new ReactiveScope()));

      method = Provider.GenerateExpression("12", new Component());
      Assert.Equal(12, method(new ReactiveScope()));

      method = Provider.GenerateExpression("12.00", new Component());
      Assert.Equal(12f, method(new ReactiveScope()));

      method = Provider.GenerateExpression("-12.00", new Component());
      Assert.Equal(-12f, method(new ReactiveScope()));
    }

    [Fact]
    public void StringExpression()
    {
      var method = Provider.GenerateExpression("'\\''", new Component());
      Assert.Equal("'", method(new ReactiveScope()));

      method = Provider.GenerateExpression("'\\\\'", new Component());
      Assert.Equal("\\", method(new ReactiveScope()));

      method = Provider.GenerateExpression("'\\}'", new Component());
      Assert.Equal("}", method(new ReactiveScope()));

      method = Provider.GenerateExpression("'\\{'", new Component());
      Assert.Equal("{", method(new ReactiveScope()));

      method = Provider.GenerateExpression("'\\\\\\{\\}\\''", new Component());
      Assert.Equal("\\{}'", method(new ReactiveScope()));

      method = Provider.GenerateExpression("'Integer + StaticInteger'", new Component());
      Assert.Equal("Integer + StaticInteger", method(new ReactiveScope()));
    }

    [Fact]
    public void BoolExpression()
    {
      var method = Provider.GenerateExpression("true", new Component());
      Assert.True((bool)method(new ReactiveScope()));

      method = Provider.GenerateExpression("false", new Component());
      Assert.False((bool)method(new ReactiveScope()));
    }

    [Fact]
    public void ComplexExpression()
    {
      var component = new Component(integer: 15, staticInt: 19);
      component.Scope = new ReactiveScope() { { "evt", new SelectEvent() } };
      var complex = Provider.GenerateExpression("Integer > StaticInteger && (false || CheckEvent(evt, StaticInteger))", component);
      Assert.False((bool)complex(component.GenerateScope()));

      component.Integer.Value = 25;
      Assert.True((bool)complex(component.GenerateScope()));
    }

    [Fact]
    public void ArithmeticExpression()
    {
      var component = new Component(integer: 15, staticInt: 10);
      var addArithmetic = Provider.GenerateExpression("Integer + StaticInteger", component);
      var minusArithmetic = Provider.GenerateExpression("Integer - StaticInteger", component);
      var multiplyArithmetic = Provider.GenerateExpression("Integer * StaticInteger", component);
      var divideArithmetic = Provider.GenerateExpression("Integer / StaticInteger", component);
      var modArithmetic = Provider.GenerateExpression("Integer % StaticInteger", component);
      Assert.Equal(25, addArithmetic(component.GenerateScope()));
      Assert.Equal(5, minusArithmetic(component.GenerateScope()));
      Assert.Equal(150, multiplyArithmetic(component.GenerateScope()));
      Assert.Equal(1, divideArithmetic(component.GenerateScope()));
      Assert.Equal(5, modArithmetic(component.GenerateScope()));

      component.Integer.Value = 25;
      Assert.Equal(35, addArithmetic(component.GenerateScope()));
      Assert.Equal(15, minusArithmetic(component.GenerateScope()));
      Assert.Equal(250, multiplyArithmetic(component.GenerateScope()));
      Assert.Equal(2, divideArithmetic(component.GenerateScope()));
      Assert.Equal(5, modArithmetic(component.GenerateScope()));

      component.StaticInteger = 25;
      Assert.Equal(50, addArithmetic(component.GenerateScope()));
      Assert.Equal(0, minusArithmetic(component.GenerateScope()));
      Assert.Equal(625, multiplyArithmetic(component.GenerateScope()));
      Assert.Equal(1, divideArithmetic(component.GenerateScope()));
      Assert.Equal(0, modArithmetic(component.GenerateScope()));
    }

    [Fact]
    public void ComparatorExpression()
    {
      var component = new Component(integer: 15, staticInt: 10);
      var gt = Provider.GenerateExpression("Integer > StaticInteger", component);
      var gte = Provider.GenerateExpression("Integer >= StaticInteger", component);
      var lt = Provider.GenerateExpression("Integer < StaticInteger", component);
      var lte = Provider.GenerateExpression("Integer <= StaticInteger", component);
      var eq = Provider.GenerateExpression("Integer == StaticInteger", component);

      Assert.True((bool)gt(component.GenerateScope()));
      Assert.True((bool)gte(component.GenerateScope()));
      Assert.False((bool)lt(component.GenerateScope()));
      Assert.False((bool)lte(component.GenerateScope()));
      Assert.False((bool)eq(component.GenerateScope()));

      component.Integer.Value = 25;
      Assert.True((bool)gt(component.GenerateScope()));
      Assert.True((bool)gte(component.GenerateScope()));
      Assert.False((bool)lt(component.GenerateScope()));
      Assert.False((bool)lte(component.GenerateScope()));
      Assert.False((bool)eq(component.GenerateScope()));

      component.StaticInteger = 25;
      Assert.False((bool)gt(component.GenerateScope()));
      Assert.True((bool)gte(component.GenerateScope()));
      Assert.False((bool)lt(component.GenerateScope()));
      Assert.True((bool)lte(component.GenerateScope()));
      Assert.True((bool)eq(component.GenerateScope()));
    }

    [Fact]
    public void BinaryExpression()
    {
      var component = new Component(integer: 15, staticInt: 10);
      var andExpr = Provider.GenerateExpression("Integer >= StaticInteger && Integer <= StaticInteger", component);
      var orExpr = Provider.GenerateExpression("Integer < StaticInteger || Integer > StaticInteger", component);

      Assert.False((bool)andExpr(component.GenerateScope()));
      Assert.True((bool)orExpr(component.GenerateScope()));

      component.Integer.Value = 25;
      Assert.False((bool)andExpr(component.GenerateScope()));
      Assert.True((bool)orExpr(component.GenerateScope()));

      component.StaticInteger = 25;
      Assert.True((bool)andExpr(component.GenerateScope()));
      Assert.False((bool)orExpr(component.GenerateScope()));
    }

    [Fact]
    public void NotExpression()
    {
      var component = new Component(integer: 15, staticInt: 10);
      var notExpr = Provider.GenerateExpression("!(Integer >= StaticInteger)", component);

      Assert.False((bool)notExpr(component.GenerateScope()));

      component.Integer.Value = 25;
      Assert.False((bool)notExpr(component.GenerateScope()));

      component.StaticInteger = 35;
      Assert.True((bool)notExpr(component.GenerateScope()));
    }

    [Fact]
    public void ScopeArithmeticExpression()
    {
      var component = new Component(integer: 15);
      component.Scope = new ReactiveScope() { { "Test", 15 } };
      var scope = Provider.GenerateExpression("Integer + Test", component);
      Assert.Equal(30, scope(component.GenerateScope()));

      component.Integer.Value = 25;
      Assert.Equal(40, scope(component.GenerateScope()));
    }

    [Fact]
    public void ComplexScopeArithmeticExpression()
    {
      var component = new Component(integer: 15, staticInt: 2);
      component.Scope = new ReactiveScope() { { "Test", 15 } };
      var scope = Provider.GenerateExpression("(Integer * StaticInteger) + Test", component);
      Assert.Equal(45, scope(component.GenerateScope()));

      component.Integer.Value = 25;
      Assert.Equal(65, scope(component.GenerateScope()));
    }

    [Fact]
    public void EmptyMethodExpression()
    {
      var method = Provider.GenerateExpression("EmptyArgs()", new Component());
      Assert.Equal("Hello", method(new ReactiveScope()));

      method = Provider.GenerateExpression("AppendWorld(EmptyArgs())", new Component());
      Assert.Equal("Hello World", method(new ReactiveScope()));
    }

    [Fact]
    public void InvokeMethodExpression()
    {
      var component = new Component(staticInt: 20);
      component.Scope = new ReactiveScope() { { "evt", new SelectEvent() } };
      var method = Provider.GenerateExpression("CheckEvent(evt, StaticInteger)", component);
      Assert.False((bool)method(component.GenerateScope()));

      component.StaticInteger = 13;
      Assert.True((bool)method(component.GenerateScope()));
    }

    [Fact]
    public void ScopeInvokeMethodExpression()
    {
      var component = new Component(staticInt: 20);
      component.Scope = new ReactiveScope() { { "evt", new SelectEvent() }, { "Test", 15 } };
      var method = Provider.GenerateExpression("AppendWorld('{StaticInteger + Test}')", component);
      Assert.Equal("35 World", method(component.GenerateScope()));

      component.StaticInteger = 13;
      Assert.Equal("28 World", method(component.GenerateScope()));
    }

    public class Nested
    {
      public readonly ReactiveProperty<int> SecondInteger;

      public Nested(int integer = 5)
      {
        SecondInteger = new ReactiveProperty<int>(integer);
      }
    }

    public class Component : UIComponent
    {
      public readonly ReactiveProperty<int> Integer;
      public readonly ReactiveProperty<string> Str;
      public readonly ReactiveCollection<string> Items;
      public readonly ReactiveProperty<Nested> Nested;

      public int StaticInteger;
      public Nested StaticNested;

      public Component(int integer = 5, string str = "Test", int staticInt = 10)
      {
        Integer = new ReactiveProperty<int>(integer);
        Str = new ReactiveProperty<string>(str);
        Items = new ReactiveCollection<string>();
        Nested = new ReactiveProperty<Nested>(new Nested());

        StaticInteger = staticInt;
        StaticNested = new Nested();
      }

      public bool CheckEvent(SelectEvent evt, int integer)
      {
        return integer % 2 == 1;
      }

      public string AppendWorld(string prefix)
      {
        return $"{prefix} World";
      }

      public string EmptyArgs()
      {
        return "Hello";
      }
    }
  }
}
