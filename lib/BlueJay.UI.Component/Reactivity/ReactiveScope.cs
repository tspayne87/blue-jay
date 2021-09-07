using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// The reactive scope that is meant to handle all the reactive items for the UI Components
  /// </summary>
  public class ReactiveScope : IReactiveProperty, IDictionary<string, object>
  {
    /// <summary>
    /// The observers that are watching changes on this style
    /// </summary>
    private List<IObserver<ReactiveEvent>> _observers;

    /// <summary>
    /// The internal scope
    /// </summary>
    private readonly Dictionary<string, object> _scope;

    /// <inheritdoc />
    public object Value
    {
      get => _scope;
      set
      {
        throw new NotImplementedException();
      }
    }

    /// <inheritdoc />
    public ReactiveScope Parent { get; set; }

    /// <inheritdoc />
    public ICollection<string> Keys => _scope.Keys;

    /// <inheritdoc />
    public ICollection<object> Values => _scope.Values;

    /// <inheritdoc />
    public int Count => _scope.Count;

    /// <inheritdoc />
    public bool IsReadOnly => ((ICollection<KeyValuePair<string, object>>)_scope).IsReadOnly;

    /// <summary>
    /// The indexer to look for items on the scope
    /// </summary>
    /// <param name="key">The key to look for items in the scope</param>
    /// <returns>Will return the item if found otherwise it will return null</returns>
    public object this[string key]
    {
      get
      {
        return Utils.GetObject(_scope, key) ?? Parent?[key];
      }
      set
      {
        if (key.Contains('.'))
        {
          var prop = Utils.GetReactiveProperty(_scope, key);
          if (prop != null)
          {
            prop.Value = value;
          }
        }
        else
        {
          _scope[key] = value;
          BindValue(key, value);
        }
      }
    }

    /// <inheritdoc />
    public IReactiveParentProperty ReactiveParent { get; set; }

    /// <summary>
    /// Basic constructor
    /// </summary>
    public ReactiveScope()
      : this(new Dictionary<string, object>()) { }

    /// <summary>
    /// Constructor to build out the reactive scope
    /// </summary>
    /// <param name="scope">The default scope</param>
    public ReactiveScope(Dictionary<string, object> scope)
    {
      _observers = new List<IObserver<ReactiveEvent>>();

      _scope = scope;
      foreach (var item in scope)
        BindValue(item.Key, item.Value);
    }

    /// <inheritdoc />
    public bool ContainsKey(string key)
    {
      return _scope.ContainsKey(key) || (Parent != null && Parent.ContainsKey(key));
    }

    /// <inheritdoc />
    public IEnumerable<IDisposable> Subscribe(Action<object> nextAction, List<string> paths)
    {
      foreach(var path in paths)
      {
        yield return Subscribe(nextAction, path);
      }
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<ReactiveEvent> nextAction, string path = null)
    {
      return Subscribe(new ReactivePropertyObserver(nextAction, path));
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<ReactiveEvent> nextAction, ReactiveEvent.EventType type, string path = null)
    {
      return Subscribe(new ReactivePropertyTypeObserver(nextAction, type, path));
    }

    /// <inheritdoc />
    public IDisposable Subscribe(IObserver<ReactiveEvent> observer)
    {
      if (Parent != null)
      {
        return Parent.Subscribe(observer);
      }

      if (!_observers.Contains(observer))
      {
        _observers.Add(observer);
        var propObserver = observer as ReactivePropertyObserver;
        if (propObserver != null)
        {
          observer.OnNext(new ReactiveEvent() { Path = propObserver.Path, Data = Utils.GetObject(this, propObserver.Path), Type = ReactiveEvent.EventType.Update });
        }
        else
        {
          observer.OnNext(new ReactiveEvent() { Data = _scope, Type = ReactiveEvent.EventType.Update });
        }
      }
      return new ReactivePropertyUnsubscriber(_observers, observer);
    }

    /// <inheritdoc />
    public void Add(string key, object value)
    {
      this[key] = value;
    }

    /// <inheritdoc />
    public bool Remove(string key)
    {
      return _scope.Remove(key);
    }

    /// <inheritdoc />
    public bool TryGetValue(string key, out object value)
    {
      value = default;
      if (_scope.ContainsKey(key))
      {
        value = _scope[key];
        return true;
      }
      return false;
    }

    /// <inheritdoc />
    public void Add(KeyValuePair<string, object> item)
    {
      this[item.Key] = item.Value;
    }

    /// <inheritdoc />
    public void Clear()
    {
      _scope.Clear();
    }

    /// <inheritdoc />
    public bool Contains(KeyValuePair<string, object> item)
    {
      return _scope.Contains(item);
    }

    /// <inheritdoc />
    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      foreach (var item in array)
        this[item.Key] = item.Value;
    }

    /// <inheritdoc />
    public bool Remove(KeyValuePair<string, object> item)
    {
      return Remove(item.Key);
    }

    /// <inheritdoc />
    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return _scope.GetEnumerator();
    }

    /// <inheritdoc />
    IEnumerator IEnumerable.GetEnumerator()
    {
      return _scope.GetEnumerator();
    }

    /// <inheritdoc />
    public void Next(object value, string path = "", ReactiveEvent.EventType type = ReactiveEvent.EventType.Update)
    {
      foreach (var observer in _observers.ToArray())
        observer.OnNext(new ReactiveEvent() { Path = path, Data = value, Type = type });

      if (ReactiveParent != null)
        ReactiveParent.Value.Next(value, string.IsNullOrWhiteSpace(path) ? ReactiveParent.Name : $"{ReactiveParent.Name}.{path}", type);
    }

    /// <summary>
    /// Binded value to attach the parent and setup the tree structure
    /// </summary>
    /// <param name="key">The key for binded value</param>
    /// <param name="value">The value we need to bound too</param>
    private void BindValue(string key, object value)
    {
      var subscriptions = new List<IDisposable>();
      if (value != null)
      {
        if (typeof(IReactiveProperty).IsAssignableFrom(value.GetType()))
        {
          var reactive = value as IReactiveProperty;
          if (reactive != null)
          {
            reactive.ReactiveParent = new ReactiveParentProperty(this, key);
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
                reactive.ReactiveParent = new ReactiveParentProperty(this, $"{key}.{field.Name}");
                reactive.Next(reactive.Value);
              }
            }
          }
        }
      }
    }
  }
}
