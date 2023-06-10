using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Interactivity.Dropdown
{
  /// <summary>
  /// Dropdown menu wrapper to show the menu in a specific location
  /// </summary>
  [View(@"
  <Container if=""ShowMenu"" Style=""Position: Absolute; TopOffset: {{TopOffset}}"">
    <Slot />
  </Container>
  ")]
  public class DropdownMenu : UIComponent
  {
    /// <summary>
    /// The font collection
    /// </summary>
    private readonly IFontCollection _fonts;

    /// <summary>
    /// The injected show menu item meant to be overriden if a provided show menu reactive property is attached
    /// </summary>
    [Inject]
    public ReactiveProperty<bool>? ShowMenu;

    /// <summary>
    /// The current top offset that this menu should exist
    /// </summary>
    public readonly ReactiveProperty<float> TopOffset;

    /// <summary>
    /// Constructor to give the show menu a default value if an injected property is not set
    /// </summary>
    public DropdownMenu(IFontCollection fonts)
    {
      TopOffset = new ReactiveProperty<float>(0);

      _fonts = fonts;
    }

    /// <inheritdoc />
    public override void Mounted()
    {
      var first = Root?.FirstOrDefault();
      if (first != null)
        TopOffset.Value = (int)first.MeasureString(" ", _fonts).Y + 5;
    }
  }
}
