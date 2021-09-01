using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Reactivity
{
  public class ReactivePropertyTypeObserver : IObserver<ReactiveUpdateEvent>
  {
    private readonly Action<ReactiveUpdateEvent> _action;
    private readonly ReactiveUpdateEvent.EventType _type;

    internal ReactivePropertyTypeObserver(Action<ReactiveUpdateEvent> action, ReactiveUpdateEvent.EventType type)
    {
      _action = action;
      _type = type;
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
      if (value.Type == _type)
        _action(value);
    }
  }
}
