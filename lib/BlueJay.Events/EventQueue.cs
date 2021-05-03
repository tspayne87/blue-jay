﻿using BlueJay.Events.Interfaces;
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
    public void AddEventListener<T>(IEventListener<T> handler)
    {
      var name = typeof(T).Name;
      if (!_handlers.ContainsKey(name)) _handlers[name] = new List<IEventListener>();
      _handlers[name].Add(handler);
    }

    /// <summary>
    /// Helper method is meant to add basic event listeners based on a callback into the system so they can interact
    /// with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="callback">The callback that should be called when the event listener is processed</param>
    /// <param name="target">The target this callback should be attached to</param>
    public void AddEventListener<T>(Func<T, bool> callback, object target = null)
    {
      AddEventListener(new CallbackListener<T>(callback, target));
    }

    /// <summary>
    /// Helper method to process the current queue
    /// </summary>
    public void Update()
    {
      while (_current.Count > 0)
      {
        var item = _current.Dequeue();
        if (_handlers.ContainsKey(item.Name))
        {
          for (var i = 0; i < _handlers[item.Name].Count; ++i)
          {
            if (_handlers[item.Name][i].ShouldProcess(item))
            {
              _handlers[item.Name][i].Process(item);

              // Break out of the look so we do not process any more handlers since stop propagation was called
              if (item.IsComplete)
              {
                break;
              }
            }
          }
        }
      }
    }

    /// <summary>
    /// Handle the draw event for the event queue
    /// </summary>
    public void Draw()
    {
      var evt = new Event<DrawEvent>(new DrawEvent());
      if (_handlers.ContainsKey(evt.Name))
      {
        for (var i = 0; i < _handlers[evt.Name].Count; ++i)
        {
          if (_handlers[evt.Name][i].ShouldProcess(evt))
          {
            _handlers[evt.Name][i].Process(evt);
          }
        }
      }
    }

    /// <summary>
    /// Helper method to push whatever is in the defered queue into the current queue
    /// </summary>
    public void Tick()
    {
      DispatchEvent(new UpdateEvent());
      while (_next.Count > 0)
      {
        _current.Enqueue(_next.Dequeue());
      }
    }
  }
}
