using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Test.Components
{
  [View("<Container>Provide: {{Provide}}<Slot /></Container>")]
  public class ProvideComponent : UIComponent
  {
    [Provide]
    public readonly ReactiveProperty<int> Provide;

    public ProvideComponent()
    {
      Provide = new ReactiveProperty<int>(0);
    }
  }
}
