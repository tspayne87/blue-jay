﻿using System;

namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// Reactive property type observer
  /// </summary>
  public class ReactivePropertyTypeObserver : IObserver<ReactiveEvent>
  {
    /// <summary>
    /// The action that should be called if the path and type are bound to next call
    /// </summary>
    private readonly Action<ReactiveEvent> _action;

    /// <summary>
    /// The type we need to filter on
    /// </summary>
    private readonly ReactiveEvent.EventType _type;

    /// <summary>
    /// The path we need to filter on
    /// </summary>
    private readonly string _path;

    /// <summary>
    /// Constructor to build out defaults
    /// </summary>
    /// <param name="action">The action that should be called if the path and type are bound to next call</param>
    /// <param name="type">The type we need to filter on</param>
    /// <param name="path">The path we need to filter on</param>
    internal ReactivePropertyTypeObserver(Action<ReactiveEvent> action, ReactiveEvent.EventType type, string path)
    {
      _action = action;
      _type = type;
      _path = path;
    }

    /// <inheritdoc />
    public void OnCompleted()
    {
      // Complete should never be called by the reactive property
      OnError(new ArgumentException("OnComplete Called"));
    }

    /// <inheritdoc />
    public void OnError(Exception error)
    {
      // TODO: Do something if error occures
    }

    /// <inheritdoc />
    public void OnNext(ReactiveEvent value)
    {
      if (value.Type == _type && (string.IsNullOrWhiteSpace(_path) || value.Path == _path))
        _action(value);
    }
  }
}
