using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events.Mouse;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;
using System;

namespace BlueJay.UI.Component.Interactivity
{
  /// <summary>
  /// The slider component
  /// </summary>
  [View(@"
<Container @MouseMove.Global=""OnMouseMove($event)"" @MouseUp.Global=""OnMouseUp()"">
  <Container :ref=""Bar"" Style=""Position: Absolute; WidthPercentage: 1; Height: 4; VerticalAlign: Center; BackgroundColor: 200, 200, 200"" />
  <Container Style=""Height: 16; Width: 16; BackgroundColor: 60, 60, 60"" :Style=""SliderStyle"" @MouseDown=""OnMouseDown()"" />
</Container>
    ")]
  public class SliderInput : UIComponent
  {
    /// <summary>
    /// The offset that the tick should be at
    /// </summary>
    private float _tickOffset;

    /// <summary>
    /// The tick x offset
    /// </summary>
    private int _xOffset;

    /// <summary>
    /// The current inner width of the input
    /// </summary>
    private int _innerWidth;

    /// <summary>
    /// The padding that should be used for the tick
    /// </summary>
    private int _padding;

    /// <summary>
    /// If we are selected
    /// </summary>
    private bool _selected;

    /// <summary>
    /// The model that determines the number for the slider
    /// </summary>
    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<int> Model;

    /// <summary>
    /// The minimum value that should be used for the slider
    /// </summary>
    [Prop]
    public readonly ReactiveProperty<int> Min;

    /// <summary>
    /// The maxiumum value that should be used for the slider
    /// </summary>
    [Prop]
    public readonly ReactiveProperty<int> Max;

    /// <summary>
    /// The number of ticks that need to use
    /// </summary>
    [Prop]
    public readonly ReactiveProperty<int> Ticks;

    /// <summary>
    /// The slider style that should be used
    /// </summary>
    [Prop]
    public readonly ReactiveStyle SliderStyle;

    /// <summary>
    /// The bar of the slider
    /// </summary>
    public IEntity Bar;

    /// <summary>
    /// Constructor to set up all the defaults for the class
    /// </summary>
    public SliderInput()
    {
      _padding = 3;
      _tickOffset = 0;
      _xOffset = 0;
      _selected = false;

      Model = new ReactiveProperty<int>(0);
      Min = new ReactiveProperty<int>(0);
      Max = new ReactiveProperty<int>(100);
      Ticks = new ReactiveProperty<int>(20);
      SliderStyle = new ReactiveStyle();
      SliderStyle.LeftOffset = _padding;
    }

    /// <summary>
    /// When the mouse down event triggers on the bar itself
    /// </summary>
    /// <returns>Will return true to continue with propegation</returns>
    public bool OnMouseDown()
    {
      _selected = true;
      if (Bar != null)
      {
        var innerWidth = Bar.GetAddon<BoundsAddon>().Bounds.Width;
        var pa = Bar.GetAddon<PositionAddon>();
        if (innerWidth > 0 && Ticks.Value > 0)
        {
          _xOffset = (int)pa.Position.X + _padding;
          _innerWidth = innerWidth - (_padding * 2) - 16;
          _tickOffset = Math.Max((float)_innerWidth / Ticks.Value, 1f);
        }
      }
      return true;
    }

    /// <summary>
    /// Mouse move event that will move the tick slider based on where the cursor goes
    /// </summary>
    /// <param name="evt">The mouse move event</param>
    /// <returns>Will return true to continue with propegation</returns>
    public bool OnMouseMove(MouseMoveEvent evt)
    {
      if (_selected && _tickOffset != 0)
      {
        var tick = (evt.Position.X - _xOffset) / _tickOffset;
        SliderStyle.LeftOffset = MathHelper.Clamp((int)((int)tick * _tickOffset) + _padding, _padding, _innerWidth);
        Model.Value = MathHelper.Clamp((int)tick * (Max.Value / Ticks.Value), Min.Value, Max.Value);
      }
      return true;
    }

    /// <summary>
    /// Mouse up to stop the selected state
    /// </summary>
    /// <returns>Will return true to continue with propegation</returns>
    public bool OnMouseUp()
    {
      _selected = false;
      return true;
    }
  }
}
