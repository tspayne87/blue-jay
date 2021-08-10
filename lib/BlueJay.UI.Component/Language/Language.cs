using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace BlueJay.UI.Component.Language
{
  public static class Language
  {
    public static (object, List<IReactiveProperty>) ParseExpression(string expression, List<LanguageScope> scopes)
    {
      throw new NotImplementedException();
      // var stream = new AntlrInputStream(expression);
      // ITokenSource lexer = new ExpressionLexer(stream);
      // ITokenStream tokens = new CommonTokenStream(lexer);
      // var parser = new ExpressionParser(tokens);

      // var expr = parser.prog();

      // var visitor = new ExpressionVisitor(scopes);
      // var result = visitor.Visit(expr);
      // return (result, visitor.ReactiveProperties);
    }

    public static (Style, List<IReactiveProperty>) ParseStyle(string styleExpression, List<LanguageScope> scopes, Style style = null)
    {
      var stream = new AntlrInputStream(styleExpression);
      ITokenSource lexer = new StyleLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new StyleParser(tokens);

      var expr = parser.prog();

      var visitor = new StyleVisitor(scopes, style);
      visitor.Visit(expr);

      return (visitor.Style, visitor.ReactiveProperties);
    }

    public static LanguageScope ParseUIComponet<T>(IServiceProvider serviceProvider)
      where T : UIComponent
    {
      var instance = ActivatorUtilities.CreateInstance(serviceProvider, typeof(T));
      var view = (ViewAttribute)Attribute.GetCustomAttribute(typeof(T), typeof(ViewAttribute));

      return ParseXML(view.XML, instance);
    }

    public static LanguageScope ParseXML(string xml, object instance)
    {
      var stream = new AntlrInputStream(xml);
      ITokenSource lexer = new BlueJayXMLLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new BlueJayXMLParser(tokens);

      var expr = parser.prog();
      
      var visitor = new BlueJayXMLVisitor(instance);
      visitor.Visit(expr);
      return visitor.Scope;
    }
  }
}
