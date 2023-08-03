using BlueJay.Component.System.Collections;
using System;
using System.Collections;
using System.Collections.Generic;

namespace BlueJay.Component.System.Interfaces
{
  /// <summary>
  /// The layer that an set of entities are on
  /// </summary>
  public interface ILayer : IList<IEntity>
  {
    /// <summary>
    /// Gets a set of entities by the key
    /// </summary>
    /// <param name="key">The key we want to find entities on</param>
    /// <returns>Will return a list of entities matching the key given</returns>
    ReadOnlySpan<IEntity> GetByKey(long key);

    /// <summary>
    /// The current id of the layer
    /// </summary>
    string Id { get; }

    /// <summary>
    /// The current weight of the layer to determine when it will be processed by the system
    /// </summary>
    int Weight { get; }

    /// <summary>
    /// Helper meant that returns a read only span
    /// </summary>
    /// <returns>Will return a read only span for the collection</returns>
    ReadOnlySpan<IEntity> AsSpan();

    /// <summary>
    /// Updates the internal cache when an entity changes
    /// </summary>
    /// <param name="item">The entity that has changed</param>
    internal void UpdateAddonTree(IEntity item);

    /// <summary>
    /// Helper method meant to sort this layers entities based on their weight from lowest to highest
    /// </summary>
    internal void SortEntities();
  }
}
