using BlueJay.UI.Component.Nodes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Elements.Attributes
{
  /// <summary>
  /// Helper interface meant to set a object as a callable type
  /// </summary>
  internal interface ICallableType
  {
    /// <summary>
    /// If we need to call the parent scope of the specific element
    /// </summary>
    bool UseParentScope { get; }

    /// <summary>
    /// The callback function to get the value from the generated lambda function
    /// </summary>
    Func<UIComponent, object?, Dictionary<string, object>?, object?> Callback { get; }

    /// <summary>
    /// The callback function meant to get the list of reactive properties that exist when calling the callback function
    /// so we can subscribe to these and update the data based on the value changes
    /// </summary>
    public Func<UIComponent, object?, Dictionary<string, object>?, List<IReactiveProperty?>> ReactiveProperties { get; }
  }

  /// <summary>
  /// Extension methods to help call the callable type and get data from it
  /// </summary>
  internal static class ICallableTypeExtensions
  {
    /// <summary>
    /// Gets the value from a callback object
    /// </summary>
    /// <param name="callable">The callable object we want to get the value from</param>
    /// <param name="scope">The current node scope we are in</param>
    /// <param name="element">The UI Entity that is calling for the object</param>
    /// <param name="eventObj">The event obj that was triggered for the callback</param>
    /// <param name="elementScope">The current element scope we are in to pull element scope variables</param>
    /// <returns>Will return the object from the callback function</returns>
    public static object? GetValue(this ICallableType callable, NodeScope scope, UIEntity element, object? eventObj, Dictionary<string, object>? elementScope)
    {
      var component = callable.UseParentScope ?
        (element.ParentScopeKey == null ? null : scope[element.ParentScopeKey.Value]) :
        (element.ScopeKey == null ? null : scope[element.ScopeKey.Value]);

      if (component == null)
        return null;
      return callable.Callback(component, eventObj, elementScope);
    }

    /// <summary>
    /// Gets the list of reactive properties that should be watched from the callable type
    /// </summary>
    /// <param name="callable">The callable object we want to get the value from</param>
    /// <param name="scope">The current node scope we are in</param>
    /// <param name="element">The UI Entity that is calling for the object</param>
    /// <param name="eventObj">The event obj that was triggered for the callback</param>
    /// <param name="elementScope">The current element scope we are in to pull element scope variables</param>
    /// <returns>Will return a list of reactive properties we need to extract from the callback</returns>
    public static List<IReactiveProperty?> GetReactiveProperties(this ICallableType callable, NodeScope scope, UIEntity element, object? eventObj, Dictionary<string, object>? elementScope)
    {
      var component = callable.UseParentScope ?
        (element.ParentScopeKey == null ? null : scope[element.ParentScopeKey.Value]) :
        (element.ScopeKey == null ? null : scope[element.ScopeKey.Value]);

      if (component == null)
        return new List<IReactiveProperty?>();
      return callable.ReactiveProperties(component, eventObj, elementScope);
    }
  }
}
