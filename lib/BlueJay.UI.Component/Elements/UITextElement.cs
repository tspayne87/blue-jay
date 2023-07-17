using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Component.Nodes;
using BlueJay.UI.Component.Reactivity;

namespace BlueJay.UI.Component.Elements
{
  /// <summary>
  /// The basic text element that will create a text entity
  /// </summary>
  internal class UITextElement : UIElement
  {
    /// <summary>
    /// The callback for the text that will need to be set for the text entity
    /// </summary>
    private Func<UIComponent, object?, Dictionary<string, object>?, object> _textCallback { get; set; }

    /// <summary>
    /// The reactive properties that were found when the text callback was created
    /// </summary>
    private List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty?>> _reactiveProperties { get; set; }

    /// <summary>
    /// A constructor to build out the text element
    /// </summary>
    /// <param name="scope">The node scope this element is currently in</param>
    /// <param name="textCallback">The callback for the text that will need to be set for the text entity</param>
    /// <param name="reactiveProperties">The reactive properties that were found when the text callback was created</param>
    public UITextElement(NodeScope scope, Func<UIComponent, object?, Dictionary<string, object>?, object> textCallback, List<Func<UIComponent, object?, Dictionary<string, object>?, IReactiveProperty?>> reactiveProperties)
      : base("text", scope, new List<UIElementAttribute>())
    {
      _textCallback = textCallback;
      _reactiveProperties = reactiveProperties;
    }

    /// <inheritdoc />
    protected override Node? CreateNode()
    {
      return new TextNode(Scope, _textCallback, _reactiveProperties);
    }
  }
}
