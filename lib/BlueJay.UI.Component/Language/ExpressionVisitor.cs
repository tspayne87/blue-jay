using System;
using System.Collections.Generic;
using BlueJay.UI.Component.Language.Antlr;
using System.Linq.Expressions;
using Antlr4.Runtime.Misc;
using System.Linq;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Language
{
  /// <summary>
  /// The expression visitor is meant to walk down and build out an expression result based on the expression context
  /// </summary>
  internal class ExpressionVisitor : ExpressionParserBaseVisitor<object>
  {
    /// <summary>
    /// The component instance we need to build lambda function out of
    /// </summary>
    private readonly UIComponent _intance;

    /// <summary>
    /// The parameter that will be used to access the reactive scope
    /// </summary>
    private readonly ParameterExpression _param;

    /// <summary>
    /// Constructor is meant to set defaults 
    /// </summary>
    /// <param name="instance"></param>
    public ExpressionVisitor(UIComponent instance)
    {
      _intance = instance;
      _param = Expression.Parameter(typeof(ReactiveScope), "x");
    }

    /// <summary>
    /// Visit the root expression to start building out the lambda function
    /// </summary>
    /// <param name="context">The current context of the root expression</param>
    /// <returns>Will return the expression result</returns>
    public override object VisitExpr([NotNull] ExpressionParser.ExprContext context)
    {
      if (context.exception != null)
        throw context.exception;

      var body = Visit(context.GetChild(0)) as BuilderExpression;
      var expression = Expression.Lambda<Func<ReactiveScope, object>>(Expression.Convert(body.Expression, typeof(object)), _param).Compile();
      return new ExpressionResult(expression, body.ScopePaths);
    }

    #region Literal Expressions
    /// <summary>
    /// Visitor for strings
    /// </summary>
    /// <param name="context">The string context</param>
    /// <returns>Will return a builder expression to creates expression pieces for the lambda function</returns>
    public override object VisitString([NotNull] ExpressionParser.StringContext context)
    {
      var str = context.GetText();
      return new BuilderExpression(Expression.Constant(str.Substring(1, str.Length - 2)));
    }

    /// <summary>
    /// Visitor for decimals
    /// </summary>
    /// <param name="context">The decimal context</param>
    /// <returns>Will return a builder expression to creates expression pieces for the lambda function</returns>
    public override object VisitDecimal([NotNull] ExpressionParser.DecimalContext context)
    {
      if (float.TryParse(context.GetText(), out var num))
        return new BuilderExpression(Expression.Constant(num));
      return null;
    }

    /// <summary>
    /// Visitor for integers
    /// </summary>
    /// <param name="context">The integer context</param>
    /// <returns>Will return a builder expression to creates expression pieces for the lambda function</returns>
    public override object VisitInteger([NotNull] ExpressionParser.IntegerContext context)
    {
      if (int.TryParse(context.GetText(), out var num))
        return new BuilderExpression(Expression.Constant(num));
      return null;
    }

    /// <summary>
    /// Visitor for booleans
    /// </summary>
    /// <param name="context">The boolean context</param>
    /// <returns>Will return a builder expression to creates expression pieces for the lambda function</returns>
    public override object VisitBoolean([NotNull] ExpressionParser.BooleanContext context)
    {
      if (bool.TryParse(context.GetText(), out var boolean))
        return new BuilderExpression(Expression.Constant(boolean));
      return null;
    }
    #endregion

    #region Identifier Expressions
    /// <summary>
    /// Visitor for an identifier that needs to look on the scope to get data from the component
    /// </summary>
    /// <param name="context">The identifier context</param>
    /// <returns>Will return a builder expression to creates expression pieces for the lambda function</returns>
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

    /// <summary>
    /// Visitor for a scope variable that will look onto the scope for a specific variable
    /// </summary>
    /// <param name="context">The scope context</param>
    /// <returns>Will return a builder expression to creates expression pieces for the lambda function</returns>
    public override object VisitScopeVarExpression([NotNull] ExpressionParser.ScopeVarExpressionContext context)
    {
      return new BuilderExpression(Expression.Property(_param, "Item", Expression.Constant(context.GetText().Substring(1))));
    }
    #endregion

    #region Functional Expressions
    /// <summary>
    /// Visitor for calling a function in the lambda expression
    /// </summary>
    /// <param name="context">The functional scope</param>
    /// <returns>Will return a builder expression to creates expression pieces for the lambda function</returns>
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

    /// <summary>
    /// Visitor argument expression to process arguments for a function so they can be called in the correct way
    /// </summary>
    /// <param name="context">The argument expression context</param>
    /// <returns>Will return a builder expression to creates expression pieces for the lambda function</returns>
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

    /// <summary>
    /// The builder expression class is meant to hold pieces of a lambda function that will get put together to create a lambda function
    /// </summary>
    private class BuilderExpression
    {
      /// <summary>
      /// The current part of the expression
      /// </summary>
      public Expression Expression { get; private set; }

      /// <summary>
      /// The paths that should be watched if this expression needs to be triggered again
      /// </summary>
      public List<string> ScopePaths { get; private set; }

      /// <summary>
      /// Constructor to build out an expression and paths
      /// </summary>
      /// <param name="expression">The current part of the expression</param>
      /// <param name="scopePaths">The paths that should be watched if this expression needs to be triggered again</param>
      public BuilderExpression(Expression expression, IEnumerable<string> scopePaths = null)
      {
        Expression = expression;
        ScopePaths = scopePaths?.ToList() ?? new List<string>();
      }
    }
  }
}
