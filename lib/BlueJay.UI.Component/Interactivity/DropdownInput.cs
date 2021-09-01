using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace BlueJay.UI.Component.Interactivity
{
  [View(@"
<Container @Select=""OpenMenu()"">
  {{GetField(Model)}}
  <Container if=""ShowMenu"" ref=""Dropdown"" Style=""Position: Absolute; TopOffset: {{Height}}"">
    <Container for=""var $item in Items"" @Select=""OnSelect($item)"" Style=""Padding: 5"">{{GetField($item)}}</Container>
  </Container>
</Container>
    ")]
  public class DropdownInput : UIComponent
  {
    private readonly FontCollection _fonts;

    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<object> Model;
    [Prop]
    public readonly ReactiveCollection<object> Items;
    [Prop]
    public readonly ReactiveProperty<string> Placeholder;
    public readonly ReactiveProperty<int> Height;
    public readonly ReactiveProperty<bool> ShowMenu;


    public IEntity Dropdown { get; set; }

    public DropdownInput(FontCollection fonts)
    {
      Model = new ReactiveProperty<object>(null);
      Placeholder = new ReactiveProperty<string>("Select Item...");
      Items = new ReactiveCollection<object>();
      ShowMenu = new ReactiveProperty<bool>(false);
      Height = new ReactiveProperty<int>(0);

      _fonts = fonts;
    }

    public override void Mounted()
    {
      Height.Value = (int)Root.MeasureString(" ", _fonts).Y + 5;
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
