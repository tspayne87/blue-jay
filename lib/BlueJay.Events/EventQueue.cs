using BlueJay.Events.Interfaces;
using System.Collections.Generic;

namespace BlueJay.Events
{
  /// <summary>
  /// The event queue is meant to handle the current queue and reset to handle the next defered queue
  /// </summary>
  public class EventQueue : IEventQueue, IEventQueueProcessor
  {
    /// <summary>
    /// The current queue we are working with on any particular frame
    /// </summary>
    private Queue<IEvent<object>> _current = new Queue<IEvent<object>>();

    /// <summary>
    /// The next queue that should store the defered events that should be handled in the next frame
    /// </summary>
    private Queue<IEvent<object>> _next = new Queue<IEvent<object>>();

    /// <summary>
    /// All the handlers we are dealing with when processing events
    /// </summary>
    private Dictionary<string, List<IEventListener<object>>> _handlers = new Dictionary<string, List<IEventListener<object>>>();

    /// <summary>
    /// Helper method is meant to dispatch events, this will defer them to the next frame for the event queue and will not be processed
    /// in the same frame it is triggered
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="evt">The event that is being triggered</param>
    /// <param name="target">The current object that this event is targeting</param>
    public void DispatchEvent<T>(T evt, object target = null)
    {
      _next.Enqueue(new Event<object>(evt, target));
    }

    /// <summary>
    /// Helper method is meant to add on event listeners into the system so they can interact with events that get dispatched
    /// </summary>
    /// <typeparam name="T">The type of event we are working with</typeparam>
    /// <param name="handler">The handler when the event is fired</param>
    public void AddEventListener<T>(IEventListener<T> handler)
    {
      var name = typeof(T).Name;
      if (!_handlers.ContainsKey(name)) _handlers[name] = new List<IEventListener<object>>();
      _handlers[name].Add(handler as IEventListener<object>);
    }

    /// <summary>
    /// Helper method to process the current queue
    /// </summary>
    /// <returns>Will return if we need to continue processing</returns>
    public bool ProcessCurrent()
    {
      while (_current.Count > 0)
      {
        var item = _current.Dequeue();
        if (_handlers.ContainsKey(item.Name))
        {
          for (var i = 0; i < _handlers[item.Name].Count; ++i)
          {
            _handlers[item.Name][i].Process(item);
          }
        }
      }
      return true;
    }

    /// <summary>
    /// Helper method to push whatever is in the defered queue into the current queue
    /// </summary>
    public void Tick()
    {
      while (_next.Count > 0)
      {
        _current.Enqueue(_next.Dequeue());
      }
    }
  }
}
