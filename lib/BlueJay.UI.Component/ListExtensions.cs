using BlueJay.UI.Component.Language;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.Component
{
  public static class ListExtensions
  {
    public static IReactiveProperty GetReactiveProperty(this List<LanguageScope> scopes, string field)
    {
      for (var i = scopes.Count - 1; i >= 0; --i)
      {
        if (scopes[i].Fields.ContainsKey(field))
          return scopes[i].Fields[field];
      }
      return null;
    }
  }
}
