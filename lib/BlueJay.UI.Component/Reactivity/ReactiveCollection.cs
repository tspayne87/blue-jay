using System.Collections;

namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// Reactive collection
  /// </summary>
  /// <typeparam name="T">The type that is meant for the reactive collection</typeparam>
  public class ReactiveCollection<T> : IReactiveProperty<IList<T>>, IList<T>
    where T : struct
  {
    /// <summary>
    /// The observers that are watching changes on this style
    /// </summary>
    private List<IObserver<ReactiveEvent>> _observers;

    /// <summary>
    /// The internal list
    /// </summary>
    private readonly List<T> _list;

    /// <inheritdoc />
    public int Count => _list.Count;

    /// <inheritdoc />
    public bool IsReadOnly => false;

    /// <summary>
    /// The value for this property
    /// </summary>
    public IList<T> Value
    {
      get => _list;
      set
      {
        if (!_list.Equals(value))
        {
          _list.Clear();
          _list.AddRange(value);
          Next(_list);
        }
      }
    }

    /// <inheritdoc />
    public bool IsFixedSize => ((IList)_list).IsFixedSize;

    /// <inheritdoc />
    public bool IsSynchronized => ((IList)_list).IsSynchronized;

    /// <inheritdoc />
    public object SyncRoot => ((IList)_list).SyncRoot;

    /// <inheritdoc />
    public T this[int index]
    {
      get => _list[index];
      set
      {
        if (index >= _list.Count) throw new ArgumentOutOfRangeException(nameof(index));
        if (!_list[index].Equals(value))
        {
          _list[index] = value;
          Next(_list);
        }
      }
    }

    /// <summary>
    /// Constructor is meant to assign the list object into the internal watchers
    /// </summary>
    /// <param name="list">The list being watched</param>
    public ReactiveCollection(IEnumerable<T> list)
    {
      _list = new List<T>(list);
      _observers = new List<IObserver<ReactiveEvent>>();
    }

    /// <summary>
    /// Constructor to take a list of items and create a reactive collection out of it
    /// </summary>
    /// <param name="list"></param>
    public ReactiveCollection(params T[] list)
      : this(list.AsEnumerable()) { }

    /// <inheritdoc cref="IList" />
    public int IndexOf(T item)
    {
      return _list.IndexOf(item);
    }

    /// <inheritdoc cref="IList" />
    public void Insert(int index, T item)
    {
      _list.Insert(index, item);
      Next(_list);
    }

    /// <inheritdoc cref="IList" />
    public void RemoveAt(int index)
    {
      if (index >= _list.Count) return;

      var item = _list[index];
      _list.RemoveAt(index);
      Next(_list, type: ReactiveEvent.EventType.Remove);
    }

    /// <inheritdoc cref="IList" />
    public void Add(T item)
    {
      _list.Add(item);
      Next(_list, type: ReactiveEvent.EventType.Add);
    }

    /// <inheritdoc cref="IList" />
    public void Clear()
    {
      var removed = _list.ToArray();
      _list.Clear();
      Next(_list, type: ReactiveEvent.EventType.Remove);
    }

    /// <inheritdoc cref="IList" />
    public bool Contains(T item)
    {
      return _list.Contains(item);
    }

    /// <inheritdoc cref="IList" />
    public void CopyTo(T[] array, int arrayIndex)
    {
      _list.CopyTo(array, arrayIndex);
      Next(_list);
    }

    /// <inheritdoc cref="IList" />
    public bool Remove(T item)
    {
      var index = _list.IndexOf(item);
      if (index == -1) return false;

      _list.RemoveAt(index);
      Next(_list, type: ReactiveEvent.EventType.Remove);
      return true;
    }

    /// <inheritdoc />
    public IEnumerator<T> GetEnumerator()
    {
      return _list.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
      return _list.GetEnumerator();
    }

    /// <inheritdoc />
    public IDisposable Subscribe(IObserver<ReactiveEvent> observer)
    {
      if (!_observers.Contains(observer))
      {
        _observers.Add(observer);
        observer.OnNext(new ReactiveEvent() { Data = _list, Type = ReactiveEvent.EventType.Update });
      }
      return new ReactivePropertyUnsubscriber(_observers, observer);
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<ReactiveEvent> nextAction)
    {
      return Subscribe(nextAction, ReactiveEvent.EventType.Add | ReactiveEvent.EventType.Update | ReactiveEvent.EventType.Remove);
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<ReactiveEvent> nextAction, ReactiveEvent.EventType type)
    {
      return Subscribe(new ReactivePropertyTypeObserver(nextAction, type));
    }

    /// <inheritdoc />
    public void Next(IList<T> value, ReactiveEvent.EventType type = ReactiveEvent.EventType.Update)
    {
      foreach (var observer in _observers.ToArray())
        observer.OnNext(new ReactiveEvent() { Data = value, Type = type });
    }
  }
}
