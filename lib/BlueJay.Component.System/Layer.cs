using BlueJay.Component.System.Interfaces;
using System.Collections;
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
    private Dictionary<AddonKey, List<IEntity>> _entityQueryCache = new Dictionary<AddonKey, List<IEntity>>();

    /// <summary>
    /// The current entity count this is mainly used as a way to store ids for the entities
    /// </summary>
    private int _entityCount = 0;

    /// <summary>
    /// Helper method meant to track when a sort should be triggered so that if a bunch of Weight changes
    /// for entities happen on the same frame the sorting is minimized to the smallest amount of sorts
    /// </summary>
    private bool _triggerSort;

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
        SortLayer();
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
    public Layer(string id, int weight)
    {
      Id = id;
      Weight = weight;

      _triggerSort = false;
    }

    /// <inheritdoc />
    public ReadOnlySpan<IEntity> GetByKey(AddonKey key)
    {
      SortLayer();
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
    public int IndexOf(IEntity item)
    {
      SortLayer();
      return _collection.IndexOf(item);
    }

    /// <inheritdoc />
    public void Insert(int index, IEntity item)
    {
      _collection.Insert(index, item);
      UpdateCache(item, CacheInsertType.Add);
      SortEntities();
    }

    /// <inheritdoc />
    public void RemoveAt(int index)
    {
      var entity = this[index];
      _collection.RemoveAt(index);
      UpdateCache(entity, CacheInsertType.Remove);
    }

    /// <inheritdoc />
    public void Add(IEntity item)
    {
      item.Id = ++_entityCount;
      _collection.Add(item);
      UpdateCache(item, CacheInsertType.Add);
      SortEntities();
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
      SortEntities();
    }

    /// <inheritdoc />
    public bool Remove(IEntity item)
    {
      UpdateCache(item, CacheInsertType.Remove);
      return _collection.Remove(item);
    }

    /// <inheritdoc />
    public IEnumerator<IEntity> GetEnumerator()
    {
      SortLayer();
      return _collection.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
      SortLayer();
      return _collection.GetEnumerator();
    }

    /// <inheritdoc />
    public ReadOnlySpan<IEntity> AsSpan()
    {
      SortLayer();
      return CollectionsMarshal.AsSpan(_collection);
    }

    /// <inheritdoc />
    public void UpdateAddonTree(IEntity item) => UpdateCache(item, CacheInsertType.Upsert);

    /// <inheritdoc />
    public void SortEntities()
    {
      _triggerSort = true;
    }

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
    /// Helper method meant to sort the internal entity collections and cache based on if a trigger for the sort has been
    /// set, method is meant to be called a bunch but should only sort if the weight has been triggered for a sort to occur
    /// </summary>
    private void SortLayer()
    {
      if (_triggerSort)
      {
        _triggerSort = false;
        _collection = _collection.OrderBy(x => x.Weight).ToList();
        foreach (var item in _entityQueryCache.ToArray())
          _entityQueryCache[item.Key] = item.Value.OrderBy(x => x.Weight).ToList();
      }
    }

    /// <summary>
    /// Internal enumeration to determine what type of cache update 
    /// </summary>
    private enum CacheInsertType { Add, Remove, Upsert };
  }
}
