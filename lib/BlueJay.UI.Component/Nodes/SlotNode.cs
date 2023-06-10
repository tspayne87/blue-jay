using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Nodes.Elements;
using Attribute = BlueJay.UI.Component.Nodes.Attributes.Attribute;

namespace BlueJay.UI.Component.Nodes
{
  internal class SlotNode : Node
  {
    public SlotNode(UIComponent uiComponent, List<Attribute> attributes, IServiceProvider provider)
      : base("slot", uiComponent, attributes, provider) { }

    protected override List<UIElement> AddEntity(Style style, UIElement? parent, Dictionary<string, object>? scope)
    {
      if (parent == null)
        throw new ArgumentNullException("Slot element must have a parent");
      return new List<UIElement>() { CreateUIElement<UISlot>(parent.Entity) };
    }
  }
}
