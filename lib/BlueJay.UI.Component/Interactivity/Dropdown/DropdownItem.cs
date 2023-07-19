using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Interactivity.Dropdown
{
  /// <summary>
  /// UI component meant to help dropdown components render the items they have
  /// </summary>
  [View(@"
    <Container @Select=""SelectItem()"">Test: {{GetText()}}</Container>
  ")]
  public class DropdownItem : UIComponent
  {
    /// <summary>
    /// The item props that the user can select from
    /// </summary>
    [Prop]
    public readonly NullableReactiveProperty<int> Value;

    /// <summary>
    /// The current placeholder that is used when nothing is selected
    /// </summary>
    [Prop]
    public readonly NullableReactiveProperty<Text> Text;

    /// <summary>
    /// Inject on select callback method meant to send the model update on the dropdown input
    /// </summary>
    [Inject]
    public Func<int?, Text, bool>? OnSelect;

    /// <summary>
    /// Constructor meant to bootstrap properties
    /// </summary>
    public DropdownItem()
    {
      Value = new NullableReactiveProperty<int>(null);
      Text = new NullableReactiveProperty<Text>(null);
    }

    /// <summary>
    /// Gets the text that should be rendered in the UI element
    /// </summary>
    /// <param name="item">The item that we need to create the string from</param>
    /// <returns>Will return the string</returns>
    public Text GetText()
    {
      return Text.Value ?? new Text(Value.Value?.ToString() ?? string.Empty);
    }

    /// <summary>
    /// Selects an item when one of the items are selected
    /// </summary>
    /// <param name="item">The current item that needs to be selected</param>
    /// <returns>Returns true to keep propegating</returns>
    public bool SelectItem()
    {
      if (OnSelect != null)
        return OnSelect(Value.Value, Text.Value ?? "--Blank--");
      return false;
    }
  }
}
