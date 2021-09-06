﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlueJay.UI.Component.Reactivity
{
  public class ReactiveScope : IReactiveProperty, IDictionary<string, object>
  {
    private List<IObserver<ReactiveUpdateEvent>> _observers;
    private readonly Dictionary<string, object> _scope;

    public object Value
    {
      get => _scope;
      set
      {
        throw new NotImplementedException();
      }
    }

    public ReactiveScope Parent { get; set; }

    public ICollection<string> Keys => _scope.Keys;

    public ICollection<object> Values => _scope.Values;

    public int Count => _scope.Count;

    public bool IsReadOnly => ((ICollection<KeyValuePair<string, object>>)_scope).IsReadOnly;

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

    public IReactiveParentProperty ReactiveParent { get; set; }

    public ReactiveScope()
      : this(new Dictionary<string, object>()) { }

    public ReactiveScope(Dictionary<string, object> scope)
    {
      _observers = new List<IObserver<ReactiveUpdateEvent>>();

      _scope = scope;
      foreach (var item in scope)
        BindValue(item.Key, item.Value);
    }

    public bool ContainsKey(string key)
    {
      return _scope.ContainsKey(key) || (Parent != null && Parent.ContainsKey(key));
    }

    public ReactiveScope NewScope()
    {
      return new ReactiveScope() { Parent = this };
    }

    public IEnumerable<IDisposable> Subscribe(Action<object> nextAction, List<string> paths)
    {
      foreach(var path in paths)
      {
        yield return Subscribe(nextAction, path);
      }
    }

    public IDisposable Subscribe(Action<ReactiveUpdateEvent> nextAction, string path = null)
    {
      return Subscribe(new ReactivePropertyObserver(nextAction, path));
    }

    public IDisposable Subscribe(Action<ReactiveUpdateEvent> nextAction, ReactiveUpdateEvent.EventType type, string path = null)
    {
      return Subscribe(new ReactivePropertyTypeObserver(nextAction, type, path));
    }

    public IDisposable Subscribe(IObserver<ReactiveUpdateEvent> observer)
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
          observer.OnNext(new ReactiveUpdateEvent() { Path = propObserver.Path, Data = Utils.GetObject(this, propObserver.Path), Type = ReactiveUpdateEvent.EventType.Update });
        }
        else
        {
          observer.OnNext(new ReactiveUpdateEvent() { Data = _scope, Type = ReactiveUpdateEvent.EventType.Update });
        }
      }
      return new ReactivePropertyUnsubscriber(_observers, observer);
    }

    public void Add(string key, object value)
    {
      this[key] = value;
    }

    public bool Remove(string key)
    {
      return _scope.Remove(key);
    }

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

    public void Add(KeyValuePair<string, object> item)
    {
      this[item.Key] = item.Value;
    }

    public void Clear()
    {
      _scope.Clear();
    }

    public bool Contains(KeyValuePair<string, object> item)
    {
      return _scope.Contains(item);
    }

    public void CopyTo(KeyValuePair<string, object>[] array, int arrayIndex)
    {
      foreach (var item in array)
        this[item.Key] = item.Value;
    }

    public bool Remove(KeyValuePair<string, object> item)
    {
      return Remove(item.Key);
    }

    public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
    {
      return _scope.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
      return _scope.GetEnumerator();
    }

    public void Next(object value, string path = "", ReactiveUpdateEvent.EventType type = ReactiveUpdateEvent.EventType.Update)
    {
      foreach (var observer in _observers.ToArray())
        observer.OnNext(new ReactiveUpdateEvent() { Path = path, Data = value, Type = type });

      if (ReactiveParent != null)
        ReactiveParent.Value.Next(value, string.IsNullOrWhiteSpace(path) ? ReactiveParent.Name : $"{ReactiveParent.Name}.{path}", type);
    }

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
