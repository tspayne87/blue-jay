using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Interactivity
{
  /// <summary>
  /// The dropdown input component
  /// </summary>
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
    /// <summary>
    /// The font collection
    /// </summary>
    private readonly IFontCollection _fonts;

    /// <summary>
    /// The model that pushes out what selected item from the dropdown
    /// </summary>
    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<object> Model;

    /// <summary>
    /// The item props that the user can select from
    /// </summary>
    [Prop]
    public readonly ReactiveCollection<object> Items;

    /// <summary>
    /// The current placeholder that is used when nothing is selected
    /// </summary>
    [Prop]
    public readonly ReactiveProperty<string> Placeholder;

    /// <summary>
    /// The menu style prop to allow for different styles for the dropdown input
    /// </summary>
    [Prop]
    public readonly ReactiveStyle MenuStyle;

    /// <summary>
    /// The hover style prop to allow for different styles for the dropdown input
    /// </summary>
    [Prop]
    public readonly ReactiveStyle MenuHoverStyle;

    /// <summary>
    /// The item style which is used to style the items the user can select
    /// </summary>
    [Prop]
    public readonly ReactiveStyle ItemStyle;

    /// <summary>
    /// The item hover style which is used to style the items the user can select
    /// </summary>
    [Prop]
    public readonly ReactiveStyle ItemHoverStyle;

    /// <summary>
    /// Reactive element to show the menu when selected
    /// </summary>
    public readonly ReactiveProperty<bool> ShowMenu;

    /// <summary>
    /// The constructor to build out all the basic configurations for the component
    /// </summary>
    /// <param name="fonts">The item props that the user can select from</param>
    public DropdownInput(IFontCollection fonts)
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

    /// <summary>
    /// Gets the text that should be rendered in the UI element
    /// </summary>
    /// <param name="item">The item that we need to create the string from</param>
    /// <returns>Will return the string</returns>
    public string GetField(object item)
    {
      return item?.ToString() ?? Placeholder.Value;
    }

    /// <summary>
    /// Opens the menu for the dropdown
    /// </summary>
    /// <returns>Returns true to keep propegating</returns>
    public bool OpenMenu()
    {
      ShowMenu.Value = !ShowMenu.Value;
      return true;
    }

    /// <summary>
    /// Selects an item when one of the items are selected
    /// </summary>
    /// <param name="item">The current item that needs to be selected</param>
    /// <returns>Returns true to keep propegating</returns>
    public bool OnSelect(object item)
    {
      Model.Value = item;
      ShowMenu.Value = false;
      return true;
    }
  }
}
