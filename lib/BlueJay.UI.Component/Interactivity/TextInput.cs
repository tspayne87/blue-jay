using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Common.Events.Keyboard;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;
using Microsoft.Xna.Framework.Input;
using BlueJay.UI.Events;
using BlueJay.Events.Interfaces;

namespace BlueJay.UI.Component.Interactivity
{
    /// <summary>
    /// Text input to give inputs for string input
    /// </summary>
    [View(@"
<Container Style=""TextAlign: Left"" @Focus=""OnFocus($evt)"" @Blur=""OnBlur($evt)"" @KeyboardUp=""OnKeyboardUp($evt)"" ref=""Root"">
  <Container Style=""Height: {{ContainerHeight}}"" @Focus=""OnFocus($evt)"" @Blur=""OnBlur($evt)"" @KeyboardUp=""OnKeyboardUp($evt)"">{{Model}}</Container>
  <Container if=""ShowCursor"" Style=""Position: Absolute; Width: 2; BackgroundColor: 60, 60, 60; TopOffset: {{CursorTopOffset}}; LeftOffset: {{CursorLeftOffset}}; Height: {{CursorHeight}}"" />
</Container>
    ")]
  public class TextInput : UIComponent
  {
    /// <summary>
    /// The collection of fonts that are used to calculate the minimum height
    /// </summary>
    private readonly IFontCollection _fonts;

    /// <summary>
    /// The event queue that needs to trigger events on the queue
    /// </summary>
    private readonly IEventQueue _eventQueue;

    /// <summary>
    /// The current position of the cursor in the string
    /// </summary>
    private int _position;

    /// <summary>
    /// The model that pushes out the data to the parent component
    /// </summary>
    [Prop(PropBinding.TwoWay)]
    public readonly ReactiveProperty<Text> Model;

    /// <summary>
    /// If the cursor should be shown
    /// </summary>
    public readonly ReactiveProperty<bool> ShowCursor;

    /// <summary>
    /// The top offset for the cursor
    /// </summary>
    public ReactiveProperty<int> CursorTopOffset;

    /// <summary>
    /// The left offset for the cursor
    /// </summary>
    public ReactiveProperty<int> CursorLeftOffset;

    /// <summary>
    /// The height of the cursor
    /// </summary>
    public ReactiveProperty<int> CursorHeight;

    /// <summary>
    /// The current height of the container
    /// </summary>
    public NullableReactiveProperty<int> ContainerHeight;

    /// <summary>
    /// The root entity found when this compnent is created
    /// </summary>
    public IEntity? Root;

    /// <summary>
    /// Constructor is meant to build out the defaults for the text input
    /// </summary>
    /// <param name="fonts">The collection of fonts that are used to calculate the minimum height</param>
    /// <param name="eventQueue">The event queue that needs to trigger events on the queue</param>
    public TextInput(IFontCollection fonts, IEventQueue eventQueue)
    {
      Model = new ReactiveProperty<Text>("");
      ShowCursor = new ReactiveProperty<bool>(false);
      CursorHeight = new ReactiveProperty<int>(0);
      CursorLeftOffset = new ReactiveProperty<int>(0);
      CursorTopOffset = new ReactiveProperty<int>(0);
      ContainerHeight = new NullableReactiveProperty<int>(null);

      _fonts = fonts;
      _eventQueue = eventQueue;
      _position = 0;
    }

    /// <summary>
    /// Event to handle when the keyboard up event is fired to manipulate the string
    /// </summary>
    /// <param name="evt">The keyboard up event</param>
    /// <returns>Will return true to continue propegation</returns>
    public bool OnKeyboardUp(KeyboardUpEvent evt)
    {
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
      if (Root != null)
        CursorHeight.Value = (int)Root.MeasureString(" ", _fonts).Y;
    }

    /// <summary>
    /// When the input is focused need to show the cursor
    /// </summary>
    /// <returns>Will return true to continue propegation</returns>
    public bool OnFocus(FocusEvent evt)
    {
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
      if (Root != null)
        ContainerHeight.Value = string.IsNullOrWhiteSpace(model) ? (int?)Root.MeasureString(" ", _fonts).Y : null;

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
      if (Root != null)
      {
        var la = Root.GetAddon<LineageAddon>();
        var sa = la.Children[0].GetAddon<BoundsAddon>();
        var fitString = Root.FitString(Model.Value.Substring(0, newPosition), sa.Bounds.Width, _fonts);
        var yOffset = Root.MeasureString(fitString, _fonts);
        var spaceOffset = Root.MeasureString(" ", _fonts);
        CursorTopOffset.Value = (int)(yOffset.Y - spaceOffset.Y);

        var split = fitString.Split('\n');
        var xOffset = Root.MeasureString(split[split.Length - 1], _fonts);
        CursorLeftOffset.Value = (int)xOffset.X;

        _position = newPosition;
      }
    }
  }
}
