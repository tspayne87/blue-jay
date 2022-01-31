using BlueJay.UI.Component.Reactivity;
using System;

namespace BlueJay.UI.Component
{
  internal static class TypeExtensions
  {
    /// <summary>
    /// Method is meant to determine if the name is a property of the type
    /// </summary>
    /// <param name="type">The current type that we want to check</param>
    /// <param name="name">The name we are looking for</param>
    /// <returns>Will return true if it is the property</returns>
    public static bool IsPropertyOf(this Type type, string name)
    {
      return
        type.GetMethod(name) != null
        || type.GetProperty(name) != null
        || type.GetField(name) != null;
    }
  }
}
