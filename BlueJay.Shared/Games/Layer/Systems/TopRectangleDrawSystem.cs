using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Containers;
using BlueJay.Shared.Games.Layer.Addons;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Shared.Games.Layer.Systems
{
  internal class TopRectangleDrawSystem : IDrawSystem
  {
    private readonly ISpriteBatchContainer _batch;
    private readonly IQuery<SizeAddon, PositionAddon, ColorAddon, TopAddon> _entities;

    public TopRectangleDrawSystem(ISpriteBatchContainer batch, IQuery<SizeAddon, PositionAddon, ColorAddon, TopAddon> entities)
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
