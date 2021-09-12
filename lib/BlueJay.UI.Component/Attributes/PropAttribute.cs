using System;

namespace BlueJay.UI.Component.Attributes
{
  /// <summary>
  /// The prop attribute to allow for data to be sent into and taking out of components without an issue
  /// This attribute should only be available for ReactiveProperty fields
  /// </summary>
  [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
  public class PropAttribute : Attribute
  {
    /// <summary>
    /// The type of binding this prop should use
    /// </summary>
    public PropBinding Binding { get; private set; }

    /// <summary>
    /// Prop attribute to determine if this field will be considered a prop or not
    /// </summary>
    /// <param name="binding">The type of binding this prop should use</param>
    public PropAttribute(PropBinding binding = PropBinding.OneWay)
    {
      Binding = binding;
    }
  }

  /// <summary>
  /// Prop binding is meant to handle the type of binding this prop will have for components using it
  /// </summary>
  public enum PropBinding
  {
    /// <summary>
    /// If this prop does not have a binding
    /// </summary>
    None,

    /// <summary>
    /// One way binding should only allow the parent to alter the data for the prop
    /// </summary>
    OneWay,

    /// <summary>
    /// Two way binding should allow for both the child and parent to alter the prop
    /// </summary>
    TwoWay
  }
}
