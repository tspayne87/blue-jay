using System;
using System.Collections.Generic;

namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// The property unsubscriber to remove the observer from the subscription
  /// </summary>
  internal class ReactivePropertyUnsubscriber : IDisposable
  {
    /// <summary>
    /// The observers to remove the observer
    /// </summary>
    private readonly List<IObserver<ReactiveEvent>> _observers;

    /// <summary>
    /// The current observer
    /// </summary>
    private readonly IObserver<ReactiveEvent> _observer;

    /// <summary>
    /// Constructor to initialize defaults
    /// </summary>
    /// <param name="observers">The observers to remove the observer</param>
    /// <param name="observer">The current observer</param>
    internal ReactivePropertyUnsubscriber(List<IObserver<ReactiveEvent>> observers, IObserver<ReactiveEvent> observer)
    {
      _observers = observers;
      _observer = observer;
    }

    /// <summary>
    /// Dispose to remove the observer from the observers
    /// </summary>
    public void Dispose()
    {
      if (_observers.Contains(_observer))
        _observers.Remove(_observer);
    }
  }
}
