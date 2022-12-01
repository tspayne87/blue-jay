using BlueJay.Interfaces;
using BlueJay.Shared.Views;
using BlueJay.UI.Component;
using BlueJay.UI.Component.Attributes;

namespace BlueJay.Shared.Components
{
  /// <summary>
  /// The view component for the layout game
  /// </summary>
  [View(@"
<Container Style=""GridColumns: 5; ColumnGap: 5, 5; TextureFont: Default"">
  <Button Style=""ColumnSpan: 2"" @Select=""OnBackToTitleClick()"">Back To Title</Button>
</Container>
    ")]
  [Component(typeof(Button))]
  internal class LayoutViewComponent : UIComponent
  {
    /// <summary>
    /// The view collection we need to switch between
    /// </summary>
    private IViewCollection _views;

    /// <summary>
    /// Constructor to build out the breakcout UI Component
    /// </summary>
    /// <param name="views">The injected views component so we can switch between views</param>
    public LayoutViewComponent(IViewCollection views)
    {
      _views = views;
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
  }
}
