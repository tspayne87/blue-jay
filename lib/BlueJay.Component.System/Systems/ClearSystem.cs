using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Component.System.Systems
{
  /// <summary>
  /// Clear system is meant to clear the screen
  /// </summary>
  public class ClearSystem : ComponentSystem
  {
    /// <summary>
    /// The current graphics device we are clearing the screen to
    /// </summary>
    private readonly GraphicsDevice _graphicsDevice;

    /// <summary>
    /// The color we should clear the screen to
    /// </summary>
    private readonly Color _color;

    /// <summary>
    /// The Identifier for this system 0 is used if we do not care about the entities
    /// </summary>
    public override long Key => 0;

    /// <summary>
    /// Constructor to build out the clear system and get it ready to clear on every frame
    /// </summary>
    /// <param name="graphicsDevice">The current graphics device</param>
    /// <param name="color">The color we should be clearing too</param>
    public ClearSystem(GraphicsDevice graphicsDevice, Color color)
    {
      _graphicsDevice = graphicsDevice;
      _color = color;
    }

    /// <summary>
    /// The draw method that will clear the screen
    /// </summary>
    /// <param name="delta">The current delta of the draw</param>
    public override void OnDraw(int delta)
    {
      _graphicsDevice.Clear(_color);
    }
  }
}
