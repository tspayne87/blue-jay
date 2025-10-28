using System;
using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Containers;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Systems;

public class NinePatchRenderSystem(IQuery<NinePatchAddon, SizeAddon, PositionAddon> query, ISpriteBatchContainer batch) : IDrawSystem
{
  private readonly IQuery _entities = query.WhereLayer(StaticNames.UILAYER);
  private readonly ISpriteBatchContainer _batch = batch;

  public void OnDraw()
  {
    _batch.Begin();
    foreach (var entity in _entities)
    {
      var ninePatchAddon = entity.GetAddon<NinePatchAddon>();
      var sizeAddon = entity.GetAddon<SizeAddon>();
      var positionAddon = entity.GetAddon<PositionAddon>();

      if (ninePatchAddon.Texture != null)
        _batch.DrawNinePatch(
          ninePatchAddon.Texture,
          sizeAddon.Size.Width,
          sizeAddon.Size.Height,
          positionAddon.Position,
          Color.White
        );
    }
    _batch.End();

  }
}
