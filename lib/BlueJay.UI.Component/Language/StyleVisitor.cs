using System;
using BlueJay.UI.Component.Language.Antlr;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Extensions.DependencyInjection;
using BlueJay.Core;
using Microsoft.Xna.Framework;
using System.Linq;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Language
{
  public class StyleVisitor : StyleParserBaseVisitor<object>
  {
    private readonly IServiceProvider _serviceProvider;
    private readonly string _name;

    public StyleVisitor(IServiceProvider serviceProvider, string name)
    {
      _serviceProvider = serviceProvider;
      _name = name;
    }

    public override object VisitExpr([NotNull] StyleParser.ExprContext context)
    {
      var styleItems = Visit(context.GetChild(0)) as List<StyleExpression>;
      return new ExpressionResult(x =>
      {
        var style = new Style();
        foreach(var item in styleItems)
        {
          var prop = style.GetType().GetProperty(item.Name);
          if (prop != null)
          {
            prop.SetValue(style, item.Data);
          }
        }
        return style;
      });
    }

    public override object VisitStyle([NotNull] StyleParser.StyleContext context)
    {
      var styles = new List<StyleExpression>();
      for (var i = 0; i < context.ChildCount; i += 2)
      {
        var expression = Visit(context.GetChild(i)) as StyleExpression;
        if (expression != null)
          styles.Add(expression);
      }
      return styles;
    }

    public override object VisitStyleItem([NotNull] StyleParser.StyleItemContext context)
    {
      var expression = Visit(context.GetChild(2)) as StyleExpression;
      if (expression != null)
        expression.Name = context.GetChild(0).GetText();
      return expression;
    }

    #region Value Expressions
    public override object VisitDecimal([NotNull] StyleParser.DecimalContext context)
    {
      if (float.TryParse(context.GetText(), out var dec))
        return new StyleExpression(dec);
      return null;
    }

    public override object VisitInteger([NotNull] StyleParser.IntegerContext context)
    {
      if (int.TryParse(context.GetText(), out var integer))
        return new StyleExpression(integer);
      return null;
    }

    public override object VisitNinePatch([NotNull] StyleParser.NinePatchContext context)
    {
      var content = _serviceProvider.GetRequiredService<ContentManager>();
      var texture = content.Load<Texture2D>(context.GetText());
      return new StyleExpression(new NinePatch(texture));
    }

    public override object VisitColor([NotNull] StyleParser.ColorContext context)
    {
      var r = Visit(context.GetChild(0)) as StyleExpression;
      var g = Visit(context.GetChild(2)) as StyleExpression;
      var b = Visit(context.GetChild(4)) as StyleExpression;
      var a = context.ChildCount > 6 ? Visit(context.GetChild(6)) as StyleExpression : new StyleExpression(255);

      return new StyleExpression(new Color((int)r.Data, (int)g.Data, (int)b.Data, (int)a.Data));
    }

    public override object VisitPoint([NotNull] StyleParser.PointContext context)
    {
      var x = Visit(context.GetChild(0)) as StyleExpression;
      var y = context.ChildCount > 2 ? Visit(context.GetChild(2)) as StyleExpression : new StyleExpression(x.Data);
      return new StyleExpression(new Point((int)x.Data, (int)y.Data));
    }

    public override object VisitWord([NotNull] StyleParser.WordContext context)
    {
      return new StyleExpression(context.GetText());
    }
    #endregion

    #region Enumeration Expressions
    public override object VisitHorizontalAlign([NotNull] StyleParser.HorizontalAlignContext context)
    {
      if (Enum.TryParse<HorizontalAlign>(context.GetText(), out var horizontalAlign))
        return new StyleExpression(horizontalAlign);
      return null;
    }

    public override object VisitVerticalAlign([NotNull] StyleParser.VerticalAlignContext context)
    {
      if (Enum.TryParse<VerticalAlign>(context.GetText(), out var verticalAlign))
        return new StyleExpression(verticalAlign);
      return null;
    }

    public override object VisitTextAlign([NotNull] StyleParser.TextAlignContext context)
    {
      if (Enum.TryParse<TextAlign>(context.GetText(), out var textAlign))
        return new StyleExpression(textAlign);
      return null;
    }

    public override object VisitTextBaseline([NotNull] StyleParser.TextBaselineContext context)
    {
      if (Enum.TryParse<TextBaseline>(context.GetText(), out var textBaseline))
        return new StyleExpression(textBaseline);
      return null;
    }

    public override object VisitPosition([NotNull] StyleParser.PositionContext context)
    {
      if (Enum.TryParse<Position>(context.GetText(), out var position))
        return new StyleExpression(position);
      return null;
    }
    #endregion

    private class StyleExpression
    {
      public string Name { get; set; }
      public object Data { get; private set; }

      public StyleExpression(object data)
      {
        Data = data;
      }
    }
  }
}
