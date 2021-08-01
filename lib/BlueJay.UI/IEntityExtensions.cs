using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI
{
  public static class IEntityExtensions
  {
    public static bool TryGetStyle<T>(this IEntity entity, Func<Style, T> expression, out T output)
    {
      output = entity.GetStyle(expression);
      return output != null;
    }

    public static T GetStyle<T>(this IEntity entity, Func<Style, T> expression)
    {
      while (entity != null)
      {
        var sa = entity.GetAddon<StyleAddon>();
        var la = entity.GetAddon<LineageAddon>();

        var style = expression(sa.CurrentStyle);
        if (style != null)
          return style;

        entity = la.Parent;
      }
      return default;
    }

    public static string FitString(this IEntity entity, string str, int width, FontCollection fonts)
    {
      if (entity == null) return string.Empty;

      if (entity.TryGetStyle(x => x.Font, out var font))
        return fonts.SpriteFonts[font].FitString(str, width);
      if (entity.TryGetStyle(x => x.TextureFont, out var textureFont))
        return fonts.TextureFonts[textureFont].FitString(str, width, entity.GetStyle(x => x.TextureFontSize) ?? 1);
      return str;
    }

    public static Vector2 MeasureString(this IEntity entity, string str, FontCollection fonts)
    {
      if (entity == null) return Vector2.Zero;

      if (entity.TryGetStyle(x => x.Font, out var font))
        return fonts.SpriteFonts[font].MeasureString(str);
      if (entity.TryGetStyle(x => x.TextureFont, out var textureFont))
        return fonts.TextureFonts[textureFont].MeasureString(str, entity.GetStyle(x => x.TextureFontSize) ?? 1);
      return Vector2.Zero;
    }
  }
}
