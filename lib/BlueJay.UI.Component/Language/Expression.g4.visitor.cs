using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Language
{
  public class ExpressionVisitor : ExpressionBaseVisitor<object>
  {
    private readonly List<LanguageScope> Scopes;

    public ExpressionVisitor(List<LanguageScope> scopes)
    {
      Scopes = scopes;
    }

    public override object VisitScopeExpression([NotNull] ExpressionParser.ScopeExpressionContext context)
    {
      var field = context.GetText();
      for (var i = Scopes.Count - 1; i >= 0; --i)
      {
        if (Scopes[i].Fields.ContainsKey(field))
          return Scopes[i].Fields[field].Value;
      }
      return null;
    }

    public override object VisitFunctionExpression([NotNull] ExpressionParser.FunctionExpressionContext context)
    {
      var args = Visit(context.children[2]) as List<object>;
      var method = context.children[0].GetText();
      for (var i = Scopes.Count - 1; i >= 0; --i)
      {
        if (Scopes[i].Methods.ContainsKey(method))
          return Scopes[i].Methods[method].Invoke(Scopes[i].Data, args.ToArray());
      }
      return null;
    }

    public override object VisitArgumentExpression([NotNull] ExpressionParser.ArgumentExpressionContext context)
    {
      var args = new List<object>();
      for (var i = 0; i < context.ChildCount; i += 2)
      {
        args.Add(Visit(context.children[i]));
      }
      return args;
    }

    public override object VisitLiteralExpression([NotNull] ExpressionParser.LiteralExpressionContext context)
    {
      var data = context.children[0].GetText();
      if (bool.TryParse(data, out var boolean))
        return boolean;
      if (int.TryParse(data, out var num))
        return num;
      if (float.TryParse(data, out var fNum))
        return fNum;
      return data.Substring(1, data.Length - 2);
    }

    public override object VisitContextVarExpression([NotNull] ExpressionParser.ContextVarExpressionContext context)
    {
      var key = context.GetText();
      if (Scopes[Scopes.Count - 1].Fields.ContainsKey(key))
        return Scopes[Scopes.Count - 1].Fields[key].Value;
      return null;
    }

    public override object VisitTernaryExpression([NotNull] ExpressionParser.TernaryExpressionContext context)
    {
      var boolValue = Visit(context.children[0]);
      if (boolValue is bool)
      {
        if ((bool)boolValue)
        {
          return Visit(context.children[2]);
        }
        return Visit(context.children[4]);
      }
      // TODO: Maybe throw exception or something
      return null;
    }

    public override object Visit([NotNull] IParseTree tree)
    {
      if (tree.ChildCount == 2) return base.Visit(tree.GetChild(0));
      return base.Visit(tree);
    }
  }
}
