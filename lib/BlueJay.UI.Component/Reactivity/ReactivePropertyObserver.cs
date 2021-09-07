using System;

namespace BlueJay.UI.Component.Reactivity
{
  internal class ReactivePropertyObserver : IObserver<ReactiveEvent>
  {
    /// <summary>
    /// The action that should be called if the path is filtered
    /// </summary>
    private readonly Action<ReactiveEvent> _action;

    /// <summary>
    /// The path we need to filter on
    /// </summary>
    private readonly string _path;

    /// <summary>
    /// The path needs to filter on
    /// </summary>
    public string Path => _path;

    /// <summary>
    /// Constructor to build out defaults
    /// </summary>
    /// <param name="action">The action that should be called if the path is filtered</param>
    /// <param name="path">The path needs to filter on</param>
    internal ReactivePropertyObserver(Action<ReactiveEvent> action, string path)
    {
      _action = action;
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
      if (string.IsNullOrWhiteSpace(_path) || value.Path == _path)
        _action(value);
    }
  }
}
