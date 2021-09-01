using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// Wrapper to cast with so that we can get the value object
  /// </summary>
  public interface IReactiveProperty : IObservable<ReactiveUpdateEvent>
  {
    /// <summary>
    /// The object value we are currently processing
    /// </summary>
    object Value { get; set; }

    /// <summary>
    /// Helper subscrption to make it easier for items to add a next action button since most of the UI elements just care about when the object is updated so
    /// they can react to those elements in real time
    /// </summary>
    /// <param name="nextAction">The action that will be called when the item is updated</param>
    /// <param name="path">The path we want to filter on when calling the next action</param>
    /// <returns>Will return a disposable that should be destored if the item is removed</returns>
    IDisposable Subscribe(Action<ReactiveUpdateEvent> nextAction, string path = null);

    /// <summary>
    /// Helper subscription is meant to be called on certain action done for the observable
    /// </summary>
    /// <param name="nextAction">The action that will be called when the item is updated</param>
    /// <param name="type">The type of method we are processing</param>
    /// <returns>Will return a disposable that should be destored if the item is removed</returns>
    IDisposable Subscribe(Action<ReactiveUpdateEvent> nextAction, ReactiveUpdateEvent.EventType type);
  }
}
