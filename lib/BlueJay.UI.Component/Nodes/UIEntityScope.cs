using BlueJay.UI.Component.Attributes;
using System.Reflection;

namespace BlueJay.UI.Component.Nodes
{
  /// <summary>
  /// Implementation of a <see cref="ParentedDictionary{TKey, TValue}" /> that represents an entity scope to deal
  /// with providing and injecting data between components based on the structure of the nodes themselves and
  /// not the structure of the components
  /// </summary>
  internal class UIEntityScope : ParentedDictionary<string, object?>
  {
    /// <inheritdoc />
    public UIEntityScope(IDictionary<string, object?>? parent = null)
      : base(parent) { }

    /// <summary>
    /// Helper method meant to get all the provided members and attach them to the provider list of the scope
    /// </summary>
    public void AttachProviders(UIComponent component)
    {
      var members = component.GetType().GetMembers()
        .Where(x => x.GetCustomAttribute<ProvideAttribute>() != null);
      foreach (var member in members)
      {
        switch (member.MemberType)
        {
          case MemberTypes.Field:
            var field = member as FieldInfo;
            if (field != null)
              this[field.Name] = field.GetValue(component);
            break;
          case MemberTypes.Property:
            var prop = member as PropertyInfo;
            if (prop != null)
              this[prop.Name] = prop.GetValue(component);
            break;
          case MemberTypes.Method:
            var method = member as MethodInfo;
            if (method != null)
            {
              var parameters = method.GetParameters().Select(x => x.ParameterType).ToList();
              parameters.Add(method.ReturnType);

              var funcType = parameters.ToFuncType();
              this[method.Name] = Delegate.CreateDelegate(funcType, component, method);
            }
            break;
        }
      }
    }

    /// <summary>
    /// Helper method meant to bind injected attributes in the class to the provided
    /// object on the current scope
    /// </summary>
    public void BindInjections(UIComponent component)
    {
      var members = component.GetType().GetMembers()
        .Where(x => x.GetCustomAttribute<InjectAttribute>() != null);

      foreach (var member in members)
      {
        if (ContainsKey(member.Name))
        {
          var item = this[member.Name];
          switch (member.MemberType)
          {
            case MemberTypes.Property:
              var prop = member as PropertyInfo;
              if (prop != null)
                prop.SetValue(component, item);
              break;
            case MemberTypes.Field:
              var field = member as FieldInfo;
              if (field != null)
                field.SetValue(component, item);
              break;
          }
        }
      }
    }
  }
}
