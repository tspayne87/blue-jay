using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Factories;

namespace BlueJay.UI.Component.Nodes
{
  /// <summary>
  /// The texture node meant to create a space for a textured entity to the UI structure
  /// </summary>
  internal class TextureNode : Node
  {
    /// <summary>
    /// The named structure as a string attribute
    /// </summary>
    private readonly StringAttribute _assetNameAttr;

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

      _assetNameAttr = attr;
    }

    /// <inheritdoc />
    protected override List<UIEntity>? AddEntity(Style style, UIEntity? parent, Dictionary<string, object>? scope)
    {
      return new List<UIEntity>() { CreateUIElement(Scope.ServiceProvider.AddUITexture(_assetNameAttr.Value, style, parent?.Entity)) };
    }
  }
}
