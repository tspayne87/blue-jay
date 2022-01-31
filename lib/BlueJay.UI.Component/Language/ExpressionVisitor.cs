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
    /// Visitor meant to process and expression and build out an expression result of a callback function
    /// meant to give the result of the expressing that was just built to handle
    /// </summary>
    /// <param name="context">The full parsed expression that should be handled by the rest of the vistors recursivly</param>
    /// <returns>Will return the expression result based on what was generated</returns>
    public override object VisitParse([NotNull] ExpressionParser.ParseContext context)
    {
      if (context.exception != null)
        throw context.exception;

      var body = Visit(context.GetChild(0)) as BuilderExpression;
      var expression = Expression.Lambda<Func<ReactiveScope, object>>(Expression.Convert(body.Expression, typeof(object)), _param).Compile();
      return new ExpressionResult(expression, body.ScopePaths);
    }

    /// <summary>
    /// Visitor method to handle the parent expression to better represent what should be grouped with each other
    /// </summary>
    /// <param name="context">The current context being processed for the paren expression</param>
    /// <returns>Will return the paren expression for further processing</returns>
    public override object VisitParenExpression([NotNull] ExpressionParser.ParenExpressionContext context)
    {
      return Visit(context.expr);
    }

    /// <summary>
    /// Visitor method to handle processing the identifier and handling what this expression should be watching for changes on
    /// </summary>
    /// <param name="context">The identifier expression meant for processing the path of where the data exists</param>
    /// <returns>Will return an invocation of a property in the scope or on the context of the component this is bound to</returns>
    public override object VisitIdentifierExpression([NotNull] ExpressionParser.IdentifierExpressionContext context)
    {
      var path = context.GetText();
      Type type = null;
      if ((path.IndexOf('.') > -1 && _intance.GetType().IsPropertyOf(path.Split('.')[0])) || _intance.GetType().IsPropertyOf(path))
      {
        type = Utils.GetObject(_intance, path)?.GetType();
        path = $"{_intance.Identifier}.{path}";
      }

      Expression expr = Expression.Property(_param, "Item", Expression.Constant(path));
      if (type != null)
        expr = Expression.Convert(expr, type);
      return new BuilderExpression(expr, new List<string>() { path });
    }

    /// <summary>
    /// Visitor is meant to handle arithmetic expressions and try to convert that expressions over to the correct
    /// types to handle a little of loosely typed expressions
    /// </summary>
    /// <param name="context">Arithmetic expression context to handle processing the left/right</param>
    /// <returns>Will return an arithmetic expression meant for further processing</returns>
    public override object VisitArithmeticExpression([NotNull] ExpressionParser.ArithmeticExpressionContext context)
    {
      var left = Visit(context.left) as BuilderExpression;
      var right = Visit(context.right) as BuilderExpression;

      var leftExpression = left.Expression;
      var rightExpression = right.Expression;
      if (leftExpression.Type == typeof(object) && rightExpression.Type == typeof(object))
        return new BuilderExpression(_param); /// TODO: Need to send error for this
      else if (leftExpression.Type == typeof(object))
        leftExpression = Expression.Convert(leftExpression, rightExpression.Type);
      else if (rightExpression.Type == typeof(object))
        rightExpression = Expression.Convert(rightExpression, leftExpression.Type);

      if (leftExpression.Type == typeof(string) && rightExpression.Type != typeof(string))
        rightExpression = Expression.Call(rightExpression, "ToString", Type.EmptyTypes);
      if (rightExpression.Type == typeof(string) && leftExpression.Type != typeof(string))
        leftExpression = Expression.Call(leftExpression, "ToString", Type.EmptyTypes);

      Expression exp = null;
      switch (context.op.Start.Type)
      {
        case ExpressionLexer.PLUS:
          if (leftExpression.Type == typeof(string) && rightExpression.Type == typeof(string))
            exp = Expression.Add(leftExpression, rightExpression, typeof(string).GetMethod("Concat", new[] { leftExpression.Type, rightExpression.Type }));
          else
            exp = Expression.Add(leftExpression, rightExpression);
          break;
        case ExpressionLexer.MINUS:
          exp = Expression.Subtract(leftExpression, rightExpression);
          break;
        case ExpressionLexer.TIMES:
          exp = Expression.Multiply(leftExpression, rightExpression);
          break;
        case ExpressionLexer.DIVIDE:
          exp = Expression.Divide(leftExpression, rightExpression);
          break;
        case ExpressionLexer.MOD:
          exp = Expression.Modulo(leftExpression, rightExpression);
          break;
      }

      if (exp != null)
        return new BuilderExpression(exp, left.ScopePaths.Concat(right.ScopePaths));

      return new BuilderExpression(_param); /// TODO: Need to send error for this
    }

    #region Logical Operators
    /// <summary>
    /// Visitor meant to handle comparator expression for basic boolean logic
    /// </summary>
    /// <param name="context">The comparator conext that should be parsed</param>
    /// <returns>Will create a comparator expression for further processing</returns>
    public override object VisitComparatorExpression([NotNull] ExpressionParser.ComparatorExpressionContext context)
    {
      var left = Visit(context.left) as BuilderExpression;
      var right = Visit(context.right) as BuilderExpression;

      var leftExpression = left.Expression;
      var rightExpression = right.Expression;

      Expression exp = null;
      switch (context.op.Start.Type)
      {
        case ExpressionLexer.GT:
          exp = Expression.GreaterThan(leftExpression, rightExpression);
          break;
        case ExpressionLexer.GTE:
          exp = Expression.GreaterThanOrEqual(leftExpression, rightExpression);
          break;
        case ExpressionLexer.LT:
          exp = Expression.LessThan(leftExpression, rightExpression);
          break;
        case ExpressionLexer.LTE:
          exp = Expression.LessThanOrEqual(leftExpression, rightExpression);
          break;
        case ExpressionLexer.EQ:
          exp = Expression.Equal(leftExpression, rightExpression);
          break;
      }

      if (exp != null)
        return new BuilderExpression(exp, left.ScopePaths.Concat(right.ScopePaths));

      return new BuilderExpression(_param); /// TODO: Need to send error for this
    }

    /// <summary>
    /// Visitor meant to handle binary expressions first a foremost, everything will go through this expression
    /// so that all processing can happen correctly
    /// </summary>
    /// <param name="context">The binary expression</param>
    /// <returns>Returns a processed binary expression for further processing</returns>
    public override object VisitBinaryExpression([NotNull] ExpressionParser.BinaryExpressionContext context)
    {
      var left = Visit(context.left) as BuilderExpression;
      if (context.right == null)
        return left;

      var right = Visit(context.right) as BuilderExpression;

      var leftExpression = left.Expression;
      var rightExpression = right.Expression;

      Expression exp = null;
      switch (context.op.Start.Type)
      {
        case ExpressionLexer.AND:
          exp = Expression.And(leftExpression, rightExpression);
          break;
        case ExpressionLexer.OR:
          exp = Expression.Or(leftExpression, rightExpression);
          break;
      }
      if (exp != null)
        return new BuilderExpression(exp, left.ScopePaths.Concat(right.ScopePaths));

      return new BuilderExpression(_param); /// TODO: Need to send error for this
    }

    /// <summary>
    /// Visitor to handle the not expression
    /// </summary>
    /// <param name="context">The not expression context that should be parsed and processed</param>
    /// <returns>Will return the not expression meant for further processing</returns>
    public override object VisitNotExpression([NotNull] ExpressionParser.NotExpressionContext context)
    {
      var builder = Visit(context.expr) as BuilderExpression;
      if (builder != null)
      {
        return new BuilderExpression(Expression.Not(builder.Expression), builder.ScopePaths);
      }
      return new BuilderExpression(_param); /// TODO: Need to send error for this
    }
    #endregion

    #region String Expressions
    /// <summary>
    /// Visitor is meant to handle the basics of a string and concat all the other processes for the string into
    /// one string that should be used
    /// </summary>
    /// <param name="context">The current context where the string expression lives</param>
    /// <returns>Will return and expression for the string that should be processed further</returns>
    public override object VisitStringExpression([NotNull] ExpressionParser.StringExpressionContext context)
    {

      Expression expr = Expression.Constant(string.Empty);
      var scopes = new List<string>();
      for (var i = 1; i < context.ChildCount - 1; ++i)
      {
        var item = Visit(context.GetChild(i)) as BuilderExpression;
        if (item != null)
        {
          expr = Expression.Add(expr, item.Expression, typeof(string).GetMethod("Concat", new[] { expr.Type, item.Expression.Type }));
          scopes.AddRange(item.ScopePaths);
        }
      }
      return new BuilderExpression(expr, scopes);
    }

    /// <summary>
    /// Visitor meant to handle an interp string that is meant to handle creating dynamic string easier
    /// </summary>
    /// <param name="context">The expression that should be processed into a string</param>
    /// <returns>Will return an expression that converts itself into a string</returns>
    public override object VisitStringInterpExpression([NotNull] ExpressionParser.StringInterpExpressionContext context)
    {
      var builder = Visit(context.expr) as BuilderExpression;
      if (builder != null)
      {
        var expr = builder.Expression;
        if (expr.Type != typeof(string))
          expr = Expression.Call(expr, "ToString", Type.EmptyTypes);

        return new BuilderExpression(expr, builder.ScopePaths);
      }
      return new BuilderExpression(_param); /// TODO: Need to send error for this
    }

    #endregion

    #region Functional Expressions
    /// <summary>
    /// Visitor is meant to process a invoking method and translates it to what needs to be called and what conversions need to happen to
    /// get that working properly
    /// </summary>
    /// <param name="context">The current context of the invoking method</param>
    /// <returns>Will return in invocation method expression for further processing</returns>
    public override object VisitInvokeMethodExpression([NotNull] ExpressionParser.InvokeMethodExpressionContext context)
    {
      var method = _intance.GetType().GetMethod(context.method.Text);
      if (method != null)
      {
        var scopes = new List<string>();
        var args = new List<Expression>();
        if (context.ChildCount > 3)
        { // Only handle arguments if we have more children then 3
          for (var i = 2; i < context.ChildCount; i += 2)
          {
            var builderExpr = Visit(context.GetChild(i)) as BuilderExpression;
            var expr = builderExpr.Expression;
            if (expr.Type == typeof(object))
              expr = Expression.Convert(expr, method.GetParameters()[args.Count].ParameterType);

            args.Add(expr);
            scopes.AddRange(builderExpr.ScopePaths);
          }
        }

        return new BuilderExpression(Expression.Call(Expression.Constant(_intance), method, args), scopes);
      }
      return new BuilderExpression(_param); /// TODO: Need to send error for this
    }
    #endregion

    #region Static Expression
    /// <summary>
    /// Visitor is meant to handle the basic string details and treat them as a basic string of what was typed
    /// </summary>
    /// <param name="context">The context for the static string</param>
    /// <returns>Will return an expression of the string being processed</returns>
    public override object VisitStringDetailsExpression([NotNull] ExpressionParser.StringDetailsExpressionContext context)
    {
      return new BuilderExpression(Expression.Constant(context.details.Text));
    }

    /// <summary>
    /// Visitor is meant to handle the static escaped string characters that could be escaped inside a string
    /// </summary>
    /// <param name="context">The current escaped string values</param>
    /// <returns>Will return an expression for the esacped character</returns>
    public override object VisitStringEscapeExpression([NotNull] ExpressionParser.StringEscapeExpressionContext context)
    {
      return new BuilderExpression(Expression.Constant(context.escape.Text.Substring(1)));
    }

    /// <summary>
    /// Visitor to handle parsing the numeric expression, this could be an integer expression or a floating expression
    /// </summary>
    /// <param name="context">The context for the numeric expression</param>
    /// <returns>Will return a numeric expression for further processing</returns>
    public override object VisitNumericExpression([NotNull] ExpressionParser.NumericExpressionContext context)
    {
      if (int.TryParse(context.num.Text, out var numInt))
        return new BuilderExpression(Expression.Constant(numInt));
      if (float.TryParse(context.num.Text, out var numFloat))
        return new BuilderExpression(Expression.Constant(numFloat));
      return new BuilderExpression(_param); /// TODO: Need to send error for this
    }

    /// <summary>
    /// Visitor to handle the boolean expression
    /// </summary>
    /// <param name="context">The context for the boolean expression</param>
    /// <returns>Will return a boolean expression for further processing</returns>
    public override object VisitBoolExpression([NotNull] ExpressionParser.BoolExpressionContext context)
    {
      if (bool.TryParse(context.expr.GetText(), out var val))
        return new BuilderExpression(Expression.Constant(val));
      return new BuilderExpression(_param); /// TODO: Need to send error for this
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
