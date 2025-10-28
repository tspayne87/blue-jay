using System;
using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Containers;

namespace BlueJay.UI.Systems;

public class TextureRenderSystem(IQuery<TextureAddon, PositionAddon, SpriteSheetAddon, FrameAddon> query, ISpriteBatchContainer batch) : IDrawSystem
{
  private readonly IQuery _entities = query.WhereLayer(StaticNames.UILAYER);
  private readonly ISpriteBatchContainer _batch = batch;

  public void OnDraw()
  {
    _batch.Begin();
    foreach (var entity in _entities)
    {
      var textureAddon = entity.GetAddon<TextureAddon>();
      var positionAddon = entity.GetAddon<PositionAddon>();
      var spriteSheetAddon = entity.GetAddon<SpriteSheetAddon>();
      var frameAddon = entity.GetAddon<FrameAddon>();

      if (textureAddon.Texture != null)
        _batch.DrawFrame(
          textureAddon.Texture,
          positionAddon.Position,
          spriteSheetAddon.Rows,
          spriteSheetAddon.Cols,
          frameAddon.Frame
        );
    }
    _batch.End();

  }
}
