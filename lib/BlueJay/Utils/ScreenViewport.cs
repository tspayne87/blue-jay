using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Utils
{
  /// <summary>
  /// Implementation of the <see cref="IScreenViewport" />
  /// </summary>
  internal class ScreenViewport : IScreenViewport
  {
    /// <summary>
    /// The internal graphics device meant to get the viewport from
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <inheritdoc />
    public int Width => _graphics.Viewport.Width;

    /// <inheritdoc />
    public int Height => _graphics.Viewport.Height;

    /// <summary>
    /// Constructor to inject the proper graphics device into
    /// </summary>
    /// <param name="graphics">The internal graphics device meant to get the viewport from</param>
    public ScreenViewport(GraphicsDevice graphics)
    {
      _graphics = graphics;
    }
  }
}
