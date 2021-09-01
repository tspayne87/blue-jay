using BlueJay.UI.Component.Reactivity;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BlueJay.UI.Component
{
  public static class Utils
  {
    private static Regex ArrayIndexRegex = new Regex(@"^\[(\d+)\]$");

    public static object GetObject(IReactiveProperty val, string path)
    {
      if (string.IsNullOrEmpty(path)) return val?.Value;

      var tree = path.Split('.');
      for (var i = 0; i < tree.Length && val != null; ++i)
      {
        if (ArrayIndexRegex.IsMatch(tree[i]))
        {
          var arr = val as IList;
          var match = ArrayIndexRegex.Match(tree[i]);
          if (arr != null && match.Groups.Count == 2 && int.TryParse(match.Groups[1].Value, out var index))
          {
            if (index >= arr.Count) return null;
            if (i >= tree.Length - 1) return arr[index];
            val = arr[index] as IReactiveProperty;
            continue;
          }
          return null;
        }
        else
        {
          var field = val.Value.GetType().GetField(tree[i]);
          val = field.GetValue(val.Value) as IReactiveProperty;
        }
      }
      return val?.Value;
    }
  }
}
