using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.Core
{
  /// <summary>
  /// A set of sprite batch extensions
  /// </summary>
  public class SpriteBatchExtension
  {
    /// <summary>
    /// The current pixel that should be used to render rectangles
    /// </summary>
    private readonly Texture2D _pixel;

    /// <summary>
    /// The current sprit batch we are rendering too
    /// </summary>
    private readonly SpriteBatch _batch;

    /// <summary>
    /// Constructor method to build out the renderer
    /// </summary>
    /// <param name="graphics">The graphics device to create a rectangle from</param>
    /// <param name="batch">The sprite batch that will be used to render stuff to the screen</param>
    public SpriteBatchExtension(GraphicsDevice graphics, SpriteBatch batch)
    {
      _batch = batch;
      _pixel = graphics.CreateRectangle(1, 1, Color.White);
    }

    /// <summary>
    /// Method is meant to draw a rectangle to the screen
    /// </summary>
    /// <param name="width">The width of the rectangle that should be drawn</param>
    /// <param name="height">The height of the rectangle that should be drawn</param>
    /// <param name="position">The position of the rectangle</param>
    /// <param name="color">The color of the rectangle</param>
    public virtual void DrawRectangle(int width, int height, Vector2 position, Color color)
    {
      if (_pixel != null)
      {
        _batch.Draw(_pixel, new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), width, height), color);
      }
    }

    /// <summary>
    /// Method is meant to draw a rectangle to the screen
    /// </summary>
    /// <param name="size">The size of the rectangle to draw</param>
    /// <param name="position">The position of the rectangle</param>
    /// <param name="color">The color of the rectangle</param>
    public virtual void DrawRectangle(Size size, Vector2 position, Color color)
    {
      DrawRectangle(size.Width, size.Height, position, color);
    }
  }
}
