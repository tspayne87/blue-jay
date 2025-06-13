using System;

namespace BlueJay.LDtk.Fields;

public class IntegerField : Field
{
  public int Value { get; }

  public IntegerField(string identifier, int value)
    : base(identifier)
  {
    Value = value;
  }
}
