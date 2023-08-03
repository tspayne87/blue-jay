using BlueJay.Core.Interfaces;
using BlueJay.Events.Interfaces;
using BlueJay.Events.Lifecycle;
using System.Runtime.CompilerServices;
using System;
using System.Runtime.InteropServices;

namespace BlueJay.Events
{
  /// <summary>
  /// The event queue is meant to handle the current queue and reset to handle the next defered queue
  /// </summary>
  internal class EventQueue : IEventQueue
  {
    /// <summary>
    /// The delta service that will handle the countdown
    /// </summary>
    private readonly IDeltaService _delta;

    /// <summary>
    /// The current null weight which allows for 100000 events to be added without a weight attached
    /// </summary>
    private int _nullWeight = int.MaxValue - 100000;

    /// <summary>
    /// The current queue we are working with on any particular frame
    /// </summary>
    private readonly Queue<IEvent?> _current = new Queue<IEvent?>();

    /// <summary>
    /// The next queue that should store the defered events that should be handled in the next frame
    /// </summary>
    private readonly Queue<IEvent?> _next = new Queue<IEvent?>();

    /// <summary>
    /// All the handlers we are dealing with when processing events
    /// </summary>
    private Dictionary<string, List<EventListenerWeight>> _handlers = new Dictionary<string, List<EventListenerWeight>>();

    /// <summary>
    /// Constructor meant to inject various services for use inside the class
    /// </summary>
    /// <param name="delta">The delta service that will handle the countdown</param>
    public EventQueue(IDeltaService delta)
    {
      _delta = delta;
    }

    /// <inheritdoc />
    public void DispatchEvent<T>(T evt, object? target = null)
    {
      DispatchDelayedEvent(evt, 0, target);
    }

    /// <inheritdoc />
    public void DispatchEventOnce<T>(T evt)
    {
      if (!_next.Any(x => x is IEvent<T>))
        DispatchDelayedEvent(evt, 0, null);
    }

    /// <inheritdoc />
    public IDisposable DispatchDelayedEvent<T>(T evt, int timeout, object? target = null)
    {
      if (evt != null)
      {
        var name = evt.GetType().Name;
        if (_handlers.ContainsKey(name) && _handlers[name].Count > 0)
        {
          object? @event; // The current event that should be the instance of the event being created
          if (typeof(T) == typeof(object))
          { // If we are dealing with just an object we need to create an event threw
            // reflection since using the T could result in the wrong type during casting
            var eType = typeof(Event<>); // Create a blank type of the event object
            var fullType = eType.MakeGenericType(evt.GetType()); // Add the generic type based on the value given
            @event = Activator.CreateInstance(fullType, new object?[] { evt, target, timeout }); // Reflectively create the object
          }
          else // We just create the event in a normal way since we do not need to reflectivly create the event object
            @event = new Event<T>(evt, target, timeout);

          if (@event != null && @event is IEvent nEvent && @event is IInternalEvent iEvent)
          {
            _next.Enqueue(nEvent);
            return new EventUnsubscriber(iEvent);
          }
        }
      }
      return new EventUnsubscriber(null);
    }

    /// <inheritdoc />
    public IDisposable Timeout(Action callback, int timeout = -1)
    {
      IDisposable? listener = null;

      listener = AddEventListener<TimeoutEvent>((x, obj) =>
      {
        if (obj is IDisposable targetListener && listener == targetListener)
        {
          callback();
          targetListener.Dispose();
        }
        return true;
      });

      return new ZipDisposable(listener, DispatchDelayedEvent(new TimeoutEvent(), timeout, listener));
    }

    /// <inheritdoc />
    public IDisposable AddEventListener<T>(IEventListener<T> handler, int? weight = null)
    {
      var name = typeof(T).Name;
      if (!_handlers.ContainsKey(name)) _handlers[name] = new List<EventListenerWeight>();
      _handlers[name].Add(new EventListenerWeight(handler, weight ?? _nullWeight++));
      _handlers[name].Sort((a, b) => a.Weight > b.Weight ? 1 : -1);

      return new Unsubscriber(_handlers, handler, name);
    }

    /// <inheritdoc />
    public IDisposable AddEventListener<T>(Func<T, bool> callback, int? weight = null)
    {
      return AddEventListener(new CallbackListener<T>((x, t) => callback(x), null, false), weight);
    }

    /// <inheritdoc />
    public IDisposable AddEventListener<T>(Func<T, object?, bool> callback, int? weight = null)
    {
      return AddEventListener(new CallbackListener<T>(callback, null, false), weight);
    }

    /// <inheritdoc />
    public IDisposable AddEventListener<T>(Func<T, bool> callback, object? target, int? weight = null)
    {
      return AddEventListener(new CallbackListener<T>((x, t) => callback(x), target, true), weight);
    }

    /// <inheritdoc />
    public IDisposable AddEventListener<T>(Func<T, object?, bool> callback, object? target, int? weight = null)
    {
      return AddEventListener(new CallbackListener<T>(callback, target, true), weight);
    }

    /// <inheritdoc />
    public void Update()
    {
      while (_current.Count > 0)
      {
        ProcessEvent(_current.Dequeue());
      }
    }

    /// <inheritdoc />
    public void Draw()
    {
      ProcessEvent(new Event<DrawEvent>(new DrawEvent()));
    }

    /// <inheritdoc />
    public void Activate()
    {
      ProcessEvent(new Event<ActivateEvent>(new ActivateEvent()));
    }

    /// <inheritdoc />
    public void Deactivate()
    {
      ProcessEvent(new Event<DeactivateEvent>(new DeactivateEvent()));
    }

    /// <inheritdoc />
    public void Exit()
    {
      ProcessEvent(new Event<ExitEvent>(new ExitEvent()));
      Tick(true);
      Update();
    }

    /// <inheritdoc />
    public void Tick(bool excludeUpdate = false)
    {
      if (!excludeUpdate)
        DispatchEvent(new UpdateEvent());
      while (_next.Count > 0)
      {
        _current.Enqueue(_next.Dequeue());
      }
    }

    /// <summary>
    /// Process and event and pass that details to the handlers
    /// </summary>
    /// <typeparam name="T">The type of event we need to process</typeparam>
    /// <param name="evt">The event we need to process</param>
    private void ProcessEvent(IEvent? evt)
    {
      if (evt == null)
        return;

      if (evt is IInternalEvent iEvt && iEvt.IsCancelled)
        return;

      if (evt is IInternalEvent internalEvt && ShouldEventNotProcess(internalEvt))
      {
        _next.Enqueue(evt);
        return;
      }

      if (_handlers.ContainsKey(evt.Name))
      {
        foreach (var handler in CollectionsMarshal.AsSpan(_handlers[evt.Name]))
        {
          if (handler != null && handler.EventListener.ShouldProcess(evt))
          {
            handler.EventListener.Process(evt);

            // Break out of the look so we do not process any more handlers since stop propagation was called
            if (evt.IsComplete)
            {
              break;
            }
          }
        }
      }
    }

    /// <summary>
    /// Helper method meant to handle if this event should be processed and update the timeout for the event itself
    /// </summary>
    /// <param name="evt">The event that should be processed</param>
    /// <returns>Will return true if we do not need to process the event</returns>
    private bool ShouldEventNotProcess(IInternalEvent evt)
    {
      evt.Timeout -= _delta.Delta;
      return evt.Timeout > 0;
    }

    /// <summary>
    /// Disposable class is meant to remove an event from the queue and not process it
    /// </summary>
    private class EventUnsubscriber : IDisposable
    {
      /// <summary>
      /// The event that needs to be cancelled
      /// </summary>
      private readonly IInternalEvent? _event;

      /// <summary>
      /// Constructor meant to create the disposable so that the event can be cancelled if needed
      /// </summary>
      /// <param name="event">The event that needs to be cancelled</param>
      public EventUnsubscriber(IInternalEvent? @event)
      {
        _event = @event;
      }

      /// <summary>
      /// Disposable that is meant to cancel the current event so it is not processed
      /// </summary>
      public void Dispose()
      {
        if (_event != null)
          _event.IsCancelled = true;
      }
    }

    /// <summary>
    /// The disposable class that is meant remove a handle from all the handlers
    /// </summary>
    private class Unsubscriber : IDisposable
    {
      /// <summary>
      /// The handlers that exist in the system
      /// </summary>
      private readonly Dictionary<string, List<EventListenerWeight>> _handlers = new Dictionary<string, List<EventListenerWeight>>();

      /// <summary>
      /// The current handler we will need to remove when the time comes
      /// </summary>
      private readonly IEventListener _handler;

      /// <summary>
      /// The current handler key we are working with
      /// </summary>
      private readonly string _key;

      /// <summary>
      /// Constructor meant to inject various pieces to find out where to remove certain things
      /// </summary>
      /// <param name="handlers"></param>
      /// <param name="handler"></param>
      /// <param name="key"></param>
      internal Unsubscriber(Dictionary<string, List<EventListenerWeight>> handlers, IEventListener handler, string key)
      {
        _handlers = handlers;
        _handler = handler;
        _key = key;
      }

      /// <summary>
      /// Dispose of the handler so that we can free up resources and not need to have to worry about his being created
      /// </summary>
      public void Dispose()
      {
        var item = _handlers[_key].FirstOrDefault(x => x.EventListener == _handler);
        if (item != null)
          _handlers[_key].Remove(item);
      }
    }
  }
}
