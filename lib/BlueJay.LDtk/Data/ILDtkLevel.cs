namespace BlueJay.LDtk.Data;

public interface ILDtkLevel
{
  /// <summary>
  /// Gets the list of layers found in the level
  /// </summary>
  IEnumerable<ILDtkLayerInstance> Layers { get; }

  /// <summary>
  /// Gets the layer based on its unique identifier.
  /// </summary>
  /// <param name="identifier">The identifier for a layer</param>
  /// <returns>Will return a layer with a unique identifier</returns>
  ILDtkLayerInstance? GetLayerByIdentifier(string identifier);
}
