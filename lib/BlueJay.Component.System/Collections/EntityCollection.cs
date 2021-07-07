using System;
using System.Collections;
using System.Collections.Generic;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.Collections
{
  public class EntityCollection
  {
    /// <summary>
    /// The current entity count this is mainly used as a way to store ids for the entities
    /// </summary>
    private int _entityCount = 0;

    /// <summary>
    /// The current collection of entities in the system
    /// </summary>
    private List<IEntity> _collection = new List<IEntity>();

    /// <summary>
    /// The current cache for entities so w
    /// </summary>
    private Dictionary<long, List<IEntity>> _entityQueryCache = new Dictionary<long, List<IEntity>>();

    /// <summary>
    /// The current count of this collection
    /// </summary>
    public int Count => _collection.Count;

    /// <summary>
    /// Gets a set of entities by the key
    /// </summary>
    /// <param name="key">The key we want to find entities on</param>
    /// <returns>Will return a list of entities matching the key given</returns>
    public List<IEntity> GetByKey(long key)
    {
      if (!_entityQueryCache.ContainsKey(key))
      {
        _entityQueryCache[key] = new List<IEntity>();
        foreach(var item in _collection)
        {
          if (item.MatchKey(key))
            _entityQueryCache[key].Add(item);
        }
      }

      return _entityQueryCache[key];
    }

    /// <summary>
    /// Add an entity to the collection
    /// </summary>
    /// <param name="item">The entity we are adding to the collection</param>
    public void Add(IEntity item)
    {
      item.Id = ++_entityCount;
      _collection.Add(item);
      UpdateCache(item, CacheInsertType.Add);
    }

    /// <summary>
    /// Remove an entity from the collection
    /// </summary>
    /// <param name="item">The entity we are trying to remove</param>
    /// <returns>Will return a boolean determining if the entity was removed from the collection</returns>
    public bool Remove(IEntity item)
    {
      UpdateCache(item, CacheInsertType.Remove);
      return _collection.Remove(item);
    }

    /// <summary>
    /// Updates the internal cache when an entity changes
    /// </summary>
    /// <param name="item">The entity that has changed</param>
    public void UpdateAddonTree(IEntity item)
    {
      UpdateCache(item, CacheInsertType.Upsert);
    }

    /// <summary>
    /// Clear the collection competely
    /// </summary>
    public void Clear()
    {
      var items = _collection.ToArray();
      for (var i = 0; i < items.Length; ++i)
      {
        Remove(items[i]);
      }
    }

    /// <summary>
    /// Method is meant generate an array from this entity so that we can remove entities without worring about indexes
    /// </summary>
    /// <returns>Will return the generate array</returns>
    public IEntity[] ToArray()
    {
      return _collection.ToArray();
    }

    /// <summary>
    /// Operator to get an entity based on the index in the collection
    /// </summary>
    /// <param name="i">The index of the entity we are trying to find</param>
    /// <returns>Will return the entity found at the index given</returns>
    public IEntity this[int i] => _collection[i];

    /// <summary>
    /// Helper method is meant to update the internal cache for querying the collection
    /// </summary>
    /// <param name="item">The item that needs updating in the cache</param>
    /// <param name="type">The type of update we are dealing with</param>
    private void UpdateCache(IEntity item, CacheInsertType type)
    {
      foreach(var cache in _entityQueryCache)
      {
        if (type != CacheInsertType.Add)
          cache.Value.Remove(item);

        if (type != CacheInsertType.Remove && item.MatchKey(cache.Key))
          cache.Value.Add(item);
      }
    }

    /// <summary>
    /// Internal enumeration to determine what type of cache update 
    /// </summary>
    private enum CacheInsertType { Add, Remove, Upsert };
  }
}