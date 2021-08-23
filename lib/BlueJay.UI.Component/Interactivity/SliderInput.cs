using BlueJay.Common.Addons;
using BlueJay.Core;
using BlueJay.Events.Mouse;
using BlueJay.UI.Addons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueJay.UI.Component.Interactivity
{
  [View(@"
<Container @MouseMove.Global=""OnMouseMove($event)"" @MouseUp.Global=""OnMouseUp()"">
  <Container Style=""Position: Absolute; WidthPercentage: 1; Height: 4; VerticalAlign: Center; BackgroundColor: 200, 200, 200"" />
  <Container Style=""Height: 16; Width: 16; BackgroundColor: 60, 60, 60; LeftOffset: {{Left}}"" @Select=""OnMouseDown()"" />
</Container>
    ")]
  public class SliderInput : UIComponent
  {
    private float _tickOffset;
    private int _xOffset;
    private int _innerWidth;
    private int _padding;
    private bool _selected;

    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<int> Model;
    [Prop]
    public readonly ReactiveProperty<int> Min;
    [Prop]
    public readonly ReactiveProperty<int> Max;
    [Prop]
    public readonly ReactiveProperty<int> Ticks;

    public readonly ReactiveProperty<int> Left;

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

      Left = new ReactiveProperty<int>(_padding);
    }

    public bool OnMouseDown()
    {
      _selected = true;

      var firstChild = Root.GetAddon<LineageAddon>().Children.FirstOrDefault();
      if (firstChild != null)
      {
        var innerWidth = firstChild.GetAddon<BoundsAddon>().Bounds.Width;
        var pa = firstChild.GetAddon<PositionAddon>();
        if (innerWidth > 0 && Ticks.Value > 0)
        {
          _xOffset = (int)pa.Position.X + _padding;
          _innerWidth = innerWidth - (_padding * 2) - 16;
          _tickOffset = Math.Max((float)_innerWidth / Ticks.Value, 1f);
        }
      }
      return true;
    }

    public bool OnMouseMove(MouseMoveEvent evt)
    {
      if (_selected && _tickOffset != 0)
      {
        var tick = (evt.Position.X - _xOffset) / _tickOffset;
        Left.Value = MathHelper.Clamp((int)((int)tick * _tickOffset) + _padding, _padding, _innerWidth);
        Model.Value = MathHelper.Clamp((int)tick * (Max.Value / Ticks.Value), Min.Value, Max.Value);
      }
      return true;
    }

    public bool OnMouseUp()
    {
      _selected = false;
      return true;
    }
  }
}
