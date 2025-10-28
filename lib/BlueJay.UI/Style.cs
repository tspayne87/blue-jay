using BlueJay.Core;
using Microsoft.Xna.Framework;

namespace BlueJay.UI;

/// <summary>
/// The current style that will be used to render different UI elements to the screen
/// </summary>
public struct Style : IEquatable<Style>
{
  /// <summary>
  /// The global style that will be used for all UI elements
  /// </summary>
  public const string GLOBAL = "GLOBAL";

  /// <summary>
  /// The current direction to render child elements on the screen
  /// </summary>
  public Direction? Direction;

  /// <summary>
  /// The current gap between child elements
  /// </summary>
  public int? Gap;

  /// <summary>
  /// The padding element for the container
  /// </summary>
  public Directions? Padding { get; set; }

  /// <summary>
  /// The offset in pixels for this component
  /// </summary>
  public Directions? Offset { get; set; }

  /// <summary>
  /// The current width that should be used for this element
  /// </summary>
  public Measurement? Width { get; set; }

  /// <summary>
  /// Gets the width fit type to determine how the width for this element should be calculated
  /// </summary>
  public FitType WidthFitType => Width?.Type ?? FitType.Fit;

  /// <summary>
  /// The current height that should be used for this element
  /// </summary>
  public Measurement? Height { get; set; }

  /// <summary>
  /// Gets the height fit type to determine how the height for this element should be calculated
  /// </summary>
  public FitType HeightFitType => Height?.Type ?? FitType.Fit;

  /// <summary>
  /// The nine patch texture to render the rectangle for the background
  /// </summary>
  public NinePatch? NinePatch { get; set; }

  /// <summary>
  /// The color of the text to render
  /// </summary>
  public Color? FontColor { get; set; }

  /// <summary>
  /// The current size of the font to use
  /// </summary>
  public int? FontSize { get; set; }

  /// <summary>
  /// The current sprite or texture for the font
  /// </summary>
  public string? Font { get; set; }

  /// <summary>
  /// The current z-index of the element
  /// </summary>
  public int? ZIndex { get; set; }

  /// <summary>
  /// The current position of the element
  /// </summary>
  public Position? Position { get; set; }

  /// <summary>
  /// The color of the background that should be used
  /// </summary>
  public Color? BackgroundColor { get; set; }

  /// <summary>
  /// The horizontal alignment of where this element should exist
  /// </summary>
  public HorizontalAlign? HorizontalAlign { get; set; }

  /// <summary>
  /// The veritcal alignment of where this element should exist
  /// </summary>
  public VerticalAlign? VerticalAlign { get; set; }

  /// <summary>
  /// Merges the current style with another style, giving priority to the other style's values.
  /// </summary>
  /// <param name="other">
  /// The style that will overwrite all styles with its values if it has any, otherwise will keep what it has.
  /// </param>
  /// <returns>Will return the merged style.</returns>
  public Style MergeStyle(Style other)
  {
    return new Style
    {
      Direction = other.Direction ?? Direction,
      Gap = other.Gap ?? Gap,
      Padding = other.Padding ?? Padding,
      Width = other.Width ?? Width,
      Height = other.Height ?? Height,
      NinePatch = other.NinePatch ?? NinePatch,
      FontColor = other.FontColor ?? FontColor,
      FontSize = other.FontSize ?? FontSize,
      Font = other.Font ?? Font,
      ZIndex = other.ZIndex ?? ZIndex,
      Position = other.Position ?? Position,
      BackgroundColor = other.BackgroundColor ?? BackgroundColor,
      HorizontalAlign = other.HorizontalAlign ?? HorizontalAlign,
      VerticalAlign = other.VerticalAlign ?? VerticalAlign,
      Offset = other.Offset ?? Offset
    };
  }

  /// <summary>
  /// Determines whether the specified <see cref="Style"/> is equal to the current <see cref="Style"/>.
  /// </summary>
  /// <param name="other">The other style to compare with.</param>
  /// <returns>Will return true if the styles are equal, otherwise false.</returns>
  public bool Equals(Style other)
  {
    return Direction == other.Direction &&
           Gap == other.Gap &&
           Padding == other.Padding &&
           Width == other.Width &&
           Height == other.Height;
  }

  /// <summary>
  /// Adds two styles together.
  /// </summary>
  /// <param name="a">The current style we want to merge to</param>
  /// <param name="b">The style we want to merge from</param>
  /// <returns>Will return the merged style.</returns>
  public static Style operator +(Style a, Style b) => a.MergeStyle(b);

  /// <summary>
  /// Determines whether the specified <see cref="Style"/> is equal to the current <see cref="Style"/>.
  /// </summary>
  /// <param name="a">The first style to compare.</param>
  /// <param name="b">The second style to compare.</param>
  /// <returns>Will return true if the styles are equal, otherwise false.</returns>
  public static bool operator ==(Style a, Style b) => Equals(a, b);

  /// <summary>
  /// Determines whether the specified <see cref="Style"/> is not equal to the current <see cref="Style"/>.
  /// </summary>
  /// <param name="a">The first style to compare.</param>
  /// <param name="b">The second style to compare.</param>
  /// <returns>Will return true if the styles are not equal, otherwise false.</returns>
  public static bool operator !=(Style a, Style b) => !Equals(a, b);

  /// <inheritdoc />
  public override bool Equals(object? obj)
  {
    return obj is Style && Equals((Style)obj);
  }

  /// <inheritdoc />
  public override int GetHashCode()
  {
    return HashCode.Combine(Direction, Gap, Padding, Width, Height);
  }
}
