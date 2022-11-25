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
    private readonly Func<T, object?, bool> _callback;

    /// <summary>
    /// If we should process the target instead of implicitly processing it with null
    /// </summary>
    private readonly bool _shouldProcessTarget;

    /// <summary>
    /// Constructor to deal with a basic callback listener so that we can pass around just
    /// functions instead of creaating a listener every time
    /// </summary>
    /// <param name="callback">The callback that should be called on processing the event</param>
    /// <param name="processTarget">The target event that this event should trigger on</param>
    /// <param name="shouldProcessTarget">If we should process the target instead of implicitly processing it with null</param>
    public CallbackListener(Func<T, object?, bool> callback, object? processTarget, bool shouldProcessTarget)
    {
      _callback = callback;
      _shouldProcessTarget = shouldProcessTarget;
      ProcessTarget = processTarget;
    }

    /// <inheritdoc />
    public override bool ShouldProcess(IEvent evt)
    {
      return !_shouldProcessTarget || ProcessTarget == evt.Target;
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<T> evt)
    {
      if (!_callback(evt.Data, evt.Target))
      {
        evt.StopPropagation();
      }
    }
  }
}
