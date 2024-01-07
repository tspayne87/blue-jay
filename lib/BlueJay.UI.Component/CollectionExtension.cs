using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.UI.Component
{
  public static class CollectionExtension
  {
    /// <summary>
    /// Helper extension to find the index of the match expression
    /// </summary>
    /// <typeparam name="T">The type of item that the enumerable holds</typeparam>
    /// <param name="list">The enumerable we need to iterate over</param>
    /// <param name="match">The matcher callback to check and see if this is the index we want</param>
    /// <returns>Will return an index in the enumeration or -1 if none are found</returns>
    public static int FindIndex<T>(this IEnumerable<T> list, Func<T, bool> match)
    {
      var index = 0;
      foreach (var item in list)
      {
        if (match(item))
          return index;
        index++;
      }
      return -1;
    }
  }
}
