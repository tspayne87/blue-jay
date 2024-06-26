﻿using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Addons
{
  public struct StyleAddon : IAddon
  {
    /// <summary>
    /// The internal hover style that should be used for hovering
    /// </summary>
    private ChildStyle? _hoverStyle;

    /// <summary>
    /// The internal style that should be used by the UI element
    /// </summary>
    private Style _style;

    /// <summary>
    /// The basic nine patch style for the UI element
    /// </summary>
    public Style Style
    {
      get => _style;
      set
      {
        var style = _hoverStyle;
        while (style?.Parent != null)
        {
          if (style.Parent == _style)
          {
            style.Parent = value;
            break;
          }
        }
        _style = value;
      }
    }

    /// <summary>
    /// The style that should be used for hovering
    /// </summary>
    public ChildStyle? HoverStyle
    {
      get => _hoverStyle;
      set
      {
        _hoverStyle = value;
        if (_hoverStyle != null)
          _hoverStyle.Parent = _style;
      }
    }

    /// <summary>
    /// The current grid position this item should be in
    /// </summary>
    public Point GridPosition { get; set; }

    /// <summary>
    /// The current style that we should be using
    /// </summary>
    public Style CurrentStyle => Hovering && _hoverStyle != null ? _hoverStyle : Style;

    /// <summary>
    /// If this style needs to change based on hovering status
    /// </summary>
    public bool Hovering { get; set; }

    /// <summary>
    /// The amount of style updates that have occured
    /// </summary>
    public int StyleUpdates { get; set; }

    /// <summary>
    /// The current calculated bounds for this frame
    /// </summary>
    public Rectangle CalculatedBounds;

    /// <summary>
    /// Constructor to give a default to the style component
    /// </summary>
    /// <param name="style">The style that should process the UI element bounds</param>
    public StyleAddon(Style? style)
      : this(style, default) { }

    /// <summary>
    /// Constructor to give a default to the style component
    /// </summary>
    /// <param name="style">The style that should process the UI element bounds</param>
    /// <param name="hoverStyle">The hover style that should be processed for the UI element</param>
    public StyleAddon(Style? style, ChildStyle? hoverStyle)
    {
      Hovering = false;
      CalculatedBounds = Rectangle.Empty;
      GridPosition = Point.Zero;
      _style = style ?? new Style();
      _hoverStyle = hoverStyle ?? new ChildStyle();
      _hoverStyle.Parent = _style;
      CalculatedBounds = Rectangle.Empty;
      StyleUpdates = 0;
    }

    /// <summary>
    /// Overriden to string function is meant to give a quick and easy way to see
    /// how this object looks while debugging
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"StyleUpdates: {StyleUpdates}";
    }
  }
}
