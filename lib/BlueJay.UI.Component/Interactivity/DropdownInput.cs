using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Interactivity
{
  [View(@"
<container>
  {{GetField(Model)}}
  <container>
    <container foreach=""var item in Items"" e:Select=""OnSelect(item)"">{{GetField(item)}}</container>
  </container>
</container>
    ")]
  public class DropdownInput : UIComponent
  {
    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<object> Model;
    [Prop]
    public readonly ReactiveProperty<string> Field;
    [Prop]
    public readonly ReactiveProperty<List<object>> Items;

    public DropdownInput()
    {
      Model = new ReactiveProperty<object>(null);
      Field = new ReactiveProperty<string>(string.Empty);
      Items = new ReactiveProperty<List<object>>(new List<object>());

      foreach(var item in Items.Value)
      {

      }
    }

    public object GetField(object item)
    {
      if (item == null) return null;

      var fieldProp = item.GetType().GetField(Field.Value);
      if (fieldProp != null) return fieldProp.GetValue(item);

      var prop = item.GetType().GetProperty(Field.Value);
      if (prop != null) return fieldProp.GetValue(item);

      return null;
    }

    public bool OnSelect(object item)
    {
      Model.Value = item;
      return true;
    }
  }
}
