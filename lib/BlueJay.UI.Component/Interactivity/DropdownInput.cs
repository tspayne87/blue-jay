using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Interactivity
{
  [View(@"
<Container @Select=""OpenMenu()"">
  {{GetField(Model)}}
  <Container if=""ShowMenu"">
    <Container for=""var $item in Items"" @Select=""OnSelect($item)"">{{GetField($item)}}</Container>
  </Container>
</Container>
    ")]
  public class DropdownInput : UIComponent
  {
    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<object> Model;
    [Prop]
    public readonly ReactiveCollection<object> Items;
    [Prop]
    public readonly ReactiveProperty<string> Placeholder;

    public readonly ReactiveProperty<bool> ShowMenu;

    public DropdownInput()
    {
      Model = new ReactiveProperty<object>(null);
      Placeholder = new ReactiveProperty<string>("Select Item...");
      Items = new ReactiveCollection<object>();
      ShowMenu = new ReactiveProperty<bool>(false);
    }

    public string GetField(object item)
    {
      return item?.ToString() ?? Placeholder.Value;
    }

    public bool OpenMenu()
    {
      ShowMenu.Value = !ShowMenu.Value;
      return true;
    }

    public bool OnSelect(object item)
    {
      Model.Value = item;
      ShowMenu.Value = false;
      return true;
    }
  }
}
