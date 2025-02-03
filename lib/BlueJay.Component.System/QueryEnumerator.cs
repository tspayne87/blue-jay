using System.Collections;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System;

/// <summary>
/// Enumerator to go through the entities in the layer collection based on the key given
/// </summary>
internal class QueryEnumerator : IEnumerator<IEntity>
{
  /// <summary>
  /// The internal collection of entities
  /// </summary>
  private readonly ILayers _layers;

  /// <summary>
  /// The key we are looking for in the entities
  /// </summary>
  private readonly AddonKey _key;

  /// <summary>
  /// The current layers we are looking at
  /// </summary>
  private List<string> _currentLayers;

  /// <summary>
  /// The current index of the enumerator
  /// </summary>
  private int _index;

  /// <summary>
  /// The current layer we are looking at
  /// </summary>
  private int _layerIndex;

  /// <summary>
  /// Constructor to build out the enumerator
  /// </summary>
  /// <param name="layers">The internal collection of entities</param>
  /// <param name="key">The key we are looking for in the entities</param>
  /// <param name="filterOnLayers">The layers we are filtering on</param>
  /// <param name="layersToExclude">The layers we are excluding</param>
  public QueryEnumerator(ILayers layers, AddonKey key, List<string>? filterOnLayers, List<string>? layersToExclude)
  {
    _layers = layers;
    _key = key;
    _index = -1;
    _layerIndex = 0;
    _currentLayers = layers
      .Where(x => filterOnLayers == null || filterOnLayers.Count == 0 || filterOnLayers.Contains(x.Id))
      .Where(x => layersToExclude == null || !layersToExclude.Contains(x.Id))
      .OrderBy(x => x.Weight)
      .Select(x => x.Id)
      .ToList();
  }

  /// <inheritdoc />
  public bool MoveNext()
  {
    if (_currentLayers.Count == 0)
      return false;

    while (true)
    {
      ++_index; // Move to the next entity

      // Short circuit if we have gone through all the entities in the layer
      if (_layers[_currentLayers[_layerIndex]]!.Count <= _index)
      {
        _index = 0; // Reset the index to the first entity
        ++_layerIndex; // Move to the next layer
      }

      // Short circuit if we have gone through all the layers
      if (_currentLayers.Count <= _layerIndex)
        return false;

      // If we found a match we break out of the loop, and return true
      var layer = _layers[_currentLayers[_layerIndex]]!;
      if (layer.Count > 0 && layer[_index]!.MatchKey(_key))
        return true;
    }
  }

  /// <inheritdoc />
  public void Reset()
  {
    _currentLayers = _layers.OrderBy(x => x.Weight).Select(x => x.Id).ToList();
    _layerIndex = 0;
    _index = -1;
  }

  /// <inheritdoc />
  public IEntity Current => _layers[_currentLayers[_layerIndex]]![_index]!;

  /// <inheritdoc />
  object IEnumerator.Current => Current;

  /// <inheritdoc />
  public void Dispose() { }
}
