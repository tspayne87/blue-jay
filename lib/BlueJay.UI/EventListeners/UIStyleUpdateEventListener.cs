using BlueJay.Component.System;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.UI.EventListeners
{
  /// <summary>
  /// Method is meant to build out the texture for the ninepatch
  /// </summary>
  public class UIStyleUpdateEventListener : EventListener<StyleUpdateEvent>
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The renderer so we can render the ninepatch to a renderable target
    /// </summary>
    private readonly RendererCollection _renderer;

    /// <summary>
    /// Constructor to build out the event listener to update the texture for the ninepatch
    /// </summary>
    /// <param name="graphics">The current graphic device we are working with</param>
    /// <param name="renderer">The renderer to add things to the graphics device</param>
    public UIStyleUpdateEventListener(GraphicsDevice graphics, RendererCollection renderer)
    {
      _graphics = graphics;
      _renderer = renderer;
    }

    /// <summary>
    /// Processor to update the ninepatch if the bounds changed for an entity
    /// </summary>
    /// <param name="evt">The event that triggered this listener</param>
    public override void Process(IEvent<StyleUpdateEvent> evt)
    {
      var sa = evt.Data.Entity.GetAddon<StyleAddon>();
      
      if (sa.CurrentStyle.NinePatch != null)
      {
        var ba = evt.Data.Entity.GetAddon<BoundsAddon>();
        var ta = evt.Data.Entity.GetAddon<TextureAddon>();

        if (ta.Texture != null)
        {
          ta.Texture.Dispose();
          ta.Texture = null;
        }

        // Create a render target and generate the ninepatch texture so we do not have todo this expensive operation
        // each frame of the game
        var target = new RenderTarget2D(_graphics, ba.Bounds.Width, ba.Bounds.Height);
        _graphics.SetRenderTarget(target);
        _graphics.Clear(Color.Transparent);
        _renderer["UI"].DrawRectangle(sa.CurrentStyle.NinePatch, ba.Bounds.Width, ba.Bounds.Height, Vector2.Zero, Color.White);
        _graphics.SetRenderTarget(null);

        sa.StyleUpdates++;
        ta.Texture = target;
      }
    }
  }
}
