using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Core.Interfaces
{
  /// <summary>
  /// Basic renderer to give a set of methods to make rendering textures and basic objects easier
  /// </summary>
  public interface IRenderer
  {
    /// <summary>
    /// The current sprit batch we are rendering too
    /// </summary>
    SpriteBatch Batch { get; }

    /// <summary>
    /// Method is meant to draw a string to a place on the screen
    /// </summary>
    /// <param name="text">The text that should be printed out</param>
    /// <param name="position">The position of the text</param>
    /// <param name="color">The color of the text</param>
    void DrawString(string text, Vector2 position, Color color);

    /// <summary>
    /// Method is meant to draw a texture to the screen at a certain position
    /// </summary>
    /// <param name="texture">The texture that should be drawn</param>
    /// <param name="position">The current position of that texture</param>
    void Draw(Texture2D texture, Vector2 position);

    /// <summary>
    /// Method is meant to draw a texture to the screen with some color spliced in at a certain position
    /// </summary>
    /// <param name="texture">The texture that should be drawn</param>
    /// <param name="position">The current position of that texture</param>
    /// <param name="color">The color that should be spliced into the texture during draw time</param>
    void Draw(Texture2D texture, Vector2 position, Color color);

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
    void DrawFrame(Texture2D texture, Vector2 position, int rows, int columns, int frame, Color color, SpriteEffects? effects);

    /// <summary>
    /// Method is meant to draw a rectangle to the screen
    /// </summary>
    /// <param name="width">The width of the rectangle that should be drawn</param>
    /// <param name="height">The height of the rectangle that should be drawn</param>
    /// <param name="position">The position of the rectangle</param>
    /// <param name="color">The color of the rectangle</param>
    void DrawRectangle(int width, int height, Vector2 position, Color color);

    /// <summary>
    /// The begin set to start the batch that should be drawn
    /// </summary>
    void Begin();

    /// <summary>
    /// The end of the sprite batch that should be drawn
    /// </summary>
    void End();
  }
}
