using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Test.Components
{
  [View("<Container>Hello From Child {{ChildCount}}<Slot /></Container>")]
  public class ChildComponent : UIComponent
  {
    public readonly ReactiveProperty<int> ChildCount;

    public ChildComponent()
    {
      ChildCount = new ReactiveProperty<int>(1);
    }
  }
}
