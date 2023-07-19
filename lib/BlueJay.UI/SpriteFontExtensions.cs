using Microsoft.Xna.Framework.Graphics;
using System.Text.RegularExpressions;

namespace BlueJay.UI
{
  /// <summary>
  /// Extension methods to help with dealing with sprite fonts
  /// </summary>
  public static class SpriteFontExtensions
  {
    /// <summary>
    /// Will fit a string into a sprite font
    /// </summary>
    /// <param name="font">The sprite font we need to use to measure the string</param>
    /// <param name="str">The current string we want to fit in the width space</param>
    /// <param name="width">The width space we are fitting the string into</param>
    /// <returns>Will return the fitted string for the space it is in</returns>
    public static string FitString(this SpriteFont font, string str, int width)
    {
      var lines = new List<string>();
      var result = string.Empty;
      var matches = new Regex(@"([^\s]+)(\s*)").Matches(str);
      foreach(Match match in matches)
      {
        if (font.MeasureString(result + match.Groups[1].Value).X > width)
        {
          if (!string.IsNullOrEmpty(result))
            lines.Add(result);
          result = match.Groups[1].Value;
        }
        else
        {
          result += match.Groups[1].Value;
        }

        if (!string.IsNullOrEmpty(match.Groups[2].Value))
        {
          for (var i = 0; i < match.Groups[2].Value.Length; ++i)
          {
            if (font.MeasureString(result + match.Groups[2].Value[i]).X > width)
            {
              if (!string.IsNullOrEmpty(result))
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
      if (result.Length > 0)
        lines.Add(result);
      return string.Join("\n", lines);
    }
  }
}
