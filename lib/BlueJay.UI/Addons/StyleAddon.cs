using BlueJay.Component.System.Addons;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Addons
{
  public class StyleAddon : Addon<StyleAddon>
  {
    /// <summary>
    /// The basic nine patch style for the UI element
    /// </summary>
    public Style Style { get; set; }

    /// <summary>
    /// The style that should be used for hovering
    /// </summary>
    public Style HoverStyle { get; set; }

    /// <summary>
    /// The current style that we should be using
    /// </summary>
    public Style CurrentStyle => Hovering && HoverStyle != null ? HoverStyle : Style;

    /// <summary>
    /// If this style needs to change based on hovering status
    /// </summary>
    public bool Hovering { get; set; }

    /// <summary>
    /// The current calculated bounds for this frame
    /// </summary>
    public Rectangle CalculatedBounds;

    /// <summary>
    /// Basic constructor to build out the style component
    /// </summary>
    public StyleAddon()
      : this(new Style(), null) { }

    /// <summary>
    /// Constructor to give a default to the style component
    /// </summary>
    /// <param name="style">The style that should process the UI element bounds</param>
    public StyleAddon(Style style)
      : this(style, null) { }

    /// <summary>
    /// Constructor to give a default to the style component
    /// </summary>
    /// <param name="style">The style that should process the UI element bounds</param>
    /// <param name="hoverStyle">The hover style that should be processed for the UI element</param>
    public StyleAddon(Style style, Style hoverStyle)
    {
      Style = style;
      HoverStyle = hoverStyle;
      if (HoverStyle != null)
        HoverStyle.Parent = Style;
      CalculatedBounds = Rectangle.Empty;
    }
  }
}
