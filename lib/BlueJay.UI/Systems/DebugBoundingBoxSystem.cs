using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Containers;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Systems
{
  internal class DebugBoundingBoxSystem : IDrawSystem
  {
    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly ISpriteBatchContainer _batch;

    /// <summary>
    /// Extensions to the sprite batch to draw to the screen basic objects
    /// </summary>
    private readonly SpriteBatchExtension _batchExtensions;

    /// <summary>
    /// The entities that we are going to draw to the screen
    /// </summary>
    private readonly IQuery<PositionAddon, BoundsAddon> _entities;

    /// <summary>
    /// The font collection need to render stuff to the screen
    /// </summary>
    private readonly IFontCollection _fonts;

    /// <summary>
    /// Constructor method is meant to build out the renderer system and inject
    /// the renderer for drawing
    /// </summary>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    /// <param name="batchExtension">Extensions to the sprite batch to draw to the screen basic objects</param>
    /// <param name="fonts">The font collection need to render stuff to the screen</param>
    /// <param name="entities">The entities that we are going to draw to the screen</param>
    public DebugBoundingBoxSystem(ISpriteBatchContainer batch, SpriteBatchExtension batchExtension, IFontCollection fonts, IQuery<PositionAddon, BoundsAddon> entities)
    {
      _batch = batch;
      _batchExtensions = batchExtension;
      _fonts = fonts;
      _entities = entities;
    }

    /// <inheritdoc />
    public void OnDraw()
    {
      _batch.Begin();
      foreach (var entity in _entities)
      {
        var pa = entity.GetAddon<PositionAddon>();
        var ba = entity.GetAddon<BoundsAddon>();

        if (!entity.MatchKey(KeyHelper.Create<TextAddon>()))
        {
          _batchExtensions.DrawRectangle(ba.Bounds.Width, 1, pa.Position, Color.LightGray);
          _batchExtensions.DrawRectangle(ba.Bounds.Width, 1, pa.Position + new Vector2(0, ba.Bounds.Height), Color.LightGray);
          _batchExtensions.DrawRectangle(1, ba.Bounds.Height, pa.Position, Color.LightGray);
          _batchExtensions.DrawRectangle(1, ba.Bounds.Height, pa.Position + new Vector2(ba.Bounds.Width, 0), Color.LightGray);

          if (_fonts.TextureFonts.Count > 0)
          {
            _batch.DrawString(_fonts.TextureFonts.FirstOrDefault().Value, entity.Id.ToString(), pa.Position, Color.Black);
          }
          else if (_fonts.SpriteFonts.Count > 0)
          {
            _batch.DrawString(_fonts.SpriteFonts.FirstOrDefault().Value, entity.Id.ToString(), pa.Position, Color.Black);
          }
        }
      }
      _batch.End();
    }
  }
}
