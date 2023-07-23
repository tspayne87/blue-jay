using BlueJay.Events.Interfaces;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Component.Reactivity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlueJay.UI.Component.Nodes
{
  /// <summary>
  /// The node root where this component starts everything and where a new component should be generated,
  /// the reason why the component is generated here is because if we have a component that exists say in
  /// a for loop we want to have different components for each of the instances of that for loop so they do not
  /// share the same context
  /// </summary>
  internal class NodeRoot : Node
  {
    /// <summary>
    /// Constructor for the node root
    /// </summary>
    /// <param name="scope">The current scope this node is in</param>
    public NodeRoot(NodeScope scope)
      : base(scope, new List<UIElementAttribute>()) { }

    /// <inheritdoc />
    protected override List<UIEntity>? AddEntity(Style style, UIEntity? parent, Dictionary<string, object>? scope)
    {
      var scopeKey = Scope.GenerateComponent(parent?.ScopeKey);
      if (parent != null && parent.ScopeKey != null)
        BindProps(parent.ScopeKey.Value, scopeKey, scope);

      var eventQueue = Scope.ServiceProvider.GetRequiredService<IEventQueue>();
      eventQueue.Timeout(() => {
        if (Scope.ContainsKey(scopeKey))
          Scope[scopeKey].Mounted();
      });

      var entityScope = new UIEntityScope(parent?.EntityScope);
      entityScope.AttachProviders(Scope[scopeKey]);
      entityScope.BindInjections(Scope[scopeKey]);

      return new List<UIEntity>() { new UIEntity(this, scopeKey, entityScope) };
    }

    /// <inheritdoc />
    protected override void RemoveElement(UIEntity element)
    {
      base.RemoveElement(element);
      RemoveScopeKey(element);
    }

    /// <summary>
    /// Helper method meant to remove a scope key and all its children scope keys
    /// </summary>
    /// <param name="element">The element we want to remove the scope key from as well as the children of the element</param>
    private void RemoveScopeKey(UIEntity element)
    {
      foreach (var child in element.Children)
        RemoveScopeKey(child);

      if (element.ScopeKey != null && !element.IsParentUsingScopeKey(element.ScopeKey.Value))
        Scope.RemoveScopeKey(element.ScopeKey.Value);
    }

    /// <summary>
    /// Helper method is meant to bind its props to the newly created component so that all
    /// the properties are bound based on the current/new scopes and interactivity between components
    /// can be established
    /// </summary>
    /// <param name="currentScope">The current node scope we want to load elements from</param>
    /// <param name="newScope">The new scope we want to load elements into</param>
    /// <param name="scope">The node element scope that has variables that have been injected into them like the for attribute</param>
    /// <returns>
    /// Will return a list of disposables where we are subscribing to various reactive props and need
    /// to remove them when this entity is destroyed
    /// </returns>
    private IEnumerable<IDisposable> BindProps(Guid currentScope, Guid newScope, Dictionary<string, object>? scope)
    {
      var fields = Scope.ComponentType.GetFields()
        .Select(x => new { Field = x, Prop = x.GetCustomAttribute<PropAttribute>() })
        .Where(x => x.Prop != null);

      var prev = Scope[currentScope];
      var curr = Scope[newScope];

      var disposables = new List<IDisposable>();
      foreach (var item in fields)
      {
        var attr = GetExpressionAttribute(item.Field.Name, prev);
        if (attr != null && item.Prop != null)
        {
          var value = attr.Callback(prev, null, scope);
          switch (item.Prop.Binding)
          {
            case PropBinding.None:
              SetValue(item.Field, curr, value);
              break;
            case PropBinding.OneWay:
              {
                SetValue(item.Field, curr, value);
                foreach (var reactive in attr.ReactiveProperties(prev, null, scope))
                {
                  if (reactive == null)
                    continue;
                  disposables.Add(reactive.Subscribe(x => SetValue(item.Field, curr, attr.Callback(prev, null, scope))));
                }
              }
              break;
            case PropBinding.TwoWay:
              {
                SetValue(item.Field, curr, value);

                var reactives = attr.ReactiveProperties(prev, null, scope);
                var valueTo = item.Field.GetValue(curr);
                if (reactives.Count == 1 && valueTo is IReactiveProperty reactiveTo)
                {
                  var reactiveFrom = reactives[0];
                  if (reactiveFrom != null)
                  {
                    disposables.Add(reactiveFrom.Subscribe(x => SetReactiveValue(reactiveTo, x.Data)));
                    disposables.Add(reactiveTo.Subscribe(x => SetReactiveValue(reactiveFrom, x.Data)));
                  }
                }
                else
                {
                  foreach (var reactive in reactives)
                  {
                    if (reactive == null)
                      continue;

                    disposables.Add(reactive.Subscribe(x => SetValue(item.Field, curr, attr.Callback(prev, null, scope))));
                  }
                }
              }
              break;
          }
        }
      }
      return disposables;
    }

    /// <summary>
    /// Gets the expression attribute based on teh component name
    /// </summary>
    /// <param name="name">The name of the expression we need to lookup</param>
    /// <param name="component">The UIComponent to load data from</param>
    /// <returns>Will return an expression attribute to extract data out of</returns>
    private ExpressionAttribute? GetExpressionAttribute(string name, UIComponent? component)
    {
      var attr = Attributes.FirstOrDefault(x => x.Name == name);
      if (attr is ExpressionAttribute expAttr)
        return expAttr;

      if (attr is StringAttribute strAttr)
        return new ExpressionAttribute(name, (c, e, s) => new Text(strAttr.Value));

      return null;
    }

    /// <summary>
    /// Helper method is meant to set a value if it is a reactive property or not
    /// </summary>
    /// <param name="field">The field being set</param>
    /// <param name="component">The object that the field exists on</param>
    /// <param name="value">The value to set to the field that the component is on</param>
    private void SetValue(FieldInfo field, UIComponent component, object? value)
    {
      if (typeof(IReactiveProperty).IsAssignableFrom(field.FieldType))
      {
        SetReactiveValue(field.GetValue(component), value);
      }
      else
      {
        field.SetValue(component, value);
      }
    }

    /// <summary>
    /// Set a reactive value based on the two objects
    /// </summary>
    /// <param name="obj">The reactive object we want to set</param>
    /// <param name="value">The value we want to set to the reactive value</param>
    private void SetReactiveValue(object? obj, object? value)
    {
      obj?.GetType().GetProperty("Value")?.SetValue(obj, value);
    }
  }
}
