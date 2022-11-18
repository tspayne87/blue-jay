using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BlueJay.Component.System
{
  /// <summary>
  /// The implementation of the layer interface
  /// </summary>
  internal class Layer : ILayer
  {
    /// <summary>
    /// The current collection of entities in the system
    /// </summary>
    private List<IEntity> _collection = new List<IEntity>();

    /// <summary>
    /// The current cache for entities so w
    /// </summary>
    private Dictionary<long, List<IEntity>> _entityQueryCache = new Dictionary<long, List<IEntity>>();

    /// <summary>
    /// The current entity count this is mainly used as a way to store ids for the entities
    /// </summary>
    private int _entityCount = 0;

    /// <inheritdoc />
    public string Id { get; private set; }

    /// <inheritdoc />
    public int Weight { get; private set; }

    /// <inheritdoc />
    public int Count => _collection.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    public IEntity this[int index]
    {
      get => _collection[index];
      set
      {
        if (_collection.Count > index)
          UpdateCache(_collection[index], CacheInsertType.Remove);
        _collection[index] = value;
        UpdateCache(_collection[index], CacheInsertType.Add);
      }
    }

    /// <summary>
    /// Constructor to build out the layer
    /// </summary>
    /// <param name="id">The id for the layer</param>
    /// <param name="weight">The current weight of the layer</param>
    public Layer(string id, int weight, IServiceProvider provider)
    {
      Id = id;
      Weight = weight;
    }

    /// <inheritdoc />
    public ReadOnlySpan<IEntity> GetByKey(long key)
    {
      if (!_entityQueryCache.ContainsKey(key))
      {
        _entityQueryCache[key] = new List<IEntity>();
        foreach (var item in CollectionsMarshal.AsSpan(_collection))
        {
          if (item.MatchKey(key))
            _entityQueryCache[key].Add(item);
        }
      }

      return CollectionsMarshal.AsSpan(_entityQueryCache[key]);
    }

    /// <inheritdoc />
    public int IndexOf(IEntity item) => _collection.IndexOf(item);

    /// <inheritdoc />
    public void Insert(int index, IEntity item) => _collection.Insert(index, item);

    /// <inheritdoc />
    public void RemoveAt(int index) => _collection.RemoveAt(index);

    /// <inheritdoc />
    public void Add(IEntity item)
    {
      item.Id = ++_entityCount;
      _collection.Add(item);
      UpdateCache(item, CacheInsertType.Add);
    }

    /// <inheritdoc />
    public void Clear()
    {
      _collection.Clear();
      _entityQueryCache.Clear();
    }

    /// <inheritdoc />
    public bool Contains(IEntity item) => _collection.Contains(item);

    /// <inheritdoc />
    public void CopyTo(IEntity[] array, int arrayIndex)
    {
      _collection.CopyTo(array, arrayIndex);
      foreach (var item in new Span<IEntity>(array))
        UpdateCache(item, CacheInsertType.Add);
    }

    /// <inheritdoc />
    public bool Remove(IEntity item)
    {
      UpdateCache(item, CacheInsertType.Remove);
      return _collection.Remove(item);
    }

    /// <inheritdoc />
    public IEnumerator<IEntity> GetEnumerator() => _collection.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();

    /// <inheritdoc />
    public ReadOnlySpan<IEntity> AsSpan() => CollectionsMarshal.AsSpan(_collection);

    /// <inheritdoc />
    public void UpdateAddonTree(IEntity item) => UpdateCache(item, CacheInsertType.Upsert);

    /// <summary>
    /// Helper method is meant to update the internal cache for querying the collection
    /// </summary>
    /// <param name="item">The item that needs updating in the cache</param>
    /// <param name="type">The type of update we are dealing with</param>
    private void UpdateCache(IEntity item, CacheInsertType type)
    {
      foreach (var cache in _entityQueryCache)
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
