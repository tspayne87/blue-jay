using System;

namespace BlueJay.UI.Component.Reactivity
{
  public interface IReactiveProperty : IObservable<ReactiveEvent>
  {
    /// <summary>
    /// Helper subscrption to make it easier for items to add a next action button since most of the UI elements just care about when the object is updated so
    /// they can react to those elements in real time
    /// </summary>
    /// <param name="nextAction">The action that will be called when the item is updated</param>
    /// <returns>Will return a disposable that should be destored if the item is removed</returns>
    IDisposable Subscribe(Action<ReactiveEvent> nextAction);

    /// <summary>
    /// Helper subscription is meant to be called on certain action done for the observable
    /// </summary>
    /// <param name="nextAction">The action that will be called when the item is updated</param>
    /// <param name="type">The type of method we are processing</param>
    /// <returns>Will return a disposable that should be destored if the item is removed</returns>
    IDisposable Subscribe(Action<ReactiveEvent> nextAction, ReactiveEvent.EventType type);
  }

  /// <summary>
  /// Wrapper to cast with so that we can get the value object
  /// </summary>
  public interface IReactiveProperty<T> : IReactiveProperty
  {
    /// <summary>
    /// The object value we are currently processing
    /// </summary>
    T Value { get; set; }

    /// <summary>
    /// Triggers what the next value will be and the path that it should follow to get to this location
    /// </summary>
    /// <param name="value">The current value to use for this next value</param>
    /// <param name="type">The type of update that is happening for this next event</param>
    void Next(T value, ReactiveEvent.EventType type = ReactiveEvent.EventType.Update);
  }
}
