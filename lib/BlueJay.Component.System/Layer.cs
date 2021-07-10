﻿using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System
{
  /// <summary>
  /// The implementation of the layer interface
  /// </summary>
  internal class Layer : ILayer
  {
    /// <inheritdoc />
    public EntityCollection Entities { get; private set; }

    /// <inheritdoc />
    public string Id { get; private set; }

    /// <inheritdoc />
    public int Weight { get; private set; }

    /// <summary>
    /// Constructor to build out the layer
    /// </summary>
    /// <param name="id">The id for the layer</param>
    /// <param name="weight">The current weight of the layer</param>
    public Layer(string id, int weight)
    {
      Id = id;
      Weight = weight;
      Entities = new EntityCollection();
    }
  }
}
