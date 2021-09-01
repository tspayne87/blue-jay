using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Reactivity
{
  internal class ReactivePropertyObserver : IObserver<ReactiveUpdateEvent>
  {
    private readonly Action<ReactiveUpdateEvent> _action;
    private readonly string _path;

    public string Path => _path;

    internal ReactivePropertyObserver(Action<ReactiveUpdateEvent> action, string path)
    {
      _action = action;
      _path = path;
    }

    public void OnCompleted()
    {
      // Complete should never be called by the reactive property
      OnError(new ArgumentException("OnComplete Called"));
    }

    public void OnError(Exception error)
    {
      // TODO: Do something if error occures
    }

    public void OnNext(ReactiveUpdateEvent value)
    {
      if (string.IsNullOrWhiteSpace(_path) || value.Path == _path)
        _action(value);
    }
  }
}
