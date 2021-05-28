using System;
using System.Collections;
using System.Collections.Generic;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.Collections
{
  public class EntityCollection
  {
    private int _entityCount = 0;
    public List<IEntity> _collection = new List<IEntity>();
    private Dictionary<long, List<IEntity>> _entityQueryCache = new Dictionary<long, List<IEntity>>();

    public int Count => _collection.Count;

    public bool IsReadOnly => false;

    public void Add(IEntity item)
    {
      item.Id = ++_entityCount;
      _collection.Add(item);
      UpdateCache(item, CacheInsertType.Add);
    }

    public List<IEntity> GetByKey(long key, bool includeInActive = false)
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

    public bool Remove(IEntity item)
    {
      UpdateCache(item, CacheInsertType.Remove);
      return _collection.Remove(item);
    }

    public void UpdateAddonTree(IEntity item)
    {
      UpdateCache(item, CacheInsertType.Upsert);
    }

    public void Clear()
    {
      var items = _collection.ToArray();
      for (var i = 0; i < items.Length; ++i)
      {
        Remove(items[i]);
      }
    }

    public IEntity this[int i]
    {
      get { return _collection[i]; }
    }

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

    private enum CacheInsertType { Add, Remove, Upsert };

    public object ToArray()
    {
      throw new NotImplementedException();
    }
  }
}