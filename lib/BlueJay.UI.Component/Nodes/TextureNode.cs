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
    /// The current amount of frames this texture has
    /// </summary>
    private readonly StringAttribute? _frameCountAttr;

    /// <summary>
    /// The amount of time in milliseconds that the frames should progress to generate an animation
    /// </summary>
    private readonly StringAttribute? _frameTickAmountAttr;

    /// <summary>
    /// The starting frame position
    /// </summary>
    private readonly StringAttribute? _frameAttr;

    /// <summary>
    /// The starting frame for the texture
    /// </summary>
    private readonly StringAttribute? _startingFrameAttr;

    /// <summary>
    /// The columns that should exist for the sprite sheet
    /// </summary>
    private readonly StringAttribute? _colsAttr;

    /// <summary>
    /// The rows that should be used for the sprite sheet
    /// </summary>
    private readonly StringAttribute? _rowsAttr;

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
      _frameCountAttr = attributes.FirstOrDefault(x => x.Name.Equals("FrameCount", StringComparison.OrdinalIgnoreCase)) as StringAttribute;
      _frameTickAmountAttr = attributes.FirstOrDefault(x => x.Name.Equals("FrameTickAmount", StringComparison.OrdinalIgnoreCase)) as StringAttribute;
      _frameAttr = attributes.FirstOrDefault(x => x.Name.Equals("Frame", StringComparison.OrdinalIgnoreCase)) as StringAttribute;
      _startingFrameAttr = attributes.FirstOrDefault(x => x.Name.Equals("StartingFrame", StringComparison.OrdinalIgnoreCase)) as StringAttribute;
      _rowsAttr = attributes.FirstOrDefault(x => x.Name.Equals("Rows", StringComparison.OrdinalIgnoreCase)) as StringAttribute;
      _colsAttr = attributes.FirstOrDefault(x => x.Name.Equals("Cols", StringComparison.OrdinalIgnoreCase)) as StringAttribute;
    }

    /// <inheritdoc />
    protected override List<UIEntity>? AddEntity(Style style, UIEntity? parent, Dictionary<string, object>? scope)
    {
      var options = new UITextureOptions(_assetNameAttr.Value)
      {
        FrameCount = _frameCountAttr == null ? null : int.Parse(_frameCountAttr!.Value),
        FrameTickAmount = _frameTickAmountAttr == null ? null : int.Parse(_frameTickAmountAttr!.Value),
        Frame = _frameAttr == null ? null : int.Parse(_frameAttr!.Value),
        StartingFrame = _startingFrameAttr == null ? null : int.Parse(_startingFrameAttr!.Value),
        Rows = _rowsAttr == null ? null : int.Parse(_rowsAttr!.Value),
        Columns = _colsAttr == null ? null : int.Parse(_colsAttr!.Value)
      };
      return new List<UIEntity>() { CreateUIElement(Scope.ServiceProvider.AddUITexture(options, style, parent?.Entity)) };
    }
  }
}
