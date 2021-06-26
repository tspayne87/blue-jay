using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace BlueJay.UI.Components
{
  public static class RegexExtensions
  {
    /// <summary>
    /// Translation method is meant to take a regex and string with a component and parse out the groups
    /// to replace them with the corresponding items in the component, only works with ReactiveProperties
    /// </summary>
    /// <param name="regex">The regex we are parsing</param>
    /// <param name="str">The string we need to translate</param>
    /// <param name="component">The component we are gathering props from</param>
    /// <returns>The calculated string we need to display to the screen</returns>
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
            return prop?.Value.ToString();
          }
        });
    }

    /// <summary>
    /// Method is meant to get all the reactive props based on the string and regex expression
    /// </summary>
    /// <param name="regex">The regex to look for props on the component</param>
    /// <param name="str">The string to sift through</param>
    /// <param name="component">The component we are gathering props from</param>
    /// <returns>A list of properties gathered from the component</returns>
    public static IEnumerable<IReactiveProperty> GetReactiveProps(this Regex regex, string str, object component)
    {
      var matches = regex.Matches(str);
      foreach(Match match in matches)
      {
        var field = component.GetType().GetField(match.Groups[1].Value);
        if (field != null)
        {
          var prop = field.GetValue(component) as IReactiveProperty;
          if (prop != null)
            yield return prop;
        }
      }
    }
  }
}
