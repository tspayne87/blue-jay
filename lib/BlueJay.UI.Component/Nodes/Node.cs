﻿using BlueJay.Common.Events.Keyboard;
using BlueJay.Common.Events.Mouse;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Events;
using BlueJay.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections;
using System.Reflection;
using static BlueJay.Common.Events.Mouse.MouseEvent;

namespace BlueJay.UI.Component.Nodes
{
  /// <summary>
  /// Abstract node of all other nodes and how the internals are bound to the underlinging entities
  /// </summary>
  internal abstract class Node : INode
  {
    /// <summary>
    /// The screen viewport to get dimensions of the game screen
    /// </summary>
    private readonly IScreenViewport _screen;

    /// <summary>
    /// The internal event queue to send various events too
    /// </summary>
    private readonly IEventQueue _eventQueue;

    /// <summary>
    /// The parent node
    /// </summary>
    private Node? _parent;

    /// <summary>
    /// The attributes that have been attached to this node
    /// </summary>
    public List<UIElementAttribute> Attributes { get; }

    /// <summary>
    /// The children that exist for this node
    /// </summary>
    public List<Node> Children { get; private set; }

    /// <summary>
    /// Helper method to get all the event attributes found in the attributes
    /// </summary>
    public IEnumerable<EventAttribute> EventAttributes => Attributes.Where(x => x is EventAttribute).Cast<EventAttribute>();

    /// <summary>
    /// The node scope that exists for this node
    /// </summary>
    public NodeScope Scope { get; set; }

    /// <summary>
    /// The current root component that exists on this node
    /// </summary>
    public UIComponent? RootComponent => Scope.RootComponent;

    /// <summary>
    /// The current parent that exists for this node
    /// </summary>
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

    /// <summary>
    /// Constructor to build out a basic node
    /// </summary>
    /// <param name="scope">The node scope that this node currently is attached too</param>
    /// <param name="attributes">The attributes that this node has</param>
    public Node(NodeScope scope, List<UIElementAttribute> attributes)
    {
      Scope = scope;
      _screen = Scope.ServiceProvider.GetRequiredService<IScreenViewport>();
      _eventQueue = Scope.ServiceProvider.GetRequiredService<IEventQueue>();

      Attributes = attributes;
      Children = new List<Node>();
    }

    /// <inheritdoc />
    public void GenerateUI(Style? style = null)
    {
      style = style ?? new Style();
      GenerateUI(style, null, null);
    }

    /// <summary>
    /// Basic method meant to create an entity and return a list of UIEntities generated by the node
    /// </summary>
    /// <param name="style">The current style that should be attached to this UIEntity</param>
    /// <param name="parent">The current parent UIEntity when generating this entity</param>
    /// <param name="scope">The current node scope for this entity</param>
    /// <returns>Will return a list of generated entities</returns>
    protected abstract List<UIEntity>? AddEntity(Style style, UIEntity? parent, Dictionary<string, object>? scope);

    /// <summary>
    /// Helper method meant to trigger a UI event based on the size of the screen
    /// </summary>
    protected void TriggerUIUpdate()
      => _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_screen.Width, _screen.Height) });

    /// <summary>
    /// Helper method meant to generate a UIEntity with a list of disposables
    /// </summary>
    /// <param name="entity">The entity that was generating in the game that should be attached to the UIEntity object</param>
    /// <param name="disposables">The disposables that were generated to update the entity that was created</param>
    /// <returns>Will return a created UIEntity based on the entity and disposables</returns>
    protected UIEntity CreateUIElement(IEntity entity, List<IDisposable>? disposables = null)
      => ActivatorUtilities.CreateInstance<UIEntity>(Scope.ServiceProvider, new object[] { entity, this, disposables ?? new List<IDisposable>() });

    /// <summary>
    /// Internal method meant to generate the UI and bind various elements to the UI element that is returned
    /// </summary>
    /// <param name="style">The current style we need to attach to these UI elements</param>
    /// <param name="parent">The current parent UIEntity that should be attached to create a tree structure for UIEntities</param>
    /// <param name="scope">The current scope for this UI element</param>
    /// <exception cref="ArgumentException">Is thrown if both if and for attribute exist on this node</exception>
    /// <exception cref="ArgumentNullException">Will return this if the instatiated component could not be found</exception>
    private void GenerateUI(Style style, UIEntity? parent = null, Dictionary<string, object>? scope = null)
    {
      var forAttribute = Attributes.FirstOrDefault(x => x is ForAttribute) as ForAttribute;
      var ifAttribute = Attributes.FirstOrDefault(x => x is IfAttribute) as IfAttribute;
      var refAttribute = Attributes.FirstOrDefault(x => x is RefAttribute) as RefAttribute;
      var eventAttributes = Attributes.Select(x => x as EventAttribute).Where(x => x != null).ToList();

      if (ifAttribute != null && forAttribute != null)
        throw new ArgumentException("Both if and for attributes cannot be on same element"); // TODO: Figure out a good way to have both for and if attributes working

      List<UIEntity>? items = null;
      if (ifAttribute != null)
      {
        if (parent?.ScopeKey == null)
          throw new ArgumentNullException("Component");
        foreach (var reactiveProp in ifAttribute.GetReactiveProperties(Scope, parent, null, scope))
        {
          if (reactiveProp == null)
            continue;

          reactiveProp.Subscribe(x =>
          {
            var updated = ifAttribute.GetValue(Scope, parent, null, scope);
            var hasUpdate = false;
            if (updated is bool boolValue)
            {
              if (boolValue)
              {
                if (items == null)
                {
                  items = BuildAndAddEntity(style, parent, scope);
                  hasUpdate = true;
                }
              }
              else
              {
                if (items != null)
                {
                  foreach (var item in items)
                    if (item != null)
                      RemoveElement(item);
                  hasUpdate = true;
                  items = null;
                }
              }

              if (hasUpdate)
                TriggerUIUpdate();
            }
          });
        }
        return;
      }

      if (forAttribute != null)
      {
        if (parent?.ScopeKey == null)
          throw new ArgumentNullException("Component");

        foreach (var reactiveProp in forAttribute.GetReactiveProperties(Scope, parent, null, scope))
        {
          if (reactiveProp == null)
            continue;

          reactiveProp.Subscribe(x =>
          {
            if (items != null)
              foreach (var item in items)
                if (item != null)
                  RemoveElement(item);

            items = new List<UIEntity>();
            var list = forAttribute.GetValue(Scope, parent, null, scope) as IEnumerable;
            if (list == null)
              throw new ArgumentNullException("Could not create for loop since value is not an IEnumerable");

            foreach (var item in list)
            {
              var newScope = new Dictionary<string, object>(scope ?? new Dictionary<string, object>());
              newScope[forAttribute.ScopeName] = item;
              var entities = BuildAndAddEntity(style, parent, newScope);
              items.AddRange(entities);
            }

            TriggerUIUpdate();
          });
        }
        return;
      }

      items = BuildAndAddEntity(style, parent, scope);
    }

    /// <summary>
    /// Helper method is meant to remove an element and dispose of all relative subscriptions for the entity and
    /// its children
    /// </summary>
    /// <param name="element">The element that should be removed</param>
    protected virtual void RemoveElement(UIEntity element)
    {
      element.Dispose();
    }

    /// <summary>
    /// Helper method meant to attach events to the newly created element
    /// </summary>
    /// <param name="scope">The current scope of variables that exist on the node tree</param>
    /// <param name="element">The UIEntity that events need to be bound too</param>
    /// <param name="events">The list of events that should be attached to this entity</param>
    /// <returns>Will return a list of disposables that were attached to this entity</returns>
    private List<IDisposable> AttachEvents(Dictionary<string, object>? scope, UIEntity element, IEnumerable<EventAttribute> events)
    {
      var result = new List<IDisposable>();
      foreach (var evt in events)
      {
        if (evt != null)
        {
          switch (evt.Name)
          {
            case "Select":
              result.Add(Scope.ServiceProvider.AddEventListener<SelectEvent>(x => InvokeEvent(element, evt, scope, x), evt.IsGlobal ? null : element.Entity));
              break;
            case "Blur":
              result.Add(Scope.ServiceProvider.AddEventListener<BlurEvent>(x => InvokeEvent(element, evt, scope, x), evt.IsGlobal ? null : element.Entity));
              break;
            case "Focus":
              result.Add(Scope.ServiceProvider.AddEventListener<FocusEvent>(x => InvokeEvent(element, evt, scope, x), evt.IsGlobal ? null : element.Entity));
              break;
            case "KeyboardUp":
              result.Add(Scope.ServiceProvider.AddEventListener<KeyboardUpEvent>(x => InvokeEvent(element, evt, scope, x), evt.IsGlobal ? null : element.Entity));
              break;
            case "MouseDown":
              result.Add(Scope.ServiceProvider.AddEventListener<MouseDownEvent>(x => InvokeEvent(element, evt, scope, x), evt.IsGlobal ? null : element.Entity));
              break;
            case "MouseMove":
              result.Add(Scope.ServiceProvider.AddEventListener<MouseMoveEvent>(x => InvokeEvent(element, evt, scope, x), evt.IsGlobal ? null : element.Entity));
              break;
            case "MouseUp":
              result.Add(Scope.ServiceProvider.AddEventListener<MouseUpEvent>(x => InvokeEvent(element, evt, scope, x), evt.IsGlobal ? null : element.Entity));
              break;
          }
        }
      }
      return result;
    }

    /// <summary>
    /// Helper method is meant to build and add the entity as well as attach various things to the entity
    /// that was generated
    /// </summary>
    /// <param name="parentStyle">The parent style</param>
    /// <param name="parent">The current parent for this entity</param>
    /// <param name="scope">The current tree scope</param>
    /// <returns>Will return a list of generated entities</returns>
    private List<UIEntity> BuildAndAddEntity(Style parentStyle, UIEntity? parent, Dictionary<string, object>? scope)
    {
      var style = new Style() { Parent = parentStyle };
      var entities = BuildEntity(style, parent, scope) ?? new List<UIEntity>();

      foreach (var entity in entities)
      {
        var styleDisposables = MergeStyles(scope, entity);
        if (styleDisposables != null)
          entity.Disposables.AddRange(styleDisposables);
        entity.Disposables.AddRange(AttachEvents(scope, entity, EventAttributes));
      }

      if (parent?.ScopeKey != null)
        BindRefs(Scope[parent.ScopeKey.Value], entities);
      return entities;
    }

    /// <summary>
    /// Helper method is meant to bind references to the current ui component
    /// </summary>
    /// <param name="component">The component to bind entities too</param>
    /// <param name="entities">The list of entities that were created and needs to be bound</param>
    private void BindRefs(UIComponent component, List<UIEntity> entities)
    {
      var refAttr = Attributes.FirstOrDefault(x => x is RefAttribute);
      if (refAttr != null)
      {
        var refField = Scope.ComponentType.GetField(refAttr.Name);
        if (refField != null)
        {
          if (typeof(IEntity).IsAssignableFrom(refField.FieldType))
            refField.SetValue(component, entities.FirstOrDefault()?.Entity);
          else
            refField.SetValue(component, entities.Select(x => x.Entity));
        }

        var refProp = Scope.ComponentType.GetProperty(refAttr.Name);
        if (refProp != null)
        {
          if (typeof(IEntity).IsAssignableFrom(refProp.PropertyType))
            refProp.SetValue(component, entities.FirstOrDefault()?.Entity);
          else
            refProp.SetValue(component, entities.Select(x => x.Entity));
        }
      }
    }

    /// <summary>
    /// Helper method will build the entity and attach its parent and generate the UI for it's children
    /// </summary>
    /// <param name="style">The current style for the entity being created</param>
    /// <param name="parent">The parent that this entity should be attached too</param>
    /// <param name="scope">The tree scope item</param>
    /// <returns>Will return a list of entities that were built</returns>
    private List<UIEntity> BuildEntity(Style style, UIEntity? parent, Dictionary<string, object>? scope)
    {
      var results = AddEntity(style, parent, scope) ?? new List<UIEntity>();
      for (var i = 0; i < results.Count; ++i)
      {
        results[i].Parent = parent;

        foreach (var child in Children)
          child.GenerateUI(style, results[i], scope);
      }
      return results;
    }

    /// <summary>
    /// Helper method is meant to merge some styles to the newly created element
    /// </summary>
    /// <param name="scope">The tree scope</param>
    /// <param name="element">The element that the style should be bound too</param>
    /// <returns>A list of disposables that were generated when the styles were merged</returns>
    /// <exception cref="ArgumentNullException">Will throw a null exception if the component has not be genearted yet</exception>
    private List<IDisposable>? MergeStyles(Dictionary<string, object>? scope, UIEntity element)
    {
      if (element.ScopeKey == null)
        throw new ArgumentNullException("Component");

      var styleAttribute = Attributes
        .Select(x => x as StyleAttribute)
        .FirstOrDefault(x => x != null);

      var component = Scope[element.ScopeKey.Value];
      if (styleAttribute != null)
      {
        if (element.Entity == null)
          throw new ArgumentNullException("Entity");

        var sa = element.Entity.GetAddon<StyleAddon>();
        var disposibles = new List<IDisposable>();
        foreach (var item in styleAttribute.Styles)
        {
          var prop = typeof(Style).GetProperty(item.Name);
          if (prop != null)
          {
            switch (item.Type)
            {
              case StyleAttribute.StyleItemType.Basic:
                prop.SetValue(sa.Style, item.GetValue(Scope, element, null, scope));
                break;
              case StyleAttribute.StyleItemType.Hover:
                prop.SetValue(sa.HoverStyle, item.GetValue(Scope, element, null, scope));
                break;
            }

            foreach (var reactive in item.GetReactiveProperties(Scope, element, null, scope))
            {
              if (reactive != null)
                disposibles.Add(reactive.Subscribe(x =>
                {
                  var sa = element.Entity.GetAddon<StyleAddon>();
                  switch (item.Type)
                  {
                    case StyleAttribute.StyleItemType.Basic:
                      prop.SetValue(sa.Style, item.GetValue(Scope, element, null, scope));
                      break;
                    case StyleAttribute.StyleItemType.Hover:
                      prop.SetValue(sa.HoverStyle, item.GetValue(Scope, element, null, scope));
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

    /// <summary>
    /// Helper method is meant to invoke an internal event
    /// </summary>
    /// <typeparam name="T">The type of event that was invoked</typeparam>
    /// <param name="element">The entity that this event was invoked on</param>
    /// <param name="evt">The event attribute this event was invoked with</param>
    /// <param name="scope">The current tree scope</param>
    /// <param name="obj">The event object that invoked this method</param>
    /// <returns>Will return a boolean to determine if the event should stop propegation</returns>
    /// <exception cref="ArgumentNullException">Will throw a null exception if the component has not be genearted yet</exception>
    private bool InvokeEvent<T>(UIEntity element, EventAttribute? evt, Dictionary<string, object>? scope, T obj)
    {
      if (element.ScopeKey == null)
        throw new ArgumentNullException("Component");

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
      var result = evt.GetValue(Scope, element, obj, scope);
      if (result == null)
        return true;
      return (bool)result;
    }
  }
}
