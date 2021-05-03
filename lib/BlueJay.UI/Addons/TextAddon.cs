using BlueJay.Component.System.Addons;

namespace BlueJay.UI.Addons
{
  /// <summary>
  /// The text addon for rendering text to the screen
  /// </summary>
  public class TextAddon : Addon<TextAddon>
  {
    /// <summary>
    /// The text that should be rendered on the screen
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    /// Constructor to build out the text addon
    /// </summary>
    /// <param name="text">The default text</param>
    public TextAddon(string text)
    {
      Text = text;
    }
  }
}
