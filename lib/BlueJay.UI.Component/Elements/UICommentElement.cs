using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Component.Nodes;

namespace BlueJay.UI.Component.Elements
{
  /// <summary>
  /// A comment that was added in the parsed string
  /// </summary>
  internal class UICommentElement : UIElement
  {
    /// <summary>
    /// Constructor of the comment element
    /// </summary>
    /// <param name="scope">The current node scope that should exist for this comment scope</param>
    public UICommentElement(NodeScope scope)
      : base("comment", scope, new List<UIElementAttribute>()) { }

    /// <inheritdoc />
    protected override Node? CreateNode()
      => null;
  }
}
