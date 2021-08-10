// using BlueJay.UI.Component.Language;
// using System;
// using System.Collections.Generic;
// using System.Reflection;
// using Xunit;

// namespace BlueJay.UI.Component.Test
// {
//   public class Expression
//   {
//     [Fact]
//     public void Basic()
//     {
//       var scopes = new List<LanguageScope>() { new Component("Test", 5).GenerateScope() };

//       Assert.Equal(30, Language.Language.ParseExpression("Method(20)", scopes).Item1);
//       Assert.Equal("Hello-World-5", Language.Language.ParseExpression("Method2('Hello-World', Integer)", scopes).Item1);
//     }

//     [Fact]
//     public void MultiScope()
//     {
//       var scopes = new List<LanguageScope>() { new Component("Test", 5).GenerateScope(), new Component2(1f, false).GenerateScope() };

//       Assert.Equal(30, Language.Language.ParseExpression("Method(20)", scopes).Item1);
//       Assert.Equal("Hello-World-5", Language.Language.ParseExpression("Method2('Hello-World', Integer)", scopes).Item1);
//       Assert.Equal(1f, Language.Language.ParseExpression("Float", scopes).Item1);
//     }

//     [Fact]
//     public void Literals()
//     {
//       var scopes = new List<LanguageScope>();

//       Assert.Equal("Hello World", Language.Language.ParseExpression("'Hello World'", scopes).Item1);
//       Assert.Equal(1.5f, Language.Language.ParseExpression("1.5", scopes).Item1);
//       Assert.Equal(20, Language.Language.ParseExpression("20", scopes).Item1);
//       Assert.True((bool)Language.Language.ParseExpression("true", scopes).Item1);
//       Assert.False((bool)Language.Language.ParseExpression("false", scopes).Item1);
//     }

//     [Fact]
//     public void Ternary()
//     {
//       var scopes = new List<LanguageScope>() { new Component("Test", 5).GenerateScope(), new Component2(1.5f, false).GenerateScope() };

//       Assert.Equal(1.5f, Language.Language.ParseExpression("true ? Float : String", scopes).Item1);
//       Assert.Equal("Test", Language.Language.ParseExpression("false ? Float : String", scopes).Item1);
//       Assert.Equal(5, Language.Language.ParseExpression("Bool ? Float : Integer", scopes).Item1);
//     }

//     [Fact]
//     public void ContextVar()
//     {
//       var scopes = new List<LanguageScope>() { new Component("Test", 5).GenerateScope(), new SelectEvent().AsEventScope() };

//       Assert.True((bool)Language.Language.ParseExpression("OnSelect($event)", scopes).Item1);
//     }

//     [View("<container>Hello World</container>")]
//     public class Component : UIComponent
//     {
//       public readonly ReactiveProperty<string> String;
//       public readonly ReactiveProperty<int> Integer;

//       public Component(string str, int integer)
//       {
//         String = new ReactiveProperty<string>(str);
//         Integer = new ReactiveProperty<int>(integer);
//       }

//       public int Method(int integer)
//       {
//         return integer + 10;
//       }

//       public string Method2(string str, int integer)
//       {
//         return $"{str}-{integer}";
//       }

//       public bool OnSelect(SelectEvent evt)
//       {
//         return true;
//       }
//     }

//     [View("<container>Hello World</container>")]
//     public class Component2 : UIComponent
//     {
//       public readonly ReactiveProperty<float> Float;
//       public readonly ReactiveProperty<bool> Bool;

//       public Component2(float num, bool boolean)
//       {
//         Float = new ReactiveProperty<float>(num);
//         Bool = new ReactiveProperty<bool>(boolean);
//       }
//     }
//   }
// }
