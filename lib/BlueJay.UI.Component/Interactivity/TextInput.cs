using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Keyboard;
using BlueJay.Events.Lifecycle;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Interactivity
{
  [View(@"
<container style=""TextAlign: Left"" onFocus=""OnFocus"" onBlur=""OnBlur"" onKeyboardUp=""OnKeyboardUp"">
  <container>{{Value}}</container>

  <container if=""{{ShowBar}}"" style=""Position: Absolute; Width: 2; Height: {{BarHeight}}; TopOffset: {{BarTop}}; LeftOffset: {{BarLeft}}; BackgroundColor: 0, 0, 0"" />
</container>
    ")]
  public class TextInput : UIComponent, IUpdateSystem
  {
    private readonly IDeltaService _delta;
    private readonly int _cooldown = 1000;
    private int _countdown;
    private bool _focused;
    private int _position;

    public ReactiveProperty<string> Value;

    public ReactiveProperty<bool> ShowBar;
    public ReactiveProperty<int> BarHeight;
    public ReactiveProperty<int> BarTop;
    public ReactiveProperty<int> BarLeft;

    public IEntity Bar;

    public long Key => 0;

    public List<string> Layers => new List<string>();

    public TextInput(IDeltaService delta, FontCollection fonts)
    {
      Value = new ReactiveProperty<string>("");
      ShowBar = new ReactiveProperty<bool>(false);
      BarHeight = new ReactiveProperty<int>(0);
      BarTop = new ReactiveProperty<int>(0);
      BarLeft = new ReactiveProperty<int>(0);

      _delta = delta;
      _countdown = _cooldown;
      _focused = false;
      _position = 0;
    }

    public bool OnKeyboardUp(KeyboardUpEvent evt)
    {
      switch (evt.Key)
      {
        case Keys.Back:
          if (Value.Value.Length > 0)
          {
            Value.Value = Value.Value.Substring(0, Value.Value.Length - 1);
            _position--;
          }
          break;
        case Keys.Enter:
          Value.Value += '\n';
          _position++;
          break;
        default:
          if (evt.TryGetCharacter(out var character))
          {
            Value.Value += character;
            _position++;
          }
          break;
      }
      return false;
    }

    public bool OnFocus(FocusEvent evt)
    {
      _focused = true;
      return true;
    }

    public bool OnBlur(BlurEvent evt)
    {
      _focused = false;
      ShowBar.Value = false;
      return true;
    }

    public void OnUpdate()
    {
      if (_focused)
      {
        _countdown -= _delta.Delta;
        if (_countdown <= 0)
        {
          _countdown += _cooldown;
          ShowBar.Value = !ShowBar.Value;
        }
      }
    }
  }
}
