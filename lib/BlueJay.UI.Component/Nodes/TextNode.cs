using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Nodes.Elements;
using BlueJay.UI.Component.Reactivity;
using BlueJay.UI.Factories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using System.Xml.Linq;

namespace BlueJay.UI.Component.Nodes
{
  internal class TextNode : Node
  {
    private Func<UIComponent, object?, Dictionary<string, object>?, object> _textCallback { get; set; }
    private List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty>> _reactiveProperties { get; set; }

    public TextNode(UIComponent uiComponent, IServiceProvider provider, Func<UIComponent, object?, Dictionary<string, object>?, object> textCallback, List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty>> reactiveProperties)
      : base("text", uiComponent, provider)
    {
      _textCallback = textCallback;
      _reactiveProperties = reactiveProperties;
    }

    protected override UIElement AddEntity(Style style, UIElement? parent, Dictionary<string, object>? scope)
    {
      var data = _textCallback(UIComponent, null, scope) as string ?? string.Empty;
      var entity = _provider.AddText(data, style, parent?.Entity);
      var callbacks = new List<IDisposable>();
      foreach (var callback in _reactiveProperties)
      {
        var prop = callback(UIComponent, null, scope);
        if (prop != null)
        {
          callbacks.Add(prop.Subscribe(evt =>
          {
            var ta = entity.GetAddon<TextAddon>();
            ta.Text = _textCallback(UIComponent, null, scope) as string ?? string.Empty;
            entity.Update(ta);
            TriggerUIUpdate();
          }));
        }
      }

      return CreateUIElement(entity, callbacks);
    }
  }
}
