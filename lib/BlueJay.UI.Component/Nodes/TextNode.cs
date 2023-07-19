using BlueJay.UI.Addons;
using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Component.Reactivity;
using BlueJay.UI.Factories;

namespace BlueJay.UI.Component.Nodes
{
  /// <summary>
  /// The text node to create a basic text entity to the UI
  /// </summary>
  internal class TextNode : Node
  {
    /// <summary>
    /// The callback function meant to get the text that should be used for the text entity
    /// </summary>
    private Func<UIComponent, object?, Dictionary<string, object>?, object> _textCallback { get; set; }

    /// <summary>
    /// The reactive properties found when generating the text callback
    /// </summary>
    private List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty?>> _reactiveProperties { get; set; }

    /// <summary>
    /// Constructor to build out the text node
    /// </summary>
    /// <param name="scope">The node scope this text node is currently in</param>
    /// <param name="textCallback">The callback function meant to get the text that should be used for the text entity</param>
    /// <param name="reactiveProperties">The reactive properties found when generating the text callback</param>
    public TextNode(NodeScope scope, Func<UIComponent, object?, Dictionary<string, object>?, object> textCallback, List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty?>> reactiveProperties)
      : base(scope, new List<UIElementAttribute>())
    {
      _textCallback = textCallback;
      _reactiveProperties = reactiveProperties;
    }

    /// <inheritdoc />
    protected override List<UIEntity> AddEntity(Style style, UIEntity? parent, Dictionary<string, object>? scope)
    {
      if (parent == null || !parent.ScopeKey.HasValue)
        throw new ArgumentNullException("Component");
      var component = Scope[parent.ScopeKey.Value];

      var data = _textCallback(component, null, scope) as string ?? string.Empty;
      var entity = Scope.ServiceProvider.AddText(data, style, parent?.Entity);
      var callbacks = new List<IDisposable>();
      foreach (var callback in _reactiveProperties)
      {
        var prop = callback(component, null, scope);
        if (prop != null)
        {
          callbacks.Add(prop.Subscribe(evt =>
          {
            var test = prop;
            var ta = entity.GetAddon<TextAddon>();
            ta.Text = _textCallback(component, null, scope) as string ?? string.Empty;
            entity.Update(ta);
            TriggerUIUpdate();
          }));
        }
      }

      return new List<UIEntity>() { CreateUIElement(entity, callbacks) };
    }
  }
}
