using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;

namespace BlueJay.UI
{
  /// <summary>
  /// Helper methods dealing with the entity object
  /// </summary>
  public static class IEntityExtensions
  {
    /// <summary>
    /// Will return a style from the entity base don the expression given
    /// </summary>
    /// <typeparam name="T">The current type of style that will be returned</typeparam>
    /// <param name="entity">The entity we want to load the style from</param>
    /// <param name="expression">Will return an expression to determine what style we are wanting to find</param>
    /// <param name="output">The output of the style found</param>
    /// <returns></returns>
    public static bool TryGetStyle<T>(this IEntity entity, Func<Style, T> expression, out T? output)
    {
      output = entity.GetStyle(expression);
      return output != null;
    }

    /// <summary>
    /// The current style we need to find on the entity
    /// </summary>
    /// <typeparam name="T">The type of object found on the style</typeparam>
    /// <param name="entity">The entity we are loading the style from</param>
    /// <param name="expression">The expression to use when searching for the property on the style</param>
    /// <returns>Will return the data from the style</returns>
    public static T? GetStyle<T>(this IEntity entity, Func<Style, T> expression)
    {
      IEntity? e = entity;
      while (e != null && e.Contains<StyleAddon, LineageAddon>())
      {
        var sa = e.GetAddon<StyleAddon>();
        var la = e.GetAddon<LineageAddon>();

        var style = expression(sa.CurrentStyle);
        if (style != null)
          return style;
        e = la.Parent;
      }
      return default;
    }

    /// <summary>
    /// Will fit a string based on the width
    /// </summary>
    /// <param name="entity">The entity that we need to fit the string into</param>
    /// <param name="str">The string we are altering to fit in the entity space</param>
    /// <param name="width">The width we are working with when fitting the string</param>
    /// <param name="fonts">The font collection we should use when figuring out how to measure the string</param>
    /// <returns>Will return a string with the proper spacing to fit in the width</returns>
    public static string FitString(this IEntity entity, string str, int width, IFontCollection fonts)
    {
      if (entity == null) return string.Empty;

      if (entity.TryGetStyle(x => x.Font, out var font) && font != null)
        return fonts.SpriteFonts[font].FitString(str, width);
      if (entity.TryGetStyle(x => x.TextureFont, out var textureFont) && textureFont != null)
        return fonts.TextureFonts[textureFont].FitString(str, width, entity.GetStyle(x => x.TextureFontSize) ?? 1);
      return str;
    }

    /// <summary>
    /// Measures the string based on the entity
    /// </summary>
    /// <param name="entity">The entity we want to measure the string on</param>
    /// <param name="str">The string we are measuring</param>
    /// <param name="fonts">The fonts we want to measure with</param>
    /// <returns>Will return the measurements for the string</returns>
    public static Vector2 MeasureString(this IEntity entity, string str, IFontCollection fonts)
    {
      if (entity == null) return Vector2.Zero;

      if (entity.TryGetStyle(x => x.Font, out var font) && font != null)
        return fonts.SpriteFonts[font].MeasureString(str);
      if (entity.TryGetStyle(x => x.TextureFont, out var textureFont) && textureFont != null)
        return fonts.TextureFonts[textureFont].MeasureString(str, entity.GetStyle(x => x.TextureFontSize) ?? 1);
      return Vector2.Zero;
    }
  }
}
