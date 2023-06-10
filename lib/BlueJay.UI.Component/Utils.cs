using System.Text.RegularExpressions;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// The internal utils that help with basic functions in the component architecture
  /// </summary>
  internal static class Utils
  {
    /// <summary>
    /// The current global identifier
    /// </summary>
    private static int ScopeIdentifier = 0;

    /// <summary>
    /// Set of all the type of functions that could be made when generating the delagate type
    /// </summary>
    private static List<Type> FuncTypes = new List<Type>()
    {
      typeof(Func<>),
      typeof(Func<,>),
      typeof(Func<,,>),
      typeof(Func<,,,,>),
      typeof(Func<,,,,,>),
      typeof(Func<,,,,,,>),
      typeof(Func<,,,,,,,>),
      typeof(Func<,,,,,,,,>),
      typeof(Func<,,,,,,,,,>),
      typeof(Func<,,,,,,,,,,>),
      typeof(Func<,,,,,,,,,,,>),
      typeof(Func<,,,,,,,,,,,,>),
      typeof(Func<,,,,,,,,,,,,,>),
      typeof(Func<,,,,,,,,,,,,,>),
      typeof(Func<,,,,,,,,,,,,,,>),
      typeof(Func<,,,,,,,,,,,,,,,>),
      typeof(Func<,,,,,,,,,,,,,,,,>),
    };

    /// <summary>
    /// Method is meant to generate an identifier for scoping, to make sure we can specify different instances on the reactive scope
    /// </summary>
    /// <returns>Will return the next identifier</returns>
    public static int GetNextIdentifier()
    {
      return ++ScopeIdentifier;
    }

    public static Type ToFuncType(this List<Type> types)
    {
      if (FuncTypes.Count < types.Count)
        throw new ArgumentOutOfRangeException(nameof(types));
      return FuncTypes[types.Count - 1].MakeGenericType(types.ToArray());
    }
  }
}
