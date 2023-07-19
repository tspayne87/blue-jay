using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Component.Nodes;

namespace BlueJay.UI.Component.Elements
{
  /// <summary>
  /// The abstract UIElement that all other UIElements will inherit from and where all the basic functionallity
  /// exists to create the internal node tree to handle the UI Entities in the game itself
  /// </summary>
  internal abstract class UIElement
  {
    /// <summary>
    /// The parent element found during creation
    /// </summary>
    private UIElement? _parent;

    /// <summary>
    /// The children that this element has
    /// </summary>
    public List<UIElement> Children { get; }

    /// <summary>
    /// The name of the element
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// The attributes that should exist for this element
    /// </summary>
    public List<UIElementAttribute> Attributes { get; }

    /// <summary>
    /// The node scope that has been attached to this element
    /// </summary>
    public NodeScope Scope { get; }

    /// <summary>
    /// The parent element that this element should have
    /// </summary>
    public UIElement? Parent
    {
      get => _parent;
      set
      {
        if (_parent != null)
          _parent.Children.Remove(this);

        _parent = value;
        if (_parent != null)
          _parent.Children.Add(this);
      }
    }

    /// <summary>
    /// Constructor meant to buidl out the UIElement
    /// </summary>
    /// <param name="name">The name of the element</param>
    /// <param name="scope">The node scope that has been attached to this element</param>
    /// <param name="attributes">The attributes that should exist for this element</param>
    public UIElement(string name, NodeScope scope, List<UIElementAttribute> attributes)
    {
      Name = name;
      Scope = scope;
      Attributes = attributes;
      Children = new List<UIElement>();
    }


    public NodeRoot GenerateShadowTree()
    {
      var node = GenerateNode(null);
      if (node == null)
        throw new ArgumentNullException("Could not generate shadow tree with a blank root node");

      var root = new NodeRoot(Scope);
      node.Parent = root;
      return root;
    }

    /// <summary>
    /// Generates a node and attaches it's parent as well as finds where all its children should go
    /// </summary>
    /// <param name="parent">The parent that this node should have</param>
    /// <returns>Will return the generated node from the element</returns>
    private Node? GenerateNode(Node? parent)
    {
      var result = CreateNode();
      if (result != null)
      { // As long as a node is created we need to setup the node with it's parent and children
        result.Parent = parent;

        var slottedNode = GetSlottedNode(result);
        foreach (var child in Children)
          child.GenerateNode(slottedNode);
      }
      return result;
    }

    /// <summary>
    /// Helper method to find the node that should be continued from when generating a node, this
    /// will most likely be itself but in some cases it could be a child node if the node was already
    /// generated with children, and a slot should be used to start adding things from there
    /// </summary>
    /// <param name="node">The node we need to find the current node to come from</param>
    /// <returns>Will return the node that should be used for the children nodes</returns>
    protected virtual Node? GetSlottedNode(Node? node)
      => node;

    /// <summary>
    /// Helper method meant to create the current node based on the type of UIElement
    /// </summary>
    /// <param name="provider">The service provider meant to help with getting injected services from</param>
    /// <returns>Will return the root node for the tree created</returns>
    protected abstract Node? CreateNode();
  }
}
