using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// Debug system is meant to print out debug information out on the screen based
  /// on the debug addon
  /// </summary>
  public class DebugSystem : IDrawSystem, IDrawEntitySystem, IDrawEndSystem
  {
    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly SpriteBatch _batch;

    /// <summary>
    /// The global fonts that will be used to render on the screen
    /// </summary>
    private readonly FontCollection _fonts;

    /// <summary>
    /// The font key to use when rendering the text
    /// </summary>
    private readonly string _fontKey;

    /// <summary>
    /// The current y position so we can offset the debug information so it can be read
    /// </summary>
    private int _y;

    /// <inheritdoc />
    public long Key => AddonHelper.Identifier<DebugAddon>();

    /// <inheritdoc />
    public List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor method to build out the system and inject the renderer into the class
    /// </summary>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    /// <param name="fonts">The font collection</param>
    /// <param name="fontKey">The font key we need to use</param>
    public DebugSystem(SpriteBatch batch, FontCollection fonts, string fontKey)
    {
      _batch = batch;
      _fontKey = fontKey;
      _fonts = fonts;
    }

    /// <inheritdoc />
    public void OnDraw()
    {
      _batch.Begin();
      _y = 10;
    }

    /// <inheritdoc />
    public void OnDraw(IEntity entity)
    {
      var dc = entity.GetAddon<DebugAddon>();
      var dAddons = entity.GetAddons(dc.KeyIdentifier);
      foreach (var addon in dAddons)
      {
        _batch.DrawString(_fonts.SpriteFonts[_fontKey], addon.ToString(), new Vector2(10, _y), Color.Black);
        _y += 20;
      }
    }

    /// <inheritdoc />
    public void OnDrawEnd()
    {
      _batch.End();
    }
  }
}
