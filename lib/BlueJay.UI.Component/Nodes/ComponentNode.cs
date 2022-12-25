using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Nodes.Elements;

namespace BlueJay.UI.Component.Nodes
{
  internal class ComponentNode : Node
  {
    private readonly Node? _root;

    public ComponentNode(UIComponent uiComponent, Type type, IServiceProvider provider)
      : base(type.Name, uiComponent, provider)
    {
      _root = provider.ParseJayTML(type) as Node;
    }

    protected override UIElement? AddEntity(Style style, UIElement? parent, Dictionary<string, object>? scope)
    {
      if (_root == null)
        throw new ArgumentNullException("Could not create root element");
      return _root.GenerateUI(style, parent, scope);
    }
  }
}
