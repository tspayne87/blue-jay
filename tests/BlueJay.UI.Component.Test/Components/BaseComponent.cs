using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Test.Components
{
  [Component(typeof(ChildComponent), typeof(PropComponent), typeof(ProvideComponent), typeof(InjectComponent), typeof(TextPropComponent))]
  public class BaseComponent : UIComponent
  {
    public readonly ReactiveProperty<int> Count;

    public BaseComponent()
    {
      Count = new ReactiveProperty<int>(0);
    }
  }
}
