using System.ComponentModel;
using System.Reflection;
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

    /// <summary>
    /// Helper method meant to get the function types based on the arguments
    /// </summary>
    /// <param name="args">The argument types that this function will need to create for</param>
    /// <returns>Will return the type of callback function that matches the current arguments</returns>
    /// <exception cref="ArgumentOutOfRangeException">If number of argument do not exist in the amount of callback types</exception>
    public static Type ToFuncType(this List<Type> args)
    {
      if (FuncTypes.Count < args.Count)
        throw new ArgumentOutOfRangeException(nameof(args));
      return FuncTypes[args.Count - 1].MakeGenericType(args.ToArray());
    }

    /// <summary>
    /// Helper method meant to reflectively get a object based on its path
    /// </summary>
    /// <param name="value">The current object value</param>
    /// <param name="path">The current path of the object we need to go down too</param>
    /// <returns>Will return null if the object could not be found or the object based on the path</returns>
    public static object? GetValue(object? value, List<string> path)
    {
      foreach (var item in path)
      {
        if (value == null)
          return null;

        var members = value.GetType().GetMember(item);
        if (members.Length == 0)
          return null;

        foreach (var member in members)
        {
          switch (member.MemberType)
          {
            case MemberTypes.Field:
              var field = member as FieldInfo;
              if (field != null)
                value = field.GetValue(value);
              continue;
            case MemberTypes.Property:
              var prop = member as PropertyInfo;
              if (prop != null)
                value = prop.GetValue(value);
              continue;
          }
        }
      }
      return value;
    }
  }
}
