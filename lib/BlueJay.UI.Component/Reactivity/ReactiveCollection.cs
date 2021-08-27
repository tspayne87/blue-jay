using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace BlueJay.UI.Component.Reactivity
{
  public class ReactiveCollection<T> : IReactiveProperty, IList<T>
  {
    private readonly List<T> _list;
    private bool _notify;
    public event PropertyChangedEventHandler PropertyChanged;

    public int Count => _list.Count;
    public bool IsReadOnly => false;

    /// <summary>
    /// The value for this property
    /// </summary>
    public List<T> Value
    {
      get => _list;
      set
      {
        if (!_list.Equals(value))
        {
          _notify = false;
          _list.Clear();
          _list.AddRange(value);
          _notify = true;
          NotifyPropertyChanged();
        }
      }
    }

    /// <summary>
    /// The value getter for IValue interface
    /// </summary>
    object IReactiveProperty.Value
    {
      get => _list;
      set
      {
        if (!_list.Equals(value))
        {
          _notify = false;
          _list.Clear();
          foreach(var item in value as IList)
          {
            if (item.GetType() == typeof(T) || !(item is IConvertible))
              _list.Add((T)item);
            else
              _list.Add((T)Convert.ChangeType(item, typeof(T)));
          }
          _notify = true;
          NotifyPropertyChanged();
        }
      }
    }

    public T this[int index]
    {
      get => _list[index];
      set => throw new NotImplementedException();
    }

    public ReactiveCollection(IEnumerable<T> list)
    {
      _list = new List<T>(list);
      _notify = true;
    }

    public ReactiveCollection(params T[] list)
      : this(list.AsEnumerable()) { }

    public int IndexOf(T item)
    {
      return _list.IndexOf(item);
    }

    public void Insert(int index, T item)
    {
      _list.Insert(index, item);
      NotifyPropertyChanged();
    }

    public void RemoveAt(int index)
    {
      _list.RemoveAt(index);
      NotifyPropertyChanged();
    }

    public void Add(T item)
    {
      _list.Add(item);
      NotifyPropertyChanged();
    }

    public void Clear()
    {
      _list.Clear();
      NotifyPropertyChanged();
    }

    public bool Contains(T item)
    {
      return _list.Contains(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
      _list.CopyTo(array, arrayIndex);
      NotifyPropertyChanged();
    }

    public bool Remove(T item)
    {
      var result = _list.Remove(item);
      NotifyPropertyChanged();
      return result;
    }

    public IEnumerator<T> GetEnumerator()
    {
      return _list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return _list.GetEnumerator();
    }

    /// <summary>
    /// Notification helper that will call the property change events
    /// </summary>
    /// <param name="property">The property name that has been changed</param>
    private void NotifyPropertyChanged([CallerMemberName] string property = "")
    {
      if (_notify)
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
    }
  }
}
