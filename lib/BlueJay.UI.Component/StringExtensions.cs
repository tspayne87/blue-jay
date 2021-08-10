using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace BlueJay.UI.Component
{
  public static class StringExtensions
  {
    public static string Splice(this string str, int index, int length = 0, string insert = "")
    {
      return str.Substring(0, index) + insert + str.Substring(index + length);
    }

    public static string Splice(this string str, int index, int length = 0, params char[] inserts)
    {
      return str.Splice(index, length, new string(inserts));
    }

    public static string KebabToPascal(this string str)
    {
      return new Regex(@"(^\w|-\w)").Replace(str, x => x.Value.Replace("-", string.Empty));
    }
  }
}
