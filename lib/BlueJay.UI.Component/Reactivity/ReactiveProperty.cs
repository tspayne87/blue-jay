﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// Reactive property that will handle when props change on the component
  /// </summary>
  /// <typeparam name="T">The type of property this is</typeparam>
  public class ReactiveProperty<T> : IReactiveProperty, IDisposable
  {
    private readonly List<IDisposable> _subscriptions;
    private readonly List<IObserver<ReactiveUpdateEvent>> _observers;

    /// <summary>
    /// The internal value that was set for this property
    /// </summary>
    private T _value;

    /// <summary>
    /// Getter that is meant to update all the observers and the internal value
    /// </summary>
    public T Value
    {
      get => _value;
      set
      {
        if ((_value == null && value != null) || (_value != null && !_value.Equals(value)))
        {
          _value = value;
          Next(_value);
          BindValue();
        }
      }
    }

    /// <summary>
    /// The value getter for IValue interface
    /// </summary>
    object IReactiveProperty.Value
    {
      get => _value;
      set
      {
        if ((_value == null && value != null) || (_value != null && !_value.Equals(value)))
        {
          if (value.GetType() == typeof(T) || !(value is IConvertible))
            _value = (T)value;
          else
            _value = (T)Convert.ChangeType(value, typeof(T));
          Next(_value);
          BindValue();
        }
      }
    }

    /// <summary>
    /// Constructor to build out a reactive property starting with a value
    /// </summary>
    /// <param name="value">The value to start with</param>
    public ReactiveProperty(T value)
    {
      _value = value;
      _observers = new List<IObserver<ReactiveUpdateEvent>>();
      _subscriptions = new List<IDisposable>();
      BindValue();
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
          observer.OnNext(new ReactiveUpdateEvent() { Data = _value, Type = ReactiveUpdateEvent.EventType.Update });
        }
      }
      return new ReactivePropertyUnsubscriber(_observers, observer);
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<ReactiveUpdateEvent> nextAction, string path = null)
    {
      return Subscribe(new ReactivePropertyObserver(nextAction, path));
    }

    /// <inheritdoc />
    public IDisposable Subscribe(Action<ReactiveUpdateEvent> nextAction, ReactiveUpdateEvent.EventType type)
    {
      return Subscribe(new ReactivePropertyTypeObserver(nextAction, type));
    }

    public void Dispose()
    {
      ClearSubscriptions();
    }

    /// <summary>
    /// Helper method is meant to notify all the observers of this subscription
    /// </summary>
    private void Next(object value, string path = "")
    {
      foreach (var observer in _observers.ToArray())
        observer.OnNext(new ReactiveUpdateEvent() { Path = path, Data = value, Type = ReactiveUpdateEvent.EventType.Update });
    }

    private void BindValue()
    {
      ClearSubscriptions();
      if (_value != null)
      {
        var fields = _value.GetType().GetFields();
        foreach(var field in fields)
        {
          if (field.IsInitOnly && typeof(IReactiveProperty).IsAssignableFrom(field.FieldType))
          {
            var reactive = field.GetValue(_value) as IReactiveProperty;
            if (reactive != null)
            {
              _subscriptions.Add(
                reactive.Subscribe(x =>
                {
                  Next(x.Data, string.IsNullOrWhiteSpace(x.Path) ? field.Name : $"{field.Name}.{x.Path}");
                })
              );
            }
          }
        }
      }
    }

    private void ClearSubscriptions()
    {
      foreach (var subscription in _subscriptions)
        subscription.Dispose();
      _subscriptions.Clear();
    }
  }
}
