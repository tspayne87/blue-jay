using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;
using BlueJay.UI.Events;

namespace BlueJay.UI.Component.Interactivity.Dropdown
{
  /// <summary>
  /// The dropdown input component
  /// </summary>
  [View(@"
<Container @Select=""ToggleMenu()"">
  {{Text}}
  <Slot />
</Container>
    ")]
  public class DropdownInput : UIComponent
  {
    /// <summary>
    /// The model that pushes out what selected item from the dropdown
    /// </summary>
    [Prop(PropBinding.TwoWay)]
    public readonly NullableReactiveProperty<int> Model;

    /// <summary>
    /// The placeholder text that will be used when an item is not selected
    /// </summary>
    [Prop]
    public readonly ReactiveProperty<Text> Placeholder;

    /// <summary>
    /// Reactive element to show the menu when selected
    /// </summary>
    [Provide]
    public readonly ReactiveProperty<bool> ShowMenu;

    /// <summary>
    /// The current text value that will be displayed if not null
    /// </summary>
    public readonly ReactiveProperty<Text> Text;

    /// <summary>
    /// The constructor to build out all the basic configurations for the component
    /// </summary>
    public DropdownInput()
    {
      Model = new NullableReactiveProperty<int>(null);
      ShowMenu = new ReactiveProperty<bool>(false);
      Placeholder = new ReactiveProperty<Text>("Select Item...");
      Text = new ReactiveProperty<Text>(Placeholder.Value);
    }

    /// <summary>
    /// Opens the menu for the dropdown
    /// </summary>
    /// <returns>Returns true to keep propegating</returns>
    public bool ToggleMenu()
    {
      ShowMenu.Value = !ShowMenu.Value;
      return true;
    }

    [Watch(nameof(Placeholder))]
    public void OnPlaceholderChange(Text placeholder)
    {
      if (Model.Value == null)
        Text.Value = placeholder;
    }

    /// <summary>
    /// Selects an item when one of the items are selected
    /// </summary>
    /// <param name="item">The current item that needs to be selected</param>
    /// <returns>Returns true to keep propegating</returns>
    [Provide]
    public bool OnSelect(int? item, Text text)
    {
      Model.Value = item;
      Text.Value = text;
      ShowMenu.Value = false;
      return true;
    }
  }
}
