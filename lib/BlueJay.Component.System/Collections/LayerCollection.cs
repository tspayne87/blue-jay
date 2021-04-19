using BlueJay.Component.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.Component.System.Collections
{
  public class LayerCollection
  {
    /// <summary>
    /// The internal layer collection we are working with
    /// </summary>
    private List<ILayer> _collection = new List<ILayer>();

    /// <summary>
    /// The current count for this layer collection
    /// </summary>
    public int Count => _collection.Count;

    /// <summary>
    /// Add an entity to a layer based on the layer type
    /// </summary>
    /// <param name="entity">The entity we are currently adding</param>
    /// <param name="layer">The layer we are working with</param>
    /// <param name="weight">The weight of this layer so it is ordered correctly</param>
    public void AddEntity(IEntity entity, string layer = "", int weight = 0)
    {
      var item = this[layer];
      if (item == null)
      {
        item = new Layer(layer, weight);
        _collection.Add(item);
        Sort();
      }

      item.Entities.Add(entity);
    }

    /// <summary>
    /// Add a layer to the collection without including a entity, however will not add the same layer to the collection
    /// </summary>
    /// <param name="layer">The layer we are working with</param>
    /// <param name="weight">The weight of this layer so it is ordered correctly</param>
    public void Add(string layer, int weight = 0)
    {
      if (!Contains(layer))
      {
        _collection.Add(new Layer(layer, weight));
        Sort();
      }
    }

    /// <summary>
    /// Method to determine if a layer exists in the collection
    /// </summary>
    /// <param name="layer">The layer we are currently looking for</param>
    /// <returns>Will return true or false based on if the layer exists in the collection</returns>
    public bool Contains(string layer)
    {
      return this[layer] != null;
    }

    private void Sort()
    {
      _collection = _collection.OrderBy(x => x.Weight).ToList();
    }
    
    /// <summary>
    /// Overloaded operator to get a location in the collection
    /// </summary>
    /// <param name="i">The i value we are currently looking for</param>
    /// <returns>Will return the layer at the specific location</returns>
    /// <exception cref="ArgumentOutOfRangeException">If a number was out of range of the collection</exception>
    public ILayer this[int i]
    {
      get { return _collection[i]; }
    }

    /// <summary>
    /// Overloaded operator to get a layer based on the id of the collection
    /// </summary>
    /// <param name="id">The id of the layer we are looking for</param>
    /// <returns>Will return the layer with the specific id</returns>
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
  }
}
