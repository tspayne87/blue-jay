using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Test.Components
{
  [View("<Container>Inject: {{Provide}}</Container>")]
  public class InjectComponent : UIComponent
  {
    [Inject]
    public ReactiveProperty<int> Provide;

    public InjectComponent()
    {
      Provide = new ReactiveProperty<int>(0);
    }
  }
}
