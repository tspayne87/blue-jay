using BlueJay.Content.App.Games.Breakout.Addons;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.Content.App.Games.Breakout.Systems
{
  /// <summary>
  /// Rendering system is meant to render all the elements to the screen every frame
  /// </summary>
  public class BreakoutRenderingSystem : ComponentSystem
  {
    /// <summary>
    /// The renderer that should be used for the breakout
    /// </summary>
    private readonly IRenderer _renderer;

    /// <summary>
    /// The layer collection that has all the entities in the game at the moment
    /// </summary>
    private readonly LayerCollection _layers;

    /// <summary>
    /// The game service that is meant to process the different states of the game
    /// </summary>
    private readonly BreakoutGameService _service;

    /// <summary>
    /// The global font that should be used
    /// </summary>
    private readonly FontCollection _font;

    /// <summary>
    /// The graphics that are bound to the screen
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The current addon key that is meant to act as a selector for the Draw/Update
    /// methods with entities
    /// </summary>
    public override long Key => TypeAddon.Identifier | BoundsAddon.Identifier;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor is meant to inject the renderer into the system for processing
    /// </summary>
    /// <param name="renderer">The renderer that should be used for the breakout</param>
    /// <param name="font">The global font</param>
    /// <param name="graphics">The graphics for the screen</param>
    /// <param name="layers">The layers we are working with</param>
    /// <param name="service">The current service that represents the game</param>
    public BreakoutRenderingSystem(IRenderer renderer, LayerCollection layers, BreakoutGameService service, FontCollection font, GraphicsDevice graphics)
    {
      _renderer = renderer;
      _layers = layers;
      _service = service;
      _font = font;
      _graphics = graphics;
    }

    /// <summary>
    /// The draw event that is called before all entitiy draw events for this system
    /// </summary>
    public override void OnDraw()
    {
      // Calculate the text that should exist on the screen
      var txt = string.Empty;
      if (_layers[LayerNames.BallLayer]?.Entities.Count == 0)
      {
        txt = $"Game Over\n\nScore: {_service.Score}";
      }
      else if (_layers[LayerNames.BallLayer]?.Entities.Count == 1)
      {
        var baa = _layers[LayerNames.BallLayer].Entities[0].GetAddon<BallActiveAddon>();
        if (!baa.IsActive)
        {
          txt = $"Round {_service.Round} Start\nPress Space To Start";
        }
      }

      // If text exist we need to render it
      if (txt.Length > 0)
      {
        var bounds = _font.TextureFonts["Default"].MeasureString(txt);
        var pos = new Vector2((_graphics.Viewport.Width - bounds.X) / 2f, (_graphics.Viewport.Height - bounds.Y) / 2f);
        _renderer.DrawString(_font.TextureFonts["Default"], txt, pos, Color.Black);
      }
    }

    /// <summary>
    /// The draw event that is called for each entity that was selected by the key
    /// for this system
    /// </summary>
    /// <param name="entity">The current entity that should be drawn</param>
    public override void OnDraw(IEntity entity)
    {
      var ta = entity.GetAddon<TypeAddon>();
      var ba = entity.GetAddon<BoundsAddon>();

      switch(ta.Type)
      {
        case EntityType.Paddle: // Draw the paddle to the screen
          _renderer.DrawRectangle(ba.Bounds.Width, ba.Bounds.Height, new Vector2(ba.Bounds.X, ba.Bounds.Y), Color.Blue);
          break;
        case EntityType.Ball:
          { // Load the texture and color of the ball and draw it to the screen
            var txa = entity.GetAddon<TextureAddon>();
            var ca = entity.GetAddon<ColorAddon>();
            _renderer.Draw(txa.Texture, new Vector2(ba.Bounds.X, ba.Bounds.Y), ca?.Color ?? Color.Black);
          }
          break;
        case EntityType.Block:
          { // Load the block index to get the color and draw it to the screen
            var bia = entity.GetAddon<BlockIndexAddon>();
            _renderer.DrawRectangle(ba.Bounds.Width, ba.Bounds.Height, new Vector2(ba.Bounds.X, ba.Bounds.Y), bia?.Color ?? Color.Black);
          }
          break;
      }
    }
  }
}
