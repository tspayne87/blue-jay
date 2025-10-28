namespace BlueJay.UI
{
  [Flags]
  public enum StyleDecorator
  {
    /// <summary>
    /// Basic decorator that is used as the base for all effects
    /// </summary>
    None = 0,

    /// <summary>
    /// Basic hover effect that can be used to indicate a UI element is interactable
    /// </summary>
    Hover = 1
  }

  /// <summary>
  /// Enumeration to determine how the bounds of a UI element should be measured
  /// </summary>
  public enum FitType
  {
    /// <summary>
    /// Fits the element based on its child elements
    /// </summary>
    Fit,

    /// <summary>
    /// Fits the element based on its parent
    /// </summary>
    Percentage,

    /// <summary>
    /// Fits the element to a fixed size
    /// </summary>
    Fixed
  }

  /// <summary>
  /// Horizontally aligned enum, meant to determine how we should align horizontally
  /// </summary>
  public enum HorizontalAlign
  {
    Right, Center, Left
  }

  /// <summary>
  /// Vertically aligned enum, meant to determine how we should align vertically
  /// </summary>
  public enum VerticalAlign
  {
    Top, Center, Bottom
  }

  /// <summary>
  /// Text aligned enum, meant to determine how we should align horizontally
  /// </summary>
  public enum TextAlign
  {
    Right, Center, Left
  }

  /// <summary>
  /// Text baseline enum, meant to determine how we should align vertically
  /// </summary>
  public enum TextBaseline
  {
    Top, Center, Bottom
  }

  /// <summary>
  /// Position of the UI element
  /// </summary>
  public enum Position
  {
    Relative, Absolute
  }

  /// <summary>
  /// How the height should be templated out
  /// </summary>
  public enum HeightTemplate
  {
    Stretch
  }

  /// <summary>
  /// The direction in which the UI element is laid out
  /// </summary>
  public enum Direction
  {
    Horizontal, Vertical
  }

  /// <summary>
  /// Overflow enum, used to determine how overflow should be handled
  /// </summary>
  public enum Overflow
  {
    None, Hidden, Scroll, Auto
  }
}
