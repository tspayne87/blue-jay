using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Interactivity
{
  [View(@"<switch min=""0"" max=""1"" model=""{{InnerModel}}"" />")]
  [Component(typeof(SliderInput))]
  public class SwitchInput : UIComponent
  {
    public ReactiveProperty<int> InnerModel;
    public ReactiveProperty<bool> Model;

    public SwitchInput()
    {
      InnerModel = new ReactiveProperty<int>(0);
      Model = new ReactiveProperty<bool>(false);

      // Add Property changes
      InnerModel.PropertyChanged += (sender, o) => Model.Value = InnerModel.Value == 1;
      Model.PropertyChanged += (sender, o) => InnerModel.Value = Model.Value ? 1 : 0;
    }
  }
}
