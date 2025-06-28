using System;

namespace BlueJay.LDtk.Fields;

public class BooleanField : Field
{
  public bool Value { get; }

  public BooleanField(string Identifer,  bool value)
    : base(Identifer)
  {
    Value = value;
  }
}
