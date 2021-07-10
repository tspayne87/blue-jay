using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// FPS system is meant to print out on the screen the current FPS for the game
  /// </summary>
  public class FPSSystem : IDrawSystem, IUpdateSystem
  {
    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly SpriteBatch _batch;

    /// <summary>
    /// The delta service that is meant to be updated every frame
    /// </summary>
    private readonly IDeltaService _deltaService;

    /// <summary>
    /// The collection of global fonts
    /// </summary>
    private readonly FontCollection _fonts;

    /// <summary>
    /// The current font key
    /// </summary>
    private readonly string _fontKey;

    /// <summary>
    /// The current fps for the system
    /// </summary>
    private int _fps = 0;

    /// <summary>
    /// How many updates have happened
    /// </summary>
    private int _updates = 0;

    /// <summary>
    /// The count down to a second based on the delta
    /// </summary>
    private int _countdown = 1000;

    /// <summary>
    /// Do not specify an entity and just use the based draw and update steps
    /// </summary>
    public long Key => 0;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor to build out the fps system
    /// </summary>
    /// <param name="batch">The delta service that is meant to be updated every frame</param>
    /// <param name="deltaService">The current delta that gets updated every frame</param>
    /// <param name="fontKey">The key for creating the font key</param>
    /// <param name="fonts">The fonts collection that we should look up sprite fonts</param>
    public FPSSystem(SpriteBatch batch, IDeltaService deltaService, FontCollection fonts, string fontKey)
    {
      _batch = batch;
      _deltaService = deltaService;
      _fonts = fonts;
      _fontKey = fontKey;
    }

    /// <summary>
    /// Update event is meant to track how many times this method is called in a second
    /// </summary>
    public void OnUpdate()
    {
      _updates++;
      _countdown -= _deltaService.Delta;
      if (_countdown <= 0)
      {
        _fps = _updates;
        _updates = 0;
        _countdown += 1000;
      }
    }

    /// <summary>
    /// Draw event is meant to print the current fps to the screen
    /// </summary>
    public void OnDraw()
    {
      _batch.Begin();
      _batch.DrawString(_fonts.SpriteFonts[_fontKey], $"fps: {_fps}", new Vector2(200, 10), Color.Black);
      _batch.End();
    }
  }
}
