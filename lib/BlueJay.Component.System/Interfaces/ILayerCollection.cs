using System;

namespace BlueJay.Component.System.Interfaces
{
  public interface ILayerCollection
  {
    /// <summary>
    /// Add an entity to a layer based on the layer type
    /// </summary>
    /// <param name="entity">The entity we are currently adding</param>
    /// <param name="layer">The layer we are working with</param>
    /// <param name="weight">The weight of this layer so it is ordered correctly</param>
    void AddEntity(IEntity entity, string layer = "", int weight = 0);

    /// <summary>
    /// Remove an entity from the layer collection based on where the entity layer it currently exists on
    /// </summary>
    /// <param name="entity">The entity we need to remove</param>
    void RemoveEntity(IEntity entity);

    /// <summary>
    /// Add a layer to the collection without including a entity, however will not add the same layer to the collection
    /// </summary>
    /// <param name="layer">The layer we are working with</param>
    /// <param name="weight">The weight of this layer so it is ordered correctly</param>
    void Add(string layer, int weight = 0);

    /// <summary>
    /// Method to determine if a layer exists in the collection
    /// </summary>
    /// <param name="layer">The layer we are currently looking for</param>
    /// <returns>Will return true or false based on if the layer exists in the collection</returns>
    bool Contains(string layer);

    /// <summary>
    /// Overloaded operator to get a location in the collection
    /// </summary>
    /// <param name="i">The i value we are currently looking for</param>
    /// <returns>Will return the layer at the specific location</returns>
    /// <exception cref="ArgumentOutOfRangeException">If a number was out of range of the collection</exception>
    ILayer this[int i] { get; }

    /// <summary>
    /// Overloaded operator to get a layer based on the id of the collection
    /// </summary>
    /// <param name="id">The id of the layer we are looking for</param>
    /// <returns>Will return the layer with the specific id</returns>
    ILayer this[string id] { get; }

    /// <summary>
    /// The current count for this layer collection
    /// </summary>
    int Count { get; }
  }
}
