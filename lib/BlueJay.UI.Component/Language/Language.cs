using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Language
{
  public static class Language
  {
    public static object ParseExpression(string expression, List<LanguageScope> scopes)
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

    public static Style ParseStyle(string styleExpression, List<LanguageScope> scopes, Style style = null)
    {
      var stream = new AntlrInputStream(styleExpression);
      ITokenSource lexer = new StyleLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new StyleParser(tokens);

      var expr = parser.prog();

      var visitor = new StyleVisitor(scopes, style);
      visitor.Visit(expr);

      return visitor.Style;
    }
  }
}
