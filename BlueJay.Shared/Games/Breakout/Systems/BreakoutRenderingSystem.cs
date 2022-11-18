using BlueJay.Shared.Games.Breakout.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using BlueJay.Core;
using BlueJay.Common.Addons;
using BlueJay.Component.System;

namespace BlueJay.Shared.Games.Breakout.Systems
{
  /// <summary>
  /// Rendering system is meant to render all the elements to the screen every frame
  /// </summary>
  public class BreakoutRenderingSystem : IDrawSystem, IDrawEntitySystem, IDrawEndSystem
  {
    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly SpriteBatch _batch;

    /// <summary>
    /// The sprite batch to draw to the screen extensions
    /// </summary>
    private readonly SpriteBatchExtension _batchExtension;

    /// <summary>
    /// The layer collection that has all the entities in the game at the moment
    /// </summary>
    private readonly ILayerCollection _layers;

    /// <summary>
    /// The game service that is meant to process the different states of the game
    /// </summary>
    private readonly BreakoutGameService _service;

    /// <summary>
    /// The global font that should be used
    /// </summary>
    private readonly IFontCollection _font;

    /// <summary>
    /// The graphics that are bound to the screen
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <inheritdoc />
    public long Key => KeyHelper.Create<TypeAddon, BoundsAddon>();

    /// <inheritdoc />
    public List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor is meant to inject the renderer into the system for processing
    /// </summary>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    /// <param name="font">The global font</param>
    /// <param name="graphics">The graphics for the screen</param>
    /// <param name="layers">The layers we are working with</param>
    /// <param name="service">The current service that represents the game</param>
    /// <param name="batchExtension">The sprite batch to draw to the screen extensions</param>
    public BreakoutRenderingSystem(SpriteBatch batch, ILayerCollection layers, BreakoutGameService service, IFontCollection font, GraphicsDevice graphics, SpriteBatchExtension batchExtension)
    {
      _batch = batch;
      _layers = layers;
      _service = service;
      _font = font;
      _graphics = graphics;
      _batchExtension = batchExtension;
    }

    /// <inheritdoc />
    public void OnDraw()
    {
      _batch.Begin();
      // Calculate the text that should exist on the screen
      var txt = string.Empty;
      if (_layers[LayerNames.BallLayer]?.Count == 0)
      {
        txt = $"Game Over\n\nScore: {_service.Score}";
      }
      else if (_layers[LayerNames.BallLayer]?.Count == 1)
      {
        var baa = _layers[LayerNames.BallLayer][0].GetAddon<BallActiveAddon>();
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
        _batch.DrawString(_font.TextureFonts["Default"], txt, pos, Color.Black);
      }
    }

    /// <inheritdoc />
    public void OnDraw(IEntity entity)
    {
      var ta = entity.GetAddon<TypeAddon>();
      var ba = entity.GetAddon<BoundsAddon>();

      switch(ta.Type)
      {
        case EntityType.Paddle: // Draw the paddle to the screen
          _batchExtension.DrawRectangle(ba.Bounds.Width, ba.Bounds.Height, new Vector2(ba.Bounds.X, ba.Bounds.Y), Color.Blue);
          break;
        case EntityType.Ball:
          { // Load the texture and color of the ball and draw it to the screen
            var txa = entity.GetAddon<TextureAddon>();
            var ca = entity.GetAddon<ColorAddon>();
            _batch.Draw(txa.Texture, new Vector2(ba.Bounds.X, ba.Bounds.Y), ca.Color);
          }
          break;
        case EntityType.Block:
          { // Load the block index to get the color and draw it to the screen
            var bia = entity.GetAddon<BlockIndexAddon>();
            _batchExtension.DrawRectangle(ba.Bounds.Width, ba.Bounds.Height, new Vector2(ba.Bounds.X, ba.Bounds.Y), bia.Color);
          }
          break;
      }
    }

    /// <inheritdoc />
    public void OnDrawEnd()
    {
      _batch.End();
    }
  }
}
