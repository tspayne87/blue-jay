﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.UI.Component.Reactivity
{
  public class ReactiveCollection<T> : IReactiveProperty, IList<T>, IList
  {
    private List<IObserver<ReactiveUpdateEvent>> _observers;
    private readonly List<T> _list;

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
          _list.Clear();
          _list.AddRange(value);
          for (var i = 0; i < value.Count; ++i)
            BindValue(value[i], i);
          Next(_list, "");
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
        if (!_list.Equals(value) && value is IList)
        {
          _list.Clear();
          foreach (var item in value as IList)
          {
            if (item.GetType() == typeof(T) || !(item is IConvertible))
              _list.Add((T)item);
            else
              _list.Add((T)Convert.ChangeType(item, typeof(T)));
          }

          for (var i = 0; i < _list.Count; ++i)
            BindValue(_list[i], i);
          Next(_list, "");
        }
      }
    }

    public bool IsFixedSize => ((IList)_list).IsFixedSize;

    public bool IsSynchronized => ((IList)_list).IsSynchronized;

    public object SyncRoot => ((IList)_list).SyncRoot;

    public IReactiveParentProperty ReactiveParent { get; set; }

    object IList.this[int index]
    {
      get => _list[index];
      set
      {
        if (index >= _list.Count) throw new ArgumentOutOfRangeException(nameof(index));
        if ((_list[index] == null && value != null) || (_list[index] != null && !_list[index].Equals(value)))
        {
          _list[index] = (T)value;
          BindValue((T)value, index);
          Next(value, $"[{index}]");
          Next(_list);
        }
      }
    }

    /// <inheritdoc cref="IList" />
    public T this[int index]
    {
      get => _list[index];
      set
      {
        if (index >= _list.Count) throw new ArgumentOutOfRangeException(nameof(index));
        if ((_list[index] == null && value != null) || (_list[index] != null && !_list[index].Equals(value)))
        {
          _list[index] = value;
          BindValue(value, index);
          Next(value, $"[{index}]");
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
      _observers = new List<IObserver<ReactiveUpdateEvent>>();
      for (var i = 0; i < _list.Count; ++i)
        BindValue(_list[i], i);
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

      for (var i = index; i < _list.Count; ++i)
      {
        BindValue(_list[i], i);
        Next(_list[index], $"[{index}]", _list.Count - 1 == i ? ReactiveUpdateEvent.EventType.Add : ReactiveUpdateEvent.EventType.Update);
      }
      Next(_list);
    }

    /// <inheritdoc cref="IList" />
    public void RemoveAt(int index)
    {
      if (index >= _list.Count) return;

      var item = _list[index];
      _list.RemoveAt(index);

      for (var i = index; i < _list.Count; ++i)
        BindValue(_list[i], i);
      Next(item, type: ReactiveUpdateEvent.EventType.Remove);
    }

    /// <inheritdoc cref="IList" />
    public void Add(T item)
    {
      _list.Add(item);
      BindValue(item, _list.Count - 1);
      Next(item, type: ReactiveUpdateEvent.EventType.Add);
    }

    /// <inheritdoc cref="IList" />
    public void Clear()
    {
      var removed = _list.ToArray();
      _list.Clear();

      foreach (var item in removed)
        Next(item, type: ReactiveUpdateEvent.EventType.Remove);
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

      for (var i = arrayIndex; i < _list.Count; ++i)
        BindValue(_list[i], i);
      Next(_list);
    }

    /// <inheritdoc cref="IList" />
    public bool Remove(T item)
    {
      var index = _list.IndexOf(item);
      if (index == -1) return false;

      _list.RemoveAt(index);

      for (var i = index; i < _list.Count; ++i)
        BindValue(_list[index], index);
      Next(item, type: ReactiveUpdateEvent.EventType.Remove);
      return true;
    }

    /// <inheritdoc cref="IList" />
    public IEnumerator<T> GetEnumerator()
    {
      return _list.GetEnumerator();
    }

    /// <inheritdoc cref="IList" />
    IEnumerator IEnumerable.GetEnumerator()
    {
      return _list.GetEnumerator();
    }

    /// <summary>
    /// Subscription method is meant to attach a subscription to the observable so we can dispose of it if needed
    /// </summary>
    /// <param name="observer">The observer we are wanting to send details to</param>
    /// <returns>The disposable object that is meant to remove the observer on dispose</returns>
    public IDisposable Subscribe(IObserver<ReactiveUpdateEvent> observer)
    {
      if (!_observers.Contains(observer))
      {
        _observers.Add(observer);
        var propObserver = observer as ReactivePropertyObserver;
        if (propObserver != null)
        {
          observer.OnNext(new ReactiveUpdateEvent() { Path = propObserver.Path, Data = Utils.GetObject(this, propObserver.Path), Type = ReactiveUpdateEvent.EventType.Update });
        }
        else
        {
          observer.OnNext(new ReactiveUpdateEvent() { Data = _list, Type = ReactiveUpdateEvent.EventType.Update });
        }
      }
      return new ReactivePropertyUnsubscriber(_observers, observer);
    }

    /// <summary>
    /// Helper subscrption to make it easier for items to add a next action button since most of the UI elements just care about when the object is updated so
    /// they can react to those elements in real time
    /// </summary>
    /// <param name="nextAction">The action that will be called when the item is updated</param>
    /// <returns>Will return a disposable that should be destored if the item is removed</returns>
    public IDisposable Subscribe(Action<ReactiveUpdateEvent> nextAction, string path = null)
    {
      return Subscribe(new ReactivePropertyObserver(nextAction, path));
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<ReactiveUpdateEvent> nextAction, ReactiveUpdateEvent.EventType type, string path = null)
    {
      return Subscribe(new ReactivePropertyTypeObserver(nextAction, type, path));
    }

    private void BindValue(T value, int index)
    {
      if (value != null)
      {
        if (typeof(IReactiveProperty).IsAssignableFrom(value.GetType()))
        {
          var reactive = value as IReactiveProperty;
          if (reactive != null)
          {
            reactive.ReactiveParent = new ReactiveParentProperty(this, $"[{index}]");
            reactive.Next(reactive.Value);
          }
        }
        else
        {
          var fields = value.GetType().GetFields();
          foreach (var field in fields)
          {
            if (field.IsInitOnly && typeof(IReactiveProperty).IsAssignableFrom(field.FieldType))
            {
              var reactive = field.GetValue(value) as IReactiveProperty;
              if (reactive != null)
              {
                reactive.ReactiveParent = new ReactiveParentProperty(this, $"[{index}].{field.Name}");
                reactive.Next(reactive.Value);
              }
            }
          }
        }
      }
    }

    public void Next(object value, string path = "", ReactiveUpdateEvent.EventType type = ReactiveUpdateEvent.EventType.Update)
    {
      foreach (var observer in _observers.ToArray())
      {
        observer.OnNext(new ReactiveUpdateEvent() { Path = path, Data = value, Type = type });
      }

      if (ReactiveParent != null)
        ReactiveParent.Value.Next(value, string.IsNullOrWhiteSpace(path) ? ReactiveParent.Name : $"{ReactiveParent.Name}.{path}", type);
    }

    public int Add(object value)
    {
      Add((T)value);
      return _list.Count - 1;
    }

    public bool Contains(object value)
    {
      return Contains((T)value);
    }

    public int IndexOf(object value)
    {
      return IndexOf((T)value);
    }

    public void Insert(int index, object value)
    {
      Insert(index, (T)value);
    }

    public void Remove(object value)
    {
      Remove((T)value);
    }

    public void CopyTo(Array array, int index)
    {
      CopyTo(array.Cast<T>().ToArray(), index);
    }
  }
}
