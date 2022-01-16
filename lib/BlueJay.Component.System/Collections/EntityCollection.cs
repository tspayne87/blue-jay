using System.Collections.Generic;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.Collections
{
  internal class EntityCollection : IEntityCollection
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

    /// <inheritdoc />
    public int Count => _collection.Count;

    /// <inheritdoc />
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

    /// <inheritdoc />
    public void Add(IEntity item)
    {
      item.Id = ++_entityCount;
      _collection.Add(item);
      UpdateCache(item, CacheInsertType.Add);
    }

    /// <inheritdoc />
    public bool Remove(IEntity item)
    {
      UpdateCache(item, CacheInsertType.Remove);
      return _collection.Remove(item);
    }

    /// <inheritdoc />
    public void UpdateAddonTree(IEntity item)
    {
      UpdateCache(item, CacheInsertType.Upsert);
    }

    /// <inheritdoc />
    public void Clear()
    {
      var items = _collection.ToArray();
      for (var i = 0; i < items.Length; ++i)
      {
        Remove(items[i]);
      }
    }

    /// <inheritdoc />
    public IEntity[] ToArray()
    {
      return _collection.ToArray();
    }

    /// <inheritdoc />
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