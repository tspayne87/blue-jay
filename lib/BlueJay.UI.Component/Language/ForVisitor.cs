using System;
using BlueJay.UI.Component.Language.Antlr;
using Antlr4.Runtime.Misc;

namespace BlueJay.UI.Component.Language
{
  /// <summary>
  /// The for language to extract out the scope name and the expression to get the enumeration
  /// </summary>
  internal class ForVisitor : ForParserBaseVisitor<object>
  {
    /// <summary>
    /// The UI Component intance we need to use to generate the function to get the enumeration
    /// </summary>
    private readonly UIComponent _intance;

    /// <summary>
    /// The service provider to create the expression from
    /// </summary>
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    /// Constructor is meant to bootstrap the private variables
    /// </summary>
    /// <param name="serviceProvider">The UI Component intance we need to use to generate the function to get the enumeration</param>
    /// <param name="instance">The service provider to create the expression from</param>
    public ForVisitor(IServiceProvider serviceProvider, UIComponent instance)
    {
      _intance = instance;
      _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Visit root expression
    /// </summary>
    /// <param name="context">The expression context</param>
    /// <returns>Will return the element for object</returns>
    public override object VisitExpr([NotNull] ForParser.ExprContext context)
    {
      var name = context.GetChild(0).GetText();
      var expression = Visit(context.GetChild(context.ChildCount - 2)) as ExpressionResult;

      if (name == PropNames.Event)
        throw new ArgumentException("Cannot use the name event");

      return new ElementFor() { DataGetter = expression.Callback, ScopePaths = expression.ScopePaths, ScopeName = name };
    }

    /// <summary>
    /// Visit a name
    /// </summary>
    /// <param name="context">The name context</param>
    /// <returns>Will return the name of this scope context</returns>
    public override object VisitName([NotNull] ForParser.NameContext context)
    {
      return context.GetText().Substring(1);
    }

    /// <summary>
    /// Visit the expression
    /// </summary>
    /// <param name="context">The expression context</param>
    /// <returns>Will return the expression result to get the callback that should be used when getting the enumeration</returns>
    public override object VisitExpression([NotNull] ForParser.ExpressionContext context)
    {
      return _serviceProvider.ParseExpression(context.GetText(), _intance);
    }
  }
}
