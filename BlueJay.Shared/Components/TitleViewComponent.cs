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
<Container Style=""Width: 66%; Height: 75%; Padding: 20; VerticalAlign: Center; HorizontalAlign: Center; NinePatch: Sample_NinePatch"">
  <Container Style=""FontSize: 2"">
    <Container Style=""Height: 100%; Padding: 15; NinePatch: Sample_NinePatch"">
      <Texture AssetName=""BlueJay_Logo"" Style=""VerticalAlign: Center; HorizontalAlign: Center"" />
    </Container>
  </Container>

  <Button @Select=""OnBreakoutClick()"">{{BreakoutTitle}}</Button>
  <Button @Select=""OnLayoutClick()"">{{LayoutTitle}}</Button>
  <Button @Select=""OnLDtkClick()"">{{LDtkTitle}}</Button>
  <Button @Select=""OnUIComponentClick()"">{{UIComponentTitle}}</Button>
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
    /// The LDtk title for this component
    /// </summary>
    public readonly ReactiveProperty<Text> LDtkTitle;

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
      LDtkTitle = new ReactiveProperty<Text>("LDtk");
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
    public bool OnLDtkClick()
    {
      _views.SetCurrent<LDtkView>();
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
