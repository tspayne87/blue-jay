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

namespace BlueJay.UI.Component.Language
{
  public class StyleVisitor : StyleParserBaseVisitor<object>
  {
    private readonly UIComponent _intance;
    private readonly IServiceProvider _serviceProvider;

    public StyleVisitor(IServiceProvider serviceProvider, UIComponent instance)
    {
      _intance = instance;
      _serviceProvider = serviceProvider;
    }

    public override object VisitExpr([NotNull] StyleParser.ExprContext context)
    {
      var styleItems = Visit(context.GetChild(0)) as List<StyleExpression>;
      var props = styleItems.SelectMany(x => x.ReactiveProps).ToList();
      return new ExpressionResult(x =>
      {
        var style = x as Style;
        if (style == null)
          style = new Style();

        foreach(var item in styleItems)
        {
          var prop = style.GetType().GetProperty(item.Name);
          if (prop != null)
          {
            prop.SetValue(style, item.Callback());
          }
        }

        return style;
      }, props);
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
        return new StyleExpression(() => dec);
      return null;
    }

    public override object VisitInteger([NotNull] StyleParser.IntegerContext context)
    {
      if (int.TryParse(context.GetText(), out var integer))
        return new StyleExpression(() => integer);
      return null;
    }

    public override object VisitNinePatch([NotNull] StyleParser.NinePatchContext context)
    {
      var content = _serviceProvider.GetRequiredService<ContentManager>();
      var texture = content.Load<Texture2D>(context.GetText());
      return new StyleExpression(() => new NinePatch(texture));
    }

    public override object VisitColor([NotNull] StyleParser.ColorContext context)
    {
      var r = Visit(context.GetChild(0)) as StyleExpression;
      var g = Visit(context.GetChild(2)) as StyleExpression;
      var b = Visit(context.GetChild(4)) as StyleExpression;
      var a = context.ChildCount > 6 ? Visit(context.GetChild(6)) as StyleExpression : new StyleExpression(() => 255);

      return new StyleExpression(() => new Color((int)r.Callback(), (int)g.Callback(), (int)b.Callback(), (int)a.Callback()));
    }

    public override object VisitPoint([NotNull] StyleParser.PointContext context)
    {
      var x = Visit(context.GetChild(0)) as StyleExpression;
      var y = context.ChildCount > 2 ? Visit(context.GetChild(2)) as StyleExpression : new StyleExpression(() => x.Callback());
      return new StyleExpression(() => new Point((int)x.Callback(), (int)y.Callback()));
    }

    public override object VisitWord([NotNull] StyleParser.WordContext context)
    {
      return new StyleExpression(() => context.GetText());
    }
    #endregion

    #region Parse Expressions
    public override object VisitExpression([NotNull] StyleParser.ExpressionContext context)
    {
      var expression = context.GetText();
      var result = _serviceProvider.ParseExpression(expression.Substring(2, expression.Length - 4), _intance);
      return new StyleExpression(() => result.Callback(null), result.ReactiveProps);
    }
    #endregion

    #region Enumeration Expressions
    public override object VisitHorizontalAlign([NotNull] StyleParser.HorizontalAlignContext context)
    {
      if (Enum.TryParse<HorizontalAlign>(context.GetText(), out var horizontalAlign))
        return new StyleExpression(() => horizontalAlign);
      return null;
    }

    public override object VisitVerticalAlign([NotNull] StyleParser.VerticalAlignContext context)
    {
      if (Enum.TryParse<VerticalAlign>(context.GetText(), out var verticalAlign))
        return new StyleExpression(() => verticalAlign);
      return null;
    }

    public override object VisitTextAlign([NotNull] StyleParser.TextAlignContext context)
    {
      if (Enum.TryParse<TextAlign>(context.GetText(), out var textAlign))
        return new StyleExpression(() => textAlign);
      return null;
    }

    public override object VisitTextBaseline([NotNull] StyleParser.TextBaselineContext context)
    {
      if (Enum.TryParse<TextBaseline>(context.GetText(), out var textBaseline))
        return new StyleExpression(() => textBaseline);
      return null;
    }

    public override object VisitPosition([NotNull] StyleParser.PositionContext context)
    {
      if (Enum.TryParse<Position>(context.GetText(), out var position))
        return new StyleExpression(() => position);
      return null;
    }
    #endregion

    private class StyleExpression
    {
      public string Name { get; set; }
      public Func<object> Callback { get; private set; }
      public List<IReactiveProperty> ReactiveProps { get; private set; }

      public StyleExpression(Func<object> callback, List<IReactiveProperty> reactiveProps = null)
      {
        Callback = callback;
        ReactiveProps = reactiveProps ?? new List<IReactiveProperty>();
      }
    }
  }
}
