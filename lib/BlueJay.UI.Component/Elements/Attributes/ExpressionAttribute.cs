using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Elements.Attributes
{
  /// <summary>
  /// The basic expression attribute that we will need to use to get the underlining object for this attribute
  /// </summary>
  internal class ExpressionAttribute : UIElementAttribute, ICallableType
  {
    /// <inheritdoc />
    public Func<UIComponent, object?, Dictionary<string, object>?, object?> Callback { get; set; }

    /// <inheritdoc />
    public Func<UIComponent, object?, Dictionary<string, object>?, List<IReactiveProperty?>> ReactiveProperties { get; private set; }

    /// <summary>
    /// Constructor meant to create a basic expression attribute meant to allow adding ':' before an attribute name to configure it
    /// to use the component scope
    /// </summary>
    /// <param name="name">The name of the attribute being created</param>
    /// <param name="value">The callback method meant to get the object configured for this attribute</param>
    /// <param name="reactiveProperties">All the reactive properties found while creating the callback value</param>
    public ExpressionAttribute(string name, Func<UIComponent, object?, Dictionary<string, object>?, object?> value, List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty?>>? reactiveProperties = null)
      : base(name)
    {
      Callback = value;
      ReactiveProperties = (component, evt, scope) => reactiveProperties?.Select(x => x(component, evt, scope)).ToList() ?? new List<IReactiveProperty?>();
    }
  }
}
