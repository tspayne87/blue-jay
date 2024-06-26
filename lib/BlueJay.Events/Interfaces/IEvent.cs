﻿namespace BlueJay.Events.Interfaces
{
  /// <summary>
  /// The event object that will be sent when an event is triggered
  /// </summary>
  public interface IEvent
  {
    /// <summary>
    /// The current name of the event
    /// </summary>
    string Name { get; }

    /// <summary>
    /// If this event is complete and needs to stop propagating
    /// </summary>
    bool IsComplete { get; }

    /// <summary>
    /// The current target we are working with
    /// </summary>
    object? Target { get; }

    /// <summary>
    /// Method is meant to stop the event cycle from moving forward and make sure that no other event listeners past the
    /// one that called this method gets ran
    /// </summary>
    void StopPropagation();
  }

  /// <summary>
  /// Internal Event is meant to handle the countdown timer for delayed events
  /// </summary>
  internal interface IInternalEvent
  {
    /// <summary>
    /// Timeout down till the event should be triggered
    /// </summary>
    int Timeout { get; set; }

    /// <summary>
    /// If this event is currently cancelled and should be removed from the queue and not processed
    /// </summary>
    bool IsCancelled { get; set; }
  }

  /// <summary>
  /// The event object that will be sent when an event is triggered
  /// </summary>
  /// <typeparam name="TData">The data that is sent from the triggered event</typeparam>
  public interface IEvent<TData> : IEvent
  {
    /// <summary>
    /// The data that comes along with the event
    /// </summary>
    TData Data { get; }
  }
}
