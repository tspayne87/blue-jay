using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Nodes.Attributes;
using BlueJay.UI.Component.Nodes.Elements;
using BlueJay.UI.Component.Reactivity;
using System;
using System.Reflection;
using System.Xml.Linq;
using Attribute = BlueJay.UI.Component.Nodes.Attributes.Attribute;

namespace BlueJay.UI.Component.Nodes
{
  internal class ComponentNode : Node
  {
    private readonly Node? _root;

    private readonly List<IDisposable> _disposables;

    public ComponentNode(UIComponent uiComponent, Type type, List<Attribute> attributes, IServiceProvider provider)
      : base(type.Name, uiComponent, attributes, provider)
    {
      _root = provider.ParseJayTML(type, uiComponent) as Node;

      _disposables = new List<IDisposable>();
      BindPropAttributes(uiComponent, type, attributes);
    }

    public override void Initialize(UIComponent? parent = null)
    {
      if (_root != null)
      {
        _root.UIComponent.InitializeProviders(parent?.Providers);
        BindInjections();
        base.Initialize(_root.UIComponent);
      }
    }

    protected override List<UIElement>? AddEntity(Style style, UIElement? parent, Dictionary<string, object>? scope)
    {
      if (_root == null)
        throw new ArgumentNullException("Could not create root element");

      var nodes = _root.GenerateUI(style, parent, scope);

      foreach (var node in nodes)
      {
        if (node != null)
        {
          var sa = node.Entity.GetAddon<StyleAddon>();
          sa.Style.Merge(style);
          node.Entity.Update(sa);
        }
      }

      // Sets the entity component root entities
      _root.UIComponent.Root = nodes.Select(x => x.Entity)
        .ToList();
      _root.UIComponent.Mounted();
      return nodes;
    }

    public void BindInjections()
    {
      if (_root != null)
      {
        var members = _root.UIComponent.GetType().GetMembers()
          .Where(x => x.GetCustomAttribute<InjectAttribute>() != null);

        foreach (var member in members)
        {
          if (_root.UIComponent.Providers != null && _root.UIComponent.Providers.ContainsKey(member.Name))
          {
            var item = _root.UIComponent.Providers[member.Name];
            switch (member.MemberType)
            {
              case MemberTypes.Property:
                var prop = member as PropertyInfo;
                if (prop != null)
                  prop.SetValue(_root.UIComponent, item);
                break;
              case MemberTypes.Field:
                var field = member as FieldInfo;
                if (field != null)
                  field.SetValue(_root.UIComponent, item);
                break;
            }
          }
        }
      }
    }

    private void BindPropAttributes(UIComponent uiComponent, Type type, List<Attribute> attributes)
    {
      if (_root?.UIComponent != null)
      {
        foreach (var attr in attributes)
        {
          if (attr is StringAttribute strAttr)
          {
            var toField = type.GetField(strAttr.Name);
            if (toField != null && toField.IsInitOnly)
            {
              var prop = toField.GetCustomAttribute<PropAttribute>();
              if (prop != null && prop.Binding != PropBinding.None)
              {
                var fromField = uiComponent.GetType().GetField(strAttr.Value);
                if (fromField != null && fromField.IsInitOnly)
                {
                  var to = toField.GetValue(_root.UIComponent);
                  var from = fromField.GetValue(uiComponent);

                  switch (prop.Binding)
                  {
                    case PropBinding.TwoWay:
                      {
                        if (to is IReactiveProperty toReactive && from is IReactiveProperty fromReactive)
                        {
                          _disposables.Add(toReactive.Subscribe(x => fromField.FieldType.GetProperty(nameof(IReactiveProperty<object>.Value))?.SetValue(from, x.Data)));
                          _disposables.Add(fromReactive.Subscribe(x => toField.FieldType.GetProperty(nameof(IReactiveProperty<object>.Value))?.SetValue(to, x.Data)));
                        }
                      }
                      break;
                    case PropBinding.OneWay:
                      {
                        if (from is IReactiveProperty fromReactive)
                        {
                          _disposables.Add(fromReactive.Subscribe(x =>
                          {
                            if (to is IReactiveProperty)
                              toField.FieldType.GetProperty(nameof(IReactiveProperty<object>.Value))?.SetValue(to, x.Data);
                            else
                              toField.SetValue(to, x.Data);
                          }));
                        }
                        else
                        {
                          if (to is IReactiveProperty)
                            toField.FieldType.GetProperty(nameof(IReactiveProperty<object>.Value))?.SetValue(to, from);
                          else
                            toField.SetValue(to, from);
                        }
                      }
                      break;
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}
