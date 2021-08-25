using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Interactivity
{
  [View(@"
<Container Style=""BackgroundColor: 200, 200, 200"" @Select=""OnSelect()"">
  <Container Style=""WidthPercentage: 0.5; HeightPercentage: 1; BackgroundColor: 60, 60, 60; HorizontalAlign: {{Align}}"" @Select=""OnSelect()"" />
</Container>
    ")]
  public class SwitchInput : UIComponent
  {
    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<bool> Model;
    public readonly ReactiveProperty<HorizontalAlign> Align;

    public SwitchInput()
    {
      Model = new ReactiveProperty<bool>(false);
      Align = new ReactiveProperty<HorizontalAlign>(HorizontalAlign.Left);

      // Add Property changes
      Model.PropertyChanged += (sender, o) =>
      {
        Emit("Input", Model.Value);
      };
    }

    public bool OnSelect()
    {
      Model.Value = !Model.Value;
      Align.Value = Model.Value ? HorizontalAlign.Right : HorizontalAlign.Left;
      return true;
    }
  }
}
