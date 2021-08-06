using BlueJay.UI.Component.Language;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BlueJay.UI.Component
{
  public static class ObjectExtensions
  {
    /// <summary>
    /// Helper extension is meant to generate a scope based on the obj given
    /// </summary>
    /// <param name="obj">The obj we currently need to generate a scope with</param>
    /// <returns>Will return the generated scope</returns>
    public static LanguageScope GenerateScope(this object obj)
    {
      var props = obj.GetType().GetFields()
        .Where(x => typeof(IReactiveProperty).IsAssignableFrom(x.FieldType))
        .ToDictionary(x => x.Name, x => x.GetValue(obj) as IReactiveProperty);
      var functions = obj.GetType().GetMethods()
        .ToDictionary(x => x.Name, x => x);

      return new LanguageScope(obj, props, functions);
    }

    public static LanguageScope AsEventScope(this object obj)
    {
      var props = new Dictionary<string, IReactiveProperty>()
      {
        { "$event", new ReactiveProperty<object>(obj) }
      };

      return new LanguageScope(null, props, new Dictionary<string, MethodInfo>());
    }
  }
}
