using BlueJay.Core.Container;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Core.Containers
{
  /// <summary>
  /// Container around the sprite batch object
  /// </summary>
  public interface ISpriteBatchContainer
  {
    /// <summary>
    /// The current spritebatch for this container
    /// </summary>
    SpriteBatch Current { get; }

    /// <summary>
    /// <see cref="ITexture2DContainer" /> draw method for <see cref="SpriteBatch.Draw(Texture2D, Rectangle, Rectangle?, Color, float, Vector2, SpriteEffects, float)" />
    /// </summary>
    void Draw(ITexture2DContainer container, Rectangle destinationRectangle, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, SpriteEffects effects, float layerDepth);

    /// <summary>
    /// <see cref="ITexture2DContainer" /> draw method for <see cref="SpriteBatch.Draw(Texture2D, Vector2, Rectangle?, Color, float, Vector2, Vector2, SpriteEffects, float)" />
    /// </summary>
    void Draw(ITexture2DContainer container, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth);

    /// <summary>
    /// <see cref="ITexture2DContainer" /> draw method for <see cref="SpriteBatch.Draw(Texture2D, Rectangle, Color)" />
    /// </summary>
    void Draw(ITexture2DContainer container, Rectangle destinationRectangle, Color color);

    /// <summary>
    /// <see cref="ITexture2DContainer "/> draw method for <see cref="SpriteBatch.Draw(Texture2D, Vector2, Rectangle?, Color, float, Vector2, float, SpriteEffects, float)"/>
    /// </summary>
    void Draw(ITexture2DContainer container, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth);

    /// <summary>
    /// <see cref="ITexture2DContainer" /> draw method for <see cref="SpriteBatch.Draw(Texture2D, Rectangle, Color)" />
    /// </summary>
    void Draw(ITexture2DContainer container, Vector2 position, Color color);

    /// <summary>
    /// <see cref="ISpriteFontContainer" /> draw method for <see cref="SpriteBatch.DrawString(SpriteFont, string, Vector2, Color)"/>
    /// </summary>
    void DrawString(ISpriteFontContainer container, string text, Vector2 position, Color color);

    /// <summary>
    /// <see cref="ITexture2DContainer "/> draw method for <see cref="SpriteBatch.Draw(Texture2D, Vector2, Rectangle?, Color, float, Vector2, float, SpriteEffects, float)"/>
    /// </summary>
    void Begin(SpriteSortMode sortMode = SpriteSortMode.Deferred, BlendState? blendState = null, SamplerState? samplerState = null, DepthStencilState? depthStencilState = null, RasterizerState? rasterizerState = null, Effect? effect = null, Matrix? transformMatrix = null);

    /// <summary>
    /// <see cref="ITexture2DContainer" /> draw method from <see cref="SpriteBatch.Begin(SpriteSortMode, BlendState, SamplerState, DepthStencilState, RasterizerState, Effect, Matrix?)" />
    /// </summary>
    void End();

    /// <summary>
    /// Method is meant to draw a string to a place on the screen
    /// </summary>
    /// <param name="spriteBatch">The sprite batch to render the string on</param>
    /// <param name="font">The texture font that is meant to render the text with</param>
    /// <param name="text">The text that should be printed out</param>
    /// <param name="position">The position of the text</param>
    /// <param name="color">The color of the text</param>
    /// <param name="scale">The scale of the font being used</param>
    void DrawString(TextureFont font, string text, Vector2 position, Color color, float scale = 1.0f);

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
    void DrawFrame(ITexture2DContainer texture, Vector2 position, int rows, int columns, int frame, Color? color = null, float rotation = 0f, Vector2? origin = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 1f);

    /// <summary>
    /// Method is meant to draw a frame on a sprite sheet
    /// </summary>
    /// <param name="spriteBatch">The sprite batch to draw the frame on</param>
    /// <param name="texture">The sprite sheet that should be drawn from</param>
    /// <param name="destinationRectangle">The desitnation where the frame should be drawn</param>
    /// <param name="rows">The number of rows in the sprite sheet</param>
    /// <param name="columns">The number of columns in the sprite sheet</param>
    /// <param name="frame">The current frame we are wanting to render on the sprite sheet</param>
    /// <param name="color">The color that should be spliced into the sprite being drawn</param>
    /// <param name="rotation">The rotation of the frame</param>
    /// <param name="origin">The origin to use for the rotation</param>
    /// <param name="effects">The effect that should be used on the sprite being drawn</param>
    /// <param name="layerDepth">A depth of the layer of this sprite</param>
    void DrawFrame(ITexture2DContainer texture, Rectangle destinationRectangle, int rows, int columns, int frame, Color? color = null, float rotation = 0f, Vector2? origin = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 1f);

    /// <summary>
    /// Method is meant to draw a rectangle to the screen
    /// </summary>
    /// <param name="spriteBatch">The sprite batch to draw the ninepatch to</param>
    /// <param name="ninePatch">The nine patch that should be used when processing</param>
    /// <param name="width">The width of the rectangle that should be drawn</param>
    /// <param name="height">The height of the rectangle that should be drawn</param>
    /// <param name="position">The position of the rectangle</param>
    /// <param name="color">The color of the rectangle</param>
    void DrawNinePatch(NinePatch ninePatch, int width, int height, Vector2 position, Color color);
  }
}
