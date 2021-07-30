using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Keyboard;
using BlueJay.Events.Lifecycle;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component.Interactivity
{
  [View(@"
<container id=""text-input"" style=""TextAlign: Left"" onFocus=""OnFocus"" onBlur=""OnBlur"" onKeyboardUp=""OnKeyboardUp"">
  {{Value}}{{Bar}}
</container>
    ")]
  public class TextInput : UIComponent, IUpdateSystem
  {
    private readonly IDeltaService _delta;
    private readonly int _cooldown = 1000;
    private int _countdown;
    private bool _focused;

    public ReactiveProperty<string> Value;
    public ReactiveProperty<string> Bar;

    public long Key => 0;

    public List<string> Layers => new List<string>();

    public TextInput(IDeltaService delta)
    {
      Value = new ReactiveProperty<string>(" ");
      Bar = new ReactiveProperty<string>("");

      _delta = delta;
      _countdown = _cooldown;
      _focused = false;
    }

    public bool OnKeyboardUp(KeyboardUpEvent evt)
    {
      switch (evt.Key)
      {
        case Keys.Back:
          Value.Value = Value.Value.Substring(0, Value.Value.Length - 1);
          break;
        case Keys.A:
        case Keys.B:
        case Keys.C:
        case Keys.D:
        case Keys.E:
        case Keys.F:
        case Keys.G:
        case Keys.H:
        case Keys.I:
        case Keys.J:
        case Keys.K:
        case Keys.L:
        case Keys.M:
        case Keys.N:
        case Keys.O:
        case Keys.P:
        case Keys.Q:
        case Keys.R:
        case Keys.S:
        case Keys.T:
        case Keys.U:
        case Keys.V:
        case Keys.W:
        case Keys.X:
        case Keys.Y:
        case Keys.Z:
          Value.Value += evt.Key.ToString().ToLower();
          break;
        case Keys.Space:
          Value.Value += " ";
          break;
        case Keys.OemComma:
          Value.Value += ",";
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
      Bar.Value = string.Empty;
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
          Bar.Value = string.IsNullOrEmpty(Bar.Value) ? "|" : string.Empty;
        }
      }
    }
  }
}
