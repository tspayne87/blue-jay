using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Core
{
  public static class GraphicsDeviceExtensions
  {
    /// <summary>
    /// Method is meant to create a rectangle texture for rendering
    /// </summary>
    /// <param name="graphics">The graphics device we need to render with</param>
    /// <param name="width">The width of the rectangle</param>
    /// <param name="height">The height of the rectangle</param>
    /// <param name="color">The color that should be used if null is passed Color.Black is used</param>
    /// <returns>Will return a texture that was created</returns>
    public static Texture2D CreateRectangle(this GraphicsDevice graphics, int width, int height, Color? color = null)
    {
      color = color ?? Color.Black;
      var rectangle = new Texture2D(graphics, width, height);
      Color[] data = new Color[width * height];
      for (var i = 0; i < data.Length; ++i) data[i] = color.Value;
      rectangle.SetData(data);
      return rectangle;
    }
  }
}
