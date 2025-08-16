using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Container;
using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Factories;
using BlueJay.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace BlueJay.UI.Component.Nodes
{
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
      var entity = Scope.ServiceProvider.AddUITexture(GetOptions(component, scope), style, parent?.Entity);
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
            callbacks.Add(item.Subscribe(evt => UpdateEntity(entity, content, component, scope, attribute.Value, attribute.Key)));
          }
        }
      }

      return new List<UIEntity>() { CreateUIElement(entity, callbacks) };
    }

    /// <summary>
    /// Gets the options for creating the texture node
    /// </summary>
    /// <param name="component">The current UIComponent that is using this texture node</param>
    /// <param name="scope">The current scope for this node</param>
    /// <returns>Will return the options for creating the texture node</returns>
    private UITextureOptions GetOptions(UIComponent component, Dictionary<string, object>? scope)
    {
      return new UITextureOptions(ExtractStringDetails(_attributes["AssetName"], component, scope))
      {
        FrameCount = ExtractIntDetails(_attributes["FrameCount"], component, scope),
        FrameTickAmount = ExtractIntDetails(_attributes["FrameTickAmount"], component, scope),
        Frame = ExtractIntDetails(_attributes["Frame"], component, scope),
        StartingFrame = ExtractIntDetails(_attributes["StartingFrame"], component, scope),
        Rows = ExtractIntDetails(_attributes["Rows"], component, scope),
        Columns = ExtractIntDetails(_attributes["Cols"], component, scope)
      };
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

    public void UpdateEntity(IEntity entity, IContentManagerContainer content, UIComponent component, Dictionary<string, object>? scope, UIElementAttribute attribute, string attributeName)
    {
      switch (attributeName)
      {
        case "AssetName":
          {
            var assetName = ExtractStringDetails(attribute, component, scope);
            if (assetName == null)
              throw new ArgumentNullException("AssetName", "Cannot be null when updating the texture entity");

            var ta = entity.GetAddon<TextureAddon>();
            ta.Texture = content.Load<ITexture2DContainer>(assetName);
            entity.Update(ta);
            TriggerUIUpdate();
          }
          break;
        case "Rows":
          {
            var ta = entity.GetAddon<SpriteSheetAddon>();
            ta.Rows = ExtractIntDetails(attribute, component, scope) ?? 1;
            entity.Update(ta);
            TriggerUIUpdate();
          }
          break;
        case "Cols":
          {
            var ta = entity.GetAddon<SpriteSheetAddon>();
            ta.Cols = ExtractIntDetails(attribute, component, scope) ?? 1;
            entity.Update(ta);
            TriggerUIUpdate();
          }
          break;
        case "FrameCount":
          {
            var ta = entity.GetAddon<FrameAddon>();
            ta.FrameCount = ExtractIntDetails(attribute, component, scope) ?? 1;
            entity.Update(ta);
            TriggerUIUpdate();
          }
          break;
        case "FrameTickAmount":
          {
            var ta = entity.GetAddon<FrameAddon>();
            ta.FrameTickAmount = ExtractIntDetails(attribute, component, scope) ?? 0;
            entity.Update(ta);
            TriggerUIUpdate();
          }
          break;
        case "Frame":
          {
            var ta = entity.GetAddon<FrameAddon>();
            ta.Frame = ExtractIntDetails(attribute, component, scope) ?? 0;
            entity.Update(ta);
            TriggerUIUpdate();
          }
          break;
        case "StartingFrame":
          {
            var ta = entity.GetAddon<FrameAddon>();
            ta.StartingFrame = ExtractIntDetails(attribute, component, scope) ?? 0;
            entity.Update(ta);
            TriggerUIUpdate();
          }
          break;
      }
    }
  }
}
