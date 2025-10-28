using System;
using System.Numerics;
using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Containers;
using BlueJay.UI.Addons;

namespace BlueJay.UI.Systems;

public class TextRenderSystem(IQuery<FontAddon, TextAddon, PositionAddon> query, ISpriteBatchContainer batch) : IDrawSystem
{
  private readonly IQuery _entities = query.WhereLayer(StaticNames.UILAYER);
  private readonly ISpriteBatchContainer _batch = batch;

  public void OnDraw()
  {
    _batch.Begin();
    foreach (var entity in _entities)
    {
      var fontAddon = entity.GetAddon<FontAddon>();
      var textAddon = entity.GetAddon<TextAddon>();
      var positionAddon = entity.GetAddon<PositionAddon>();

      if (fontAddon.SpriteFont != null)
        _batch.DrawString(fontAddon.SpriteFont, textAddon.Text, positionAddon.Position, fontAddon.Color);
      else if (fontAddon.TextureFont != null)
        _batch.DrawString(fontAddon.TextureFont, textAddon.Text, positionAddon.Position, fontAddon.Color, fontAddon.Size);
    }
    _batch.End();
  }
}
