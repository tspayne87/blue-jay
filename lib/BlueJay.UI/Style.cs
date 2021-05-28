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
    private TextAlign? _textAlign = null;
    private TextBaseline? _textBaseline = null;
    public int? _gridColumns = null;
    public Point? _columnGap = null;
    public int? _columnSpan = null;
    public int? _columnOffset = null;

    public Style Parent { get; set; }

    public int? Width { get => _width ?? Parent?.Width; set => _width = value; }
    public float? WidthPercentage { get => _widthPercentage ?? Parent?.WidthPercentage; set => _widthPercentage = value; }

    public int? Height { get => _height ?? Parent?.Height; set => _height = value; }
    public float? HeightPercentage { get => _heightPercentage ?? Parent?.HeightPercentage; set => _heightPercentage = value; }

    public int? TopOffset { get => _topOffset ?? Parent?.TopOffset; set => _topOffset = value; }
    public int? LeftOffset { get => _leftOffset ?? Parent?.LeftOffset; set => _leftOffset = value; }

    public int? Padding { get => _padding ?? Parent?.Padding; set => _padding = value; }

    public HorizontalAlign? HorizontalAlign { get => _horizontalAlign ?? Parent?.HorizontalAlign; set => _horizontalAlign = value; }
    public VerticalAlign? VerticalAlign { get => _verticalAlign ?? Parent?.VerticalAlign; set => _verticalAlign = value; }

    public NinePatch NinePatch { get => _ninePatch ?? Parent?.NinePatch; set => _ninePatch = value; }

    public Color? TextColor { get => _textColor ?? Parent?.TextColor; set => _textColor = value; }
    public TextAlign? TextAlign { get => _textAlign ?? Parent?.TextAlign; set => _textAlign = value; }
    public TextBaseline? TextBaseline { get => _textBaseline ?? Parent?.TextBaseline; set => _textBaseline = value; }

    public int GridColumns { get => _gridColumns ?? Parent?.GridColumns ?? 1; set => _gridColumns = value; }
    public Point ColumnGap { get => _columnGap ?? Parent?.ColumnGap ?? Point.Zero; set => _columnGap = value; }
    public int ColumnSpan { get => _columnSpan ?? Parent?.ColumnSpan ?? 1; set => _columnSpan = Math.Max(value, 0); }
    public int ColumnOffset { get => _columnOffset ?? Parent?.ColumnOffset ?? 0; set => _columnOffset = Math.Max(value, 0); }
  }
}
