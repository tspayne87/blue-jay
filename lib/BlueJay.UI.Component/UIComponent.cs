using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Language;
using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// The abstract UI component meant to be a base for all UI components in the system
  /// </summary>
  public abstract class UIComponent
  {
    private readonly List<IDisposable> _subscriptions = new List<IDisposable>();

    /// <summary>
    /// The root entity that was created for this UI component
    /// </summary>
    public IEntity Root { get; set; }

    /// <summary>
    /// The parent of this UI component so we can process emits and bubble up events
    /// </summary>
    public UIComponent Parent { get; private set; }

    /// <summary>
    /// The events that were found on the node creating this
    /// </summary>
    internal List<ElementEvent> Events { get; private set; }

    /// <summary>
    /// The identifier that exists on the scope
    /// </summary>
    internal string Identifier { get; set; }

    /// <summary>
    /// Initialization method is meant to set all the basic properties on this component
    /// </summary>
    /// <param name="parent">The parent component we are processing</param>
    internal void Initialize(UIComponent parent, List<ElementEvent> events)
    {
      Parent = parent;
      Events = events;

      ClearSubscriptions();
    }

    /// <summary>
    /// Helper method is called when the component is mounted to the UI tree
    /// </summary>
    public virtual void Mounted() { }

    /// <summary>
    /// Emitter method is meant to bubble up events that were bound in the internal parent view when using the component
    /// </summary>
    /// <typeparam name="T">The type of emitable data we are sending</typeparam>
    /// <param name="eventName">The event name that will be concatinated with 'on' to determine what method to grab from the xml</param>
    /// <param name="data">The data being passed to the method</param>
    /// <returns>Method will need to return a boolean to determine if propegation should continue</returns>
    public bool Emit<T>(string eventName, T data)
    {
      var evt = Events.FirstOrDefault(x => x.Name == eventName);
      var reactiveRoot = Root as ReactiveEntity;
      if (reactiveRoot != null && evt != null)
      {
        return ElementHelper.InvokeEvent(evt, reactiveRoot.Scope, data);
      }
      return true;
    }

    /// <summary>
    /// Helper method is meant to process watch expressions on a reactive entity
    /// </summary>
    internal void ProcessWatch()
    {
      var items = GetType().GetMethods()
        .Select(x => new { Method = x, WatchProp = x.GetCustomAttributes(typeof(WatchAttribute), false).FirstOrDefault() as WatchAttribute })
        .Where(x => x.WatchProp != null);

      foreach (var item in items)
      {
        var field = GetType().GetField(item.WatchProp.Prop);
        if (field.IsInitOnly && typeof(IReactiveProperty).IsAssignableFrom(field.FieldType))
        {
          var reactive = field.GetValue(this) as IReactiveProperty;
          if (reactive != null)
          {
            _subscriptions.Add(reactive.Subscribe(x => item.Method.Invoke(this, new object[] { x.Data })));
          }
        }
      }
    }

    /// <summary>
    /// Clear subscriptions so we can clear up subscriptions if this entity is removed
    /// </summary>
    private void ClearSubscriptions()
    {
      foreach (var subscription in _subscriptions)
        subscription.Dispose();
      _subscriptions.Clear();
    }
  }
}
