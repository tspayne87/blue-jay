using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Systems
{
  public class DebugBoundingBoxSystem : IDrawSystem, IDrawEntitySystem, IDrawEndSystem
  {
    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly SpriteBatch _batch;

    /// <summary>
    /// Extensions to the sprite batch to draw to the screen basic objects
    /// </summary>
    private readonly SpriteBatchExtension _batchExtensions;

    /// <inheritdoc />
    public long Key => AddonHelper.Identifier<PositionAddon, BoundsAddon>();

    /// <inheritdoc />
    public List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor method is meant to build out the renderer system and inject
    /// the renderer for drawing
    /// </summary>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    /// <param name="batchExtension">Extensions to the sprite batch to draw to the screen basic objects</param>
    public DebugBoundingBoxSystem(SpriteBatch batch, SpriteBatchExtension batchExtension)
    {
      _batch = batch;
      _batchExtensions = batchExtension;
    }

    /// <inheritdoc />
    public void OnDraw()
    {
      _batch.Begin();
    }

    /// <inheritdoc />
    public void OnDraw(IEntity entity)
    {
      var pa = entity.GetAddon<PositionAddon>();
      var ba = entity.GetAddon<BoundsAddon>();

      if (!entity.MatchKey(AddonHelper.Identifier<TextAddon>()))
      {
        _batchExtensions.DrawRectangle(ba.Bounds.Width, 1, pa.Position, Color.LightGray);
        _batchExtensions.DrawRectangle(ba.Bounds.Width, 1, pa.Position + new Vector2(0, ba.Bounds.Height), Color.LightGray);
        _batchExtensions.DrawRectangle(1, ba.Bounds.Height, pa.Position, Color.LightGray);
        _batchExtensions.DrawRectangle(1, ba.Bounds.Height, pa.Position + new Vector2(ba.Bounds.Width, 0), Color.LightGray);
      }
    }

    /// <inheritdoc />
    public void OnDrawEnd()
    {
      _batch.End();
    }
  }
}
