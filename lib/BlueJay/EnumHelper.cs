using System;
using System.Collections.Generic;

namespace BlueJay
{
  /// <summary>
  /// Helper class meant to build out default dictionaries for the systems so that they can trigger events
  /// on button presses
  /// </summary>
  internal static class EnumHelper
  {
    /// <summary>
    /// Method is meant to build out a dictionary to track button presses for the user and generate
    /// events based on those presses
    /// </summary>
    /// <typeparam name="T">The enumeration type that should be used for each button press</typeparam>
    /// <typeparam name="V">The default value each of the elements should start with</typeparam>
    /// <param name="defaultValue">The value that each value should start with</param>
    /// <returns>Will return the primed dictionary to track states</returns>
    public static Dictionary<T, V> GenerateEnumDictionary<T, V>(V defaultValue)
    {
      var result = new Dictionary<T, V>();
      foreach (var key in Enum.GetValues(typeof(T)))
      {
        result.Add((T)key, defaultValue);
      }
      return result;
    }
  }
}
