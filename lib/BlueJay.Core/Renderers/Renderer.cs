using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.Core.Renderers
{
  /// <summary>
  /// Basic renderer to help with drawing things to the screen
  /// </summary>
  public class Renderer : IRenderer
  {
    /// <summary>
    /// The current basic font that should be used when drawing a string
    /// </summary>
    private readonly SpriteFont _font;

    /// <summary>
    /// The current pixel that should be used to render rectangles
    /// </summary>
    private readonly Texture2D _pixel;

    /// <summary>
    /// The current sprit batch we are rendering too
    /// </summary>
    public SpriteBatch Batch { get; }

    /// <summary>
    /// Constructor method to build out the renderer
    /// </summary>
    /// <param name="graphics">The graphics device to create a rectangle from</param>
    /// <param name="batch">The sprite batch that will be used to render stuff to the screen</param>
    /// <param name="font">The global font</param>
    public Renderer(GraphicsDevice graphics, SpriteBatch batch, SpriteFont font)
    {
      Batch = batch;
      _font = font;
      _pixel = graphics.CreateRectangle(1, 1, Color.White);
    }

    /// <summary>
    /// Method is meant to draw a string to a place on the screen
    /// </summary>
    /// <param name="text">The text that should be printed out</param>
    /// <param name="position">The position of the text</param>
    /// <param name="color">The color of the text</param>
    public virtual void DrawString(string text, Vector2 position, Color color)
    {
      if (_font != null)
      {
        Begin();
        Batch.DrawString(_font, text, position, color);
        End();
      }
    }

    /// <summary>
    /// Method is meant to draw a texture to the screen at a certain position
    /// </summary>
    /// <param name="texture">The texture that should be drawn</param>
    /// <param name="position">The current position of that texture</param>
    public virtual void Draw(Texture2D texture, Vector2 position)
    {
      Draw(texture, position, Color.White);
    }

    /// <summary>
    /// Method is meant to draw a texture to the screen with some color spliced in at a certain position
    /// </summary>
    /// <param name="texture">The texture that should be drawn</param>
    /// <param name="position">The current position of that texture</param>
    /// <param name="color">The color that should be spliced into the texture during draw time</param>
    public virtual void Draw(Texture2D texture, Vector2 position, Color color)
    {
      if (texture != null)
      {
        Begin();
        Batch.Draw(texture, position, color);
        End();
      }
    }

    /// <summary>
    /// Method is meant to draw a frame on a sprite sheet
    /// </summary>
    /// <param name="texture">The sprite sheet that should be drawn from</param>
    /// <param name="position">The position where the sprite should be drawn</param>
    /// <param name="rows">The number of rows in the sprite sheet</param>
    /// <param name="columns">The number of columns in the sprite sheet</param>
    /// <param name="frame">The current frame we are wanting to render on the sprite sheet</param>
    /// <param name="color">The color that should be spliced into the sprite being drawn</param>
    /// <param name="effects">The effect that should be used on the sprite being drawn</param>
    public void DrawFrame(Texture2D texture, Vector2 position, int rows, int columns, int frame, Color color, SpriteEffects? effects)
    {
      if (texture != null)
      {
        var width = texture.Width / columns;
        var height = texture.Height / rows;
        var source = new Rectangle((frame % columns) * width, (frame / columns) * height, width, height);

        Begin();
        Batch.Draw(texture, new Rectangle(position.ToPoint(), new Point(width, height)), source, color, 0f, Vector2.Zero, effects ?? SpriteEffects.None, 1f);
        End();
      }
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
        Begin();
        Batch.Draw(_pixel, new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), width, height), color);
        End();
      }
    }

    /// <summary>
    /// Method is meant to draw a rectangle to the screen
    /// </summary>
    /// <param name="ninePatch">The nine patch that should be used when processing</param>
    /// <param name="width">The width of the rectangle that should be drawn</param>
    /// <param name="height">The height of the rectangle that should be drawn</param>
    /// <param name="position">The position of the rectangle</param>
    /// <param name="color">The color of the rectangle</param>
    public void DrawRectangle(NinePatch ninePatch, int width, int height, Vector2 position, Color color)
    {
      if (width % ninePatch.Break.X != 0) throw new ArgumentException("Width needs to have a 0 modulas of the ninepatch width", nameof(width));
      if (height % ninePatch.Break.Y != 0) throw new ArgumentException("Height needs to have a 0 modulas of the ninepatch height", nameof(height));

      var innerHeight = height - (ninePatch.Break.Y * 2);
      var innerWidth = width - (ninePatch.Break.X * 2);

      var heightLength = innerHeight / ninePatch.Break.Y;
      var widthLength = innerWidth / ninePatch.Break.X;

      Begin();
      // Draw four corners
      Batch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint(), ninePatch.Break), ninePatch.TopLeft, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      Batch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, 0), ninePatch.Break), ninePatch.TopRight, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      Batch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(0, height - ninePatch.Break.Y), ninePatch.Break), ninePatch.BottomLeft, color, 0f, Vector2.Zero, SpriteEffects.None, 1f);
      Batch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, height - ninePatch.Break.Y), ninePatch.Break), ninePatch.BottomRight, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);

      // Draw top and bottom lines
      for (var i = 0; i < widthLength; ++i)
      {
        var xPos = ninePatch.Break.X + (i * ninePatch.Break.X);
        Batch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(xPos, 0), ninePatch.Break), ninePatch.Top, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        Batch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(xPos, height - ninePatch.Break.Y), ninePatch.Break), ninePatch.Bottom, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      }

      // Draw middle of rectangle
      for (var y = 0; y < heightLength; ++y)
      {
        // Draw left and right lines
        var yPos = ninePatch.Break.Y + (y * ninePatch.Break.Y);
        Batch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(0, yPos), ninePatch.Break), ninePatch.MiddleLeft, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        Batch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, yPos), ninePatch.Break), ninePatch.MiddleRight, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);

        // Fill in middle
        for (var x = 0; x < widthLength; ++x)
        {
          Batch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(ninePatch.Break.X + (x * ninePatch.Break.X), yPos), ninePatch.Break), ninePatch.Middle, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }
      }

      
      End();
    }

    /// <summary>
    /// The begin set to start the batch that should be drawn
    /// </summary>
    public virtual void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState blendState = null, SamplerState samplerState = null, DepthStencilState depthStencilState = null, RasterizerState rasterizerState = null, Effect effect = null, Matrix? transformMatrix = null)
    {
      Batch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
    }

    /// <summary>
    /// The end of the sprite batch that should be drawn
    /// </summary>
    public virtual void End()
    {
      Batch.End();
    }
  }
}
