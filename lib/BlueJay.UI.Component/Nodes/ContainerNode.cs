using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Factories;

namespace BlueJay.UI.Component.Nodes
{
  /// <summary>
  /// The container node meant to create a basic container element to wrap other elements in
  /// </summary>
  internal class ContainerNode : Node
  {
    /// <summary>
    /// Constructor to create the container node
    /// </summary>
    /// <param name="scope">The current node scope this container node exists in</param>
    /// <param name="attributes">The attributes that should be attached to this component</param>
    public ContainerNode(NodeScope scope, List<UIElementAttribute> attributes)
      : base(scope, attributes) { }

    /// <inheritdoc />
    protected override List<UIEntity>? AddEntity(Style style, UIEntity? parent, Dictionary<string, object>? scope)
    {
      return new List<UIEntity>() { CreateUIElement(Scope.ServiceProvider.AddContainer(style, null, parent?.Entity)) };
    }
  }
}
