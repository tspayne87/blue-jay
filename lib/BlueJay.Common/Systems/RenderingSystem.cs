using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Containers;
using Microsoft.Xna.Framework;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// Basic rendering system to draw a texture at a position
  /// </summary>
  public class RenderingSystem : IDrawSystem
  {
    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly ISpriteBatchContainer _batch;

    /// <summary>
    /// The query to get the entities that should be rendered on the screen
    /// </summary>
    private readonly IQuery<PositionAddon, TextureAddon> _entities;

    /// <summary>
    /// Constructor method is meant to build out the renderer system and inject
    /// the renderer for drawing
    /// </summary>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    public RenderingSystem(ISpriteBatchContainer batch, IQuery<PositionAddon, TextureAddon> entities)
    {
      _batch = batch;
      _entities = entities;
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
