using System;
using Microsoft.Xna.Framework;

namespace BlueJay.LDtk.Fields;

public class PointField : Field
{
  public Point Value { get; }

  public PointField(string identifier, Point value)
    : base(identifier)
  {
    Value = value;
  }
}
