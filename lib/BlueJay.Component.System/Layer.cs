using BlueJay.Component.System.Interfaces;
using System.Collections;

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
    private List<IEntity> _collection;

    /// <inheritdoc />
    public string Id { get; private set; }

    /// <inheritdoc />
    public int Weight { get; private set; }

    /// <inheritdoc />
    public int Count => _collection.Count;

    /// <inheritdoc />
    public IEntity this[int index] => _collection[index];

    /// <summary>
    /// Constructor to build out the layer
    /// </summary>
    /// <param name="id">The id for the layer</param>
    /// <param name="weight">The current weight of the layer</param>
    public Layer(string id, int weight)
    {
      Id = id;
      Weight = weight;

      _collection = [];
    }

    /// <inheritdoc />
    public void Add(IEntity item)
    {
      _collection.Add(item);
      Sort();
    }

    /// <inheritdoc />
    public void Remove(IEntity item) => _collection.Remove(item);

    /// <inheritdoc />
    public void Clear() => _collection.Clear();

    /// <inheritdoc />
    public IEnumerator<IEntity> GetEnumerator() => _collection.GetEnumerator();

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();

    /// <summary>
    /// Method to sort the collection based on the weight of the entities
    /// </summary>
    private void Sort()
    {
      _collection = _collection.OrderBy(x => x.Weight).ToList();
    }
  }
}
