using BlueJay.App.Games.Breakout.Addons;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BlueJay.App.Games.Breakout.Systems
{
  /// <summary>
  /// Rendering system is meant to render all the elements to the screen every frame
  /// </summary>
  public class BreakoutRenderingSystem : ComponentSystem
  {
    /// <summary>
    /// The renderer that should be used for the breakout
    /// </summary>
    private readonly IRenderer _renderer;

    /// <summary>
    /// The current addon key that is meant to act as a selector for the Draw/Update
    /// methods with entities
    /// </summary>
    public override long Key => TypeAddon.Identifier | BoundsAddon.Identifier;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor is meant to inject the renderer into the system for processing
    /// </summary>
    /// <param name="renderer">The renderer that should be used for the breakout</param>
    public BreakoutRenderingSystem(IRenderer renderer)
    {
      _renderer = renderer;
    }

    /// <summary>
    /// The draw event that is called for each entity that was selected by the key
    /// for this system
    /// </summary>
    /// <param name="entity">The current entity that should be drawn</param>
    public override void OnDraw(IEntity entity)
    {
      var ta = entity.GetAddon<TypeAddon>();
      var ba = entity.GetAddon<BoundsAddon>();

      switch(ta.Type)
      {
        case EntityType.Paddle: // Draw the paddle to the screen
          _renderer.DrawRectangle(ba.Bounds.Width, ba.Bounds.Height, new Vector2(ba.Bounds.X, ba.Bounds.Y), Color.Blue);
          break;
        case EntityType.Ball:
          { // Load the texture and color of the ball and draw it to the screen
            var txa = entity.GetAddon<TextureAddon>();
            var ca = entity.GetAddon<ColorAddon>();
            _renderer.Draw(txa.Texture, new Vector2(ba.Bounds.X, ba.Bounds.Y), ca?.Color ?? Color.Black);
          }
          break;
        case EntityType.Block:
          { // Load the block index to get the color and draw it to the screen
            var bia = entity.GetAddon<BlockIndexAddon>();
            _renderer.DrawRectangle(ba.Bounds.Width, ba.Bounds.Height, new Vector2(ba.Bounds.X, ba.Bounds.Y), bia?.Color ?? Color.Black);
          }
          break;
      }
    }
  }
}
