using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using BlueJay.UI.Component.Common;
using System.Linq.Expressions;

namespace BlueJay.UI.Component.Language
{
  public class BlueJayXMLVisitor : BlueJayXMLBaseVisitor<object>
  {
    private List<Type> _globals = new List<Type>() { typeof(Container), typeof(Slot) };

    public readonly LanguageScope Scope;

    public BlueJayXMLVisitor(object instance)
    {
      Scope = new LanguageScope(instance);
    }

    public override object VisitElementExpression([NotNull] BlueJayXMLParser.ElementExpressionContext context)
    {
      if (context.children[1].GetText() != context.children[context.ChildCount - 2].GetText())
        return null; // TODO: Need to throw error or something since we cannot find cooresponding tag

      var elementName = context.children[1].GetText();

      var scope = new LanguageScope(null);

      var i = 2;
      for (; i < context.ChildCount && context.children[i].GetText() != ">"; ++i)
      {
        if (context.children[i].GetText().Trim().Length > 0)
        {
          var obj = Visit(context.children[i]);
          var (type, name, value) = obj as Tuple<AttributeType, string, object>;
          switch (type)
          {
            case AttributeType.Prop:
              scope.Props.Add(name, value);
              break;
          }
        }
      }

      return VisitChildren(context);
    }



    public override object VisitStringAttributeExpression([NotNull] BlueJayXMLParser.StringAttributeExpressionContext context)
    {
      var name = context.children[0].GetText();
      var value = context.children[context.ChildCount - 2].GetText();
      return new Tuple<AttributeType, string, object>(AttributeType.Prop, name, value);
    }

    public override object VisitBindingAttributeExpression([NotNull] BlueJayXMLParser.BindingAttributeExpressionContext context)
    {
      var name = context.children[0].GetText();
      var param = Expression.Parameter(typeof(object), "x");
      var body = Visit(context.children[context.ChildCount - 2]) as BuilderExpression;
      var expression = Expression.Lambda<Func<object, object>>(body.Expression, param).Compile();
      return new Tuple<AttributeType, string, object>(AttributeType.Binding, name, expression);
    }

    #region Literal Evaluators
    public override object VisitString([NotNull] BlueJayXMLParser.StringContext context)
    {
      var str = context.children[1].GetText();
      return new BuilderExpression(Expression.Constant(str), new List<object>() { str });
    }

    public override object VisitDecimal([NotNull] BlueJayXMLParser.DecimalContext context)
    {
      if (float.TryParse(context.GetText(), out var num))
        return new BuilderExpression(Expression.Constant(num), new List<object>() { num });
      return new BuilderExpression(Expression.Constant(null), null);
    }

    public override object VisitInteger([NotNull] BlueJayXMLParser.IntegerContext context)
    {
      if (int.TryParse(context.GetText(), out var num))
        return new BuilderExpression(Expression.Constant(num), new List<object>() { num });
      return new BuilderExpression(Expression.Constant(null), null);
    }

    public override object VisitBoolean([NotNull] BlueJayXMLParser.BooleanContext context)
    {
      if (bool.TryParse(context.GetText(), out var boolean))
        return new BuilderExpression(Expression.Constant(boolean), new List<object>() { boolean });
      return new BuilderExpression(Expression.Constant(null), null);
    }
    #endregion

    public override object VisitIdentifier([NotNull] BlueJayXMLParser.IdentifierContext context)
    {
      var propName = context.GetText();
      var prop = Scope.Instance.GetType().GetProperty(propName);
      if (prop != null)
      {
        var reactive = prop.GetValue(Scope.Instance) as IReactiveProperty;
        if (reactive != null)
        {

        }
        else
        {
          return new BuilderExpression(Expression.Property(Expression.Constant(Scope.Instance), propName), new List<object>() { prop.GetValue(Scope.Instance) });
        }
      }

      var field = Scope.Instance.GetType().GetField(propName);
      if (field != null)
      {

      }

      return new BuilderExpression(Expression.Constant(null), null);
    }

    private enum AttributeType
    {
      Prop, Event, Binding, Style, If, Foreach
    }

    private class BuilderExpression
    {
      public Expression Expression { get; private set; }
      public List<object> Data { get; private set; }

      public BuilderExpression(Expression expression, List<object> data)
      {
        Expression = expression;
        Data = data;
      }
    }
  }
}
