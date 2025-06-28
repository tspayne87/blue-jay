using System;
using Microsoft.Xna.Framework;

namespace BlueJay.LDtk.Fields;

public class ColorField : Field
{
  public Color Value { get; }

  public ColorField(string identifier, Color value)
    : base(identifier)
  {
    Value = value;
  }
}
