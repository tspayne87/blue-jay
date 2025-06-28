using System;
using BlueJay.LDtk.Fields;

namespace BlueJay.LDtk.Data;

public interface ILDtkFieldInstance
{
  /// <summary>
  /// Converts the field instance to a field object
  /// </summary>
  /// <returns>Will return a field object based on this instance</returns>
  Field AsField();
}
