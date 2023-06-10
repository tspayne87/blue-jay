using BlueJay.Core;
using Microsoft.Xna.Framework;

namespace BlueJay.UI
{
  public class ChildStyle : Style
  {
    /// <inheritdoc />
    public override int? Width { get => base.Width ?? Parent?.Width; set => base.Width = value; }

    /// <inheritdoc />
    public override float? WidthPercentage { get => base.WidthPercentage ?? Parent?.WidthPercentage; set => base.WidthPercentage = value; }

    /// <inheritdoc />
    public override int? Height { get => base.Height ?? Parent?.Height; set => base.Height = value; }

    /// <inheritdoc />
    public override float? HeightPercentage { get => base.HeightPercentage ?? Parent?.HeightPercentage; set => base.HeightPercentage = value; }

    /// <inheritdoc />
    public override int? TopOffset { get => base.TopOffset ?? Parent?.TopOffset; set => base.TopOffset = value; }

    /// <inheritdoc />
    public override int? LeftOffset { get => base.LeftOffset ?? Parent?.LeftOffset; set => base.LeftOffset = value; }

    /// <inheritdoc />
    public override int? Padding { get => base.Padding ?? Parent?.Padding; set => base.Padding = value; }

    /// <inheritdoc />
    public override HorizontalAlign? HorizontalAlign { get => base.HorizontalAlign ?? Parent?.HorizontalAlign; set => base.HorizontalAlign = value; }

    /// <inheritdoc />
    public override VerticalAlign? VerticalAlign { get => base.VerticalAlign ?? Parent?.VerticalAlign; set => base.VerticalAlign = value; }

    /// <inheritdoc />
    public override Position? Position { get => base.Position ?? Parent?.Position; set => base.Position = value; }

    /// <inheritdoc />
    public override NinePatch? NinePatch { get => base.NinePatch ?? Parent?.NinePatch; set => base.NinePatch = value; }

    /// <inheritdoc />
    public override Color? TextColor { get => base.TextColor ?? Parent?.TextColor; set => base.TextColor = value; }

    /// <inheritdoc />
    public override Color? BackgroundColor { get => base.BackgroundColor ?? Parent?.BackgroundColor; set => base.BackgroundColor = value; }

    /// <inheritdoc />
    public override TextAlign? TextAlign { get => base.TextAlign ?? Parent?.TextAlign; set => base.TextAlign = value; }

    /// <inheritdoc />
    public override TextBaseline? TextBaseline { get => base.TextBaseline ?? Parent?.TextBaseline; set => base.TextBaseline = value; }

    /// <inheritdoc />
    public override HeightTemplate? HeightTemplate { get => base.HeightTemplate ?? Parent?.HeightTemplate; set => base.HeightTemplate = value; }

    /// <inheritdoc />
    public override string? Font { get => base.Font ?? Parent?.Font; set => base.Font = value; }

    /// <inheritdoc />
    public override string? TextureFont { get => base.TextureFont ?? Parent?.TextureFont; set => base.TextureFont = value; }

    /// <inheritdoc />
    public override int? TextureFontSize { get => base.TextureFontSize ?? Parent?.TextureFontSize; set => base.TextureFontSize = value; }

    /// All the following properties need to use the parent props and not itself
    /// <inheritdoc />
    public override int GridColumns { get => Parent?.GridColumns ?? 1; set { if (Parent != null) Parent.GridColumns = value; } }

    /// <inheritdoc />
    public override int ColumnSpan { get => Parent?.ColumnSpan ?? 1; set { if (Parent != null) Parent.ColumnSpan = value; } }

    /// <inheritdoc />
    public override int ColumnOffset { get => Parent?.ColumnOffset ?? 0; set { if (Parent != null) Parent.ColumnOffset = value; } }

    /// <inheritdoc />
    public override Point ColumnGap { get => Parent?.ColumnGap ?? Point.Zero; set { if (Parent != null) Parent.ColumnGap = value; } }
  }
}
