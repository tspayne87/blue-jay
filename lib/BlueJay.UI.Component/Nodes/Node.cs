using BlueJay.Common.Events.Keyboard;
using BlueJay.Common.Events.Mouse;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Nodes.Attributes;
using BlueJay.UI.Component.Nodes.Elements;
using BlueJay.UI.Component.Reactivity;
using BlueJay.UI.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using static BlueJay.Common.Events.Mouse.MouseEvent;
using Attribute = BlueJay.UI.Component.Nodes.Attributes.Attribute;

namespace BlueJay.UI.Component.Nodes
{
  internal abstract class Node : INode
  {
    private readonly GraphicsDevice _graphics;
    private readonly IEventQueue _eventQueue;

    protected readonly IServiceProvider _provider;

    private Node? _parent;

    public readonly List<Attribute> Attributes;

    public ParentDictionary? Providers;

    public List<Node> Children { get; private set; }

    public IEnumerable<EventAttribute> EventAttributes => Attributes.Where(x => x is EventAttribute).Cast<EventAttribute>();

    public Node? Parent
    {
      get => _parent;
      set
      {
        if (_parent != null)
          _parent.Children.Remove(this);

        _parent = value;
        if (_parent != null)
          _parent.Children.Add(this);
      }
    }

    public string Name { get; }
    public UIComponent UIComponent { get; }
    public IEntity? SlotEntity { get; protected set; }

    public Node(string name, UIComponent uiComponent, List<Attribute> attributes, IServiceProvider provider)
    {
      Name = name;
      UIComponent = uiComponent;
      Attributes = attributes;
      Providers = null;
      Children = new List<Node>();

      _provider = provider;

      _graphics = provider.GetRequiredService<GraphicsDevice>();
      _eventQueue = provider.GetRequiredService<IEventQueue>();
    }

    public virtual void Initialize(UIComponent? parent = null)
    {
      UIComponent.InitializeProviders(parent?.Providers);

      foreach (var child in Children)
        child.Initialize(parent ?? UIComponent);
    }

    /// <inheritdoc />
    public void GenerateUI(Style? style = null)
    {
      style = style ?? new Style();
      var root = GenerateUI(style, null, null)
        .ToList();

      // Sets the entity component root entities
      UIComponent.Root = root.Select(x => x.Entity)
        .ToList();
      UIComponent.Mounted();
    }

    protected abstract List<UIElement>? AddEntity(Style style, UIElement? parent, Dictionary<string, object>? scope);

    protected void TriggerUIUpdate()
      => _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });

    protected UIElement CreateUIElement(IEntity entity, List<IDisposable>? disposables = null)
      => ActivatorUtilities.CreateInstance<UIElement>(_provider, new object[] { entity, this, disposables ?? new List<IDisposable>() });

    protected UIElement CreateUIElement<T>(IEntity entity, List<IDisposable>? disposables = null)
      where T : UIElement
      => ActivatorUtilities.CreateInstance<T>(_provider, new object[] { entity, this, disposables ?? new List<IDisposable>() });

    internal List<UIElement> GenerateUI(Style style, UIElement? parent = null, Dictionary<string, object>? scope = null)
    {
      var forAttribute = Attributes.FirstOrDefault(x => x is ForAttribute) as ForAttribute;
      var ifAttribute = Attributes.FirstOrDefault(x => x is IfAttribute) as IfAttribute;
      var refAttribute = Attributes.FirstOrDefault(x => x is RefAttribute) as RefAttribute;
      var eventAttributes = Attributes.Select(x => x as EventAttribute).Where(x => x != null).ToList();

      if (ifAttribute != null && forAttribute != null)
        throw new ArgumentException(""); // TODO: Figure out a good way to have both for and if attributes working

      List<UIElement>? items = null;

      if (ifAttribute != null)
      {
        var ifValue = ifAttribute.Condition(UIComponent, null, scope);
        if (ifAttribute.ReactiveProperties.Count > 0)
        {
          foreach (var prop in ifAttribute.ReactiveProperties)
          {
            var reactiveProp = prop(UIComponent, null, scope);
            if (reactiveProp != null)
            {
              reactiveProp.Subscribe(x =>
              {
                if (x.Data is bool boolValue)
                {
                  if (boolValue)
                    items = BuildAndAddEntity(style, parent, scope);
                  else
                  {
                    if (items != null)
                      foreach (var item in items)
                        item.Dispose();

                    items = null;
                  }

                  TriggerUIUpdate();
                }
              });
            }
          }
        }


        if (ifValue is bool ifBoolValue && !ifBoolValue)
          return new List<UIElement>();
      }

      if (forAttribute != null)
      {
        var list = forAttribute.Value(UIComponent, null, scope) as IEnumerable;
        if (list == null)
          throw new ArgumentNullException("Could not create for loop since value is not an IEnumerable");

        items = new List<UIElement>();
        foreach (var item in list)
        {
          var newScope = new Dictionary<string, object>(scope ?? new Dictionary<string, object>());
          newScope[forAttribute.ScopeName] = item;
          var entities = BuildAndAddEntity(style, parent, newScope);
          items.AddRange(entities);
        }
        return items;
      }
      else
        items = BuildAndAddEntity(style, parent, scope);
      return items;
    }

    private List<UIElement> BuildAndAddEntity(Style parentStyle, UIElement? parent, Dictionary<string, object>? scope)
    {
      var style = new Style() { Parent = parentStyle };
      var entities = BuildEntity(style, parent, scope) ?? new List<UIElement>();

      foreach (var entity in entities)
      {
        var styleDisposables = MergeStyles(scope, entity);
        entity.Parent = parent;
        if (styleDisposables != null)
          entity.Disposables.AddRange(styleDisposables);
        entity.Disposables.AddRange(AttachEvents(scope, entity.Entity, EventAttributes));
      }

      BindRefs(entities);
      return entities;
    }

    private void BindRefs(List<UIElement> entities)
    {
      var refAttr = Attributes.FirstOrDefault(x => x is RefAttribute);
      if (refAttr != null)
      {
        var refField = UIComponent.GetType().GetField(refAttr.Name);
        if (refField != null)
        {
          if (typeof(IEntity).IsAssignableFrom(refField.FieldType))
            refField.SetValue(UIComponent, entities.FirstOrDefault()?.Entity);
          else
            refField.SetValue(UIComponent, entities.Select(x => x.Entity));
        }

        var refProp = UIComponent.GetType().GetProperty(refAttr.Name);
        if (refProp != null)
        {
          if (typeof(IEntity).IsAssignableFrom(refProp.PropertyType))
            refProp.SetValue(UIComponent, entities.FirstOrDefault()?.Entity);
          else
            refProp.SetValue(UIComponent, entities.Select(x => x.Entity));
        }
      }
    }

    private List<UIElement> BuildEntity(Style style, UIElement? parent, Dictionary<string, object>? scope)
    {
      var results = AddEntity(style, parent, scope) ?? new List<UIElement>();
      for (var i = 0; i < results.Count; ++i)
      {
        if (this is ComponentNode componentNode)
        { /// TODO: Put this in the component node somehow
          var slot = FindSlot(results[i]);
          if (slot != null)
            results[i] = slot;
        }

        foreach (var child in Children)
          child.GenerateUI(style, results[i], scope);
      }
      return results;
    }

    protected virtual List<IDisposable> AttachEvents(Dictionary<string, object>? scope, IEntity entity, IEnumerable<EventAttribute> events)
    {
      var result = new List<IDisposable>();
      foreach (var evt in events)
      {
        if (evt != null)
        {
          switch (evt.Name)
          {
            case "Select":
              result.Add(_provider.AddEventListener<SelectEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity));
              break;
            case "Blur":
              result.Add(_provider.AddEventListener<BlurEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity));
              break;
            case "Focus":
              result.Add(_provider.AddEventListener<FocusEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity));
              break;
            case "KeyboardUp":
              result.Add(_provider.AddEventListener<KeyboardUpEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity));
              break;
            case "MouseDown":
              result.Add(_provider.AddEventListener<MouseDownEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity));
              break;
            case "MouseMove":
              result.Add(_provider.AddEventListener<MouseMoveEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity));
              break;
            case "MouseUp":
              result.Add(_provider.AddEventListener<MouseUpEvent>(x => InvokeEvent(evt, scope, x), evt.IsGlobal ? null : entity));
              break;
          }
        }
      }
      return result;
    }

    protected virtual List<IDisposable>? MergeStyles(Dictionary<string, object>? scope, UIElement element)
    {
      var styleAttribute = Attributes
        .Select(x => x as StyleAttribute)
        .FirstOrDefault(x => x != null);

      if (styleAttribute != null)
      {
        var sa = element.Entity.GetAddon<StyleAddon>();
        var disposibles = new List<IDisposable>();
        foreach (var item in styleAttribute.Styles)
        {
          var prop = typeof(Style).GetProperty(item.Name);
          if (prop != null)
          {
            var callback = item.Callback;
            switch (item.Type)
            {
              case StyleAttribute.StyleItemType.Basic:
                prop.SetValue(sa.Style, callback(UIComponent, null, scope));
                break;
              case StyleAttribute.StyleItemType.Hover:
                prop.SetValue(sa.HoverStyle, callback(UIComponent, null, scope));
                break;
            }

            foreach (var r in item.ReactiveProperties)
            {
              var reactive = r(UIComponent, null, scope);
              if (reactive != null)
                disposibles.Add(reactive.Subscribe(x =>
                {
                  var sa = element.Entity.GetAddon<StyleAddon>();
                  switch (item.Type)
                  {
                    case StyleAttribute.StyleItemType.Basic:
                      prop.SetValue(sa.Style, callback(UIComponent, null, scope));
                      break;
                    case StyleAttribute.StyleItemType.Hover:
                      prop.SetValue(sa.HoverStyle, callback(UIComponent, null, scope));
                      break;
                  }
                  element.Entity.Update(sa);
                }));
            }
          }
        }

        element.Entity.Update(sa);
        return disposibles;
      }
      return null;
    }

    private UISlot? FindSlot(UIElement element)
    {
      foreach (var child in element.Children)
      {
        if (child is UISlot slot)
          return slot;

        var found = FindSlot(child);
        if (found != null)
          return found;
      }
      return null;
    }

    private bool InvokeEvent<T>(EventAttribute? evt, Dictionary<string, object>? scope, T obj)
    {
      if (evt == null)
        return false;

      if (!string.IsNullOrWhiteSpace(evt.Modifier) && !evt.IsGlobal)
      {
        var mouseObj = obj as MouseEvent;
        if (mouseObj != null && Enum.TryParse<ButtonType>(evt.Modifier, out var buttonType) && mouseObj.Button != buttonType)
          return true;

        /// If we have a modifier we want to make sure that is the only key that will invoke the callback
        /// Example: @KeyboardUp.Enter="OnEnterCallback()"
        var keyboardObj = obj as KeyboardEvent;
        if (keyboardObj != null && Enum.TryParse<Keys>(evt.Modifier, out var keyType) && keyboardObj.Key != keyType)
          return true;
      }
      var result = (bool)evt.Callback(UIComponent, obj, scope);
      if (result && Parent != null)
        result = Parent.InvokeEvent(evt, scope, obj);
      return result;
    }
  }
}
