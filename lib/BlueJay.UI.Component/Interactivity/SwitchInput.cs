using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Interactivity
{
  /// <summary>
  /// The switch input for the UI
  /// </summary>
  [View(@"
<Container Style=""BackgroundColor: 200, 200, 200"" @Select=""OnSelect()"">
  <Container Style=""WidthPercentage: 0.5; HeightPercentage: 1; BackgroundColor: 60, 60, 60"" :Style=""SwitchStyle"" @Select=""OnSelect()"" />
</Container>
    ")]
  public class SwitchInput : UIComponent
  {
    /// <summary>
    /// The model for the switch input to determine if it is on or off
    /// </summary>
    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<bool> Model;

    /// <summary>
    /// The style of the switch that should be used
    /// </summary>
    [Prop]
    public readonly ReactiveStyle SwitchStyle;

    /// <summary>
    /// Constructor that will build out the switch input
    /// </summary>
    public SwitchInput()
    {
      Model = new ReactiveProperty<bool>(false);
      SwitchStyle = new ReactiveStyle();
      SwitchStyle.HorizontalAlign = HorizontalAlign.Left;
    }

    /// <summary>
    /// On select of the input to switch the state of the switch input
    /// </summary>
    /// <returns>Will return true to continue propegation</returns>
    public bool OnSelect()
    {
      Model.Value = !Model.Value;
      SwitchStyle.HorizontalAlign = Model.Value ? HorizontalAlign.Right : HorizontalAlign.Left;
      return true;
    }
  }
}
