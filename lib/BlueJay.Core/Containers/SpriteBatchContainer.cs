using BlueJay.Core.Container;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Core.Containers
{
  /// <summary>
  /// Implementation of the <see cref="ISpriteBatchContainer" />
  /// </summary>
  internal class SpriteBatchContainer : ISpriteBatchContainer
  {
    /// <summary>
    /// The current pixel that should be used to render rectangles
    /// </summary>
    private readonly Texture2D _pixel;

    /// <summary>
    /// The current sprite batch for this container
    /// </summary>
    private readonly SpriteBatch _spriteBatch;

    /// <inheritdoc />
    public SpriteBatch Current => _spriteBatch;

    /// <inheritdoc />
    public SpriteBatchContainer(SpriteBatch spriteBatch)
    {
      _spriteBatch = spriteBatch;
      _pixel = new Texture2D(spriteBatch.GraphicsDevice, 1, 1);
    }

    /// <inheritdoc />
    public void Draw(ITexture2DContainer container, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth)
    {
      if (container.Current == null)
        throw new ArgumentNullException("container.Current");
      _spriteBatch.Draw(container.Current, destinationRectangle, sourceRectangle, color, rotation, origin, effects, layerDepth);
    }

    /// <inheritdoc />
    public void Draw(ITexture2DContainer container, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
      if (container.Current == null)
        throw new ArgumentNullException("container.Current");
      _spriteBatch.Draw(container.Current, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
    }

    /// <inheritdoc />
    public void Draw(ITexture2DContainer container, Rectangle destinationRectangle, Color color)
    {
      if (container.Current == null)
        throw new ArgumentNullException("container.Current");
      _spriteBatch.Draw(container.Current, destinationRectangle, color);
    }

    /// <inheritdoc />
    public void Draw(ITexture2DContainer container, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
    {
      if (container.Current == null)
        throw new ArgumentNullException("container.Current");
      _spriteBatch.Draw(container.Current, position, sourceRectangle, color, rotation, origin, scale, effects, layerDepth);
    }

    /// <inheritdoc />
    public void Draw(ITexture2DContainer container, Vector2 position, Color color)
    {
      if (container.Current == null)
        throw new ArgumentNullException("container.Current");
      _spriteBatch.Draw(container.Current, position, color);
    }

    /// <inheritdoc />
    public void DrawString(ISpriteFontContainer container, string text, Vector2 position, Color color)
    {
      if (container.Current == null)
        throw new ArgumentNullException("container.Current");
      _spriteBatch.DrawString(container.Current, text, position, color);
    }

    /// <inheritdoc />
    public void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState? blendState = null, SamplerState? samplerState = null, DepthStencilState? depthStencilState = null, RasterizerState? rasterizerState = null, Effect? effect = null, Matrix? transformMatrix = null)
    {
      _spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
    }

    /// <inheritdoc />
    public void End()
    {
      _spriteBatch.End();
    }

    /// <inheritdoc />
    public void DrawString(TextureFont font, string text, Vector2 position, Color color, float scale = 1.0f)
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
          Draw(font.Texture, pos, bounds, color, 0f, Vector2.Zero, scale, SpriteEffects.None, 0f);
          pos += new Vector2(font.Width * scale, 0);
        }
      }
    }

    /// <inheritdoc />
    public void DrawFrame(ITexture2DContainer container, Vector2 position, int rows, int columns, int frame, Color? color = null, float rotation = 0f, Vector2? origin = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 1f)
    {
      if (container != null)
      {
        var width = container.Width / columns;
        var height = container.Height / rows;
        var source = new Rectangle((frame % columns) * width, (frame / columns) * height, width, height);

        Draw(container, new Rectangle(position.ToPoint(), new Point(width, height)), source, color ?? Color.White, rotation, origin ?? Vector2.Zero, effects, layerDepth);
      }
    }

    /// <inheritdoc />
    public void DrawFrame(ITexture2DContainer container, Rectangle destinationRectangle, int rows, int columns, int frame, Color? color = null, float rotation = 0f, Vector2? origin = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 1f)
    {
      var width = container.Width / columns;
      var height = container.Height / rows;
      var source = new Rectangle((frame % columns) * width, (frame / columns) * height, width, height);

      Draw(container, destinationRectangle, source, color ?? Color.White, rotation, origin ?? Vector2.Zero, effects, layerDepth);
    }

    /// <inheritdoc />
    public void DrawNinePatch(NinePatch ninePatch, int width, int height, Vector2 position, Color color)
    {
      var innerHeight = height - (ninePatch.Break.Y * 2);
      var innerWidth = width - (ninePatch.Break.X * 2);

      var heightLength = (int)Math.Ceiling(innerHeight / (float)ninePatch.Break.Y);
      var widthLength = (int)Math.Ceiling(innerWidth / (float)ninePatch.Break.X);

      var heightOffset = innerHeight % ninePatch.Break.Y;
      var widthOffset = innerWidth % ninePatch.Break.X;

      // Draw middle of rectangle
      for (var y = 0; y < heightLength; ++y)
      {
        var yPos = ninePatch.Break.Y + (y * ninePatch.Break.Y);

        // Fill in middle
        for (var x = 0; x < widthLength; ++x)
        {
          var dest = new Rectangle(position.ToPoint() + new Point(ninePatch.Break.X + (x * ninePatch.Break.X), yPos), ninePatch.Break);
          var source = ninePatch.Middle;
          if (widthOffset > 0 && x == widthLength - 1)
          {
            dest.Width = widthOffset;
            source.Width = widthOffset;
          }

          if (heightOffset > 0 && y == heightLength - 1)
          {
            dest.Height = heightOffset;
            source.Height = heightOffset;
          }

          Draw(ninePatch.Texture, dest, source, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
        }

        // Draw left and right lines
        var leftDest = new Rectangle(position.ToPoint() + new Point(0, yPos), ninePatch.Break);
        var leftSource = ninePatch.MiddleLeft;
        if (heightOffset > 0 && y == heightLength - 1)
        {
          leftDest.Height = heightOffset;
          leftSource.Height = heightOffset;
        }
        Draw(ninePatch.Texture, leftDest, leftSource, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);

        var rightDest = new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, yPos), ninePatch.Break);
        var rightSource = ninePatch.MiddleRight;
        if (heightOffset > 0 && y == heightLength - 1)
        {
          rightDest.Height = heightOffset;
          rightSource.Height = heightOffset;
        }
        Draw(ninePatch.Texture, rightDest, rightSource, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      }

      // Draw top and bottom lines
      for (var i = 0; i < widthLength; ++i)
      {
        var xPos = ninePatch.Break.X + (i * ninePatch.Break.X);

        var topDest = new Rectangle(position.ToPoint() + new Point(xPos, 0), ninePatch.Break);
        var topSource = ninePatch.Top;
        if (widthOffset > 0 && i == widthLength - 1)
        {
          topDest.Width = widthOffset;
          topSource.Width = widthOffset;
        }
        Draw(ninePatch.Texture, topDest, topSource, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);

        var bottomDest = new Rectangle(position.ToPoint() + new Point(xPos, height - ninePatch.Break.Y), ninePatch.Break);
        var bottomSource = ninePatch.Bottom;
        if (widthOffset > 0 && i == widthLength - 1)
        {
          bottomDest.Width = widthOffset;
          bottomSource.Width = widthOffset;
        }
        Draw(ninePatch.Texture, bottomDest, bottomSource, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      }

      // Draw four corners
      Draw(ninePatch.Texture, new Rectangle(position.ToPoint(), ninePatch.Break), ninePatch.TopLeft, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, 0), ninePatch.Break), ninePatch.TopRight, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
      Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(0, height - ninePatch.Break.Y), ninePatch.Break), ninePatch.BottomLeft, color, 0f, Vector2.Zero, SpriteEffects.None, 1f);
      Draw(ninePatch.Texture, new Rectangle(position.ToPoint() + new Point(width - ninePatch.Break.X, height - ninePatch.Break.Y), ninePatch.Break), ninePatch.BottomRight, color, 0f, Vector2.Zero, SpriteEffects.None, 0f);
    }

    public void DrawLine(Vector2 pointA, Vector2 pointB, int weight, Color color)
    {
      var offset = pointA - pointB;
      var angle = (float)Math.Atan2(offset.Y, offset.X);
      var distance = Vector2.Distance(pointA, pointB);
      var origin = new Vector2();
      var pixelRect = new Rectangle(0, 0, 1, 1);
      var scale = new Vector2(weight, distance);

      _spriteBatch.Draw(_pixel, pointA, pixelRect, color, angle, origin, scale, SpriteEffects.None, 0);
    }
  }
}
