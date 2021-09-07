using System;

namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// Wrapper to cast with so that we can get the value object
  /// </summary>
  public interface IReactiveProperty : IObservable<ReactiveEvent>
  {
    /// <summary>
    /// The object value we are currently processing
    /// </summary>
    object Value { get; set; }

    /// <summary>
    /// The reactive property to go down the line to fire off events based on this property
    /// </summary>
    IReactiveParentProperty ReactiveParent { get; set; }

    /// <summary>
    /// Helper subscrption to make it easier for items to add a next action button since most of the UI elements just care about when the object is updated so
    /// they can react to those elements in real time
    /// </summary>
    /// <param name="nextAction">The action that will be called when the item is updated</param>
    /// <param name="path">The path we want to filter on when calling the next action</param>
    /// <returns>Will return a disposable that should be destored if the item is removed</returns>
    IDisposable Subscribe(Action<ReactiveEvent> nextAction, string path = null);

    /// <summary>
    /// Helper subscription is meant to be called on certain action done for the observable
    /// </summary>
    /// <param name="nextAction">The action that will be called when the item is updated</param>
    /// <param name="type">The type of method we are processing</param>
    /// <returns>Will return a disposable that should be destored if the item is removed</returns>
    IDisposable Subscribe(Action<ReactiveEvent> nextAction, ReactiveEvent.EventType type, string path = null);

    /// <summary>
    /// Triggers what the next value will be and the path that it should follow to get to this location
    /// </summary>
    /// <param name="value">The current value to use for this next value</param>
    /// <param name="path">The path we should be following</param>
    /// <param name="type">The type of update that is happening for this next event</param>
    void Next(object value, string path = "", ReactiveEvent.EventType type = ReactiveEvent.EventType.Update);
  }

  /// <summary>
  /// The parent property to determine the value and name we need to use when calling the parents next path
  /// </summary>
  public interface IReactiveParentProperty
  {
    /// <summary>
    /// The parent value we need to call the next from
    /// </summary>
    IReactiveProperty Value { get; set; }

    /// <summary>
    /// The name of the property that exists on the parent element
    /// </summary>
    string Name { get; set; }
  }
}
