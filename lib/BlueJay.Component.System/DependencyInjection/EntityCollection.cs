using System.Collections;
using System.Collections.Generic;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.DependencyInjection
{
  public class EntityCollection : IEntityCollection
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

    public void Clear()
    {
      _collection.Clear();
      _entityQueryCache.Clear();
    }

    public void CopyTo(IEntity[] array, int arrayIndex)
    {
      _collection.CopyTo(array, arrayIndex);

      foreach(var item in array)
        UpdateCache(item, CacheInsertType.Add);
    }

    public IEnumerable<IEntity> GetByKey(long key, bool includeInActive = false)
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

    public bool Contains(IEntity item) => _collection.Contains(item);
    public IEnumerator<IEntity> GetEnumerator() => _collection.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();

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
  }
}