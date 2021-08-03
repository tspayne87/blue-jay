using BlueJay.Interfaces;
using BlueJay.Shared.Views;
using BlueJay.UI;
using BlueJay.UI.Component;
using BlueJay.UI.Component.Interactivity;

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
</container>
    ")]
  [Component(typeof(Button), typeof(TextInput), typeof(SwitchInput), typeof(SliderInput))]
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
    /// Constructor to build out the breakcout UI Component
    /// </summary>
    /// <param name="views">The injected views component so we can switch between views</param>
    public UIComponentTestComponent(IViewCollection views)
    {
      Switch = new ReactiveProperty<bool>(false);
      Slider = new ReactiveProperty<int>(0);

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
  }
}
