using System.Text.RegularExpressions;
using BlueJay.Core.Container;
using Microsoft.Xna.Framework;

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
    private readonly ITexture2DContainer _container;

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
    public int Width => _container.Width / _cols;

    /// <summary>
    /// The current height of the font face
    /// </summary>
    public int Height => _container.Height / _rows;

    /// <summary>
    /// The texture for the font face
    /// </summary>
    public ITexture2DContainer Texture => _container;

    /// <summary>
    /// Constructor of the sprite font to build out a way to render a sprite as a font
    /// </summary>
    /// <param name="texture">The texture that represents the font</param>
    /// <param name="rows">The rows that exist in the texture</param>
    /// <param name="cols">The columns that exist in the texture</param>
    /// <param name="alphabet">The alphabet that exists in the texture</param>
    public TextureFont(ITexture2DContainer container, int rows, int cols, string alphabet = "abcdefghijklmnopqrstuvwxyz1234567890!@#$%^&*()/.,';\\][=-?><\":|}{+_`")
    {
      _container = container;
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
      var index = _alphabet.IndexOf(letter.ToString(), StringComparison.OrdinalIgnoreCase);
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

    /// <summary>
    /// Method is meant to calculate a string based on the width of the space
    /// </summary>
    /// <param name="str">The string we want to fit</param>
    /// <param name="width">The width we want to fit into</param>
    /// <param name="size">The size of the font</param>
    /// <returns>Will return a string that fits into the width of its space</returns>
    public string FitString(string str, int width, int size)
    {
      var lines = new List<string>();
      var result = string.Empty;
      var matches = new Regex(@"([^\s]+)(\s*)").Matches(str);
      foreach (Match match in matches)
      {
        if (MeasureString(result + match.Groups[1].Value, size).X > width)
        {
          if (!string.IsNullOrWhiteSpace(result))
            lines.Add(result.Trim());
          result = match.Groups[0].Value;
        }
        else
        {
          result += match.Groups[0].Value;
        }

        if (!string.IsNullOrWhiteSpace(match.Groups[2].Value))
        {
          for (var i = 0; i < match.Groups[2].Value.Length; ++i)
          {
            if (MeasureString(result + match.Groups[2].Value[i], size).X > width)
            {
              if (!string.IsNullOrWhiteSpace(result))
                lines.Add(result);
              result = match.Groups[2].Value[i].ToString();
            }
            else
            {
              result += match.Groups[2].Value[i];
            }
          }
        }
      }
      if (!string.IsNullOrWhiteSpace(result))
        lines.Add(result);
      return string.Join("\n", lines);
    }
  }
}
