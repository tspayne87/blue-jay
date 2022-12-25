using BlueJay.Component.System.Interfaces;

namespace BlueJay.UI.Component.Nodes.Elements
{
  internal class UISlot : UIElement
  {
    public UISlot(ILayerCollection layers, Node node, IEntity entity, List<IDisposable> disposables)
      : base(layers, node, entity, disposables) { }
  }
}
