using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Containers;
using Microsoft.Xna.Framework;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// Debug system is meant to print out debug information out on the screen based
  /// on the debug addon
  /// </summary>
  public class DebugSystem : IDrawSystem
  {
    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly ISpriteBatchContainer _batch;

    /// <summary>
    /// The global fonts that will be used to render on the screen
    /// </summary>
    private readonly IFontCollection _fonts;

    /// <summary>
    /// The font key to use when rendering the text
    /// </summary>
    private readonly string _fontKey;

    /// <summary>
    /// The debug query meant to view details about an addon
    /// </summary>
    private readonly IQuery<DebugAddon> _debugQuery;

    /// <summary>
    /// Constructor method to build out the system and inject the renderer into the class
    /// </summary>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    /// <param name="fonts">The font collection</param>
    /// <param name="debugQuery">The debug query to view details about an addon</param>
    /// <param name="fontKey">The font key we need to use</param>
    public DebugSystem(ISpriteBatchContainer batch, IFontCollection fonts, IQuery<DebugAddon> debugQuery, string fontKey)
    {
      _batch = batch;
      _fontKey = fontKey;
      _fonts = fonts;
      _debugQuery = debugQuery;
    }

    /// <inheritdoc />
    public void OnDraw()
    {
      _batch.Begin();
      var y = 10;
      foreach (var entity in _debugQuery)
      {
        var dc = entity.GetAddon<DebugAddon>();
        var dAddons = entity.GetAddons(dc.KeyIdentifier);
        foreach (var addon in dAddons)
        {
          _batch.DrawString(_fonts.SpriteFonts[_fontKey], addon?.ToString() ?? string.Empty, new Vector2(10, y), Color.Black);
          y += 20;
        }
      }
      _batch.End();
    }
  }
}
