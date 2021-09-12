using BlueJay.Core;
using Microsoft.Xna.Framework;
using System;

namespace BlueJay.UI
{
  public class Style
  {
    private int? _width = null;
    private float? _widthPercentage = null;
    private int? _height = null;
    private float? _heightPercentage = null;
    private int? _topOffset = null;
    private int? _leftOffset = null;
    private int? _padding = null;
    private HorizontalAlign? _horizontalAlign = null;
    private VerticalAlign? _verticalAlign = null;
    private NinePatch _ninePatch = null;
    private Color? _textColor = null;
    private Color? _backgroundColor = null;
    private TextAlign? _textAlign = null;
    private TextBaseline? _textBaseline = null;
    private Position? _position = null;
    private int? _gridColumns = null;
    private Point? _columnGap = null;
    private int? _columnSpan = null;
    private int? _columnOffset = null;
    private string _font = null;
    private string _textureFont = null;
    private int? _textureFontSize = null;

    private Style _parent;

    public Style Parent {
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

    public int? Width { get => _width ?? Parent?.Width; set => _width = value; }
    public float? WidthPercentage { get => _widthPercentage ?? Parent?.WidthPercentage; set => _widthPercentage = value; }

    public int? Height { get => _height ?? Parent?.Height; set => _height = value; }
    public float? HeightPercentage { get => _heightPercentage ?? Parent?.HeightPercentage; set => _heightPercentage = value; }

    public int? TopOffset { get => _topOffset ?? Parent?.TopOffset; set => _topOffset = value; }
    public int? LeftOffset { get => _leftOffset ?? Parent?.LeftOffset; set => _leftOffset = value; }

    public int? Padding { get => _padding ?? Parent?.Padding; set => _padding = value; }

    public HorizontalAlign? HorizontalAlign { get => _horizontalAlign ?? Parent?.HorizontalAlign; set => _horizontalAlign = value; }
    public VerticalAlign? VerticalAlign { get => _verticalAlign ?? Parent?.VerticalAlign; set => _verticalAlign = value; }
    public Position? Position { get => _position ?? Parent?.Position; set => _position = value; }

    public NinePatch NinePatch { get => _ninePatch ?? Parent?.NinePatch; set => _ninePatch = value; }

    public Color? TextColor { get => _textColor ?? Parent?.TextColor; set => _textColor = value; }
    public Color? BackgroundColor { get => _backgroundColor ?? Parent?.BackgroundColor; set => _backgroundColor = value; }
    public TextAlign? TextAlign { get => _textAlign ?? Parent?.TextAlign; set => _textAlign = value; }
    public TextBaseline? TextBaseline { get => _textBaseline ?? Parent?.TextBaseline; set => _textBaseline = value; }

    public int GridColumns { get => _gridColumns ?? Parent?.GridColumns ?? 1; set => _gridColumns = value; }
    public Point ColumnGap { get => _columnGap ?? Parent?.ColumnGap ?? Point.Zero; set => _columnGap = value; }
    public int ColumnSpan { get => _columnSpan ?? Parent?.ColumnSpan ?? 1; set => _columnSpan = Math.Max(value, 0); }
    public int ColumnOffset { get => _columnOffset ?? Parent?.ColumnOffset ?? 0; set => _columnOffset = Math.Max(value, 0); }

    public string Font { get => _font ?? Parent?.Font; set => _font = value; }
    public string TextureFont { get => _textureFont ?? Parent?.TextureFont; set => _textureFont = value; }
    public int? TextureFontSize { get => _textureFontSize ?? Parent?.TextureFontSize; set { if (value != null) _textureFontSize = Math.Max(value ?? 1, 1); else _textureFontSize = null; } }

    private bool WouldCreateCircularReference(Style t)
    {
      if (Parent == null) return false;
      if (Parent == t) return true;
      return Parent.WouldCreateCircularReference(t);
    }

    public void Merge(Style merge)
    {
      if (merge._width != null) Width = merge._width;
      if (merge._widthPercentage != null) WidthPercentage = merge._widthPercentage;
      if (merge._height != null) Height = merge._height;
      if (merge._heightPercentage != null) HeightPercentage = merge._heightPercentage;
      if (merge._topOffset != null) TopOffset = merge._topOffset;
      if (merge._leftOffset != null) LeftOffset = merge._leftOffset;
      if (merge._padding != null) Padding = merge._padding;
      if (merge._horizontalAlign != null) HorizontalAlign = merge._horizontalAlign;
      if (merge._verticalAlign != null) VerticalAlign = merge._verticalAlign;
      if (merge._position != null) Position = merge._position;
      if (merge._ninePatch != null) NinePatch = merge._ninePatch;
      if (merge._textColor != null) TextColor = merge._textColor;
      if (merge._backgroundColor != null) BackgroundColor = merge._backgroundColor;
      if (merge._textAlign != null) TextAlign = merge._textAlign;
      if (merge._textBaseline != null) TextBaseline = merge._textBaseline;
      if (merge._gridColumns != null) GridColumns = merge._gridColumns.Value;
      if (merge._columnGap != null) ColumnGap = merge._columnGap.Value;
      if (merge._columnSpan != null) ColumnSpan = merge._columnSpan.Value;
      if (merge._columnOffset != null) ColumnOffset = merge._columnOffset.Value;
      if (merge._font != null) Font = merge._font;
      if (merge._textureFont != null) TextureFont = merge._textureFont;
      if (merge._textureFontSize != null) TextureFontSize = merge._textureFontSize;
    }
  }
}
