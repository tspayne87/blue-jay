using System;
using BlueJay.UI.Component.Language.Antlr;
using System.Collections.Generic;
using Antlr4.Runtime.Misc;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BlueJay.Core;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Component.Language
{
  /// <summary>
  /// The style visitor that is meant to parse the expression for styles
  /// </summary>
  internal class StyleVisitor : StyleParserBaseVisitor<object>
  {
    /// <summary>
    /// The content manager to load textures from
    /// </summary>
    private readonly ContentManager _content;

    public StyleVisitor(ContentManager content)
    {
      _content = content;
    }

    /// <summary>
    /// Visitor for root expression for a style
    /// </summary>
    /// <param name="context">The root expression for a style</param>
    /// <returns>Will return an expression result meant to generate styles from</returns>
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

    /// <summary>
    /// Visitor for all the styles in the expression
    /// </summary>
    /// <param name="context">The style context</param>
    /// <returns>Will return a list of style expressions to process the style props</returns>
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

    /// <summary>
    /// Visitor for all style items
    /// </summary>
    /// <param name="context">The style item context</param>
    /// <returns>Will return a style expression based on the expression given</returns>
    public override object VisitStyleItem([NotNull] StyleParser.StyleItemContext context)
    {
      var expression = Visit(context.GetChild(2)) as StyleExpression;
      if (expression != null)
        expression.Name = context.GetChild(0).GetText();
      return expression;
    }

    #region Value Expressions
    /// <summary>
    /// Visitor for Decimals
    /// </summary>
    /// <param name="context">The decimal context</param>
    /// <returns>Will return a style expression</returns>
    public override object VisitDecimal([NotNull] StyleParser.DecimalContext context)
    {
      if (float.TryParse(context.GetText(), out var dec))
        return new StyleExpression(dec);
      return null;
    }

    /// <summary>
    /// Visitor for integers
    /// </summary>
    /// <param name="context">The integer context</param>
    /// <returns>Will return a style expression</returns>
    public override object VisitInteger([NotNull] StyleParser.IntegerContext context)
    {
      if (int.TryParse(context.GetText(), out var integer))
        return new StyleExpression(integer);
      return null;
    }

    /// <summary>
    /// Visitor for nine patch
    /// </summary>
    /// <param name="context">The nine patch context</param>
    /// <returns>Will return a style expression</returns>
    public override object VisitNinePatch([NotNull] StyleParser.NinePatchContext context)
    {
      return new StyleExpression(new NinePatch(_content.Load<Texture2D>(context.GetText())));
    }

    /// <summary>
    /// Visitor for the color
    /// </summary>
    /// <param name="context">The color context</param>
    /// <returns>Will return a style expression</returns>
    public override object VisitColor([NotNull] StyleParser.ColorContext context)
    {
      var r = Visit(context.GetChild(0)) as StyleExpression;
      var g = Visit(context.GetChild(2)) as StyleExpression;
      var b = Visit(context.GetChild(4)) as StyleExpression;
      var a = context.ChildCount > 6 ? Visit(context.GetChild(6)) as StyleExpression : new StyleExpression(255);

      return new StyleExpression(new Color((int)r.Data, (int)g.Data, (int)b.Data, (int)a.Data));
    }

    /// <summary>
    /// Visitor for the point
    /// </summary>
    /// <param name="context">The point context</param>
    /// <returns>Will return a style expression</returns>
    public override object VisitPoint([NotNull] StyleParser.PointContext context)
    {
      var x = Visit(context.GetChild(0)) as StyleExpression;
      var y = context.ChildCount > 2 ? Visit(context.GetChild(2)) as StyleExpression : new StyleExpression(x.Data);
      return new StyleExpression(new Point((int)x.Data, (int)y.Data));
    }

    /// <summary>
    /// Visitor for a word
    /// </summary>
    /// <param name="context">The word context</param>
    /// <returns>Will return a style expression</returns>
    public override object VisitWord([NotNull] StyleParser.WordContext context)
    {
      return new StyleExpression(context.GetText());
    }
    #endregion

    #region Enumeration Expressions
    /// <summary>
    /// Visitor for horizontal align
    /// </summary>
    /// <param name="context">The horizontal align context</param>
    /// <returns>Will return a style expression</returns>
    public override object VisitHorizontalAlign([NotNull] StyleParser.HorizontalAlignContext context)
    {
      if (Enum.TryParse<HorizontalAlign>(context.GetText(), out var horizontalAlign))
        return new StyleExpression(horizontalAlign);
      return null;
    }

    /// <summary>
    /// Visitor for vertical align
    /// </summary>
    /// <param name="context">The vertical align context</param>
    /// <returns>Will return a style expression</returns>
    public override object VisitVerticalAlign([NotNull] StyleParser.VerticalAlignContext context)
    {
      if (Enum.TryParse<VerticalAlign>(context.GetText(), out var verticalAlign))
        return new StyleExpression(verticalAlign);
      return null;
    }

    /// <summary>
    /// Visitor for text align
    /// </summary>
    /// <param name="context">The text align context</param>
    /// <returns>Will return a style expression</returns>
    public override object VisitTextAlign([NotNull] StyleParser.TextAlignContext context)
    {
      if (Enum.TryParse<TextAlign>(context.GetText(), out var textAlign))
        return new StyleExpression(textAlign);
      return null;
    }

    /// <summary>
    /// Visitor for text baseline
    /// </summary>
    /// <param name="context">The text baseline context</param>
    /// <returns>Will return a style expression</returns>
    public override object VisitTextBaseline([NotNull] StyleParser.TextBaselineContext context)
    {
      if (Enum.TryParse<TextBaseline>(context.GetText(), out var textBaseline))
        return new StyleExpression(textBaseline);
      return null;
    }

    /// <summary>
    /// Visitor for the position
    /// </summary>
    /// <param name="context">The position context</param>
    /// <returns>Will return a style expression</returns>
    public override object VisitPosition([NotNull] StyleParser.PositionContext context)
    {
      if (Enum.TryParse<Position>(context.GetText(), out var position))
        return new StyleExpression(position);
      return null;
    }

    public override object VisitHeightTemplate([NotNull] StyleParser.HeightTemplateContext context)
    {
      if (Enum.TryParse<HeightTemplate>(context.GetText(), out var heightTemplate))
        return new StyleExpression(heightTemplate);
      return null;
    }
    #endregion

    /// <summary>
    /// Style expression is meant to return the name and data for a specific style
    /// </summary>
    private class StyleExpression
    {
      /// <summary>
      /// The prop name on a style
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// The data that should be assigned to that prop
      /// </summary>
      public object Data { get; private set; }

      /// <summary>
      /// Constructor to give defaults to the data
      /// </summary>
      /// <param name="data">The data that should be assigned to that prop</param>
      public StyleExpression(object data)
      {
        Data = data;
      }
    }
  }
}
