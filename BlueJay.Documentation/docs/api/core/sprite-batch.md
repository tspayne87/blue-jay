# SpriteBatch Extensions Methods

## DrawString
Extension method meant to render texture fonts to the screen much like the sprite font methods
do for the sprite batch

```csharp
  /// @param font: The texture font that is meant to render the text with
  /// @param text: The text that should be printed out
  /// @param position: The position of the text
  /// @param color: The color of the text
  /// @param scale: The scale of the font being used
  public static void DrawString(TextureFont font, string text, Vector2 position, Color color, float scale = 1.0f);
```
## DrawFrame
Set of extension methods meant to draw frames from a texture and render the frame to the screen based
on the row and column position of the sprite position in the texture

```csharp
  /// @param texture: The sprite sheet that should be drawn from
  /// @param position: The position where the sprite should be drawn
  /// @param rows: The number of rows in the sprite sheet
  /// @param columns: The number of columns in the sprite sheet
  /// @param frame: The current frame we are wanting to render on the sprite sheet
  /// @param color: The color that should be spliced into the sprite being drawn
  /// @param rotation: The rotation of the frame
  /// @param origin: The origin to use for the rotation
  /// @param effects: The effect that should be used on the sprite being drawn
  /// @param layerDepth: A depth of the layer of this sprite
  public static void DrawFrame(Texture2D texture, Vector2 position, int rows, int columns, int frame, Color? color = null, float rotation = 0f, Vector2? origin = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 1f);

  /// @param texture: The sprite sheet that should be drawn from
  /// @param destinationRectangle: The desitnation where the frame should be drawn
  /// @param rows: The number of rows in the sprite sheet
  /// @param columns: The number of columns in the sprite sheet
  /// @param frame: The current frame we are wanting to render on the sprite sheet
  /// @param color: The color that should be spliced into the sprite being drawn
  /// @param rotation: The rotation of the frame
  /// @param origin: The origin to use for the rotation
  /// @param effects: The effect that should be used on the sprite being drawn
  /// @param layerDepth: A depth of the layer of this sprite
  public static void DrawFrame(Texture2D texture, Rectangle destinationRectangle, int rows, int columns, int frame, Color? color = null, float rotation = 0f, Vector2? origin = null, SpriteEffects effects = SpriteEffects.None, float layerDepth = 1f);
```
## DrawNinePatch
Extension method meant to handle drawing the nine patch rectangle to the screen based on stretching the ninepatch
areas to fill in the rectangle properly

```csharp
  /// @param ninePatch: The nine patch that should be used when processing
  /// @param width: The width of the rectangle that should be drawn
  /// @param height: The height of the rectangle that should be drawn
  /// @param position: The position of the rectangle
  /// @param color: The color of the rectangle
  public static void DrawNinePatch(NinePatch ninePatch, int width, int height, Vector2 position, Color color)
```