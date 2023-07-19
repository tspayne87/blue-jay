using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Component.Nodes;

namespace BlueJay.UI.Component.Elements
{
  /// <summary>
  /// The slot element meant to create the slot node for where the node tree should continue on for
  /// a component element
  /// </summary>
  internal class UISlotElement : UIElement
  {
    /// <summary>
    /// The slot element constructo meant to attach the node scope and attributes for the scope itself
    /// </summary>
    /// <param name="scope">The node scope for this slot</param>
    /// <param name="attributes">The attributes for this slot</param>
    public UISlotElement(NodeScope scope, List<UIElementAttribute> attributes)
      : base("slot", scope, attributes) { }

    /// <inheritdoc />
    protected override Node? CreateNode()
    {
      return new SlotNode(Scope, Attributes);
    }
  }
}
