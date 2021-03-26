using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public static List<Texture2D> SplitTexture(this Texture2D texture, int frames)
    {
      var textures = new List<Texture2D>();
      for (var i = 0; i < frames; ++i)
      {
        var newTexture = new Texture2D(texture.GraphicsDevice, texture.Width / frames, texture.Height);
        var rect = new Rectangle(i * (texture.Width / frames), 0, newTexture.Width, newTexture.Height);

        var colors = new Color[newTexture.Width * newTexture.Height];
        texture.GetData(0, rect, colors, 0, colors.Length);
        newTexture.SetData(colors);

        textures.Add(newTexture);
      }
      return textures;
    }
  }
}
