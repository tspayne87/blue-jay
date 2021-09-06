using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace BlueJay.UI.Component
{
  internal static class Utils
  {
    private static Regex ArrayIndexRegex = new Regex(@"^\[(\d+)\]$");
    private static int ScopeIdentifier = 0;

    public static int GetNextIdentifier()
    {
      return ++ScopeIdentifier;
    }

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
