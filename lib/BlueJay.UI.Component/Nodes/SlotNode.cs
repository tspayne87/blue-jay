using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Nodes.Elements;

namespace BlueJay.UI.Component.Nodes
{
  internal class SlotNode : Node
  {
    public SlotNode(UIComponent uiComponent, IServiceProvider provider)
      : base("slot", uiComponent, provider) { }

    protected override UIElement? AddEntity(Style style, UIElement? parent, Dictionary<string, object>? scope)
    {
      if (parent == null)
        throw new ArgumentNullException("Slot element must have a parent");
      return CreateUIElement<UISlot>(parent.Entity);
    }
  }
}
