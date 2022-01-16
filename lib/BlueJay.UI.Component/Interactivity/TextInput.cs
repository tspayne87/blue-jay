using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Common.Events.Keyboard;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;
using Microsoft.Xna.Framework.Input;
using System;

namespace BlueJay.UI.Component.Interactivity
{
  /// <summary>
  /// Text input to give inputs for string input
  /// </summary>
  [View(@"
<Container Style=""TextAlign: Left"" @Focus=""OnFocus($event)"" @Blur=""OnBlur($event)"" @KeyboardUp=""OnKeyboardUp($event)"">
  <Container :Style=""ContainerStyle"" @Focus=""OnFocus($event)"" @Blur=""OnBlur($event)"" @KeyboardUp=""OnKeyboardUp($event)"">{{Model}}</Container>
  <Container :if=""ShowCursor"" Style=""Position: Absolute; Width: 2; BackgroundColor: 60, 60, 60"" :Style=""CursorStyle"" />
</Container>
    ")]
  public class TextInput : UIComponent
  {
    /// <summary>
    /// The collection of fonts that are used to calculate the minimum height
    /// </summary>
    private readonly IFontCollection _fonts;

    /// <summary>
    /// The current position of the cursor in the string
    /// </summary>
    private int _position;

    /// <summary>
    /// The model that pushes out the data to the parent component
    /// </summary>
    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<string> Model;

    /// <summary>
    /// The style of the cursor
    /// </summary>
    [Prop]
    public readonly ReactiveStyle CursorStyle;

    /// <summary>
    /// The container style
    /// </summary>
    public readonly ReactiveStyle ContainerStyle;

    /// <summary>
    /// If the cursor should be shown
    /// </summary>
    public readonly ReactiveProperty<bool> ShowCursor;

    /// <summary>
    /// Constructor is meant to build out the defaults for the text input
    /// </summary>
    /// <param name="fonts">The collection of fonts that are used to calculate the minimum height</param>
    public TextInput(IFontCollection fonts)
    {
      Model = new ReactiveProperty<string>("");
      ShowCursor = new ReactiveProperty<bool>(false);
      CursorStyle = new ReactiveStyle();
      ContainerStyle = new ReactiveStyle();
      CursorStyle.Height = 0;
      CursorStyle.TopOffset = 0;
      CursorStyle.LeftOffset = 0;

      _fonts = fonts;
      _position = 0;
    }

    /// <summary>
    /// Event to handle when the keyboard up event is fired to manipulate the string
    /// </summary>
    /// <param name="evt">The keyboard up event</param>
    /// <returns>Will return true to continue propegation</returns>
    public bool OnKeyboardUp(KeyboardUpEvent evt)
    {
      Emit("KeyboardUp", evt);
      switch (evt.Key)
      {
        case Keys.Back:
          if (Model.Value.Length > 0 && _position > 0)
          {
            UpdatePosition(Math.Max(_position - 1, 0));
            Model.Value = Model.Value.Splice(_position, 1);
          }
          break;
        case Keys.Left:
          UpdatePosition(Math.Max(_position - 1, 0));
          break;
        case Keys.Right:
          UpdatePosition(Math.Min(_position + 1, Model.Value.Length));
          break;
        case Keys.End:
          UpdatePosition(Model.Value.Length);
          break;
        default:
          if (evt.TryGetCharacter(out var character))
          {
            Model.Value = Model.Value.Splice(_position, 0, character);
            UpdatePosition(_position + 1);
          }
          break;
      }
      return false;
    }

    /// <summary>
    /// Mounted life cycle hook is meant to determine the cursor height
    /// </summary>
    public override void Mounted()
    {
      CursorStyle.Height = (int)Root.MeasureString(" ", _fonts).Y;
    }

    /// <summary>
    /// When the input is focused need to show the cursor
    /// </summary>
    /// <returns>Will return true to continue propegation</returns>
    public bool OnFocus(FocusEvent evt)
    {
      Emit("Focus", evt);
      UpdatePosition(Model.Value.Length);
      ShowCursor.Value = true;
      return true;
    }

    /// <summary>
    /// When the input is not been unfocused
    /// </summary>
    /// <returns>Will return true to continue propegation</returns>
    public bool OnBlur(BlurEvent evt)
    {
      Emit("Blur", evt);
      ShowCursor.Value = false;
      return true;
    }

    /// <summary>
    /// Helper method is meant to watch on the model reactive property and make changes to it when things are updated
    /// </summary>
    /// <param name="model">The new model being added</param>
    [Watch(nameof(Model))]
    public void OnModelUpdate(string model)
    {
      ContainerStyle.Height = string.IsNullOrWhiteSpace(model) ? (int?)Root.MeasureString(" ", _fonts).Y : null;

      if (Model.Value.Length < _position)
      {
        UpdatePosition(Model.Value.Length);
      }
    }

    /// <summary>
    /// Helper method is meant to calculate the new position of the cursor in the string itself
    /// </summary>
    /// <param name="newPosition">The new position we need to process for</param>
    private void UpdatePosition(int newPosition)
    {
      var la = Root.GetAddon<LineageAddon>();
      var sa = la.Children[0].GetAddon<BoundsAddon>();
      var fitString = Root.FitString(Model.Value.Substring(0, newPosition), sa.Bounds.Width, _fonts);
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
