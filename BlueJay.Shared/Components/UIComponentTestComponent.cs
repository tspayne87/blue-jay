using BlueJay.Core;
using BlueJay.Interfaces;
using BlueJay.Shared.Views;
using BlueJay.UI;
using BlueJay.UI.Component;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Interactivity;
using BlueJay.UI.Component.Reactivity;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BlueJay.Shared.Components
{
  [View(@"
<Container Style=""GridColumns: 5; ColumnGap: 5, 5; TextureFont: Default"">
  <Button Style=""ColumnSpan: 2"" @Select=""OnBackToTitleClick()"">Back To Title</Button>

  <TextInput Style=""NinePatch: Sample_NinePatch; Padding: 13; ColumnSpan: 5"" />

  <SwitchInput Style=""Height: 25"" :Model=""Switch"" />
  <Container :if=""Switch"" Style=""ColumnSpan: 4; TextAlign: Left"">Switch On</Container>

  <SliderInput :Model=""Slider"" Max=""20"" Style=""ColumnSpan: 3"" />
  <Container Style=""ColumnSpan: 2; TextAlign: Left"">Slider: {{Slider}}</Container>

  <DropdownInput Style=""ColumnSpan: 2"" :Items=""DropdownItems"" :Model=""Dropdown"" :MenuStyle=""DropdownMenuStyle"" :ItemHoverStyle=""DropdownHoverItemStyle"" />
  <Container Style=""ColumnSpan: 2; GridColumns: 1; ColumnGap: 5, 5"">
    <Button @Select=""AddItem()"">Add Item</Button>
    <Button @Select=""InsertItem()"">Insert Item</Button>
    <Button @Select=""SwitchItem()"">Switch Item</Button>
    <Button @Select=""RemoveRandomItem()"">Remove Random</Button>
  </Container>
  <Container Style=""TextAlign: Left"">{{ShowDropdownItem(Dropdown)}}</Container>
</Container>
    ")]
  [Component(typeof(Button), typeof(TextInput), typeof(SwitchInput), typeof(SliderInput), typeof(DropdownInput))]
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
    /// The model value for the dropdown
    /// </summary>
    public readonly ReactiveProperty<DropdownItem> Dropdown;

    /// <summary>
    /// The list of items that can be chosen from
    /// </summary>
    public readonly ReactiveCollection<DropdownItem> DropdownItems;

    /// <summary>
    /// The dropdown menu style
    /// </summary>
    public readonly ReactiveStyle DropdownMenuStyle;

    /// <summary>
    /// The hover item style that should be used for the dropdown
    /// </summary>
    public readonly ReactiveStyle DropdownHoverItemStyle;

    /// <summary>
    /// Constructor to build out the breakcout UI Component
    /// </summary>
    /// <param name="views">The injected views component so we can switch between views</param>
    public UIComponentTestComponent(IViewCollection views, ContentManager content)
    {
      Switch = new ReactiveProperty<bool>(false);
      Slider = new ReactiveProperty<int>(0);
      Dropdown = new ReactiveProperty<DropdownItem>(null);
      DropdownItems = new ReactiveCollection<DropdownItem>(new List<DropdownItem>()
      {
        new DropdownItem() { Name = "Item 1", Id = 1 },
        new DropdownItem() { Name = "Item 2", Id = 2 },
        new DropdownItem() { Name = "Item 3", Id = 3 },
        new DropdownItem() { Name = "Item 4", Id = 4 },
        new DropdownItem() { Name = "Item 5", Id = 5 }
      });
      DropdownMenuStyle = new ReactiveStyle();
      DropdownMenuStyle.NinePatch = new NinePatch(content.Load<Texture2D>("Sample_NinePatch"));
      DropdownMenuStyle.Padding = 13;

      DropdownHoverItemStyle = new ReactiveStyle();
      DropdownHoverItemStyle.BackgroundColor = new Microsoft.Xna.Framework.Color(217, 87, 99);

      _views = views;
      _rand = new Random();
    }

    /// <summary>
    /// Callback method is meant to switch to the title component on click
    /// </summary>
    /// <param name="evt">The event that was sent from the triggered event</param>
    /// <returns>Will return true representing we should keep propegating this event</returns>
    public bool OnBackToTitleClick()
    {
      _views.SetCurrent<TitleView>();
      return true;
    }

    public bool AddItem()
    {
      DropdownItems.Add(new DropdownItem()
      {
        Name = $"Item {DropdownItems.Count + 1}",
        Id = DropdownItems.Count + 1
      });
      return true;
    }

    public bool InsertItem()
    {
      DropdownItems.Insert(1, new DropdownItem() { Name = "Changed Item 2", Id = 20000000 });
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

    public string ShowDropdownItem(DropdownItem item)
    {
      return item?.ToString() ?? string.Empty;
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

      /// <inheritdoc />
      public override string ToString()
      {
        return Name;
      }
    }
  }
}
