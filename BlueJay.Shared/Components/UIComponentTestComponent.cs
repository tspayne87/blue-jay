using BlueJay.Interfaces;
using BlueJay.Shared.Views;
using BlueJay.UI;
using BlueJay.UI.Component;
using BlueJay.UI.Component.Interactivity;
using System.Collections.Generic;

namespace BlueJay.Shared.Components
{
  [View(@"
<container style=""GridColumns: 5; ColumnGap: 5, 5; TextureFont: Default"">
  <button style=""ColumnSpan: 2"" onSelect=""OnBackToTitleClick"">Back To Title</button>

  <text-input style=""NinePatch: Sample_NinePatch; Padding: 13; ColumnSpan: 5"" />

  <switch-input style=""Height: 25"" Model=""{{Switch}}"" />
  <container if=""{{Switch}}"" style=""ColumnSpan: 4; TextAlign: Left"">Switch On</container>

  <slider-input Model=""{{Slider}}"" Max=""20"" style=""ColumnSpan: 3"" />
  <container style=""ColumnSpan: 2; TextAlign: Left"">Slider: {{Slider}}</container>

  <dropdown-input Model=""{{Dropdown}}"" Field=""Name"" Items=""DropdownItems"" style=""ColumnSpan: 2"" />
</container>
    ")]
  [Component(typeof(Button), typeof(TextInput), typeof(SwitchInput), typeof(SliderInput), typeof(DropdownInput))]
  public class UIComponentTestComponent : UIComponent
  {
    /// <summary>
    /// The view collection we need to switch between
    /// </summary>
    private IViewCollection _views;

    /// <summary>
    /// The switch value that is currently set in the input component
    /// </summary>
    public readonly ReactiveProperty<bool> Switch;

    /// <summary>
    /// The slider value that is currently being set by the input component
    /// </summary>
    public readonly ReactiveProperty<int> Slider;

    /// <summary>
    /// The model value for the dropdown
    /// </summary>
    public readonly ReactiveProperty<DropdownItem> Dropdown;

    /// <summary>
    /// The list of items that can be chosen from
    /// </summary>
    public readonly ReactiveProperty<List<DropdownItem>> DropdownItems;

    /// <summary>
    /// Constructor to build out the breakcout UI Component
    /// </summary>
    /// <param name="views">The injected views component so we can switch between views</param>
    public UIComponentTestComponent(IViewCollection views)
    {
      Switch = new ReactiveProperty<bool>(false);
      Slider = new ReactiveProperty<int>(0);
      Dropdown = new ReactiveProperty<DropdownItem>(null);
      DropdownItems = new ReactiveProperty<List<DropdownItem>>(new List<DropdownItem>()
      {
        new DropdownItem() { Name = "Item 1", Id = 1 },
        new DropdownItem() { Name = "Item 2", Id = 2 },
        new DropdownItem() { Name = "Item 3", Id = 3 },
        new DropdownItem() { Name = "Item 4", Id = 4 },
        new DropdownItem() { Name = "Item 5", Id = 5 }
      });

      _views = views;
    }

    /// <summary>
    /// Callback method is meant to switch to the title component on click
    /// </summary>
    /// <param name="evt">The event that was sent from the triggered event</param>
    /// <returns>Will return true representing we should keep propegating this event</returns>
    public bool OnBackToTitleClick(SelectEvent evt)
    {
      _views.SetCurrent<TitleView>();
      return true;
    }

    /// <summary>
    /// A dropdown item for the dropdown input
    /// </summary>
    public class DropdownItem
    {
      /// <summary>
      /// The name of the dropdown item
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// The id of the dropdown item
      /// </summary>
      public int Id { get; set; }
    }
  }
}
