using BlueJay.Content.App.Components;
using BlueJay.Content.App.Views;
using BlueJay.Interfaces;
using BlueJay.UI;
using BlueJay.UI.Components;

namespace BlueJay.Content.App.Games.Breakout
{
  /// <summary>
  /// The breakout view component we need to show some UI on the screen
  /// </summary>
  [View(@"
<container style=""GridColumns: 5; ColumnGap: 5, 5"">
  <button onSelect=""OnBackToTitleClick"">Back To Title</button>

  <text style=""TextureFont: Default"">Round: {{Round}}</text>
  <text style=""TextureFont: Default"">Balls: {{Balls}}</text>
  <text style=""TextureFont: Default"">Score: {{Score}}</text>
</container>
    ")]
  [Component(typeof(Button))]
  public class BreakoutViewComponent
  {
    /// <summary>
    /// The view collection we need to switch between
    /// </summary>
    private IViewCollection _views;

    /// <summary>
    /// The score that should be bound to the view
    /// </summary>
    public ReactiveProperty<int> Score;

    /// <summary>
    /// The round that should be bound to the view
    /// </summary>
    public ReactiveProperty<int> Round;

    /// <summary>
    /// The balls that should be bound to the view
    /// </summary>
    public ReactiveProperty<int> Balls;

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
    public bool OnBackToTitleClick(SelectEvent evt)
    {
      _views.SetCurrent<TitleView>();
      return true;
    }
  }
}
