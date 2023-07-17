using BlueJay.Interfaces;
using BlueJay.Shared.Views;
using BlueJay.UI;
using BlueJay.UI.Component;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Interactivity;
using BlueJay.UI.Component.Interactivity.Dropdown;
using BlueJay.UI.Component.Reactivity;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.Shared.Components
{
    [View(@"
<Container Style=""GridColumns: 5; ColumnGap: 5, 5; TextureFont: Default; Padding: 10"">
  <Button Style=""ColumnSpan: 2"" @Select=""OnBackToTitleClick()"">Back To Title</Button>

  <TextInput Model=""TextInput"" Style=""NinePatch: Sample_NinePatch; Padding: 13; ColumnSpan: 2; ColumnOffset: 3"" @KeyboardUp.Enter=""ClearTextInput()"" />
  <Container Style=""ColumnSpan: 2"">{{TextInput}}</Container>
  <Button @Select=""ClearTextInput()"">Clear</Button>

  <SwitchInput Style=""Height: 25"" Model=""Switch"" />
  <Container if=""Switch"" Style=""ColumnSpan: 4; TextAlign: Left"">Switch On</Container>

  <SliderInput Model=""Slider"" Max=""20"" Style=""ColumnSpan: 3"" />
  <Container Style=""ColumnSpan: 2; TextAlign: Left"">Slider: {{Slider}}</Container>

  <DropdownInput Style=""ColumnSpan: 2; NinePatch: Sample_NinePatch; Padding: 13; WidthPercentage: 1; NinePatch::Hover: Sample_Hover_NinePatch"" Model=""Dropdown"">
    <DropdownMenu Style=""NinePatch: Dropdown_Background_Ninepatch; Padding: 13"">
      <DropdownItem for=""#item in {{DropdownItems}}"" Style=""BackgroundColor: 217, 87, 99"" Value=""#item.Id"" Text=""#item.Name"" />
    </DropdownMenu>
  </DropdownInput>

  <Container Style=""ColumnSpan: 2; GridColumns: 1; ColumnGap: 5, 5"">
    <Button @Select=""AddItem()"">Add Item</Button>
    <Button @Select=""InsertItem()"">Insert Item</Button>
    <Button @Select=""SwitchItem()"">Switch Item</Button>
    <Button @Select=""RemoveRandomItem()"">Remove Random</Button>
  </Container>
  <Container Style=""TextAlign: Left"">{{ShowDropdownItem(Dropdown)}}</Container>
</Container>
    ")]
  [Component(typeof(Button), typeof(TextInput), typeof(SwitchInput), typeof(SliderInput), typeof(DropdownInput), typeof(DropdownMenu), typeof(DropdownItem))]
  public class UIComponentTestComponent : UIComponent
  {
    /// <summary>
    /// The view collection we need to switch between
    /// </summary>
    private IViewCollection _views;

    /// <summary>
    /// Random builder
    /// </summary>
    private Random _rand;

    /// <summary>
    /// The switch value that is currently set in the input component
    /// </summary>
    public readonly ReactiveProperty<bool> Switch;

    /// <summary>
    /// The slider value that is currently being set by the input component
    /// </summary>
    public readonly ReactiveProperty<int> Slider;

    /// <summary>
    /// The text input value that is currently being set by the input component
    /// </summary>
    public readonly ReactiveProperty<Text> TextInput;

    /// <summary>
    /// The model value for the dropdown
    /// </summary>
    public readonly NullableReactiveProperty<int> Dropdown;

    /// <summary>
    /// The list of items that can be chosen from
    /// </summary>
    public readonly ReactiveCollection<SelectableItem> DropdownItems;

    /// <summary>
    /// Constructor to build out the breakcout UI Component
    /// </summary>
    /// <param name="views">The injected views component so we can switch between views</param>
    public UIComponentTestComponent(IViewCollection views, ContentManager content)
    {
      Switch = new ReactiveProperty<bool>(false);
      Slider = new ReactiveProperty<int>(0);
      Dropdown = new NullableReactiveProperty<int>(null);
      TextInput = new ReactiveProperty<Text>("");
      DropdownItems = new ReactiveCollection<SelectableItem>(new List<SelectableItem>()
      {
        new SelectableItem() { Name = "Item 1", Id = 1 },
        new SelectableItem() { Name = "Item 2", Id = 2 },
        new SelectableItem() { Name = "Item 3", Id = 3 },
        new SelectableItem() { Name = "Item 4", Id = 4 },
        new SelectableItem() { Name = "Item 5", Id = 5 }
      });

      _views = views;
      _rand = new Random();
    }

    /// <summary>
    /// Callback method is meant to switch to the title component on click
    /// </summary>
    /// <returns>Will return true representing we should keep propegating this event</returns>
    public bool OnBackToTitleClick()
    {
      _views.SetCurrent<TitleView>();
      return true;
    }

    public bool AddItem()
    {
      DropdownItems.Add(new SelectableItem()
      {
        Name = $"Item {DropdownItems.Count + 1}",
        Id = DropdownItems.Count + 1
      });
      return true;
    }

    public bool InsertItem()
    {
      DropdownItems.Insert(1, new SelectableItem() { Name = "Changed Item 2", Id = 20000000 });
      return true;
    }

    public bool SwitchItem()
    {
      var item = DropdownItems[0];
      DropdownItems[0] = DropdownItems[DropdownItems.Count - 1];
      DropdownItems[DropdownItems.Count - 1] = item;
      return true;
    }

    public bool RemoveRandomItem()
    {
      if (DropdownItems.Count > 0)
        DropdownItems.RemoveAt(_rand.Next(0, DropdownItems.Count));
      return true;
    }

    public bool ClearTextInput()
    {
      TextInput.Value = "";
      return true;
    }

    public string ShowDropdownItem(int? item)
    {
      SelectableItem? selected = null;
      if (DropdownItems.Any(x => x.Id == item))
        selected = DropdownItems.FirstOrDefault(x => x.Id == item);
      return selected?.Name ?? string.Empty;
    }

    [Watch(nameof(Dropdown))]
    public void OnDropdownChange(int? item)
    {
      SelectableItem? selected = null;
      if (DropdownItems.Any(x => x.Id == item))
        selected = DropdownItems.FirstOrDefault(x => x.Id == item);
      TextInput.Value = selected?.Name ?? string.Empty;
    }

    /// <summary>
    /// A dropdown item for the dropdown input
    /// </summary>
    public struct SelectableItem
    {
      /// <summary>
      /// The name of the dropdown item
      /// </summary>
      public Text Name { get; set; }

      /// <summary>
      /// The id of the dropdown item
      /// </summary>
      public int Id { get; set; }
    }
  }
}
