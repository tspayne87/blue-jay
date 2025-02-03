namespace BlueJay.Component.System.Interfaces
{
  internal interface ILayers : IEnumerable<ILayer>
  {
    /// <summary>
    /// Add an entity to a layer based on the layer type
    /// </summary>
    /// <param name="entity">The entity we are currently adding</param>
    /// <param name="layer">The layer we are working with</param>
    /// <param name="weight">The weight of this layer so it is ordered correctly</param>
    void Add(IEntity entity, string layer = "", int weight = 0);

    /// <summary>
    /// Add a layer to the collection without including a entity, however will not add the same layer to the collection
    /// </summary>
    /// <param name="layer">The layer we are working with</param>
    /// <param name="weight">The weight of this layer so it is ordered correctly</param>
    void Add(string layer, int weight = 0);

    /// <summary>
    /// Remove an entity from the layer collection based on where the entity layer it currently exists on
    /// </summary>
    /// <param name="entity">The entity we need to remove</param>
    void Remove(IEntity entity);

    /// <summary>
    /// Method to determine if a layer exists in the collection
    /// </summary>
    /// <param name="layer">The layer we are currently looking for</param>
    /// <returns>Will return true or false based on if the layer exists in the collection</returns>
    bool Contains(string layer);

    /// <summary>
    /// Overloaded operator to get a layer based on the id of the collection
    /// </summary>
    /// <param name="id">The id of the layer we are looking for</param>
    /// <returns>Will return the layer with the specific id</returns>
    ILayer? this[string id] { get; }

    /// <summary>
    /// Clear all layers from the collection
    /// </summary>
    void Clear();
  }
}
