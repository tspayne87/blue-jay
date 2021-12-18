using BlueJay.Events.Interfaces;
using BlueJay.Events.Lifecycle;
using System;
using System.Collections.Generic;

namespace BlueJay.Events
{
  /// <summary>
  /// The event queue is meant to handle the current queue and reset to handle the next defered queue
  /// </summary>
  public class EventQueue
  {
    /// <summary>
    /// The current queue we are working with on any particular frame
    /// </summary>
    private Queue<IEvent> _current = new Queue<IEvent>();

    /// <summary>
    /// The next queue that should store the defered events that should be handled in the next frame
    /// </summary>
    private Queue<IEvent> _next = new Queue<IEvent>();

    /// <summary>
    /// All the handlers we are dealing with when processing events
    /// </summary>
    private Dictionary<string, List<IEventListener>> _handlers = new Dictionary<string, List<IEventListener>>();

    /// <summary>
    /// Helper method is meant to dispatch events, this will defer them to the next frame for the event queue and will not be processed
    /// in the same frame it is triggered
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="evt">The event that is being triggered</param>
    public void DispatchEvent<T>(T evt, object target = null)
    {
      var name = typeof(T).Name;
      if (_handlers.ContainsKey(name) && _handlers[name].Count > 0)
      {
        _next.Enqueue(new Event<T>(evt, target));
      }
    }

    /// <summary>
    /// Helper method is meant to add on event listeners into the system so they can interact with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="handler">The handler when the event is fired</param>
    public IDisposable AddEventListener<T>(IEventListener<T> handler)
    {
      var name = typeof(T).Name;
      if (!_handlers.ContainsKey(name)) _handlers[name] = new List<IEventListener>();
      _handlers[name].Add(handler);

      return new Unsubscriber(_handlers, handler, name);
    }

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    public IDisposable AddEventListener<T>(Func<T, bool> callback)
    {
      return AddEventListener (new CallbackListener<T>((x, t) => callback(x), null, false));
    }

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    public IDisposable AddEventListener<T>(Func<T, object, bool> callback)
    {
      return AddEventListener(new CallbackListener<T>(callback, null, false));
    }

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    /// <param name="target">The target this callback should be attached to</param>
    public IDisposable AddEventListener<T>(Func<T, bool> callback, object target)
    {
      return AddEventListener(new CallbackListener<T>((x, t) => callback(x), target, true));
    }

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    /// <param name="target">The target this callback should be attached to</param>
    public IDisposable AddEventListener<T>(Func<T, object, bool> callback, object target)
    {
      return AddEventListener(new CallbackListener<T>(callback, target, true));
    }

    /// <summary>
    /// Helper method to process the current queue
    /// </summary>
    public void Update()
    {
      while (_current.Count > 0)
      {
        ProcessEvent(_current.Dequeue());
      }
    }

    /// <summary>
    /// Handle the draw event for the event queue
    /// </summary>
    public void Draw()
    {
      ProcessEvent(new Event<DrawEvent>(new DrawEvent()));
    }

    /// <summary>
    /// Handle the activate event for the event queue
    /// </summary>
    public void Activate()
    {
      ProcessEvent(new Event<ActivateEvent>(new ActivateEvent()));
    }

    /// <summary>
    /// Handle the deactivate event for the event queue
    /// </summary>
    public void Deactivate()
    {
      ProcessEvent(new Event<DeactivateEvent>(new DeactivateEvent()));
    }

    /// <summary>
    /// Handle the exit event for the event queue
    /// </summary>
    public void Exit()
    {
      ProcessEvent(new Event<ExitEvent>(new ExitEvent()));
      Tick(true);
      Update();
    }

    /// <summary>
    /// Helper method to push whatever is in the defered queue into the current queue
    /// </summary>
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
    private void ProcessEvent(IEvent evt)
    {
      if (_handlers.ContainsKey(evt.Name))
      {
        for (var i = 0; i < _handlers[evt.Name].Count; ++i)
        {
          if (_handlers[evt.Name][i].ShouldProcess(evt))
          {
            _handlers[evt.Name][i].Process(evt);

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
    /// The disposable class that is meant remove a handle from all the handlers
    /// </summary>
    private class Unsubscriber : IDisposable
    {
      /// <summary>
      /// The handlers that exist in the system
      /// </summary>
      private readonly Dictionary<string, List<IEventListener>> _handlers = new Dictionary<string, List<IEventListener>>();

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
      internal Unsubscriber(Dictionary<string, List<IEventListener>> handlers, IEventListener handler, string key)
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
        if (_handlers.ContainsKey(_key) && _handlers[_key].Contains(_handler))
          _handlers[_key].Remove(_handler);
      }
    }
  }
}
