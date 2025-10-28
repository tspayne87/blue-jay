using BlueJay.Core.Container;
using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Interfaces;
using BlueJay.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.UI.Component.Nodes;

/// <summary>
/// The texture node meant to create a space for a textured entity to the UI structure
/// </summary>
internal class TextureNode : Node
{
  /// <summary>
  /// The list of attributes that are avaiable for the texture node
  /// </summary>
  private readonly Dictionary<string, UIElementAttribute?> _attributes;

  /// <summary>
  /// Constructor to build out the texture node from the scope and attributes attached to this node
  /// </summary>
  /// <param name="scope">The node scope we are working with</param>
  /// <param name="attributes">The current attributes attached to this node</param>
  /// <exception cref="ArgumentNullException">Will return null exception if the 'AssetName' is not set as an attribute</exception>
  public TextureNode(NodeScope scope, List<UIElementAttribute> attributes)
    : base(scope, attributes)
  {
    var attr = attributes.FirstOrDefault(x => x.Name.Equals("AssetName", StringComparison.OrdinalIgnoreCase)) as StringAttribute;
    if (attr == null)
      throw new ArgumentNullException("AssetName", "Does not exist as an attribute on the element");

    _attributes = new Dictionary<string, UIElementAttribute?>()
    {
      { "AssetName", attr },
      { "FrameCount", attributes.FirstOrDefault(x => x.Name.Equals("FrameCount", StringComparison.OrdinalIgnoreCase)) },
      { "FrameTickAmount", attributes.FirstOrDefault(x => x.Name.Equals("FrameTickAmount", StringComparison.OrdinalIgnoreCase)) },
      { "Frame", attributes.FirstOrDefault(x => x.Name.Equals("Frame", StringComparison.OrdinalIgnoreCase)) },
      { "StartingFrame", attributes.FirstOrDefault(x => x.Name.Equals("StartingFrame", StringComparison.OrdinalIgnoreCase)) },
      { "Rows", attributes.FirstOrDefault(x => x.Name.Equals("Rows", StringComparison.OrdinalIgnoreCase)) },
      { "Cols", attributes.FirstOrDefault(x => x.Name.Equals("Cols", StringComparison.OrdinalIgnoreCase)) }
    };
  }

  /// <inheritdoc />
  protected override List<UIEntity>? AddEntity(Style style, UIEntity? parent, Dictionary<string, object>? scope)
  {
    if (parent == null || !parent.ScopeKey.HasValue)
      throw new ArgumentNullException("Component");

    var content = Scope.ServiceProvider.GetRequiredService<IContentManagerContainer>();
    var component = Scope[parent.ScopeKey.Value];
    var assetName = ExtractStringDetails(_attributes["AssetName"], component, scope);
    if (assetName == null)
      throw new ArgumentNullException("AssetName", "Cannot be null when updating the texture entity");

    var element = Scope.ServiceProvider.CreateUITextureNode(content.Load<ITexture2DContainer>(assetName), style, parent?.Element);
    element.Rows = ExtractIntDetails(_attributes["Rows"], component, scope) ?? 1;
    element.Cols = ExtractIntDetails(_attributes["Cols"], component, scope) ?? 1;
    element.FrameCount = ExtractIntDetails(_attributes["FrameCount"], component, scope) ?? 1;
    element.FrameTickAmount = ExtractIntDetails(_attributes["FrameTickAmount"], component, scope) ?? 0;
    element.Frame = ExtractIntDetails(_attributes["Frame"], component, scope) ?? 0;
    element.StartingFrame = ExtractIntDetails(_attributes["StartingFrame"], component, scope) ?? 0;

    var callbacks = new List<IDisposable>();

    foreach (var attribute in _attributes)
    {
      if (attribute.Value is ExpressionAttribute expression)
      {
        var items = expression.ReactiveProperties(component, null, scope);
        foreach (var item in items)
        {
          if (item == null)
            continue;
          callbacks.Add(item.Subscribe(evt => UpdateElement(element, content, component, scope, attribute.Value, attribute.Key)));
        }
      }
    }

    return new List<UIEntity>() { CreateUIElement(element, callbacks) };
  }

  private string? ExtractStringDetails(UIElementAttribute? attribute, UIComponent component, Dictionary<string, object>? scope)
  {
    if (attribute == null)
      return null;
    if (attribute is StringAttribute stringAttribute)
        return stringAttribute.Value;
    if (attribute is ExpressionAttribute expressionAttribute)
      return expressionAttribute.Callback(component, null, scope)?.ToString() ?? null;
    throw new ArgumentException($"Attribute '{attribute?.Name}' is not a valid string attribute.");
  }

  private int? ExtractIntDetails(UIElementAttribute? attribute, UIComponent component, Dictionary<string, object>? scope)
  {
    if (attribute == null)
      return null;
    if (attribute is StringAttribute intAttribute)
        return int.Parse(intAttribute.Value);
    if (attribute is ExpressionAttribute expressionAttribute)
      return expressionAttribute.Callback(component, null, scope) is int intValue ? intValue : null;
    throw new ArgumentException($"Attribute '{attribute?.Name}' is not a valid int attribute.");
  }

  public void UpdateElement(ITextureElement element, IContentManagerContainer content, UIComponent component, Dictionary<string, object>? scope, UIElementAttribute attribute, string attributeName)
  {
    switch (attributeName)
    {
      case "AssetName":
        {
          var assetName = ExtractStringDetails(attribute, component, scope);
          if (assetName == null)
            throw new ArgumentNullException("AssetName", "Cannot be null when updating the texture entity");
          element.Texture = content.Load<ITexture2DContainer>(assetName);
        }
        break;
      case "Rows":
        element.Rows = ExtractIntDetails(attribute, component, scope) ?? 1;
        break;
      case "Cols":
        element.Cols = ExtractIntDetails(attribute, component, scope) ?? 1;
        break;
      case "FrameCount":
        element.FrameCount = ExtractIntDetails(attribute, component, scope) ?? 1;
        break;
      case "FrameTickAmount":
        element.FrameTickAmount = ExtractIntDetails(attribute, component, scope) ?? 0;
        break;
      case "Frame":
        element.Frame = ExtractIntDetails(attribute, component, scope) ?? 0;
        break;
      case "StartingFrame":
        element.StartingFrame = ExtractIntDetails(attribute, component, scope) ?? 0;
        break;
    }
  }
}
