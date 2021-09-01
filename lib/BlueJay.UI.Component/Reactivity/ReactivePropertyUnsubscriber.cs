using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Reactivity
{
  internal class ReactivePropertyUnsubscriber : IDisposable
  {
    private readonly List<IObserver<ReactiveUpdateEvent>> _observers;
    private readonly IObserver<ReactiveUpdateEvent> _observer;

    internal ReactivePropertyUnsubscriber(List<IObserver<ReactiveUpdateEvent>> observers, IObserver<ReactiveUpdateEvent> observer)
    {
      _observers = observers;
      _observer = observer;
    }

    public void Dispose()
    {
      if (_observers.Contains(_observer))
        _observers.Remove(_observer);
    }
  }
}
