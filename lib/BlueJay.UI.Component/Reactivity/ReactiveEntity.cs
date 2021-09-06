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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlueJay.UI.Component.Reactivity
{
  public class ReactiveEntity : Entity
  {
    private readonly EventQueue _eventQueue;
    private readonly GraphicsDevice _graphics;

    private ReactiveScope _scope;
    private List<IDisposable> _subscriptions;

    public List<IDisposable> Subscriptions => _subscriptions;
    public object Data { get; set; }
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
    public ElementNode Node { get; set; }

    public ReactiveEntity(LayerCollection layerCollection, EventQueue eventQueue, GraphicsDevice graphics)
      : base(layerCollection, eventQueue)
    {
      _eventQueue = eventQueue;
      _graphics = graphics;
      _subscriptions = new List<IDisposable>();
    }

    public void ProcessProperties()
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

    private void SetActive(IEntity entity, bool active)
    {
      entity.Active = active;

      var la = entity.GetAddon<LineageAddon>();
      for (var i = 0; i < la.Children.Count; ++i)
        SetActive(la.Children[i], active);
    }

    private void ClearSubscriptions()
    {
      foreach (var subscription in _subscriptions)
        subscription.Dispose();
      _subscriptions.Clear();
    }

    public override void Dispose()
    {
      ClearSubscriptions();
    }
  }
}
