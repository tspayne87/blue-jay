using BlueJay.Shared.Views;
using BlueJay.Interfaces;
using BlueJay.UI;
using BlueJay.UI.Component;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.Shared.Components
{
  /// <summary>
  /// The breakout view component we need to show some UI on the screen
  /// </summary>
  [View(@"
<Container Style=""GridColumns: 5; ColumnGap: 5, 5; TextureFont: Default"">
  <Button Style=""ColumnSpan: 2"" @Select=""OnBackToTitleClick()"">Back To Title</Button>

  <Container>Round: {{Round}}</Container>
  <Container>Balls: {{Balls}}</Container>
  <Container>Score: {{Score}}</Container>
</Container>
    ")]
  [Component(typeof(Button))]
  public class BreakoutViewComponent : UIComponent
  {
    /// <summary>
    /// The view collection we need to switch between
    /// </summary>
    private IViewCollection _views;

    /// <summary>
    /// The score that should be bound to the view
    /// </summary>
    public readonly ReactiveProperty<int> Score;

    /// <summary>
    /// The round that should be bound to the view
    /// </summary>
    public readonly ReactiveProperty<int> Round;

    /// <summary>
    /// The balls that should be bound to the view
    /// </summary>
    public readonly ReactiveProperty<int> Balls;

    /// <summary>
    /// Constructor to build out the breakcout UI Component
    /// </summary>
    /// <param name="views">The injected views component so we can switch between views</param>
    public BreakoutViewComponent(IViewCollection views)
    {
      _views = views;

      Score = new ReactiveProperty<int>(0);
      Round = new ReactiveProperty<int>(0);
      Balls = new ReactiveProperty<int>(3);
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
