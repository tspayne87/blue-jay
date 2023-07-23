using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Component.Nodes;

namespace BlueJay.UI.Component.Elements
{
  /// <summary>
  /// The basic container node that will create a container ui entity
  /// </summary>
  internal class UIContainerElement : UIElement
  {
    /// <summary>
    /// Constructor meant to build out a basic container element
    /// </summary>
    /// <param name="name">The name of the container element being created</param>
    /// <param name="scope">The current node scope that this container is attached too</param>
    /// <param name="attributes">The attributes that should be assigned for this container</param>
    public UIContainerElement(string name, NodeScope scope, List<UIElementAttribute> attributes)
      : base(name, scope, attributes) { }

    /// <inheritdoc />
    protected override Node? CreateNode()
    {
      return Name.Equals("Texture", StringComparison.OrdinalIgnoreCase) ?
        new TextureNode(Scope, Attributes) : new ContainerNode(Scope, Attributes);
    }
  }
}
