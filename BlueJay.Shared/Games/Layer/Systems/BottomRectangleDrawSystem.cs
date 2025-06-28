using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Containers;
using BlueJay.Shared.Games.Layer.Addons;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Shared.Games.Layer.Systems
{
  internal class BottomRectangleDrawSystem : IDrawSystem
  {
    private readonly ISpriteBatchContainer _batch;
    private readonly IQuery<SizeAddon, PositionAddon, ColorAddon, BottomAddon> _entities;

    public BottomRectangleDrawSystem(ISpriteBatchContainer batch, IQuery<SizeAddon, PositionAddon, ColorAddon, BottomAddon> entities)
    {
      _batch = batch;
      _entities = entities;
    }

    public void OnDraw()
    {
      _batch.Begin();
      foreach (var entity in _entities)
      {
        var sa = entity.GetAddon<SizeAddon>();
        var pa = entity.GetAddon<PositionAddon>();
        var ca = entity.GetAddon<ColorAddon>();

        _batch.DrawRectangle(sa.Size, pa.Position, ca.Color);
      }
      _batch.End();
    }
  }
}
