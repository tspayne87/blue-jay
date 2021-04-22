using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using BlueJay.Core.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.UI.Systems
{
  /// <summary>
  /// UI system to build out the nine patch texture and cache it for rendering
  /// </summary>
  public class UINinePatchTextureSystem : ComponentSystem
  {
    /// <summary>
    /// The current graphic device we are working with
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The renderer so we can render the ninepatch to a renderable target
    /// </summary>
    private readonly IRenderer _renderer;

    /// <summary>
    /// The key that we should filter on for this system to work with
    /// </summary>
    public override long Key => TextureAddon.Identifier | StyleAddon.Identifier | BoundsAddon.Identifier;

    /// <summary>
    /// The layers this system should be working on
    /// </summary>
    public override List<string> Layers => new List<string>() { UIStatic.LayerName };

    /// <summary>
    /// Constructor to build out the system and inject various items into it
    /// </summary>
    /// <param name="graphics">The current graphic device we are working with</param>
    /// <param name="renderer">The renderer to add things to the graphics device</param>
    public UINinePatchTextureSystem(GraphicsDevice graphics, IRenderer renderer)
    {
      _graphics = graphics;
      _renderer = renderer;
    }

    /// <summary>
    /// Update event that will trigger a render of the texture if it does not exist
    /// </summary>
    /// <param name="entity">The entity we are currently working on</param>
    public override void OnUpdate(IEntity entity)
    {
      var ta = entity.GetAddon<TextureAddon>();
      var sa = entity.GetAddon<StyleAddon>();
      var ba = entity.GetAddon<BoundsAddon>();

      if (ta.Texture == null)
      {
        // Add some pixels to fix the mod before rendering the texture so we have a seemless pattern without clipping
        var widthMod = ba.Bounds.Width % sa.NinePatch.Break.X;
        if (widthMod != 0)
          ba.Bounds.Width += sa.NinePatch.Break.X - widthMod;

        // Add some pixels to fix the mod before rendering the texture so we have a seemless patern without clipping
        var heightMod = ba.Bounds.Height % sa.NinePatch.Break.Y;
        if (heightMod != 0)
          ba.Bounds.Height += sa.NinePatch.Break.X - heightMod;

        var target = new RenderTarget2D(_graphics, ba.Bounds.Width, ba.Bounds.Height);
        _graphics.SetRenderTarget(target);
        _graphics.Clear(Color.Transparent);
        _renderer.DrawRectangle(sa.NinePatch, ba.Bounds.Width, ba.Bounds.Height, Vector2.Zero, Color.White);
        _graphics.SetRenderTarget(null);

        ta.Texture = target;
        sa.CachedTexture = target;
      }
    }
  }
}
