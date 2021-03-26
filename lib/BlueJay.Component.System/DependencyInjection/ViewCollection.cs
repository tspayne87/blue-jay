using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.DependencyInjection
{
  public class ViewCollection : IViewCollection
  {
    private List<IView> _collection = new List<IView>();

    public IView Current { get; private set; }

    public int Count => _collection.Count;

    public bool IsReadOnly => false;

    public void Add(IView item)
    {
      _collection.Add(item);
      if (Current == null) Current = item;
    }

    public void Clear()
    {
      _collection.Clear();
      Current = null;
    }

    public bool Remove(IView item)
    {
      var result = _collection.Remove(item);
      if (result && item == Current) {
        Current = Count == 0 ? null : _collection[0];
      }
      return result;
    }

    public void SetCurrent<T>() where T : IView
    {
      var item = _collection.FirstOrDefault(x => x.GetType() == typeof(T));
      if (item != null)
        Current = item;
    }
    
    public bool Contains(IView item) => _collection.Contains(item);

    public void CopyTo(IView[] array, int arrayIndex) => _collection.CopyTo(array, arrayIndex);

    public IEnumerator<IView> GetEnumerator() => _collection.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _collection.GetEnumerator();
  }
}