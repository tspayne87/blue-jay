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
    /// Helper method is meant to translate an xml attribute to a style
    /// </summary>
    /// <param name="attr">The attribute we are processing</param>
    /// <param name="contentManager">The content manager we need to open texture for style attributes</param>
    /// <returns>The generate style from the xml attribute</returns>
    public static Style GenerateStyle(this XmlAttribute attr, ContentManager contentManager)
    {
      var style = new Style();

      var styles = attr.InnerText.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
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
              // TODO: Handle Color
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
