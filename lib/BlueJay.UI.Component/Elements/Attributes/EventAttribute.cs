using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Elements.Attributes
{
  /// <summary>
  /// An event attribute meant to be triggered when an event happens on the entity once it is created
  /// </summary>
  internal class EventAttribute : UIElementAttribute, ICallableType
  {
    /// <summary>
    /// The internal modifier that was attached to this event
    /// </summary>
    public string Modifier { get; set; }

    /// <inheritdoc />
    public Func<UIComponent, object?, Dictionary<string, object>?, object> Callback { get; private set; }

    /// <inheritdoc />
    public Func<UIComponent, object?, Dictionary<string, object>?, List<IReactiveProperty?>> ReactiveProperties { get; private set; }

    /// <summary>
    /// Boolean to determine if this is a global event type
    /// </summary>
    public bool IsGlobal => Modifier.Equals("Global", StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Constructor method meant to build out the internal properties for this object
    /// </summary>
    /// <param name="name">The name of the event being called</param>
    /// <param name="modifier">The modifier for the event itself</param>
    /// <param name="callback">The callback method that will be triggered when the event is invoked</param>
    public EventAttribute(string name, string modifier, Func<UIComponent, object?, Dictionary<string, object>?, object> callback)
      : base(name)
    {
      Callback = callback;
      Modifier = modifier;
      ReactiveProperties = (c, e, s) => new List<IReactiveProperty?>();
    }
  }
}
