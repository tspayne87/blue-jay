using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Elements.Attributes
{
  /// <summary>
  /// The if condition for the node to determine if is should be rendered or not
  /// </summary>
  internal class IfAttribute : UIElementAttribute, ICallableType
  {
    /// <inheritdoc />
    public Func<UIComponent, object?, Dictionary<string, object>?, object> Callback { get; private set; }

    /// <inheritdoc />
    public Func<UIComponent, object?, Dictionary<string, object>?, List<IReactiveProperty?>> ReactiveProperties { get; private set; }

    /// <summary>
    /// Constructor to build a if attribute to determine if this node should be generated or not
    /// </summary>
    /// <param name="condition">The callback method to be triggered to determine if to render the following node</param>
    /// <param name="reactiveProperties">The reactive properties that were found in the conditional callback</param>
    public IfAttribute(Func<UIComponent, object?, Dictionary<string, object>?, object> condition, List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty?>> reactiveProperties)
      : base("if")
    {
      Callback = condition;
      ReactiveProperties = (component, evt, scope) => reactiveProperties.Select(x => x(component, evt, scope)).ToList();
    }
  }
}
