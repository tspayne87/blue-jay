using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Component.Nodes;

namespace BlueJay.UI.Component.Elements
{
  /// <summary>
  /// The representation of the component element
  /// </summary>
  internal class UIComponentElement : UIElement
  {
    /// <summary>
    /// The type of component we will need to create when generating the node
    /// </summary>
    protected readonly Type _type;

    /// <summary>
    /// Constructo meant to create a component element to unwrap on creation
    /// </summary>
    /// <param name="scope">The current node scope this component should exist on</param>
    /// <param name="type">The type of component that will need to be generated</param>
    /// <param name="attributes">The attributes that should be attached to the component being created</param>
    public UIComponentElement(NodeScope scope, Type type, List<UIElementAttribute> attributes)
      : base(type.Name, scope, attributes)
    {
      _type = type;
    }

    /// <inheritdoc />
    protected override Node? CreateNode()
    {
      var node = Scope.ServiceProvider.ParseJayTML(_type, Scope.AddChild(_type));
      if (node is NodeRoot root)
      {
        InjectAttributes(root);
        return root;
      }
      return null;
    }

    /// <inheritdoc />
    protected override Node? GetSlottedNode(Node? node)
    {
      if (node != null)
      {
        var slot = FindSlot(node);
        if (slot != null)
          return slot;
      }
      return null;
    }

    /// <summary>
    /// Helper method meant to find the slot node from the generated node
    /// </summary>
    /// <param name="node">The node we need to check children on to find the slot node</param>
    /// <returns>Will return the first slot node found</returns>
    private SlotNode? FindSlot(Node node)
    {
      foreach (var child in node.Children)
      {
        if (child is SlotNode slot)
          return slot;

        var found = FindSlot(child);
        if (found != null)
          return found;
      }
      return null;
    }

    /// <summary>
    /// Helper method meant to inject the attributes on this element into the base node that was created
    /// </summary>
    /// <param name="node"></param>
    private void InjectAttributes(Node node)
    {
      /// If for some reason this is not a NodeRoot we want to return and not attach anything
      if (node.Children.Count != 1 || !(node is NodeRoot))
        return;

      /// Iterate over all the current attributes to add them to the found node
      foreach (var attr in Attributes)
      {
        if (attr is ForAttribute || attr is IfAttribute || attr is ExpressionAttribute || attr is StringAttribute)
        {
          node.Attributes.Add(attr);
          continue;
        }

        attr.UseParentScope = true;
        if (attr is StyleAttribute styleAttr)
        {
          var nodeStyle = node.Children[0].Attributes.FirstOrDefault(x => x is StyleAttribute) as StyleAttribute;
          if (nodeStyle != null)
          {
            nodeStyle.Styles.AddRange(styleAttr.Styles);
            continue;
          }
        }
        node.Children[0].Attributes.Add(attr);
      }
    }
  }
}