using BlueJay.Component.System.Interfaces;
using System.Xml;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// The abstract UI component meant to be a base for all UI components in the system
  /// </summary>
  public abstract class UIComponent
  {
    /// <summary>
    /// The current xml node that represents this component
    /// </summary>
    public XmlNode Node { get; private set; }

    /// <summary>
    /// The current UI component we are parsing from, meaning the UI component that has all the data for the global components
    /// </summary>
    public UIComponent Current { get; private set; }

    /// <summary>
    /// The root entity that was created for this UI component
    /// </summary>
    public IEntity Root { get; set; }

    /// <summary>
    /// The parent of this UI component so we can process emits and bubble up events
    /// </summary>
    public UIComponent Parent { get; private set; }

    /// <summary>
    /// Initialization method is menat to set all the basic properties on this component
    /// </summary>
    /// <param name="node">The node we are working with for this component</param>
    /// <param name="current">The current component we are processing</param>
    /// <param name="parent">The parent component we are processing</param>
    public void Initialize(XmlNode node, UIComponent current, UIComponent parent)
    {
      Node = node;
      Current = current;
      Parent = parent;
    }

    /// <summary>
    /// Helper method is meant to render an entity based on the component, the view attribute should not be used if
    /// the developer is planning on using this method
    /// </summary>
    /// <param name="parent">The current parent entity we need to bind to</param>
    /// <returns>Will return an entity that was created during render</returns>
    public virtual IEntity Render(IEntity parent)
    {
      return null;
    }

    /// <summary>
    /// Emitter method is meant to bubble up events that were bound in the internal parent view when using the component
    /// </summary>
    /// <typeparam name="T">The type of emitable data we are sending</typeparam>
    /// <param name="eventName">The event name that will be concatinated with 'on' to determine what method to grab from the xml</param>
    /// <param name="data">The data being passed to the method</param>
    /// <returns>Method will need to return a boolean to determine if propegation should continue</returns>
    public bool Emit<T>(string eventName, T data)
    {
      var method = Current?.GetType().GetMethod(Node?.Attributes?[$"on{eventName}"]?.InnerText ?? string.Empty);
      if (method != null)
      {
        return (bool)method.Invoke(Current, new object[] { data });
      }
      return true;
    }
  }
}
