using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BlueJay.Core
{
  /// <summary>
  /// Set of extension methods built around drawing things on the sprite batch
  /// </summary>
  public static class SpriteBatchExtensions
  {
    /// <summary>
    /// Method is meant to draw a string to a place on the screen
    /// </summary>
    /// <param name="spriteBatch">The sprite batch to render the string on</param>
    /// <param name="font">The texture font that is meant to render the text with</param>
    /// <param name="text">The text that should be printed out</param>
    /// <param name="position">The position of the text</param>
    /// <param name="color">The color of the text</param>
    public static void DrawString(this SpriteBatch spriteBatch, TextureFont font, string text, Vector2 position, Color color)
    {
      DrawString(spriteBatch, font, text, position, color, 1.0f);
    }

    /// <summary>
    /// Method is meant to draw a string to a place on the screen
    /// </summary>
    /// <param name="spriteBatch">The sprite batch to render the string on</param>
    /// <param name="font">The texture font that is meant to render the text with</param>
    /// <param name="text">The text that should be printed out</param>
    /// <param name="position">The position of the text</param>
    /// <param name="color">The color of the text</param>
    /// <param name="scale">The scale of the font being used</param>
    public static void DrawString(this SpriteBatch spriteBatch, TextureFont font, string text, Vector2 position, Color color, float scale)
    {
      var pos = position;
      for (var i = 0; i < text.Length; ++i)
      {
        if (text[i] == '\n')
        {
          pos = new Vector2(position.X, pos.Y + (font.Height * scale));
        }
        else if (text[i] == ' ')
        {
          pos += new Vector2((font.Width * scale), 0);
        }
        else
        {
          var bounds = font.GetBounds(text[i]);
          spriteBatch.Draw(font.Texture, pos, bounds, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
          pos += new Vector2(font.Width * scale, 0);
        }
      }
    }

    /// <summary>
    /// Method is meant to draw a frame on a sprite sheet
    /// </summary>
    /// <param name="spriteBatch">The sprite batch to draw the frame on</param>
    /// <param name="texture">The sprite sheet that should be drawn from</param>
    /// <param name="position">The position where the sprite should be drawn</param>
    /// <param name="rows">The number of rows in the sprite sheet</param>
    /// <param name="columns">The number of columns in the sprite sheet</param>
    /// <param name="frame">The current frame we are wanting to render on the sprite sheet</param>
    /// <param name="color">The color that should be spliced into the sprite being drawn</param>
    /// <param name="rotation">The rotation of the frame</param>
    /// <param name="origin">The origin to use for the rotation</param>
    /// <param name="effects">The effect that should be used on the sprite being drawn</param>
    /// <param name="layerDepth">A depth of the layer of this sprite</param>
    public static void DrawFrame(this SpriteBatch spriteBatch, Texture2D texture, Vector2 position, int rows, int columns, int frame, Color? color = null, float rotation = 0f, Vector2? origin = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 1f)
    {
      if (texture != null)
      {
        var width = texture.Width / columns;
        var height = texture.Height / rows;
        var source = new Rectangle((frame % columns) * width, (frame / columns) * height, width, height);

        spriteBatch.Draw(texture, new Rectangle(position.ToPoint(), new Point(width, height)), source, color ?? Color.White, rotation, origin ?? Vector2.Zero, effects, layerDepth);
      }
    }

    /// <summary>
    /// Method is meant to draw the list of particles to the scene
    /// </summary>
    /// <param name="spriteBatch">The sprite batch to draw the particles on</param>
    /// <param name="texture">The texture that should be used for the particles</param>
    /// <param name="particles">The particles that should be rendered</param>
    public static void DrawParticles(this SpriteBatch spriteBatch, Texture2D texture, List<Particle> particles)
    {
      if (particles.Count > 0)
      {
        for (var i = 0; i < particles.Count; ++i)
        {
          spriteBatch.Draw(texture, particles[i].Position, particles[i].Color);
        }
      }
    }

    /// <summary>
    /// Method is meant to draw a rectangle to the screen
    /// </summary>
    /// <param name="spriteBatch">The sprite batch to draw the ninepatch to</param>
    /// <param name="ninePatch">The nine patch that should be used when processing</param>
    /// <param name="width">The width of the rectangle that should be drawn</param>
    /// <param name="height">The height of the rectangle that should be drawn</param>
    /// <param name="position">The position of the rectangle</param>
    /// <param name="color">The color of the rectangle</param>
    public static void DrawNinePatch(this SpriteBatch spriteBatch, NinePatch ninePatch, int width, int height, Vector2 position, Color color)
    {
      var innerHeight = height - (ninePatch.Break.Y * 2);
      var innerWidth = width - (ninePatch.Break.X * 2);

      var heightLength = (int)Math.Ceiling(innerHeight / (float)ninePatch.Break.Y);
      var widthLength = (int)Math.Ceiling(innerWidth / (float)ninePatch.Break.X);

      // Draw four corners
      spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint(), ninePatch.Break), ninePatch.TopLeft, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, 0), ninePatch.Break), ninePatch.TopRight, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(0, height - ninePatch.Break.Y), ninePatch.Break), ninePatch.BottomLeft, color, 0f, Vector2.Zero, SpriteEffects.None, 1f);
      spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, height - ninePatch.Break.Y), ninePatch.Break), ninePatch.BottomRight, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);

      // Draw middle of rectangle
      for (var y = 0; y < heightLength; ++y)
      {
        var yPos = ninePatch.Break.Y + (y * ninePatch.Break.Y);

        // Fill in middle
        for (var x = 0; x < widthLength; ++x)
        {
          spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(ninePatch.Break.X + (x * ninePatch.Break.X), yPos), ninePatch.Break), ninePatch.Middle, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }

        // Draw left and right lines
        spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(0, yPos), ninePatch.Break), ninePatch.MiddleLeft, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, yPos), ninePatch.Break), ninePatch.MiddleRight, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      }

      // Draw top and bottom lines
      for (var i = 0; i < widthLength; ++i)
      {
        var xPos = ninePatch.Break.X + (i * ninePatch.Break.X);
        spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(xPos, 0), ninePatch.Break), ninePatch.Top, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(xPos, height - ninePatch.Break.Y), ninePatch.Break), ninePatch.Bottom, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      }

      // Draw four corners
      spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint(), ninePatch.Break), ninePatch.TopLeft, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, 0), ninePatch.Break), ninePatch.TopRight, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(0, height - ninePatch.Break.Y), ninePatch.Break), ninePatch.BottomLeft, color, 0f, Vector2.Zero, SpriteEffects.None, 1f);
      spriteBatch.Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, height - ninePatch.Break.Y), ninePatch.Break), ninePatch.BottomRight, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
    }
  }
}
