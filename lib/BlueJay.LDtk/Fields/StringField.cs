using System;

namespace BlueJay.LDtk.Fields;

public class StringField : Field
{
  public string Value { get; }

  public StringField(string identifier, string value)
    : base(identifier)
  {
    Value = value ?? throw new ArgumentNullException(nameof(value), "Value cannot be null");
  }
}
