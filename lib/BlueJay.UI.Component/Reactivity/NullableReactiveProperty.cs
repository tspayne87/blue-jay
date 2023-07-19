namespace BlueJay.UI.Component.Reactivity
{
  public class NullableReactiveProperty<T> : IReactiveProperty<T?>
    where T : struct
  {
    /// <summary>
    /// The observers that are watching changes on this style
    /// </summary>
    private readonly List<IObserver<ReactiveEvent>> _observers;

    /// <summary>
    /// The internal value that was set for this property
    /// </summary>
    private T? _value;

    /// <summary>
    /// Getter that is meant to update all the observers and the internal value
    /// </summary>
    public T? Value
    {
      get => _value;
      set
      {
        if (!_value.Equals(value))
        {
          _value = value;
          Next(_value);
        }
      }
    }

    /// <summary>
    /// Constructor to build out a reactive property starting with a value
    /// </summary>
    /// <param name="value">The value to start with</param>
    public NullableReactiveProperty(T? value)
    {
      _value = value;
      _observers = new List<IObserver<ReactiveEvent>>();
    }

    /// <summary>
    /// Subscription method is meant to attach a subscription to the observable so we can dispose of it if needed
    /// </summary>
    /// <param name="observer">The observer we are wanting to send details to</param>
    /// <returns>The disposable object that is meant to remove the observer on dispose</returns>
    public IDisposable Subscribe(IObserver<ReactiveEvent> observer)
    {
      if (!_observers.Contains(observer))
      {
        _observers.Add(observer);
        observer.OnNext(new ReactiveEvent() { Data = _value, Type = ReactiveEvent.EventType.Update });
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
    public void Next(T? value, ReactiveEvent.EventType type = ReactiveEvent.EventType.Update)
    {
      foreach (var observer in _observers.ToArray())
        observer.OnNext(new ReactiveEvent() { Data = value, Type = type });
    }
  }
}
