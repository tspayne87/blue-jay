using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Nodes;
using System.Reflection;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// The abstract UI component meant to be a base for all UI components in the system
  /// </summary>
  public abstract class UIComponent
  {
    /// <summary>
    /// The parent ui element that was created for this ui element
    /// </summary>
    private UIComponent? _parent;

    /// <summary>
    /// The identifier that exists on the scope
    /// </summary>
    internal string Identifier { get; private set; } = $"CI_{Utils.GetNextIdentifier()}";

    /// <summary>
    /// The parent that should exist for this ui element is also meant to attach itself to the children and keep
    /// the node tree intact
    /// </summary>
    public UIComponent? Parent
    {
      get => _parent;
      set
      {
        // Remove this parent if it already exists on a different parent
        if (_parent != null)
          _parent.Children.Remove(this);

        // Add the parent to the new child and update the parent that it has a new child
        _parent = value;
        if (_parent != null)
          _parent.Children.Add(this);
      }
    }

    /// <summary>
    /// The parent component meant to bind to this child component
    /// </summary>
    public List<UIComponent> Children { get; } = new List<UIComponent>();

    /// <summary>
    /// Helper method is called when the component is mounted to the UI tree
    /// </summary>
    public virtual void Mounted() { }
  }
}
