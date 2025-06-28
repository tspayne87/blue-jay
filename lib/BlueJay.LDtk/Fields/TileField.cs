using System;
using BlueJay.Core.Container;
using Microsoft.Xna.Framework;

namespace BlueJay.LDtk.Fields;

public class TileField : Field
{
  public ITexture2DContainer Texture { get; }
  public Rectangle SourceRectangle { get; }

  public TileField(string identifier, ITexture2DContainer texture, Rectangle sourceRectangle)
    : base(identifier)
  {
    Texture = texture ?? throw new ArgumentNullException(nameof(texture), "Texture cannot be null");
    SourceRectangle = sourceRectangle;
  }
}
