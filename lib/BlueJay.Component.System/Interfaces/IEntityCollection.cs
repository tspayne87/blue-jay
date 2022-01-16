using System.Collections.Generic;

namespace BlueJay.Component.System.Interfaces
{
  public interface IEntityCollection
  {
    /// <summary>
    /// Gets a set of entities by the key
    /// </summary>
    /// <param name="key">The key we want to find entities on</param>
    /// <returns>Will return a list of entities matching the key given</returns>
    List<IEntity> GetByKey(long key);

    /// <summary>
    /// Add an entity to the collection
    /// </summary>
    /// <param name="item">The entity we are adding to the collection</param>
    void Add(IEntity item);

    /// <summary>
    /// Remove an entity from the collection
    /// </summary>
    /// <param name="item">The entity we are trying to remove</param>
    /// <returns>Will return a boolean determining if the entity was removed from the collection</returns>
    bool Remove(IEntity item);

    /// <summary>
    /// Updates the internal cache when an entity changes
    /// </summary>
    /// <param name="item">The entity that has changed</param>
    void UpdateAddonTree(IEntity item);

    /// <summary>
    /// Clear the collection competely
    /// </summary>
    void Clear();

    /// <summary>
    /// Method is meant generate an array from this entity so that we can remove entities without worring about indexes
    /// </summary>
    /// <returns>Will return the generate array</returns>
    IEntity[] ToArray();

    /// <summary>
    /// Operator to get an entity based on the index in the collection
    /// </summary>
    /// <param name="i">The index of the entity we are trying to find</param>
    /// <returns>Will return the entity found at the index given</returns>
    IEntity this[int i] { get; }

    /// <summary>
    /// The current count of this collection
    /// </summary>
    int Count { get; }
  }
}
