using BlueJay.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Linq;
using System.Xml;

namespace BlueJay.UI.Component
{
  public static class XmlAttributeExtensions
  {
    /// <summary>
    /// Helper method is meant to translate a string to a style
    /// </summary>
    /// <param name="text">The text we are processing</param>
    /// <param name="contentManager">The content manager we need to open texture for style attributes</param>
    /// <returns>The generate style from the xml attribute</returns>
    public static Style GenerateStyle(this string text, ContentManager contentManager)
    {
      var style = new Style();

      var styles = text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries).Select(y => y.Trim()).ToArray());
      var props = typeof(Style).GetProperties();

      foreach (var item in styles)
      {
        if (item.Length == 2)
        {
          var prop = props.FirstOrDefault(x => x.Name.Equals(item[0], StringComparison.OrdinalIgnoreCase));
          if (prop != null)
          {
            if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
            {
              if (int.TryParse(item[1], out var integer))
                prop.SetValue(style, integer);
            }
            else if (prop.PropertyType == typeof(float) || prop.PropertyType == typeof(float?))
            {
              if (float.TryParse(item[1], out var floating))
                prop.SetValue(style, floating);
            }
            else if (prop.PropertyType == typeof(string))
            {
              prop.SetValue(style, item[1]);
            }
            else if (prop.PropertyType == typeof(Color) || prop.PropertyType == typeof(Color?))
            {
              var rgba = item[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(y => y.Trim()).ToArray();
              if (rgba.Length == 3 && int.TryParse(rgba[0], out var r0) && int.TryParse(rgba[1], out var g0) && int.TryParse(rgba[2], out var b0))
                prop.SetValue(style, new Color(r0, g0, b0));
              else if (rgba.Length == 4 && int.TryParse(rgba[0], out var r1) && int.TryParse(rgba[1], out var g1) && int.TryParse(rgba[2], out var b1) && int.TryParse(rgba[3], out var a1))
                prop.SetValue(style, new Color(r1, g1, b1, a1));
            }
            else if (prop.PropertyType == typeof(Point) || prop.PropertyType == typeof(Point?))
            {
              var xy = item[1].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(y => y.Trim()).ToArray();
              if (xy.Length == 1 && int.TryParse(xy[0], out var xVal))
                prop.SetValue(style, new Point(xVal));
              else if (xy.Length == 2 && int.TryParse(xy[0], out var x) && int.TryParse(xy[1], out var y))
                prop.SetValue(style, new Point(x, y));
            }
            else if (prop.PropertyType == typeof(NinePatch))
            {
              prop.SetValue(style, new NinePatch(contentManager.Load<Texture2D>(item[1])));
            }
            else if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>) && prop.PropertyType.GetGenericArguments()[0].IsEnum)
            {
              prop.SetValue(style, Enum.Parse(prop.PropertyType.GetGenericArguments()[0], item[1], true));
            }
          }
        }
      }

      return style;
    }
  }
}
