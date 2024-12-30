using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Containers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.UI.Systems
{
  /// <summary>
  /// Basic rendering system to draw a texture at a position
  /// </summary>
  internal class UIRenderingSystem : IDrawSystem
  {
    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly ISpriteBatchContainer _batch;

    private readonly IQuery _entities;

    /// <summary>
    /// Constructor method is meant to build out the renderer system and inject
    /// the renderer for drawing
    /// </summary>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    public UIRenderingSystem(ISpriteBatchContainer batch, IQuery<PositionAddon, TextureAddon> entities)
    {
      _batch = batch;
      _entities = entities.WhereLayer(UIStatic.LayerName);
    }

    /// <inheritdoc />
    public void OnDraw()
    {
      _batch.Begin();
      foreach (var entity in _entities)
      {
        var pc = entity.GetAddon<PositionAddon>();
        var tc = entity.GetAddon<TextureAddon>();

        if (tc.Texture != null)
        {
          var color = entity.TryGetAddon<ColorAddon>(out var ca) ? ca.Color : Color.White;
          if (entity.TryGetAddon<FrameAddon>(out var fa) && entity.TryGetAddon<SpriteSheetAddon>(out var ssa))
            _batch.DrawFrame(tc.Texture, pc.Position, ssa.Rows, ssa.Cols, fa.Frame, color);
          else
            _batch.Draw(tc.Texture, pc.Position, color);
        }
      }
      _batch.End();
    }
  }
}
