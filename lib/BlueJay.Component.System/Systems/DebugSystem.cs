using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BlueJay.Component.System.Systems
{
  /// <summary>
  /// Debug system is meant to print out debug information out on the screen based
  /// on the debug addon
  /// </summary>
  public class DebugSystem : ComponentSystem
  {
    /// <summary>
    /// The current renderer so we can render data to the screen
    /// </summary>
    private readonly IRenderer _renderer;

    /// <summary>
    /// The global fonts that will be used to render on the screen
    /// </summary>
    private readonly FontCollection _fonts;

    /// <summary>
    /// The font key to use when rendering the text
    /// </summary>
    private readonly string _fontKey;

    /// <summary>
    /// The current y position so we can offset the debug information so it can be
    /// read
    /// </summary>
    private int _y;

    /// <summary>
    /// The key that should select the entities based on the addons
    /// </summary>
    public override long Key => DebugAddon.Identifier;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>() { };

    /// <summary>
    /// Constructor method to build out the system and inject the renderer into the class
    /// </summary>
    /// <param name="collection">The renderer collection</param>
    /// <param name="fonts">The font collection</param>
    /// <param name="fontKey">The font key we need to use</param>
    /// <param name="renderer">The current renderer</param>
    public DebugSystem(RendererCollection collection, FontCollection fonts, string fontKey, string renderer)
    {
      _renderer = collection[renderer];
      _fontKey = fontKey;
      _fonts = fonts;
    }

    /// <summary>
    /// We need to reset the y position on draw so we always draw in the same place
    /// </summary>
    public override void OnDraw()
    {
      _y = 10;
    }

    /// <summary>
    /// The draw event where we are going to render the draw information to the screen
    /// </summary>
    /// <param name="entity">The current entity we are working with</param>
    public override void OnDraw(IEntity entity)
    {
      var dc = entity.GetAddon<DebugAddon>();
      var dAddons = entity.GetAddons(dc.KeyIdentifier);
      foreach (var addon in dAddons)
      {
        _renderer.DrawString(_fonts.SpriteFonts[_fontKey], addon.ToString(), new Vector2(10, _y), Color.Black);
        _y += 20;
      }
    }
  }
}
