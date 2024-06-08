using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.UI.Systems
{
  /// <summary>
  /// Basic rendering system to draw a texture at a position
  /// </summary>
  internal class UIRenderingSystem : IDrawSystem, IDrawEntitySystem, IDrawEndSystem
  {
    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly SpriteBatch _batch;

    /// <inheritdoc />
    public AddonKey Key => KeyHelper.Create<PositionAddon, TextureAddon>();

    /// <inheritdoc />
    public List<string> Layers => new List<string>() { UIStatic.LayerName };

    /// <summary>
    /// Constructor method is meant to build out the renderer system and inject
    /// the renderer for drawing
    /// </summary>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    public UIRenderingSystem(SpriteBatch batch)
    {
      _batch = batch;
    }

    /// <inheritdoc />
    public void OnDraw()
    {
      _batch.Begin();
    }

    /// <inheritdoc />
    public void OnDraw(IEntity entity)
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

    /// <inheritdoc />
    public void OnDrawEnd()
    {
      _batch.End();
    }
  }
}
