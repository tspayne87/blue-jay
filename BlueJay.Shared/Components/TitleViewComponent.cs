using BlueJay.Shared.Views;
using BlueJay.Interfaces;
using BlueJay.UI;
using BlueJay.UI.Component;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.Shared.Components
{
  /// <summary>
  /// The basic title view
  /// </summary>
  [View(@"
<Container Style=""WidthPercentage: 0.66; HeightPercentage: 0.75; Padding: 20; VerticalAlign: Center; HorizontalAlign: Center; GridColumns: 3; ColumnGap: 5, 5; NinePatch: Sample_NinePatch; TextureFont: Default"">
  <Container Style=""ColumnSpan: 3; TextureFontSize: 2; HeightTemplate: Stretch; TextBaseline: Center"">
    <Container Style=""HeightPercentage: 1; Padding: 15; NinePatch: Sample_NinePatch"">
      <Texture AssetName=""BlueJay_Logo"" Style=""VerticalAlign: Center; HorizontalAlign: Center"" />
    </Container>
  </Container>

  <Button @Select=""OnBreakoutClick()"">{{BreakoutTitle}}</Button>
  <Button @Select=""OnLayoutClick()"">{{LayoutTitle}}</Button>
  <Button @Select=""OnTetrisClick()"">{{TetrisTitle}}</Button>
  <Button Style=""ColumnSpan: 3"" @Select=""OnUIComponentClick()"">{{UIComponentTitle}}</Button>
</Container>
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
    public readonly ReactiveProperty<Text> BreakoutTitle;

    /// <summary>
    /// The Tetris title we are using for this component
    /// </summary>
    public readonly ReactiveProperty<Text> TetrisTitle;

    /// <summary>
    /// The UI Component title
    /// </summary>
    public readonly ReactiveProperty<Text> UIComponentTitle;

    /// <summary>
    /// The layout component title
    /// </summary>
    private readonly ReactiveProperty<Text> LayoutTitle;

    /// <summary>
    /// Constructor is meant to bootstrap the component
    /// </summary>
    /// <param name="views">The views collection we need to switch between views</param>
    public TitleViewComponent(IViewCollection views)
    {
      _views = views;

      BreakoutTitle = new ReactiveProperty<Text>("Breakout");
      TetrisTitle = new ReactiveProperty<Text>("Tetris");
      UIComponentTitle = new ReactiveProperty<Text>("UI Components");
      LayoutTitle = new ReactiveProperty<Text>("Layout");
    }

    /// <summary>
    /// Callback method that is triggered when the user clicks the element in the component
    /// </summary>
    /// <param name="evt">The select event</param>
    /// <returns>will return true to continue propegation</returns>
    public bool OnBreakoutClick()
    {
      _views.SetCurrent<BreakOutView>();
      return true;
    }

    /// <summary>
    /// Callback method that is triggered when the user clicks the element in the component
    /// </summary>
    /// <param name="evt">The select event</param>
    /// <returns>will return true to continue propegation</returns>
    public bool OnLayoutClick()
    {
      _views.SetCurrent<LayerView>();
      return true;
    }

    /// <summary>
    /// Callback method that is triggered when the user clicks the element in the component
    /// </summary>
    /// <param name="evt">The select event</param>
    /// <returns>will return true to continue propegation</returns>
    public bool OnTetrisClick()
    {
      // TODO: Set Current to Tetris
      return true;
    }

    /// <summary>
    /// Callback method that is triggered when the user clicks the element in the component
    /// </summary>
    /// <param name="evt">The select event</param>
    /// <returns>Will return true to continue propegation</returns>
    public bool OnUIComponentClick()
    {
      _views.SetCurrent<UIComponentView>();
      return true;
    }
  }
}
