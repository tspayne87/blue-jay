using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BlueJay.UI
{
  public static class SpriteFontExtensions
  {
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
