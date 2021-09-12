using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// The internal utils that help with basic functions in the component architecture
  /// </summary>
  internal static class Utils
  {
    /// <summary>
    /// Helper regex is meant to get the index out of at string with brackets
    /// </summary>
    private static Regex ArrayIndexRegex = new Regex(@"^\[(\d+)\]$");

    /// <summary>
    /// The current global identifier
    /// </summary>
    private static int ScopeIdentifier = 0;

    /// <summary>
    /// Method is meant to generate an identifier for scoping, to make sure we can specify different instances on the reactive scope
    /// </summary>
    /// <returns>Will return the next identifier</returns>
    public static int GetNextIdentifier()
    {
      return ++ScopeIdentifier;
    }

    /// <summary>
    /// Helper method is meant to go down a path string and get the value based on the starting point
    /// </summary>
    /// <param name="val">The root value we need to go down the object tree to get the item we want</param>
    /// <param name="path">The current path we need to traverse down</param>
    /// <returns>Will return the object or null if not found</returns>
    public static object GetObject(object val, string path)
    {
      if (string.IsNullOrEmpty(path)) return val is IReactiveProperty ? ((IReactiveProperty)val)?.Value : val;

      var tree = path.Split('.');
      for (var i = 0; i < tree.Length && val != null; ++i)
      {
        if (val is IReactiveProperty) val = ((IReactiveProperty)val).Value;

        if (ArrayIndexRegex.IsMatch(tree[i]))
        {
          var arr = val as IList;
          var match = ArrayIndexRegex.Match(tree[i]);
          if (arr != null && match.Groups.Count == 2 && int.TryParse(match.Groups[1].Value, out var index))
          {
            if (index >= arr.Count) return null;
            if (i >= tree.Length - 1) return arr[index];
            val = arr[index];
            continue;
          }
          return null;
        }
        else if (val is IDictionary<string, object>)
        {
          var dict = val as IDictionary<string, object>;
          if (dict != null && dict.ContainsKey(tree[i]))
          {
            val = dict[tree[i]];
            continue;
          }
          return null;
        }
        else
        {
          var member = val.GetType().GetMember(tree[i]);
          if (member.Length == 0) return null;

          val = member[0] is FieldInfo ? ((FieldInfo)member[0]).GetValue(val) : ((PropertyInfo)member[0]).GetValue(val);
        }
      }
      return val is IReactiveProperty ? ((IReactiveProperty)val)?.Value : val;
    }

    /// <summary>
    /// Helper method is meant to get the reactive property much like <see cref="GetObject" />
    /// </summary>
    /// <param name="val">The current value we need to find the reactive property from</param>
    /// <param name="path">The current path we need to traverse down</param>
    /// <returns>Will return null or the reactive property found based on the path</returns>
    public static IReactiveProperty GetReactiveProperty(object val, string path)
    {
      if (string.IsNullOrEmpty(path)) return val as IReactiveProperty;

      var tree = path.Split('.');
      for (var i = 0; i < tree.Length && val != null; ++i)
      {
        if (val is IReactiveProperty) val = ((IReactiveProperty)val).Value;

        if (ArrayIndexRegex.IsMatch(tree[i]))
        {
          var arr = val as IList;
          var match = ArrayIndexRegex.Match(tree[i]);
          if (arr != null && match.Groups.Count == 2 && int.TryParse(match.Groups[1].Value, out var index))
          {
            if (index >= arr.Count) return null;
            if (i >= tree.Length - 1) return arr[index] as IReactiveProperty;
            val = arr[index];
            continue;
          }
          return null;
        }
        else if (val is IDictionary<string, object>)
        {
          var dict = val as IDictionary<string, object>;
          if (dict != null && dict.ContainsKey(tree[i]))
          {
            val = dict[tree[i]];
            continue;
          }
          return null;
        }
        else
        {
          var member = val.GetType().GetMember(tree[i]);
          if (member.Length == 0) return null;

          val = member[0] is FieldInfo ? ((FieldInfo)member[0]).GetValue(val) : ((PropertyInfo)member[0]).GetValue(val);
        }
      }
      return val as IReactiveProperty;
    }
  }
}
