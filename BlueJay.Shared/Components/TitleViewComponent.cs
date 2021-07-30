using BlueJay.Shared.Views;
using BlueJay.Interfaces;
using BlueJay.UI;
using BlueJay.UI.Component;

namespace BlueJay.Shared.Components
{
  /// <summary>
  /// The basic title view
  /// </summary>
  [View(@"
<container style=""WidthPercentage: 0.66; TopOffset: 50; HorizontalAlign: Center; GridColumns: 3; ColumnGap: 5, 5; NinePatch: Sample_NinePatch; Padding: 13; TextureFont: Default"">
  <container style=""ColumnSpan: 3; Padding: 15; TextureFontSize: 2"">BlueJay Component System</container>

  <button onSelect=""OnBreakoutClick"">{{BreakoutTitle}}</button>
  <button style=""ColumnOffset: 1"" onSelect=""OnTetrisClick"">{{TetrisTitle}}</button>
  <button style=""ColumnSpan: 3"" onSelect=""OnUIComponentClick"">{{UIComponentTitle}}</button>
</container>
    ")]
  [Component(typeof(Button))]
  public class TitleViewComponent : UIComponent
  {
    /// <summary>
    /// The view collection we will use to transition between games
    /// </summary>
    private IViewCollection _views;

    /// <summary>
    /// The breakout title we should be using for breakout
    /// </summary>
    public ReactiveProperty<string> BreakoutTitle;

    /// <summary>
    /// The Tetris title we are using for this component
    /// </summary>
    public ReactiveProperty<string> TetrisTitle;

    /// <summary>
    /// The UI Component title
    /// </summary>
    public ReactiveProperty<string> UIComponentTitle;

    /// <summary>
    /// Constructor is meant to bootstrap the component
    /// </summary>
    /// <param name="views">The views collection we need to switch between views</param>
    public TitleViewComponent(IViewCollection views)
    {
      _views = views;

      BreakoutTitle = new ReactiveProperty<string>("Breakout");
      TetrisTitle = new ReactiveProperty<string>("Tetris");
      UIComponentTitle = new ReactiveProperty<string>("UI Components");
    }

    /// <summary>
    /// Callback method that is triggered when the user clicks the element in the component
    /// </summary>
    /// <param name="evt">The select event</param>
    /// <returns>will return true to continue propegation</returns>
    public bool OnBreakoutClick(SelectEvent evt)
    {
      _views.SetCurrent<BreakOutView>();
      return true;
    }

    /// <summary>
    /// Callback method that is triggered when the user clicks the element in the component
    /// </summary>
    /// <param name="evt">The select event</param>
    /// <returns>will return true to continue propegation</returns>
    public bool OnTetrisClick(SelectEvent evt)
    {
      // TODO: Set Current to Tetris
      return true;
    }

    /// <summary>
    /// Callback method that is triggered when the user clicks the element in the component
    /// </summary>
    /// <param name="evt">The select event</param>
    /// <returns>Will return true to continue propegation</returns>
    public bool OnUIComponentClick(SelectEvent evt)
    {
      _views.SetCurrent<UIComponentView>();
      return true;
    }
  }
}
