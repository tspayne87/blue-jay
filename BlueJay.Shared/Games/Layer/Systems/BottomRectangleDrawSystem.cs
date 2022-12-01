using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Shared.Games.Layer.Addons;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.Shared.Games.Layer.Systems
{
  internal class BottomRectangleDrawSystem : IDrawSystem, IDrawEntitySystem, IDrawEndSystem
  {
    private readonly SpriteBatch _batch;
    private readonly SpriteBatchExtension _batchExtension;

    public long Key => KeyHelper.Create<SizeAddon, PositionAddon, ColorAddon, BottomAddon>();

    public List<string> Layers => new List<string>();

    public BottomRectangleDrawSystem(SpriteBatch batch, SpriteBatchExtension batchExtension)
    {
      _batch = batch;
      _batchExtension = batchExtension;
    }

    public void OnDraw()
    {
      _batch.Begin();
    }

    public void OnDraw(IEntity entity)
    {
      var sa = entity.GetAddon<SizeAddon>();
      var pa = entity.GetAddon<PositionAddon>();
      var ca = entity.GetAddon<ColorAddon>();

      _batchExtension.DrawRectangle(sa.Size, pa.Position, ca.Color);
    }

    public void OnDrawEnd()
    {
      _batch.End();
    }
  }
}
