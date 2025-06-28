using BlueJay.Shared.Games.Breakout.Addons;
using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BlueJay.Core;
using BlueJay.Common.Addons;
using BlueJay.Core.Containers;
using System.Linq;

namespace BlueJay.Shared.Games.Breakout.Systems
{
  /// <summary>
  /// Rendering system is meant to render all the elements to the screen every frame
  /// </summary>
  public class BreakoutRenderingSystem : IDrawSystem
  {
    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly ISpriteBatchContainer _batch;

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

    private readonly IQuery<TypeAddon, BoundsAddon> _entities;

    private readonly IQuery _ballEntities;

    /// <summary>
    /// Constructor is meant to inject the renderer into the system for processing
    /// </summary>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    /// <param name="font">The global font</param>
    /// <param name="graphics">The graphics for the screen</param>
    /// <param name="service">The current service that represents the game</param>
    /// <param name="entities">The entities that we want to process in this system</param>
    /// <param name="ballEntities">The entities that we want to process in this system</param>
    public BreakoutRenderingSystem(ISpriteBatchContainer batch, BreakoutGameService service, IFontCollection font, GraphicsDevice graphics, IQuery<TypeAddon, BoundsAddon> entities, IQuery ballEntities)
    {
      _batch = batch;
      _service = service;
      _font = font;
      _graphics = graphics;

      _entities = entities;
      _ballEntities = ballEntities.WhereLayer(LayerNames.BallLayer);
    }

    /// <inheritdoc />
    public void OnDraw()
    {
      _batch.Begin();
      // Calculate the text that should exist on the screen
      var txt = string.Empty;
      var balls = _ballEntities.ToList();
      if (balls.Count == 0)
      {
        txt = $"Game Over\n\nScore: {_service.Score}";
      }
      else if (balls.Count == 1)
      {
        var baa = balls[0].GetAddon<BallActiveAddon>();
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

      foreach (var entity in _entities)
      {
        var ta = entity.GetAddon<TypeAddon>();
        var ba = entity.GetAddon<BoundsAddon>();

        switch (ta.Type)
        {
          case EntityType.Paddle: // Draw the paddle to the screen
            _batch.DrawRectangle(ba.Bounds.Width, ba.Bounds.Height, new Vector2(ba.Bounds.X, ba.Bounds.Y), Color.Blue);
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
              _batch.DrawRectangle(ba.Bounds.Width, ba.Bounds.Height, new Vector2(ba.Bounds.X, ba.Bounds.Y), bia.Color);
            }
            break;
        }
      }
      _batch.End();
    }
  }
}
