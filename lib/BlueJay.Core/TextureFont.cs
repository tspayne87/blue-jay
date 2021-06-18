using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Core
{
  /// <summary>
  /// A texture representation of a font that renders areas of the texture for the font itself
  /// </summary>
  public class TextureFont
  {
    /// <summary>
    /// The texture of the font face
    /// </summary>
    private readonly Texture2D _texture;

    /// <summary>
    /// The rows that exist in the texture for the font face
    /// </summary>
    private readonly int _rows;

    /// <summary>
    /// The cols that exist in the texture for the font face
    /// </summary>
    private readonly int _cols;

    /// <summary>
    /// The alphabet of the font face
    /// </summary>
    private readonly string _alphabet;

    /// <summary>
    /// The current width of the font face
    /// </summary>
    public int Width => _texture.Width / _cols;

    /// <summary>
    /// The current height of the font face
    /// </summary>
    public int Height => _texture.Height / _rows;

    /// <summary>
    /// The texture for the font face
    /// </summary>
    public Texture2D Texture => _texture;

    /// <summary>
    /// Constructor of the sprite font to build out a way to render a sprite as a font
    /// </summary>
    /// <param name="texture">The texture that represents the font</param>
    /// <param name="rows">The rows that exist in the texture</param>
    /// <param name="cols">The columns that exist in the texture</param>
    /// <param name="alphabet">The alphabet that exists in the texture</param>
    public TextureFont(Texture2D texture, int rows, int cols, string alphabet = "abcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()/.,’;\\][=-?><“:|}{+_`")
    {
      _texture = texture;
      _rows = rows;
      _cols = cols;
      _alphabet = alphabet;
    }

    /// <summary>
    /// Method is meant to get the bounds of the texture for rendering the letter in the texture
    /// </summary>
    /// <param name="letter">The letter we are rendering</param>
    /// <returns>The bounds of the letter found, empty if letter was not found</returns>
    public Rectangle GetBounds(char letter)
    {
      var index = _alphabet.IndexOf(letter, StringComparison.OrdinalIgnoreCase);
      if (index == -1) return Rectangle.Empty;
      return new Rectangle((index % _cols) * Width, (index / _cols) * Height, Width, Height);
    }

    /// <summary>
    /// Measures how big the string will be when it is rendered to the screen
    /// </summary>
    /// <param name="str">The string to measure</param>
    /// <param name="size">The size of the font being renderered</param>
    /// <returns>Will return the width and hight of the string</returns>
    public Vector2 MeasureString(string str, int size = 1)
    {
      var lines = str.Split('\n');
      var longestString = lines.Select(x => x.Length).Max();
      return new Vector2(longestString * (Width * size), lines.Count() * (Height * size));
    }
  }
}
