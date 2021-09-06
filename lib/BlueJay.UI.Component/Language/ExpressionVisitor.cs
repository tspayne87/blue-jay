using System;
using System.Collections.Generic;
using BlueJay.UI.Component.Language.Antlr;
using System.Text;
using System.Linq.Expressions;
using Antlr4.Runtime.Misc;
using System.Reflection;
using System.Linq;
using BlueJay.UI.Component.Reactivity;

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

      _param = Expression.Parameter(typeof(ReactiveScope), "x");
      var body = Visit(context.GetChild(0)) as BuilderExpression;
      var expression = Expression.Lambda<Func<ReactiveScope, object>>(Expression.Convert(body.Expression, typeof(object)), _param).Compile();
      return new ExpressionResult(expression, body.ScopePaths);
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
      Expression expression = Expression.Property(_param, "Item", Expression.Constant(_intance.Identifier));
      expression = Expression.Convert(expression, _intance.GetType());
      expression = Expression.PropertyOrField(expression, propName);
      if (typeof(IReactiveProperty).IsAssignableFrom(expression.Type))
        expression = Expression.PropertyOrField(expression, "Value");

      return new BuilderExpression(expression, new List<string>() { $"{_intance.Identifier}.{propName}" });
    }

    public override object VisitScopeVarExpression([NotNull] ExpressionParser.ScopeVarExpressionContext context)
    {
      return new BuilderExpression(Expression.Property(_param, "Item", Expression.Constant(context.GetText().Substring(1))));
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
          if (method.GetParameters()[i].ParameterType == typeof(object)) return x.Expression;
          if (x.Expression.Type == typeof(object)) return Expression.Convert(x.Expression, method.GetParameters()[i].ParameterType);
          return x.Expression;
        });
        return new BuilderExpression(Expression.Call(Expression.Constant(_intance), method, args), props.SelectMany(x => x.ScopePaths));
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
      public List<string> ScopePaths { get; private set; }

      public BuilderExpression(Expression expression, IEnumerable<string> scopePaths = null)
      {
        Expression = expression;
        ScopePaths = scopePaths?.ToList() ?? new List<string>();
      }
    }
  }
}
