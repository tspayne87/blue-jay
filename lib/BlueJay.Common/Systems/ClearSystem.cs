using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// Clear system is meant to clear the screen
  /// </summary>
  public class ClearSystem : IDrawSystem
  {
    /// <summary>
    /// The current graphics device we are clearing the screen to
    /// </summary>
    private readonly GraphicsDevice _graphicsDevice;

    /// <summary>
    /// The color we should clear the screen to
    /// </summary>
    private readonly Color _color;

    /// <inheritdoc />
    public long Key => 0;

    /// <inheritdoc />
    public List<string> Layers => new List<string>();

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

    /// <inheritdoc />
    public void OnDraw()
    {
      _graphicsDevice.Clear(_color);
    }
  }
}
