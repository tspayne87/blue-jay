namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// Reactive class meant to handle lambda expressions that need to updated when a reactive state is triggered
  /// </summary>
  /// <typeparam name="T">The type of object that is stored for the computed variable</typeparam>
  public class ComputedProperty<T> : IReactiveProperty<T>, IDisposable
    where T : struct
  {
    /// <summary>
    /// The current getter for this computed value
    /// </summary>
    private readonly Func<T> _getter;

    /// <summary>
    /// The setter that should be called if value is set for this computed
    /// </summary>
    private readonly Action<T> _setter;

    /// <summary>
    /// The current list of watchers we need to keep track for this computed
    /// </summary>
    private readonly IReactiveProperty[] _watchers;

    /// <summary>
    /// The list of disposable values configured from the watchers to update the computed property
    /// </summary>
    private readonly List<IDisposable> _disposables;

    /// <summary>
    /// The current latest computed value that is given
    /// </summary>
    private T? _latestValue;

    /// <summary>
    /// The observers that are watching changes on this computed
    /// </summary>
    private readonly List<IObserver<ReactiveEvent>> _observers;

    /// <inheritdoc />
    public T Value
    {
      get => _latestValue ?? default(T);
      set
      {
        _setter(value);
        Next(_getter());
      }
    }

    /// <summary>
    /// Constructor to build out a computed getter
    /// </summary>
    /// <param name="getter">The getter for the computed lambda expression</param>
    /// <param name="watchers">The watchers that should force an update in the lambda expression</param>
    public ComputedProperty(Func<T> getter, params IReactiveProperty[] watchers)
      : this(getter, (val) => { }, watchers) { }

    /// <summary>
    /// Constructor to build out a computed getter
    /// </summary>
    /// <param name="getter">The getter for the computed lambda expression</param>
    /// <param name="setter">The setter that should be called when the value is set for this computed object</param>
    /// <param name="watchers">The watchers that should force an update in the lambda expression</param>
    public ComputedProperty(Func<T> getter, Action<T> setter, params IReactiveProperty[] watchers)
    {
      _latestValue = getter();
      _observers = new List<IObserver<ReactiveEvent>>();

      _getter = getter;
      _setter = setter;
      _watchers = watchers;

      _disposables = _watchers.Select(x => x.Subscribe(y => Next(_getter()))).ToList();
    }

    /// <inheritdoc />
    public IDisposable Subscribe(IObserver<ReactiveEvent> observer)
    {
      if (!_observers.Contains(observer))
      {
        _observers.Add(observer);
        observer.OnNext(new ReactiveEvent() { Data = _getter(), Type = ReactiveEvent.EventType.Update });
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
    public void Next(T value, ReactiveEvent.EventType type = ReactiveEvent.EventType.Update)
    {
      if (!value.Equals(_latestValue))
      {
        _latestValue = value;
        foreach (var observer in _observers.ToArray())
          observer.OnNext(new ReactiveEvent() { Data = value, Type = type });
      }
    }

    /// <inheritdoc />
    public void Dispose()
    {
      foreach (var disposable in _disposables)
        disposable.Dispose();
    }
  }
}
