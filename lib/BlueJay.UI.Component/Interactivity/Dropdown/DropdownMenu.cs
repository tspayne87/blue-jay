using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;
using BlueJay.UI.Interfaces;

namespace BlueJay.UI.Component.Interactivity.Dropdown
{
  /// <summary>
  /// Dropdown menu wrapper to show the menu in a specific location
  /// </summary>
  [View(@"
  <Container if=""ShowMenu"" Style=""Position: Absolute; Offset: {{Offset}}"" ref=""Root"">
    <Slot />
  </Container>
  ")]
  public class DropdownMenu : UIComponent
  {
    /// <summary>
    /// The injected show menu item meant to be overriden if a provided show menu reactive property is attached
    /// </summary>
    [Inject]
    public ReactiveProperty<bool>? ShowMenu;

    /// <summary>
    /// The current top offset that this menu should exist
    /// </summary>
    public readonly ReactiveProperty<Directions> Offset;

    /// <summary>
    /// The root entity found when this compnent is created
    /// </summary>
    public IElement? Root;

    /// <summary>
    /// Constructor to give the show menu a default value if an injected property is not set
    /// </summary>
    public DropdownMenu()
    {
      Offset = new ReactiveProperty<Directions>(new Directions(0));
    }

    /// <inheritdoc />
    public override void Mounted()
    {
      if (Root != null)
      {
        var top = (int)Root.MeasureString(" ").Y + 5;
        Offset.Value = new Directions(top, 0, 0, 0);
      }
    }
  }
}
