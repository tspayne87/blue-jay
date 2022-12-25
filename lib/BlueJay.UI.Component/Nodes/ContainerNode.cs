using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Nodes.Elements;
using BlueJay.UI.Factories;

namespace BlueJay.UI.Component.Nodes
{
    internal class ContainerNode : Node
  {
    public ContainerNode(string name, UIComponent uiComponent, IServiceProvider provider)
      : base(name, uiComponent, provider) { }

    protected override UIElement? AddEntity(Style style, UIElement? parent, Dictionary<string, object>? scope)
    {
      return CreateUIElement(_provider.AddContainer(style, null, parent?.Entity));
    }
  }
}
