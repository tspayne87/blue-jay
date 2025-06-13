using System;

namespace BlueJay.LDtk.Data;

public interface ILDtkWorld
{
  /// <summary>
  /// Gets a list of levels in the world.
  /// </summary>
  IEnumerable<ILDtkLevel> Levels { get; }

  /// <summary>
  /// Gets the level by its unique identifier.
  /// This identifier is typically a string that uniquely identifies the level within the world.
  /// </summary>
  /// <param name="identifier">The identifier for a level</param>
  /// <returns>Will return a level with a unique identifier</returns>
  ILDtkLevel? GetLevelByIdentifier(string identifier);
}
