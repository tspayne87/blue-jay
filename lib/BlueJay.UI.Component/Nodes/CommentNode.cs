using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Nodes.Elements;

namespace BlueJay.UI.Component.Nodes
{
  internal class CommentNode : Node
  {
    public CommentNode(UIComponent uiComponent, IServiceProvider provider)
      : base("comment", uiComponent, new List<Attributes.Attribute>(), provider) { }


    protected override List<UIElement>? AddEntity(Style style, UIElement? parent, Dictionary<string, object>? scope)
    {
      return null;
    }
  }
}
