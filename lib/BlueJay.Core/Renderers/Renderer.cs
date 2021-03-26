using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.Core.Renderers
{
  public class Renderer : IRenderer
  {
    private readonly SpriteFont _font;
    private readonly ContentManager _content;
    private readonly Texture2D _pixel;

    public SpriteBatch Batch { get; }

    public Renderer(GraphicsDevice graphics, SpriteBatch batch, ContentManager content, SpriteFont font)
    {
      Batch = batch;
      _font = font;
      _content = content;
      _pixel = graphics.CreateRectangle(1, 1, Color.White);
    }

    public virtual void DrawString(string text, Vector2 position, Color color)
    {
      if (_font != null)
      {
        Begin();
        Batch.DrawString(_font, text, position, color);
        End();
      }
    }

    public virtual void Draw(Texture2D texture, Vector2 position)
    {
      Draw(texture, position, Color.White);
    }

    public virtual void Draw(Texture2D texture, Vector2 position, Color color)
    {
      if (texture != null)
      {
        Begin();
        Batch.Draw(texture, position, color);
        End();
      }
    }

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

    public virtual void DrawRectangle(int width, int height, Vector2 position, Color color)
    {
      if (_pixel != null)
      {
        Begin();
        Batch.Draw(_pixel, new Rectangle((int)Math.Round(position.X), (int)Math.Round(position.Y), width, height), color);
        End();
      }
    }

    public virtual void Begin()
    {
      Batch.Begin();
    }

    public virtual void End()
    {
      Batch.End();
    }
  }
}
