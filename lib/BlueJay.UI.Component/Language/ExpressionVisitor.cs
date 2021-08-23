using System;
using System.Collections.Generic;
using BlueJay.UI.Component.Language.Antlr;
using System.Text;
using System.Linq.Expressions;
using Antlr4.Runtime.Misc;
using System.Reflection;
using System.Linq;

namespace BlueJay.UI.Component.Language
{
  public class ExpressionVisitor : ExpressionParserBaseVisitor<object>
  {
    private readonly UIComponent _intance;
    private ParameterExpression _param;

    public ExpressionVisitor(UIComponent instance)
    {
      _intance = instance;
    }

    public override object VisitExpr([NotNull] ExpressionParser.ExprContext context)
    {
      if (context.exception != null)
        throw context.exception;

      _param = Expression.Parameter(typeof(object), "x");
      var body = Visit(context.GetChild(0)) as BuilderExpression;
      var expression = Expression.Lambda<Func<object, object>>(Expression.Convert(body.Expression, typeof(object)), _param).Compile();
      return new ExpressionResult(expression, body.ReactiveItems);
    }

    #region Literal Expressions
    public override object VisitString([NotNull] ExpressionParser.StringContext context)
    {
      var str = context.GetText();
      return new BuilderExpression(Expression.Constant(str.Substring(1, str.Length - 2)));
    }

    public override object VisitDecimal([NotNull] ExpressionParser.DecimalContext context)
    {
      if (float.TryParse(context.GetText(), out var num))
        return new BuilderExpression(Expression.Constant(num));
      return null;
    }

    public override object VisitInteger([NotNull] ExpressionParser.IntegerContext context)
    {
      if (int.TryParse(context.GetText(), out var num))
        return new BuilderExpression(Expression.Constant(num));
      return null;
    }

    public override object VisitBoolean([NotNull] ExpressionParser.BooleanContext context)
    {
      if (bool.TryParse(context.GetText(), out var boolean))
        return new BuilderExpression(Expression.Constant(boolean));
      return null;
    }
    #endregion

    #region Identifier Expressions
    public override object VisitIdentifier([NotNull] ExpressionParser.IdentifierContext context)
    {
      var propName = context.GetText();
      var member = _intance.GetType().GetMember(propName)?[0];
      if (member != null)
      {
        var obj = member is FieldInfo ? ((FieldInfo)member).GetValue(_intance) : ((PropertyInfo)member).GetValue(_intance);
        var expression = Expression.PropertyOrField(Expression.Constant(_intance), propName);

        var reactiveProps = new List<IReactiveProperty>();
        if (obj is IReactiveProperty)
        {
          expression = Expression.PropertyOrField(expression, "Value");
          reactiveProps.Add(obj as IReactiveProperty);
        }

        return new BuilderExpression(expression, reactiveProps);
      }

      return new BuilderExpression(Expression.Constant(null), null);
    }

    public override object VisitContextVarExpression([NotNull] ExpressionParser.ContextVarExpressionContext context)
    {
      return new BuilderExpression(_param);
    }
    #endregion

    #region Functional Expressions
    public override object VisitFunctionExpression([NotNull] ExpressionParser.FunctionExpressionContext context)
    {
      var methodName = context.children[0].GetText();
      var props = context.ChildCount == 4 ? Visit(context.children[2]) as List<BuilderExpression> : new List<BuilderExpression>();

      var method = _intance.GetType().GetMethod(methodName);
      if (method != null && props != null)
      {
        var args = props.Select((x, i) =>
        {
          if (x.Expression == _param) return Expression.Convert(x.Expression, method.GetParameters()[i].ParameterType);
          return x.Expression;
        });
        return new BuilderExpression(Expression.Call(Expression.Constant(_intance), method, args), props.SelectMany(x => x.ReactiveItems));
      }
      return new BuilderExpression(Expression.Constant(null), null);
    }

    public override object VisitArgumentExpression([NotNull] ExpressionParser.ArgumentExpressionContext context)
    {
      var props = new List<BuilderExpression>();
      for (var i = 0; i < context.ChildCount; ++i)
      {
        var str = context.children[i].GetText();
        if (str != "," && str.Trim().Length > 0)
        {
          props.Add(Visit(context.children[i]) as BuilderExpression);
        }
      }
      return props;
    }
    #endregion

    private class BuilderExpression
    {
      public Expression Expression { get; private set; }
      public List<IReactiveProperty> ReactiveItems { get; private set; }

      public BuilderExpression(Expression expression, IEnumerable<IReactiveProperty> reactiveItems = null)
      {
        Expression = expression;
        ReactiveItems = reactiveItems?.ToList() ?? new List<IReactiveProperty>();
      }
    }
  }
}
