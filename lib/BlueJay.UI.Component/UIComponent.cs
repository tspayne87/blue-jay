using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Nodes;
using System.Reflection;

namespace BlueJay.UI.Component
{
  /// <summary>
  /// The abstract UI component meant to be a base for all UI components in the system
  /// </summary>
  public abstract class UIComponent
  {
    /// <summary>
    /// The root entity that was created for this UI component
    /// </summary>
    public List<IEntity>? Root { get; set; }

    /// <summary>
    /// The identifier that exists on the scope
    /// </summary>
    internal string Identifier { get; private set; } = $"CI_{Utils.GetNextIdentifier()}";

    /// <summary>
    /// The current providers for this component
    /// </summary>
    internal ParentDictionary? Providers { get; private set; }

    /// <summary>
    /// Helper method is called when the component is mounted to the UI tree
    /// </summary>
    public virtual void Mounted() { }

    internal void InitializeProviders(ParentDictionary? providers = null)
    {
      if (Providers != null)
        return;

      var members = GetType().GetMembers()
        .Where(x => x.GetCustomAttribute<ProvideAttribute>() != null);

      var result = new ParentDictionary(providers);
      foreach (var member in members)
      {
        switch (member.MemberType)
        {
          case MemberTypes.Field:
            var field = member as FieldInfo;
            if (field != null)
              result[field.Name] = field.GetValue(this);
            break;
          case MemberTypes.Property:
            var prop = member as PropertyInfo;
            if (prop != null)
              result[prop.Name] = prop.GetValue(this);
            break;
          case MemberTypes.Method:
            var method = member as MethodInfo;
            if (method != null) {
              var parameters = method.GetParameters().Select(x => x.ParameterType).ToList();
              parameters.Add(method.ReturnType);

              var funcType = parameters.ToFuncType();
              result[method.Name] = Delegate.CreateDelegate(funcType, this, method);
            }
            break;
        }
      }
      Providers = result;
    }
  }
}
