using System;

namespace BlueJay.LDtk.Fields;

public class MultilinesField : Field
{
  public string[] Lines { get; }

  public MultilinesField(string identifier, string[] lines)
    : base(identifier)
  {
    Lines = lines ?? throw new ArgumentNullException(nameof(lines), "Lines cannot be null");
  }
}
