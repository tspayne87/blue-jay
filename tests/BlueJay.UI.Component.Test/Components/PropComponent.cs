using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Test.Components
{
  [View("<Container>TwoWay: {{TwoWay}}, OneWay: {{OneWay}}, None: {{None}} <Slot /></Container>")]
  public class PropComponent : UIComponent
  {
    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<int> TwoWay;

    [Prop]
    public readonly ReactiveProperty<int> OneWay;

    [Prop(PropBinding.None)]
    public readonly ReactiveProperty<int> None;

    public PropComponent()
    {
      TwoWay = new ReactiveProperty<int>(0);
      OneWay = new ReactiveProperty<int>(0);
      None = new ReactiveProperty<int>(0);
    }
  }
}
