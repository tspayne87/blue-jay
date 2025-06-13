using System;

namespace BlueJay.LDtk.Fields;

public class FloatField : Field
{
  public float Value { get; }

  public FloatField(string identifier, float value)
    : base(identifier)
  {
    Value = value;
  }
}
