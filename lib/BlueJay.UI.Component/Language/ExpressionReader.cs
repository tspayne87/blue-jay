using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Language
{
  public static class ExpressionReader
  {
    public static object Parse(string expression, List<ExpressionScope> scopes)
    {
      var stream = new AntlrInputStream(expression);
      ITokenSource lexer = new ExpressionLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new ExpressionParser(tokens);

      var expr = parser.prog();

      var visitor = new ExpressionVisitor(scopes);
      var result = visitor.Visit(expr);
      return result;
    }
  }
}
