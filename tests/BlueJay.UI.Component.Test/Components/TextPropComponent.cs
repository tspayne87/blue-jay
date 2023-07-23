using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Test.Components
{
  [View("<Container>{{Text}}</Container>")]
  public class TextPropComponent : UIComponent
  {
    [Prop]
    public readonly ReactiveProperty<Text> Text;

    public TextPropComponent()
    {
      Text = new ReactiveProperty<Text>("Empty");
    }
  }
}
