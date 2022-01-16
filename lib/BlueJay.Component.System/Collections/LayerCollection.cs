using BlueJay.Component.System.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.Component.System.Collections
{
  internal class LayerCollection : ILayerCollection
  {
    private readonly IServiceProvider _provider;

    /// <summary>
    /// The internal layer collection we are working with
    /// </summary>
    private List<ILayer> _collection = new List<ILayer>();

    /// <inheritdoc />
    public int Count => _collection.Count;

    /// <summary>
    /// Constructor meant to inject various services into this collection
    /// </summary>
    /// <param name="provider">The service provider we need to create layers</param>
    public LayerCollection(IServiceProvider provider)
    {
      _provider = provider;
    }

    /// <inheritdoc />
    public void AddEntity(IEntity entity, string layer = "", int weight = 0)
    {
      var item = this[layer];
      if (item == null)
      {
        item = ActivatorUtilities.CreateInstance<Layer>(_provider, new object[] { layer, weight });
        _collection.Add(item);
        Sort();
      }

      item.Entities.Add(entity);
    }

    /// <inheritdoc />
    public void RemoveEntity(IEntity entity)
    {
      if (Contains(entity.Layer))
      {
        this[entity.Layer].Entities.Remove(entity);
      }
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
    public bool Contains(string layer)
    {
      return this[layer] != null;
    }

    /// <inheritdoc />
    public ILayer this[int i]
    {
      get { return _collection[i]; }
    }

    /// <inheritdoc />
    public ILayer this[string id]
    {
      get
      {
        for (var i = 0; i < _collection.Count; ++i)
          if (_collection[i].Id == id)
            return _collection[i];
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
