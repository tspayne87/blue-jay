using BlueJay.Component.System.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace BlueJay.Component.System.Collections
{
  /// <summary>
  /// The implmentation of the layer collections
  /// </summary>
  internal class LayerCollection : ILayerCollection
  {
    /// <summary>
    /// Service provider needed to generate layers
    /// </summary>
    private readonly IServiceProvider _provider;

    /// <summary>
    /// The internal layer collection we are working with
    /// </summary>
    private List<ILayer> _collection;

    /// <inheritdoc />
    public int Count => _collection.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <inheritdoc />
    ILayer IList<ILayer>.this[int index] { get => _collection[index]; set => _collection[index] = value; }

    /// <summary>
    /// Constructor meant to inject various services into this collection
    /// </summary>
    /// <param name="provider">The service provider we need to create layers</param>
    public LayerCollection(IServiceProvider provider)
    {
      _provider = provider;
      _collection = new List<ILayer>();
    }

    /// <inheritdoc />
    public void Add(IEntity entity, string layer = "", int weight = 0)
    {
      var item = this[layer];
      if (item == null)
      {
        item = ActivatorUtilities.CreateInstance<Layer>(_provider, new object[] { layer, weight });
        _collection.Add(item);
        Sort();
      }

      item.Add(entity);
    }

    /// <inheritdoc />
    public void Add(string layer, int weight = 0)
    {
      if (!Contains(layer))
      {
        _collection.Add(ActivatorUtilities.CreateInstance<Layer>(_provider, new object[] { layer, weight }));
        Sort();
      }
    }

    /// <inheritdoc />
    public void Remove(IEntity entity)
    {
      if (Contains(entity.Layer))
      {
        this[entity.Layer]?.Remove(entity);
      }
    }

    /// <inheritdoc />
    public bool Contains(string layer) => _collection.Any(x => x.Id == layer);

    /// <inheritdoc />
    public ReadOnlySpan<ILayer> AsSpan() => CollectionsMarshal.AsSpan(_collection);

    /// <inheritdoc />
    public int IndexOf(ILayer item) => _collection.IndexOf(item);

    /// <inheritdoc />
    public void Insert(int index, ILayer item) => _collection.Insert(index, item);

    /// <inheritdoc />
    public void RemoveAt(int index) => _collection.RemoveAt(index);

    /// <inheritdoc />
    public void Add(ILayer item)
    {
      _collection.Add(item);
      Sort();
    }

    /// <inheritdoc />
    public void Clear() => _collection.Clear();

    /// <inheritdoc />
    public bool Contains(ILayer item) => _collection.Contains(item);

    /// <inheritdoc />
    public void CopyTo(ILayer[] array, int arrayIndex) => _collection.CopyTo(array, arrayIndex);

    /// <inheritdoc />
    public bool Remove(ILayer item) => _collection.Remove(item);

    /// <inheritdoc />
    public IEnumerator<ILayer> GetEnumerator() => _collection.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();

    /// <inheritdoc />
    public ILayer? this[string id]
    {
      get
      {
        foreach (var layer in AsSpan())
          if (layer.Id == id)
            return layer;
        return null;
      }
    }

    /// <summary>
    /// Calls when we need to sort the collections so they are processed in the correct order
    /// </summary>
    private void Sort()
    {
      _collection = _collection.OrderBy(x => x.Weight).ToList();
    }
  }
}
