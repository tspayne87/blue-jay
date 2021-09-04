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
    private readonly UIComponent _intance;
    private readonly IServiceProvider _serviceProvider;
    private readonly string _name;

    public StyleVisitor(IServiceProvider serviceProvider, UIComponent instance, string name)
    {
      _intance = instance;
      _serviceProvider = serviceProvider;
      _name = name;
    }

    public override object VisitExpr([NotNull] StyleParser.ExprContext context)
    {
      var styleItems = Visit(context.GetChild(0)) as List<StyleExpression>;
      var props = styleItems.SelectMany(x => x.ScopePaths).ToList();
      return new ExpressionResult(x =>
      {
        var style = (x?.ContainsKey(_name) == true ? x[_name] as Style : null) ?? new Style();

        foreach(var item in styleItems)
        {
          var prop = style.GetType().GetProperty(item.Name);
          if (prop != null)
          {
            prop.SetValue(style, item.Callback(x));
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
        return new StyleExpression(x => dec);
      return null;
    }

    public override object VisitInteger([NotNull] StyleParser.IntegerContext context)
    {
      if (int.TryParse(context.GetText(), out var integer))
        return new StyleExpression(x => integer);
      return null;
    }

    public override object VisitNinePatch([NotNull] StyleParser.NinePatchContext context)
    {
      var content = _serviceProvider.GetRequiredService<ContentManager>();
      var texture = content.Load<Texture2D>(context.GetText());
      return new StyleExpression(x => new NinePatch(texture));
    }

    public override object VisitColor([NotNull] StyleParser.ColorContext context)
    {
      var r = Visit(context.GetChild(0)) as StyleExpression;
      var g = Visit(context.GetChild(2)) as StyleExpression;
      var b = Visit(context.GetChild(4)) as StyleExpression;
      var a = context.ChildCount > 6 ? Visit(context.GetChild(6)) as StyleExpression : new StyleExpression(x => 255);

      return new StyleExpression(x => new Color((int)r.Callback(x), (int)g.Callback(x), (int)b.Callback(x), (int)a.Callback(x)));
    }

    public override object VisitPoint([NotNull] StyleParser.PointContext context)
    {
      var x = Visit(context.GetChild(0)) as StyleExpression;
      var y = context.ChildCount > 2 ? Visit(context.GetChild(2)) as StyleExpression : new StyleExpression(z => x.Callback(z));
      return new StyleExpression(z => new Point((int)x.Callback(z), (int)y.Callback(z)));
    }

    public override object VisitWord([NotNull] StyleParser.WordContext context)
    {
      return new StyleExpression(x => context.GetText());
    }
    #endregion

    #region Parse Expressions
    public override object VisitExpression([NotNull] StyleParser.ExpressionContext context)
    {
      var expression = context.GetText();
      var result = _serviceProvider.ParseExpression(expression.Substring(2, expression.Length - 4), _intance);
      return new StyleExpression(x => result.Callback(x), result.ScopePaths);
    }
    #endregion

    #region Enumeration Expressions
    public override object VisitHorizontalAlign([NotNull] StyleParser.HorizontalAlignContext context)
    {
      if (Enum.TryParse<HorizontalAlign>(context.GetText(), out var horizontalAlign))
        return new StyleExpression(x => horizontalAlign);
      return null;
    }

    public override object VisitVerticalAlign([NotNull] StyleParser.VerticalAlignContext context)
    {
      if (Enum.TryParse<VerticalAlign>(context.GetText(), out var verticalAlign))
        return new StyleExpression(x => verticalAlign);
      return null;
    }

    public override object VisitTextAlign([NotNull] StyleParser.TextAlignContext context)
    {
      if (Enum.TryParse<TextAlign>(context.GetText(), out var textAlign))
        return new StyleExpression(x => textAlign);
      return null;
    }

    public override object VisitTextBaseline([NotNull] StyleParser.TextBaselineContext context)
    {
      if (Enum.TryParse<TextBaseline>(context.GetText(), out var textBaseline))
        return new StyleExpression(x => textBaseline);
      return null;
    }

    public override object VisitPosition([NotNull] StyleParser.PositionContext context)
    {
      if (Enum.TryParse<Position>(context.GetText(), out var position))
        return new StyleExpression(x => position);
      return null;
    }
    #endregion

    private class StyleExpression
    {
      public string Name { get; set; }
      public Func<ReactiveScope, object> Callback { get; private set; }
      public List<string> ScopePaths { get; private set; }

      public StyleExpression(Func<ReactiveScope, object> callback, List<string> scopePaths = null)
      {
        Callback = callback;
        ScopePaths = scopePaths ?? new List<string>();
      }
    }
  }
}
