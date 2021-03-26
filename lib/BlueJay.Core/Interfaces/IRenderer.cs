using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Core.Interfaces
{
  public interface IRenderer
  {
    SpriteBatch Batch { get; }

    void DrawString(string text, Vector2 position, Color color);

    void Draw(Texture2D texture, Vector2 position);
    void Draw(Texture2D texture, Vector2 position, Color color);
    void DrawFrame(Texture2D texture, Vector2 position, int rows, int columns, int frame, Color color, SpriteEffects? effects);
    void DrawRectangle(int width, int height, Vector2 position, Color color);

    void Begin();
    void End();
  }
}
