using System;
using BlueJay.LDtk.Fields;

namespace BlueJay.LDtk.Data;

public interface ILDtkEntityInstance
{
  /// <summary>
  /// Retrieves the field data from the entity instance and loads various field types based on how they are defined in the LDtk project.
  /// </summary>
  /// <returns>Will return a list of fields found for the entity instance</returns>
  IEnumerable<Field> Fields { get; }
}
