using System;
using BlueJay.Core.Container;
using Microsoft.Xna.Framework;

namespace BlueJay.LDtk.Fields;

public class EnumField : Field
{
  public string Enum { get; }
  public ITexture2DContainer? Texture { get; }
  public Rectangle? SourceRectangle { get; }

  public EnumField(string identifier, string enumValue, ITexture2DContainer? texture = null, Rectangle? sourceRectangle = null)
    : base(identifier)
  {
    Enum = enumValue ?? throw new ArgumentNullException(nameof(enumValue), "Enum value cannot be null");
    Texture = texture;
    SourceRectangle = sourceRectangle;
  }
}
