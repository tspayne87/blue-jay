using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BlueJay.UI.Components
{
  public static class RegexExtensions
  {
    public static string TranslateText(this Regex regex, string str, object component)
    {
      return regex
        .Replace(str, m =>
        {
          var field = component.GetType().GetField(m.Groups[1].Value);
          if (field == null) return m.Value;
          else
          {
            var prop = field.GetValue(component) as IReactiveProperty;
            return prop.Value.ToString();
          }
        });
    }

    public static IEnumerable<IReactiveProperty> GetReactiveProps(this Regex regex, string str, object component)
    {
      var matches = regex.Matches(str);
      foreach(Match match in matches)
      {
        var field = component.GetType().GetField(match.Groups[1].Value);
        if (field != null)
        {
          yield return field.GetValue(component) as IReactiveProperty;
        }
      }
    }
  }
}
