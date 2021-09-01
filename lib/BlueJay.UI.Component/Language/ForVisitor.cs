using System;
using BlueJay.UI.Component.Language.Antlr;
using System.Collections.Generic;
using System.Text;
using Antlr4.Runtime.Misc;

namespace BlueJay.UI.Component.Language
{
  public class ForVisitor : ForParserBaseVisitor<object>
  {
    private readonly UIComponent _intance;
    private readonly IServiceProvider _serviceProvider;

    public ForVisitor(IServiceProvider serviceProvider, UIComponent instance)
    {
      _intance = instance;
      _serviceProvider = serviceProvider;
    }

    public override object VisitExpr([NotNull] ForParser.ExprContext context)
    {
      var name = (Visit(context.GetChild(1)) as string).Substring(1);
      var expression = Visit(context.GetChild(context.ChildCount - 2)) as ExpressionResult;

      if (name == "event")
        throw new ArgumentException("Cannot use the name event");

      return new ElementFor() { DataGetter = expression.Callback, ScopePaths = expression.ScopePaths, ScopeName = name };
    }

    public override object VisitName([NotNull] ForParser.NameContext context)
    {
      return context.GetText().Substring(1);
    }

    public override object VisitExpression([NotNull] ForParser.ExpressionContext context)
    {
      return _serviceProvider.ParseExpression(context.GetText(), _intance);
    }
  }
}
