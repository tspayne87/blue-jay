using System;

namespace BlueJay.LDtk.Fields;

public class Field
{
  public string Identifier { get; }

  public Field(string identifier)
  {
    Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier), "Identifier cannot be null");
  }
}
