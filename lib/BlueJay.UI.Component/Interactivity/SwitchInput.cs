using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Interactivity
{
  [View(@"
<Container Style=""BackgroundColor: 200, 200, 200"" @Select=""OnSelect()"">
  <Container Style=""WidthPercentage: 0.5; HeightPercentage: 1; BackgroundColor: 60, 60, 60"" :Style=""SwitchStyle"" @Select=""OnSelect()"" />
</Container>
    ")]
  public class SwitchInput : UIComponent
  {
    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<bool> Model;
    [Prop]
    public readonly ReactiveStyle SwitchStyle;

    public SwitchInput()
    {
      Model = new ReactiveProperty<bool>(false);
      SwitchStyle = new ReactiveStyle();
      SwitchStyle.HorizontalAlign = HorizontalAlign.Left;
    }

    public bool OnSelect()
    {
      Model.Value = !Model.Value;
      SwitchStyle.HorizontalAlign = Model.Value ? HorizontalAlign.Right : HorizontalAlign.Left;
      return true;
    }
  }
}
