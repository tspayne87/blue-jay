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
  <Container :if=""ShowMenu"" Style=""Position: Absolute"" :Style=""MenuStyle"" :HoverStyle=""MenuHoverStyle"">
    <Container :for=""$item in Items"" @Select=""OnSelect($item)"" Style=""Padding: 5"" :Style=""ItemStyle"" :HoverStyle=""ItemHoverStyle"">{{GetField($item)}}</Container>
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
    [Prop]
    public readonly ReactiveStyle MenuStyle;
    [Prop]
    public readonly ReactiveStyle MenuHoverStyle;
    [Prop]
    public readonly ReactiveStyle ItemStyle;
    [Prop]
    public readonly ReactiveStyle ItemHoverStyle;
    public readonly ReactiveProperty<bool> ShowMenu;

    public DropdownInput(FontCollection fonts)
    {
      Model = new ReactiveProperty<object>(null);
      Placeholder = new ReactiveProperty<string>("Select Item...");
      Items = new ReactiveCollection<object>();
      ShowMenu = new ReactiveProperty<bool>(false);
      MenuStyle = new ReactiveStyle();
      MenuHoverStyle = new ReactiveStyle();
      ItemStyle = new ReactiveStyle();
      ItemHoverStyle = new ReactiveStyle();

      _fonts = fonts;
    }

    public override void Mounted()
    {
      MenuStyle.TopOffset = (int)Root.MeasureString(" ", _fonts).Y + 5;
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
