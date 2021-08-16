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

    public static ElementNode ParseUIComponet<T>(this IServiceProvider serviceProvider)
      where T : UIComponent
    {
      return ParseUIComponet(serviceProvider, typeof(T), out var instance);
    }

    internal static ElementNode ParseUIComponet(this IServiceProvider serviceProvider, Type type, out object instance)
    {
      instance = ActivatorUtilities.CreateInstance(serviceProvider, type);
      var view = (ViewAttribute)Attribute.GetCustomAttribute(type, typeof(ViewAttribute));
      var components = (ComponentAttribute)Attribute.GetCustomAttribute(type, typeof(ComponentAttribute));

      return ParseXML(serviceProvider, view.XML, instance, components?.Components);
    }

    public static ElementNode ParseXML(this IServiceProvider serviceProvider, string xml, object instance, List<Type> components = null)
    {
      var stream = new AntlrInputStream(xml);
      ITokenSource lexer = new BlueJayXMLLexer(stream);
      ITokenStream tokens = new CommonTokenStream(lexer);
      var parser = new BlueJayXMLParser(tokens);

      var expr = parser.prog();
      
      var visitor = new BlueJayXMLVisitor(serviceProvider, instance, components);
      visitor.Visit(expr);
      return visitor.Root;
    }
  }
}
