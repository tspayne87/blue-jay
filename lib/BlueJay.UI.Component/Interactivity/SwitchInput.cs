using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Interactivity
{
  /// <summary>
  /// The switch input for the UI
  /// </summary>
  [View(@"
<Container Style=""BackgroundColor: 200, 200, 200"" @Select=""OnSelect()"">
  <Container Style=""WidthPercentage: 0.5; HeightPercentage: 1; BackgroundColor: 60, 60, 60; HorizontalAlign: {{Alignment}}"" @Select=""OnSelect()"" />
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
    /// The current alignment that should be used for the switch item
    /// </summary>
    public readonly ReactiveProperty<HorizontalAlign> Alignment;

    /// <summary>
    /// Constructor that will build out the switch input
    /// </summary>
    public SwitchInput()
    {
      Model = new ReactiveProperty<bool>(false);
      Alignment = new ReactiveProperty<HorizontalAlign>(HorizontalAlign.Left);
    }

    /// <summary>
    /// On select of the input to switch the state of the switch input
    /// </summary>
    /// <returns>Will return true to continue propegation</returns>
    public bool OnSelect()
    {
      Model.Value = !Model.Value;
      Alignment.Value = Model.Value ? HorizontalAlign.Right : HorizontalAlign.Left;
      return true;
    }
  }
}
