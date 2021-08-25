using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Language;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// The abstract UI component meant to be a base for all UI components in the system
  /// </summary>
  public abstract class UIComponent
  {
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
    public List<ElementEvent> Events { get; private set; }

    /// <summary>
    /// Initialization method is menat to set all the basic properties on this component
    /// </summary>
    /// <param name="parent">The parent component we are processing</param>
    public void Initialize(UIComponent parent, List<ElementEvent> events)
    {
      Parent = parent;
      Events = events;
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
      if (evt != null)
      {
        return (bool)evt.Callback(new Dictionary<string, object>() { { "event", data } });
      }
      return true;
    }
  }
}
