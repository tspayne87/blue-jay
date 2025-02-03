namespace BlueJay.Component.System.Interfaces
{
  /// <summary>
  /// The layer that an set of entities are on
  /// </summary>
  public interface ILayer : IEnumerable<IEntity>
  {
    /// <summary>
    /// The current id of the layer
    /// </summary>
    string Id { get; }

    /// <summary>
    /// The current weight of the layer to determine when it will be processed by the system
    /// </summary>
    int Weight { get; }

    /// <summary>
    /// Gets the current count of the entities in the layer
    /// </summary>
    int Count { get; }

    /// <summary>
    /// Get an entity based on the index of the collection
    /// </summary>
    /// <param name="id">The current id to get the current t</param>
    /// <returns>Gets the current entity</returns>
    IEntity? this[int id] { get; }

    /// <summary>
    /// Adds an entity to the layer
    /// </summary>
    /// <param name="item">The entity to add to the layer</param>
    void Add(IEntity item);

    /// <summary>
    /// Removes an entity from the layer
    /// </summary>
    /// <param name="item">The entity to remove from the layer</param>
    void Remove(IEntity item);

    /// <summary>
    /// Clears all entities from the layer
    /// </summary>
    void Clear();
  }
}
