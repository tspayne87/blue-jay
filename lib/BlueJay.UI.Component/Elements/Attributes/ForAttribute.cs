using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Elements.Attributes
{
  /// <summary>
  /// The for attribute to setup for repeating the element with this attribute
  /// </summary>
  internal class ForAttribute : UIElementAttribute, ICallableType
  {
    /// <summary>
    /// The name of the scope variable that should be attached to the data scope object
    /// </summary>
    public string ScopeName { get; set; }

    /// <inheritdoc />
    public Func<UIComponent, object?, Dictionary<string, object>?, object> Callback { get; set; }

    /// <inheritdoc />
    public Func<UIComponent, object?, Dictionary<string, object>?, List<IReactiveProperty?>> ReactiveProperties { get; private set; }

    /// <summary>
    /// The for attribute to create multiple versions of this same set of elements
    /// </summary>
    /// <param name="scopeName">The name of the scope variable being used</param>
    /// <param name="value">The value to trigger to get the object that represents the enumerable object</param>
    /// <param name="reactiveProperties">The reactive properties that were found when generating the callback function</param>
    public ForAttribute(string scopeName, Func<UIComponent, object?, Dictionary<string, object>?, object> value, List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty?>> reactiveProperties)
      : base("for")
    {
      ScopeName = scopeName;
      Callback = value;
      ReactiveProperties = (component, evt, scope) => reactiveProperties.Select(x => x(component, evt, scope)).ToList();
    }
  }
}
