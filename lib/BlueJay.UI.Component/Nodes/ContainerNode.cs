using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Nodes.Elements;
using BlueJay.UI.Factories;
using Attribute = BlueJay.UI.Component.Nodes.Attributes.Attribute;

namespace BlueJay.UI.Component.Nodes
{
  internal class ContainerNode : Node
  {
    public ContainerNode(string name, UIComponent uiComponent, List<Attribute> attributes, IServiceProvider provider)
      : base(name, uiComponent, attributes, provider) { }

    protected override List<UIElement>? AddEntity(Style style, UIElement? parent, Dictionary<string, object>? scope)
    {
      return new List<UIElement>() { CreateUIElement(_provider.AddContainer(style, null, parent?.Entity)) };
    }
  }
}
