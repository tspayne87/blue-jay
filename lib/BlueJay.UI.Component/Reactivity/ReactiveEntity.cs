using BlueJay.Component.System;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Attributes;
using BlueJay.UI.Component.Language;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlueJay.UI.Component.Reactivity
{
  /// <summary>
  /// The reactive entity
  /// </summary>
  public class ReactiveEntity : Entity
  {
    /// <summary>
    /// The event queue to trigger UI update events
    /// </summary>
    private readonly EventQueue _eventQueue;

    /// <summary>
    /// The graphics device to get the screen width
    /// </summary>
    private readonly GraphicsDevice _graphics;

    /// <summary>
    /// The reactive scope bound to this entity
    /// </summary>
    private ReactiveScope _scope;

    /// <summary>
    /// The current subscriptions for this entity
    /// </summary>
    private List<IDisposable> _subscriptions;

    /// <summary>
    /// This list of subscriptions
    /// </summary>
    public List<IDisposable> Subscriptions => _subscriptions;

    /// <summary>
    /// The data that should be bound to this entity
    /// </summary>
    public object Data { get; set; }

    /// <summary>
    /// The reactive scope that is meant to process properties
    /// </summary>
    public ReactiveScope Scope
    {
      get => _scope;
      set
      {
        ClearSubscriptions();
        _scope = value;
        ProcessProperties();
      }
    }

    /// <summary>
    /// The element node being processed
    /// </summary>
    internal ElementNode Node { get; set; }

    /// <summary>
    /// Constructor to inject data into the entitiy
    /// </summary>
    /// <param name="layerCollection">The current layer collection</param>
    /// <param name="eventQueue">The event queue to trigger UI update events</param>
    /// <param name="graphics">The graphics device to get the screen width</param>
    public ReactiveEntity(LayerCollection layerCollection, EventQueue eventQueue, GraphicsDevice graphics)
      : base(layerCollection, eventQueue)
    {
      _eventQueue = eventQueue;
      _graphics = graphics;
      _subscriptions = new List<IDisposable>();
    }

    /// <summary>
    /// Process properties for the element nodes
    /// </summary>
    private void ProcessProperties()
    {
      foreach (var prop in Node.Props)
      {
        switch (prop.Name)
        {
          case PropNames.Text:
            ProcessText(prop);
            break;
          case PropNames.Style:
            ProcessStyle(prop);
            break;
          case PropNames.HoverStyle:
            ProcessHoverStyle(prop);
            break;
          case PropNames.If:
            ProcessIf(prop);
            break;
          default:
            ProcessBoundProps(prop);
            break;
        }
      }
    }

    /// <summary>
    /// Process a text property to update the text addon when something updates
    /// </summary>
    /// <param name="prop">The prop being processed</param>
    private void ProcessText(ElementProp prop)
    {
      if (prop.ScopePaths.Count > 0)
      {
        _subscriptions.AddRange(
          Scope.Subscribe(x =>
          {
            var ta = GetAddon<TextAddon>();
            ta.Text = prop.DataGetter(Scope) as string;
            Update(ta);
            _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
          }, prop.ScopePaths)
        );
        return;
      }

      var t = GetAddon<TextAddon>();
      t.Text = prop.DataGetter(Scope) as string;
      Update(t);
    }

    /// <summary>
    /// Process a style property to update the style when something updates
    /// </summary>
    /// <param name="prop">The prop being processed</param>
    private void ProcessStyle(ElementProp prop)
    {
      if (prop.ScopePaths.Count > 0)
      {
        _subscriptions.AddRange(
          Scope.Subscribe(x =>
          {
            var sa = GetAddon<StyleAddon>();
            sa.Style = prop.DataGetter(Scope) as Style;
            Update(sa);
            _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
          }, prop.ScopePaths)
        );
        return;
      }

      var s = GetAddon<StyleAddon>();
      s.Style = prop.DataGetter(Scope) as Style;
      Update(s);
    }

    /// <summary>
    /// Process a hover style property to update the style when something updates
    /// </summary>
    /// <param name="prop">The prop being processed</param>
    private void ProcessHoverStyle(ElementProp prop)
    {
      if (prop.ScopePaths.Count > 0)
      {
        _subscriptions.AddRange(
          Scope.Subscribe(x =>
          {
            var node = Node;
            var sa = GetAddon<StyleAddon>();
            var newStyle = prop.DataGetter(Scope) as Style;
            newStyle.Parent = null;
            sa.HoverStyle = newStyle;
            Update(sa);

            _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
          }, prop.ScopePaths)
        );
        return;
      }

      var s = GetAddon<StyleAddon>();
      s.HoverStyle = prop.DataGetter(Scope) as Style;
      Update(s);
    }

    /// <summary>
    /// Process a if property to update the if when something updates
    /// </summary>
    /// <param name="prop">The prop being processed</param>
    private void ProcessIf(ElementProp prop)
    {
      if (prop.ScopePaths.Count > 0)
      {
        _subscriptions.AddRange(
          Scope.Subscribe(x =>
          {
            SetActive(this, (bool)prop.DataGetter(Scope));
            _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
          }, prop.ScopePaths)
        );
        return;
      }

      SetActive(this, (bool)prop.DataGetter(Scope));
    }

    /// <summary>
    /// Process a bound props to bind them for two way and one way bindings
    /// </summary>
    /// <param name="prop">The prop being processed</param>
    private void ProcessBoundProps(ElementProp prop)
    {
      if (prop.ScopePaths?.Count > 0)
      {
        var bindable = Node.Instance.GetType().GetField(prop.Name);
        if (bindable != null)
        {
          var bAttr = bindable.GetCustomAttributes(typeof(PropAttribute), false).FirstOrDefault() as PropAttribute;
          if (bAttr != null)
          {
            if (bAttr.Binding == PropBinding.TwoWay && prop.ScopePaths.Count == 1)
            {
              _subscriptions.Add(Scope.Subscribe(x => Scope[prop.ScopePaths[0]] = x.Data, $"{Node.Instance.Identifier}.{prop.Name}"));
              _subscriptions.Add(Scope.Subscribe(x => Scope[$"{Node.Instance.Identifier}.{prop.Name}"] = x.Data, prop.ScopePaths[0]));
            }
            else if (bAttr.Binding == PropBinding.OneWay)
            {
              _subscriptions.AddRange(Scope.Subscribe(x => Scope[$"{Node.Instance.Identifier}.{prop.Name}"] = prop.DataGetter(Scope), prop.ScopePaths));
            }
          }
        }
      }
    }

    /// <summary>
    /// Recursive solution to activate a root entity
    /// </summary>
    /// <param name="entity">The entity we need to set active or not</param>
    /// <param name="active">The active boolean that needs to be set</param>
    private void SetActive(IEntity entity, bool active)
    {
      entity.Active = active;

      var la = entity.GetAddon<LineageAddon>();
      for (var i = 0; i < la.Children.Count; ++i)
        SetActive(la.Children[i], active);
    }

    /// <summary>
    /// Clear subscriptions so we can clear up subscriptions if this entity is removed
    /// </summary>
    private void ClearSubscriptions()
    {
      foreach (var subscription in _subscriptions)
        subscription.Dispose();
      _subscriptions.Clear();
    }

    /// <inheritdoc />
    public override void Dispose()
    {
      ClearSubscriptions();
    }
  }
}
