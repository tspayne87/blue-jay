using BlueJay.Events.Interfaces;
using System;

namespace BlueJay.Events
{
  /// <summary>
  /// The callback listener to create a generic listener for basic event functions that do not need a full event listener class
  /// </summary>
  /// <typeparam name="T">The type of event we are dealing with</typeparam>
  public class CallbackListener<T> : EventListener<T>
  {
    /// <summary>
    /// The callback that should be processed when this is triggered
    /// </summary>
    private readonly Func<T, bool> _callback;

    /// <summary>
    /// Constructor to deal with a basic callback listener so that we can pass around just
    /// functions instead of creaating a listener every time
    /// </summary>
    /// <param name="callback">The callback that should be called on processing the event</param>
    /// <param name="processTarget">The target event that this event should trigger on</param>
    public CallbackListener(Func<T, bool> callback, object processTarget = null)
    {
      _callback = callback;
      ProcessTarget = processTarget;
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<T> evt)
    {
      if (!_callback(evt.Data))
      {
        evt.StopPropagation();
      }
    }
  }
}
