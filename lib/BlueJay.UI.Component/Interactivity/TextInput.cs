using BlueJay.Common.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Events.Keyboard;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;
using Microsoft.Xna.Framework.Input;
using System;

namespace BlueJay.UI.Component.Interactivity
{
  [View(@"
<Container Style=""TextAlign: Left"" @Focus=""OnFocus()"" @Blur=""OnBlur()"" @KeyboardUp=""OnKeyboardUp($event)"">
  {{Value}}
  <Container :if=""ShowBar"" Style=""Position: Absolute; Width: 2; BackgroundColor: 60, 60, 60"" :Style=""CursorStyle"" />
</Container>
    ")]
  public class TextInput : UIComponent
  {
    private readonly FontCollection _fonts;
    private int _position;

    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<string> Value;
    [Prop]
    public readonly ReactiveStyle CursorStyle;
    public readonly ReactiveProperty<bool> ShowBar;

    public TextInput(FontCollection fonts)
    {
      Value = new ReactiveProperty<string>("");
      ShowBar = new ReactiveProperty<bool>(false);
      CursorStyle = new ReactiveStyle();
      CursorStyle.Height = 0;
      CursorStyle.TopOffset = 0;
      CursorStyle.LeftOffset = 0;

      _fonts = fonts;
      _position = 0;
    }

    public bool OnKeyboardUp(KeyboardUpEvent evt)
    {
      switch (evt.Key)
      {
        case Keys.Back:
          if (Value.Value.Length > 0)
          {
            Value.Value = Value.Value.Splice(_position - 1, 1);
            UpdatePosition(_position - 1);
          }
          break;
        case Keys.Enter:
          Value.Value = Value.Value.Splice(_position, 0, '\n');
          UpdatePosition(_position + 1);
          break;
        case Keys.Left:
          UpdatePosition(Math.Max(_position - 1, 0));
          break;
        case Keys.Right:
          UpdatePosition(Math.Min(_position + 1, Value.Value.Length));
          break;
        case Keys.End:
          UpdatePosition(Value.Value.Length);
          break;
        default:
          if (evt.TryGetCharacter(out var character))
          {
            Value.Value = Value.Value.Splice(_position, 0, character);
            UpdatePosition(_position + 1);
          }
          break;
      }
      return false;
    }

    public override void Mounted()
    {
      CursorStyle.Height = (int)Root.MeasureString(" ", _fonts).Y;
    }

    public bool OnFocus()
    {
      UpdatePosition(Value.Value.Length);
      ShowBar.Value = true;
      return true;
    }

    public bool OnBlur()
    {
      ShowBar.Value = false;
      return true;
    }

    private void UpdatePosition(int newPosition)
    {
      var la = Root.GetAddon<LineageAddon>();
      var sa = la.Children[0].GetAddon<BoundsAddon>();
      var fitString = Root.FitString(Value.Value.Substring(0, newPosition), sa.Bounds.Width, _fonts);
      var yOffset = Root.MeasureString(fitString, _fonts);
      var spaceOffset = Root.MeasureString(" ", _fonts);
      CursorStyle.TopOffset = (int)(yOffset.Y - spaceOffset.Y);

      var split = fitString.Split('\n');
      var xOffset = Root.MeasureString(split[split.Length - 1], _fonts);
      CursorStyle.LeftOffset = (int)xOffset.X;

      _position = newPosition;
    }
  }
}
