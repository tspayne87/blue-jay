using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.UI.Events.EventListeners
{
  /// <summary>
  /// Method is meant to build out the texture for the ninepatch
  /// </summary>
  internal class UIStyleUpdateEventListener : EventListener<StyleUpdateEvent>
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly SpriteBatch _batch;

    /// <summary>
    /// Extensions for the sprite batch
    /// </summary>
    private readonly SpriteBatchExtension _batchExtension;

    /// <summary>
    /// Constructor to build out the event listener to update the texture for the ninepatch
    /// </summary>
    /// <param name="graphics">The current graphic device we are working with</param>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    /// <param name="batchExtension">Extensions for the sprite batch</param>
    public UIStyleUpdateEventListener(GraphicsDevice graphics, SpriteBatch batch, SpriteBatchExtension batchExtension)
    {
      _graphics = graphics;
      _batch = batch;
      _batchExtension = batchExtension;
    }

    /// <summary>
    /// Processor to update the ninepatch if the bounds changed for an entity
    /// </summary>
    /// <param name="evt">The event that triggered this listener</param>
    public override void Process(IEvent<StyleUpdateEvent> evt)
    {
      if (!evt.Data.Entity.MatchKey(KeyHelper.Create<TextAddon>()))
      {
        var sa = evt.Data.Entity.GetAddon<StyleAddon>();
        var ba = evt.Data.Entity.GetAddon<BoundsAddon>();
        var ta = evt.Data.Entity.GetAddon<TextureAddon>();

        if (ta.Texture != null)
          ta.Texture.Dispose();

        if (ba.Bounds.Width > 0 && ba.Bounds.Height > 0)
        {
          // Create a render target and generate the ninepatch texture so we do not have todo this expensive operation
          // each frame of the game
          var target = new RenderTarget2D(_graphics, ba.Bounds.Width, ba.Bounds.Height);
          _graphics.SetRenderTarget(target);
          _graphics.Clear(Color.Transparent);
          _batch.Begin();
          if (sa.CurrentStyle.NinePatch != null)
          {
            _batch.DrawNinePatch(sa.CurrentStyle.NinePatch, ba.Bounds.Width, ba.Bounds.Height, Vector2.Zero, Color.White);
          }
          else if (sa.CurrentStyle.BackgroundColor != null)
          {
            _batchExtension.DrawRectangle(ba.Bounds.Width, ba.Bounds.Height, Vector2.Zero, sa.CurrentStyle.BackgroundColor.Value);
          }
          _batch.End();
          _graphics.SetRenderTarget(null);

          sa.StyleUpdates++;
          ta.Texture = target;
        }

        evt.Data.Entity.Update(sa);
        evt.Data.Entity.Update(ta);
      }
    }
  }
}
