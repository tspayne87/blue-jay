using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Shared.Games.Layer.Addons;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Shared.Games.Layer.Systems
{
  internal class BottomRectangleDrawSystem : IDrawSystem
  {
    private readonly SpriteBatch _batch;
    private readonly SpriteBatchExtension _batchExtension;
    private readonly IQuery<SizeAddon, PositionAddon, ColorAddon, BottomAddon> _entities;

    public BottomRectangleDrawSystem(SpriteBatch batch, SpriteBatchExtension batchExtension, IQuery<SizeAddon, PositionAddon, ColorAddon, BottomAddon> entities)
    {
      _batch = batch;
      _batchExtension = batchExtension;
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

        _batchExtension.DrawRectangle(sa.Size, pa.Position, ca.Color);
      }
      _batch.End();
    }
  }
}
