using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System
{
  public static class Extensions
  {
    /// <summary>
    /// Determines if the current long has the flags of the second number
    /// </summary>
    /// <param name="num">The current number we are looking at</param>
    /// <param name="num2">The checker for the flags</param>
    /// <returns>Will return true or false if the numbers are flags of each other</returns>
    public static bool HasFlag(this long num, long num2)
    {
      return (num & num2) > 0;
    }

    /// <summary>
    /// Will create a long number based on the flags given
    /// </summary>
    /// <typeparam name="TSource">The type of object the flags exist on</typeparam>
    /// <param name="list">The list of sources we need to determine the big flags for</param>
    /// <param name="selector">The selector we should use to find the flags</param>
    /// <returns>Will return a OR sum based on the flags given</returns>
    public static long SumOr<TSource>(this IEnumerable<TSource> list, Func<TSource, long> selector)
    {
      long result = 0L;
      foreach (var item in list) result = result | selector(item);
      return result;
    }

    /// <summary>
    /// Helper method to dispose of a list of disposables
    /// </summary>
    /// <typeparam name="T">The type of disposables</typeparam>
    /// <param name="source">The list of disposables</param>
    /// <returns>Will return the list of disposed items</returns>
    public static IEnumerable<T> Dispose<T>(this IEnumerable<T> source)
      where T: IDisposable
    {
      foreach (var item in source) item.Dispose();
      return source;
    }
  }
}
