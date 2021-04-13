using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BlueJay.Interfaces;

namespace BlueJay.Views
{
  /// <summary>
  /// The collection of views we are working with
  /// </summary>
  public class ViewCollection : IViewCollection
  {
    /// <summary>
    /// The list of collections so we can switch between them
    /// </summary>
    private List<IView> _collection = new List<IView>();

    /// <summary>
    /// The current rendering view
    /// </summary>
    public IView Current { get; private set; }

    /// <summary>
    /// The number of views that have been added to the collection
    /// </summary>
    public int Count => _collection.Count;

    /// <summary>
    /// Is this collection read only
    /// </summary>
    public bool IsReadOnly => false;

    /// <summary>
    /// Method is meant to add a view to the collection and if it is the first one set the current
    /// </summary>
    /// <param name="item">The view we are adding</param>
    public void Add(IView item)
    {
      _collection.Add(item);
      if (Current == null) Current = item;
    }

    /// <summary>
    /// Method is meant to clear the collection and null out the current
    /// </summary>
    public void Clear()
    {
      _collection.Clear();
      Current = null;
    }

    /// <summary>
    /// Method is meant to remove the item from the collection and update the current to what is needed
    /// </summary>
    /// <param name="item">The item we are removing</param>
    /// <returns>If the item was removed</returns>
    public bool Remove(IView item)
    {
      var result = _collection.Remove(item);
      if (result && item == Current) {
        Current = Count == 0 ? null : _collection[0];
      }
      return result;
    }

    /// <summary>
    /// Helper method is meant to set the current view we are working with
    /// </summary>
    /// <typeparam name="T">The view we want to find</typeparam>
    public void SetCurrent<T>() where T : IView
    {
      var item = _collection.FirstOrDefault(x => x.GetType() == typeof(T));
      if (item != null)
        Current = item;
    }
    
    /// <summary>
    /// If the collection contains the item
    /// </summary>
    /// <param name="item">The item to check with</param>
    /// <returns>True if the item exists in the collection</returns>
    public bool Contains(IView item) => _collection.Contains(item);

    /// <summary>
    /// Method is meant to copy the contents of the current collection into this one
    /// </summary>
    /// <param name="array">The array we want to copy to</param>
    /// <param name="arrayIndex">The index we want to start at</param>
    public void CopyTo(IView[] array, int arrayIndex) => _collection.CopyTo(array, arrayIndex);

    /// <summary>
    /// Gets the enumerator for this collection
    /// </summary>
    /// <returns>Will return the enumerator for this collection</returns>
    public IEnumerator<IView> GetEnumerator() => _collection.GetEnumerator();

    /// <summary>
    /// Gets the enumerator for this collection
    /// </summary>
    /// <returns>Will return the enumerator for this collection</returns>
    IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();
  }
}