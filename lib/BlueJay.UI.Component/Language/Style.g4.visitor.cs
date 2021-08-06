using Antlr4.Runtime.Misc;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.UI.Component.Language
{
  public class StyleVisitor : StyleBaseVisitor<object>
  {
    private readonly List<LanguageScope> Scopes;

    public Style Style { get; private set; }

    public StyleVisitor(List<LanguageScope> scopes, Style style = null)
    {
      Scopes = scopes;
      Style = style ?? new Style();
    }

    //public override object VisitStyleColorExpression([NotNull] StyleParser.StyleColorExpressionContext context)
    //{
    //  if (context.ChildCount == 5 && int.TryParse(context.children[0].GetText(), out var r) && int.TryParse(context.children[2].GetText(), out var g) && int.TryParse(context.children[4].GetText(), out var b))
    //    return new Color(r, g, b);
    //  if (context.ChildCount == 7 && int.TryParse(context.children[0].GetText(), out var ar) && int.TryParse(context.children[2].GetText(), out var ag) && int.TryParse(context.children[4].GetText(), out var ab) && int.TryParse(context.children[6].GetText(), out var a))
    //    return new Color(ar, ag, ab, a);
    //  return null;
    //}

    //public override object VisitStylePointExpression([NotNull] StyleParser.StylePointExpressionContext context)
    //{
    //  if (context.ChildCount == 1 && int.TryParse(context.children[0].GetText(), out var value))
    //    return new Point(value);
    //  if (int.TryParse(context.children[0].GetText(), out var x) && int.TryParse(context.children[2].GetText(), out var y))
    //    return new Point(x, y);
    //  return null;
    //}

    public override object VisitStyleItemExpression([NotNull] StyleParser.StyleItemExpressionContext context)
    {
      var left = context.children[0].GetText();
      var right = context.children[2].GetText();

      var prop = Style.GetType().GetProperties()
        .FirstOrDefault(x => x.Name.Equals(left, StringComparison.OrdinalIgnoreCase));
      if (prop == null) return null;

      if (right == "{{")
      {
        var obj = Language.ParseExpression(context.children[3].GetText(), Scopes);
        prop.SetValue(Style, obj);
        return null;
      }

      switch (left.ToLower())
      {
        case "width":
        case "height":
        case "topoffset":
        case "leftoffset":
        case "padding":
        case "gridcolumns":
        case "columnspan":
        case "columnoffset":
        case "texturefontsize":
          if (int.TryParse(right, out var integer))
            prop.SetValue(Style, integer);
          return null;
        case "horizontalalign":
          if (Enum.TryParse<HorizontalAlign>(right, out var horizontalAlign))
            prop.SetValue(Style, horizontalAlign);
          return null;
        case "verticalalign":
          if (Enum.TryParse<VerticalAlign>(right, out var verticalAlign))
            prop.SetValue(Style, verticalAlign);
          return null;
        case "position":
          if (Enum.TryParse<Position>(right, out var position))
            prop.SetValue(Style, position);
          return null;
        case "ninepatch":
          // TODO
          break;
        case "textcolor":
        case "backgroundcolor":
          if (context.ChildCount == 7 && int.TryParse(context.children[2].GetText(), out var r) && int.TryParse(context.children[4].GetText(), out var g) && int.TryParse(context.children[6].GetText(), out var b))
            prop.SetValue(Style, new Color(r, g, b));
          if (context.ChildCount == 9 && int.TryParse(context.children[2].GetText(), out var ar) && int.TryParse(context.children[4].GetText(), out var ag) && int.TryParse(context.children[6].GetText(), out var ab) && int.TryParse(context.children[8].GetText(), out var a))
            prop.SetValue(Style, new Color(ar, ag, ab, a));
          return null;
        case "columngap":
          if (context.ChildCount == 3 && int.TryParse(context.children[2].GetText(), out var value))
            prop.SetValue(Style, new Point(value));
          if (context.ChildCount == 5 && int.TryParse(context.children[2].GetText(), out var x) && int.TryParse(context.children[4].GetText(), out var y))
            prop.SetValue(Style, new Point(x, y));
          return null;
        case "textalign":
          if (Enum.TryParse<TextAlign>(right, out var textAlign))
            prop.SetValue(Style, textAlign);
          return null;
        case "textbaseline":
          if (Enum.TryParse<TextBaseline>(right, out var textBaseline))
            prop.SetValue(Style, textBaseline);
          return null;
        case "font":
        case "texturefont":
          prop.SetValue(Style, right);
          return null;
      }
      return null;
    }
  }
}
