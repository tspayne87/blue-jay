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
      foreach (var prop in Node.Props)
      {
        if (prop.Name == PropNames.If)
          Active = (bool)prop.DataGetter(Scope);

        if (prop.ScopePaths?.Count > 0)
        {
          switch (prop.Name)
          {
            case PropNames.Text:
              DisposableEvents.AddRange(
                Scope.Subscribe(x =>
                {
                  var ta = GetAddon<TextAddon>();
                  var txt = prop.DataGetter(Scope) as string;
                  ta.Text = txt;
                  Update(ta);
                  _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
                }, prop.ScopePaths)
              );
              break;
            case PropNames.Style:
              DisposableEvents.AddRange(
                Scope.Subscribe(x =>
                {
                  var newScope = Scope.NewScope();
                  newScope[PropNames.Style] = GetAddon<StyleAddon>().Style;
                  prop.DataGetter(newScope);
                  _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
                }, prop.ScopePaths)
              );
              break;
            case PropNames.HoverStyle:
              DisposableEvents.AddRange(
                Scope.Subscribe(x =>
                {
                  var newScope = Scope.NewScope();
                  newScope[PropNames.Style] = GetAddon<StyleAddon>().Style;
                  prop.DataGetter(newScope);
                  _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
                }, prop.ScopePaths)
              );
              break;
            case PropNames.If:
              DisposableEvents.AddRange(
                Scope.Subscribe(x =>
                {
                  SetActive(this, (bool)prop.DataGetter(Scope));
                  _eventQueue.DispatchEvent(new UIUpdateEvent() { Size = new Size(_graphics.Viewport.Width, _graphics.Viewport.Height) });
                }, prop.ScopePaths)
              );
              break;
            default:
              var bindable = Node.Instance.GetType().GetField(prop.Name);
              if (bindable != null)
              {
                var bAttr = bindable.GetCustomAttributes(typeof(PropAttribute), false).FirstOrDefault() as PropAttribute;
                if (bAttr != null)
                {
                  if (bAttr.Binding == PropBinding.TwoWay && prop.ScopePaths.Count == 1)
                  {
                    DisposableEvents.Add(Scope.Subscribe(x => Scope.Parent[prop.ScopePaths[0]] = x.Data, $"{PropNames.Identifier}.{prop.Name}"));
                    DisposableEvents.Add(Scope.Parent.Subscribe(x => Scope[$"{PropNames.Identifier}.{prop.Name}"] = x.Data, prop.ScopePaths[0]));
                  }
                  else if (bAttr.Binding == PropBinding.OneWay)
                  {
                    DisposableEvents.AddRange(Scope.Subscribe(x => Scope[$"{PropNames.Identifier}.{prop.Name}"] = prop.DataGetter(Scope.Parent), prop.ScopePaths));
                  }
                }
              }
              break;
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
  }
}
