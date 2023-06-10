using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using BlueJay.UI.Component.Nodes.Attributes;
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
      : base("text", uiComponent, new List<Attributes.Attribute>(), provider)
    {
      _textCallback = textCallback;
      _reactiveProperties = reactiveProperties;
    }

    protected override List<UIElement> AddEntity(Style style, UIElement? parent, Dictionary<string, object>? scope)
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
            var test = prop;
            var ta = entity.GetAddon<TextAddon>();
            ta.Text = _textCallback(UIComponent, null, scope) as string ?? string.Empty;
            entity.Update(ta);
            TriggerUIUpdate();
          }));
        }
      }

      return new List<UIElement>() { CreateUIElement(entity, callbacks) };
    }

    /// <inheritdoc />
    /// <remarks>
    /// Overriding this since the text node will not have events attach to them but should register the parents
    /// events to itself since the text node will be a item on the page that will consume events and will not propogate them
    /// down to their parent
    /// </remarks>
    protected override List<IDisposable> AttachEvents(Dictionary<string, object>? scope, IEntity entity, IEnumerable<EventAttribute> events)
    {
      return base.AttachEvents(scope, entity, Parent?.EventAttributes ?? events);
    }
  }
}
