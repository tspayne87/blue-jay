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
using System.Text;

namespace BlueJay.UI.Component.Reactivity
{
  public class ReactiveEntity : Entity
  {
    private readonly EventQueue _eventQueue;
    private readonly GraphicsDevice _graphics;

    public List<IDisposable> DisposableEvents { get; private set; }
    public object Data { get; set; }
    public ReactiveScope Scope { get; set; }
    public ElementNode Node { get; set; }

    public ReactiveEntity(LayerCollection layerCollection, EventQueue eventQueue, GraphicsDevice graphics)
      : base(layerCollection, eventQueue)
    {
      _eventQueue = eventQueue;
      _graphics = graphics;
      DisposableEvents = new List<IDisposable>();
    }

    public void ProcessProperties()
    {
      //foreach (var prop in Node.Props)
      //{
      //  if (prop.Name == PropNames.If)
      //    Active = (bool)prop.DataGetter(Scope);

      //  if (prop.ReactiveProps?.Count > 0)
      //  {
      //    foreach (var reactive in prop.ReactiveProps)
      //    {
      //      switch (prop.Name)
      //      {
      //        case PropNames.Text:
      //          DisposableEvents.Add(
      //            reactive.Subscribe(x =>
      //            {
      //              var ta = GetAddon<TextAddon>();
      //              var txt = prop.DataGetter(Scope) as string;
      //              ta.Text = txt;
      //              Update(ta);
      //              _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
      //            })
      //          );
      //          break;
      //        case PropNames.Style:
      //          DisposableEvents.Add(
      //            reactive.Subscribe(x =>
      //            {
      //              var newScope = Scope != null ? new Dictionary<string, object>(Scope) : new Dictionary<string, object>();
      //              newScope[PropNames.Style] = GetAddon<StyleAddon>().Style;
      //              prop.DataGetter(newScope);
      //              _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
      //            })
      //          );
      //          break;
      //        case PropNames.HoverStyle:
      //          DisposableEvents.Add(
      //            reactive.Subscribe(x =>
      //            {
      //              var newScope = Scope != null ? new Dictionary<string, object>(Scope) : new Dictionary<string, object>();
      //              newScope[PropNames.Style] = GetAddon<StyleAddon>().Style;
      //              prop.DataGetter(newScope);
      //              _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
      //            })
      //          );
      //          break;
      //        case PropNames.If:
      //          DisposableEvents.Add(
      //            reactive.Subscribe(x =>
      //            {
      //              SetActive(this, (bool)prop.DataGetter(Scope));
      //              _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
      //            })
      //          );
      //          break;
      //        default:
      //          if (prop.ReactiveProps?.Count > 0)
      //          { // Process binding properties together, so they can keep the one-way and two-way binding
      //            var bindable = Node.Instance.GetType().GetField(prop.Name);
      //            if (bindable != null)
      //            {
      //              var bAttr = bindable.GetCustomAttributes(typeof(PropAttribute), false).FirstOrDefault() as PropAttribute;
      //              var reactiveAttr = bindable.GetValue(Node.Instance) as IReactiveProperty;
      //              if (bAttr != null && reactiveAttr != null)
      //              {
      //                if (bAttr.Binding == PropBinding.TwoWay && prop.ReactiveProps.Count == 1)
      //                {
      //                  DisposableEvents.Add(reactiveAttr.Subscribe(x => prop.ReactiveProps[0].Value = x));
      //                  DisposableEvents.Add(prop.ReactiveProps[0].Subscribe(x => reactiveAttr.Value = x));
      //                }
      //                else if (bAttr.Binding == PropBinding.OneWay)
      //                {
      //                  foreach (var item in prop.ReactiveProps)
      //                  {
      //                    // TODO: This could cause an issue with scoped elements
      //                    DisposableEvents.Add(item.Subscribe(x => reactiveAttr.Value = prop.DataGetter(Scope)));
      //                  }
      //                }
      //              }
      //            }
      //          }
      //          break;
      //      }
      //    }
      //  }
      //}
    }

    private void SetActive(IEntity entity, bool active)
    {
      entity.Active = active;

      var la = entity.GetAddon<LineageAddon>();
      for (var i = 0; i < la.Children.Count; ++i)
        SetActive(la.Children[i], active);
    }
  }
}
