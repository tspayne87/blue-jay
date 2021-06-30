using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Interfaces;
using System.Collections.Generic;

namespace BlueJay.Component.System.Systems
{
  /// <summary>
  /// Basic rendering system to draw a texture at a position
  /// </summary>
  public class RenderingSystem : ComponentSystem
  {
    /// <summary>
    /// The renderer to draw textures to the screen
    /// </summary>
    private readonly IRenderer _renderer;

    /// <summary>
    /// The Selector to determine that Position and Texture addons are needed
    /// for this system
    /// </summary>
    public override long Key => PositionAddon.Identifier | TextureAddon.Identifier;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor method is meant to build out the renderer system and inject
    /// the renderer for drawing
    /// </summary>
    /// <param name="collection">The renderer collection we need to pull from</param>
    /// <param name="renderer">The renderer that should be used one draw</param>
    public RenderingSystem(RendererCollection collection, string renderer)
    {
      _renderer = collection[renderer];
    }

    /// <summary>
    /// Draw method is meant to draw the entity to the screen based on the texture
    /// and position of the entity
    /// </summary>
    /// <param name="delta">The current delta for this frame</param>
    /// <param name="entity">The current entity that should be drawn</param>
    public override void OnDraw(IEntity entity)
    {
      var pc = entity.GetAddon<PositionAddon>();
      var tc = entity.GetAddon<TextureAddon>();

      _renderer.Draw(tc.Texture, pc.Position);
    }
  }
}
