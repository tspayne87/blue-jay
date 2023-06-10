using BlueJay.Core;
using Microsoft.Xna.Framework;

namespace BlueJay.UI
{
  /// <summary>
  /// Style class meant to manipulate the appearance of UI elements
  /// </summary>
  public class Style
  {

    /// <summary>
    /// The color of the text that should be used
    /// </summary>
    private Color? _textColor = null;

    /// <summary>
    /// The current number of grid columns the internals for this element should have
    /// </summary>
    private int? _gridColumns = null;

    /// <summary>
    /// The gap in pixels where each column should be rendered
    /// </summary>
    private Point? _columnGap = null;

    /// <summary>
    /// The column span this element should use in its parent elemenet
    /// </summary>
    private int? _columnSpan = null;

    /// <summary>
    /// The column offset this element should use in its parent element
    /// </summary>
    private int? _columnOffset = null;

    /// <summary>
    /// The current basic sprite font name this element should use to render text
    /// </summary>
    private string? _font = null;

    /// <summary>
    /// The texture font that should be used to render text for this element
    /// </summary>
    private string? _textureFont = null;

    /// <summary>
    /// The texture font size that should be used when rendering the text element
    /// </summary>
    private int? _textureFontSize = null;

    /// <summary>
    /// The parent style that will determine certain styles
    /// </summary>
    private Style? _parent;

    /// <summary>
    /// The parent style that will determine certain styles
    /// </summary>
    public Style? Parent {
      get => _parent;
      set
      {
        var oldParent = _parent;
        _parent = value;
        if (WouldCreateCircularReference(this))
        {
          _parent = oldParent;
          // TODO: Need to send a warning
        }
      }
    }

    /// <summary>
    /// The current width in pixels of the UI element
    /// </summary>
    public virtual int? Width { get; set; }

    /// <summary>
    /// The current width percentage of the UI element
    /// </summary>
    public virtual float? WidthPercentage { get; set; }

    /// <summary>
    /// The current height in pixels of the UI element
    /// </summary>
    public virtual int? Height { get; set; }

    /// <summary>
    /// The current height percentage of the UI element
    /// </summary>
    public virtual float? HeightPercentage { get; set; }

    /// <summary>
    /// The offset of this element for the top based on its parent position
    /// </summary>
    public virtual int? TopOffset { get; set; }

    /// <summary>
    /// The offset of this element for the left based on its parent position
    /// </summary>
    public virtual int? LeftOffset { get; set; }

    /// <summary>
    /// The padding this element should have around the contents of it
    /// </summary>
    public virtual int? Padding { get; set; }

    /// <summary>
    /// The horizontal alignment of where this element should exist
    /// </summary>
    public virtual HorizontalAlign? HorizontalAlign { get; set; }

    /// <summary>
    /// The veritcal alignment of where this element should exist
    /// </summary>
    public virtual VerticalAlign? VerticalAlign { get; set; }

    /// <summary>
    /// The type of position this element should use when determining offsets
    /// </summary>
    public virtual Position? Position { get; set; }

    /// <summary>
    /// The nine patch texture to render the rectangle for the background
    /// </summary>
    public virtual NinePatch? NinePatch { get; set; }

    /// <summary>
    /// The color of the text that should be used
    /// </summary>
    public virtual Color? TextColor { get => _textColor ?? _parent?.TextColor; set => _textColor = value; }

    /// <summary>
    /// The color of the background that should be used
    /// </summary>
    public virtual Color? BackgroundColor { get; set; }

    /// <summary>
    /// The alignment of the text that should be used
    /// </summary>
    public virtual TextAlign? TextAlign { get; set; }

    /// <summary>
    /// The alignment of the baseline for the text that should be used
    /// </summary>
    public virtual TextBaseline? TextBaseline { get; set; }

    /// <summary>
    /// How the height shold be handled
    /// </summary>
    public virtual HeightTemplate? HeightTemplate { get; set; }

    /// <summary>
    /// The current number of grid columns the internals for this element should have
    /// </summary>
    public virtual int GridColumns { get => _gridColumns ?? 1; set => _gridColumns = value; }

    /// <summary>
    /// The gap in pixels where each column should be rendered
    /// </summary>
    public virtual Point ColumnGap { get => _columnGap ?? Point.Zero; set => _columnGap = value; }

    /// <summary>
    /// The column span this element should use in its parent elemenet
    /// </summary>
    public virtual int ColumnSpan { get => _columnSpan ?? 1; set => _columnSpan = Math.Max(value, 0); }

    /// <summary>
    /// The column offset this element should use in its parent element
    /// </summary>
    public virtual int ColumnOffset { get => _columnOffset ?? 0; set => _columnOffset = Math.Max(value, 0); }

    /// <summary>
    /// The current basic sprite font name this element should use to render text
    /// </summary>
    public virtual string? Font { get => _font ?? Parent?.Font; set => _font = value; }

    /// <summary>
    /// The texture font that should be used to render text for this element
    /// </summary>
    public virtual string? TextureFont { get => _textureFont ?? Parent?.TextureFont; set => _textureFont = value; }

    /// <summary>
    /// The texture font size that should be used when rendering the text element
    /// </summary>
    public virtual int? TextureFontSize { get => _textureFontSize ?? Parent?.TextureFontSize; set { if (value != null) _textureFontSize = Math.Max(value ?? 1, 1); else _textureFontSize = null; } }

    /// <summary>
    /// Helper method to determine if a circular refernce would happen
    /// </summary>
    /// <param name="t">The style we are determining if a circular refernce would happen</param>
    /// <returns>Will return true if a circular reference happens</returns>
    private bool WouldCreateCircularReference(Style t)
    {
      if (Parent == null) return false;
      if (Parent == t) return true;
      return Parent.WouldCreateCircularReference(t);
    }

    /// <summary>
    /// Helper method meant to merge a style into this style
    /// </summary>
    /// <param name="merge">The style we want to merge into this one</param>
    public void Merge(Style merge)
    {
      if (merge.Width != null) Width = merge.Width;
      if (merge.WidthPercentage != null) WidthPercentage = merge.WidthPercentage;
      if (merge.Height != null) Height = merge.Height;
      if (merge.HeightPercentage != null) HeightPercentage = merge.HeightPercentage;
      if (merge.TopOffset != null) TopOffset = merge.TopOffset;
      if (merge.LeftOffset != null) LeftOffset = merge.LeftOffset;
      if (merge.Padding != null) Padding = merge.Padding;
      if (merge.HorizontalAlign != null) HorizontalAlign = merge.HorizontalAlign;
      if (merge.VerticalAlign != null) VerticalAlign = merge.VerticalAlign;
      if (merge.Position != null) Position = merge.Position;
      if (merge.NinePatch != null) NinePatch = merge.NinePatch;
      if (merge._textColor != null) TextColor = merge._textColor;
      if (merge.BackgroundColor != null) BackgroundColor = merge.BackgroundColor;
      if (merge.TextAlign != null) TextAlign = merge.TextAlign;
      if (merge.TextBaseline != null) TextBaseline = merge.TextBaseline;
      if (merge._gridColumns != null) GridColumns = merge._gridColumns.Value;
      if (merge._columnGap != null) ColumnGap = merge._columnGap.Value;
      if (merge._columnSpan != null) ColumnSpan = merge._columnSpan.Value;
      if (merge._columnOffset != null) ColumnOffset = merge._columnOffset.Value;
      if (merge._font != null) Font = merge._font;
      if (merge._textureFont != null) TextureFont = merge._textureFont;
      if (merge._textureFontSize != null) TextureFontSize = merge._textureFontSize;
      if (merge.HeightTemplate != null) HeightTemplate = merge.HeightTemplate;
    }
  }
}
