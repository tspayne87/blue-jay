using BlueJay.Component.System.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Collections;

namespace BlueJay.Component.System
{
  /// <summary>
  /// The implmentation of the layer collections
  /// </summary>
  internal class Layers : ILayers
  {
    /// <summary>
    /// Service provider needed to generate layers
    /// </summary>
    private readonly IServiceProvider _provider;

    /// <summary>
    /// The internal layer collection we are working with
    /// </summary>
    private List<ILayer> _collection;

    /// <summary>
    /// The current size of the collection
    /// </summary>
    private int _size;

    /// <inheritdoc />
    public int Count => _size;

    /// <inheritdoc />
    public ILayer? this[string id] => _collection.FirstOrDefault(x => x.Id == id);

    /// <summary>
    /// Constructor meant to inject various services into this collection
    /// </summary>
    /// <param name="provider">The service provider we need to create layers</param>
    public Layers(IServiceProvider provider)
    {
      _provider = provider;
      _collection = [];
    }

    /// <inheritdoc />
    public void Add(IEntity entity, string layer = "", int weight = 0)
    {
      var item = this[layer];
      if (item == null)
      {
        item = ActivatorUtilities.CreateInstance<Layer>(_provider, [layer, weight]);
        Add(item);
      }
      item.Add(entity);
    }

    /// <inheritdoc />
    public void Add(string layer, int weight = 0)
    {
      if (!Contains(layer))
        Add(ActivatorUtilities.CreateInstance<Layer>(_provider, [layer, weight]));
    }

    /// <inheritdoc />
    public void Remove(IEntity entity)
    {
      if (Contains(entity.Layer))
        this[entity.Layer]?.Remove(entity);
    }

    /// <inheritdoc />
    public void Clear()
    {
      for (var i = 0; i < _size; i++)
        _collection[i]?.Clear();
    }

    /// <inheritdoc />
    public bool Contains(string layer) => _collection.Any(x => x?.Id == layer);

    /// <inheritdoc />
    public IEnumerator<ILayer> GetEnumerator() => _collection.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();

    /// <summary>
    /// Method to add a layer to the collection
    /// </summary>
    /// <param name="item">The current item that is being added to the collection</param>
    private void Add(ILayer item) => _collection.Add(item);
  }
}
