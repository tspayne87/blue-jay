using BlueJay.UI.Component.Elements.Attributes;

namespace BlueJay.UI.Component.Nodes
{
  /// <summary>
  /// The slot node that represents where the current scope should be based on the children entities
  /// </summary>
  internal class SlotNode : Node
  {
    /// <summary>
    /// Constructor to build out the scope node
    /// </summary>
    /// <param name="scope">The current node scope for the slot node</param>
    /// <param name="attributes">The attribute that are attached to the scope</param>
    public SlotNode(NodeScope scope, List<UIElementAttribute> attributes)
      : base(scope, attributes) { }

    /// <inheritdoc />
    protected override List<UIEntity> AddEntity(Style style, UIEntity? parent, Dictionary<string, object>? scope)
    {
      var root = GetRoot(parent);
      if (root?.Parent?.ScopeKey == null)
        throw new ArgumentNullException("Slot element must have a parent or root");
      if (parent == null)
        throw new ArgumentNullException(nameof(parent));
      return new List<UIEntity>() { new UIEntity(this, root.Parent.ScopeKey.Value, null) };
    }

    /// <summary>
    /// Helper method is meant to get the first root entity for this slot since we need to use it's parent
    /// scope when determing where the UIEntity currently is
    /// </summary>
    /// <param name="element">The UI entity that we are looking at</param>
    /// <returns>Will return the first parent node root found in the tree structure</returns>
    public UIEntity? GetRoot(UIEntity? element)
    {
      if (element == null)
        return null;

      if (element.Node is NodeRoot)
        return element;
      return GetRoot(element.Parent);
    }
  }
}
