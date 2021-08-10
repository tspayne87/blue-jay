using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using BlueJay.UI.Component.Common;

namespace BlueJay.UI.Component.Language
{
  public class BlueJayXMLVisitor : BlueJayXMLBaseVisitor<object>
  {
    private List<Type> _globals = new List<Type>() { typeof(Container), typeof(Slot) };

    public readonly LanguageScope Scope;

    public BlueJayXMLVisitor(object instance)
    {
      Scope = new LanguageScope(instance);
    }

    public override object VisitElementExpression([NotNull] BlueJayXMLParser.ElementExpressionContext context)
    {
      if (context.children[1].GetText() != context.children[context.ChildCount - 2].GetText())
        return null; // TODO: Need to throw error or something since we cannot find cooresponding tag

      var elementName = context.children[1].GetText().KebabToPascal();

      var scope = new LanguageScope(null);

      var i = 2;
      for (; i < context.ChildCount && context.children[i].GetText() != ">"; ++i)
      {
        if (context.children[i].GetText().Trim().Length > 0)
        {
          var (type, name, value) = Visit(context.children[i]) as Tuple<AttributeType, string, object>;
          switch (type)
          {
            case AttributeType.Prop:
              scope.Props.Add(name, value);
              break;
          }
        }
      }

      return VisitChildren(context);
    }

    public override object VisitStringAttributeExpression([NotNull] BlueJayXMLParser.StringAttributeExpressionContext context)
    {
      var name = context.children[0].GetText().KebabToPascal();
      var value = context.children[context.ChildCount - 2].GetText();
      return (AttributeType.Prop, name, value);
    }

    private enum AttributeType
    {
      Prop, Event, Binding, Style, If, Foreach
    }
  }
}
