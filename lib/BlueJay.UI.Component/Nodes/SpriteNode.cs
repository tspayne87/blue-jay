using BlueJay.UI.Component.Elements.Attributes;
using BlueJay.UI.Factories;

namespace BlueJay.UI.Component.Nodes
{
  internal class SpriteNode : Node
  {
    /// <summary>
    /// The named structure as a string attribute
    /// </summary>
    private readonly StringAttribute _assetNameAttr;

    /// <summary>
    /// The frame count for the current sprite node
    /// </summary>
    private readonly StringAttribute _frameCountAttr;

    /// <summary>
    /// The frame tick amount of how long it it should be to switch between frames
    /// </summary>
    private readonly StringAttribute _frameTickAmountAttr;

    /// <summary>
    /// The amount of columns that exist for this sprite in the sprite sheet
    /// </summary>
    private readonly StringAttribute _colsAttr;

    /// <summary>
    /// The amount of rows that exist for this sprite in the sprite sheet
    /// </summary>
    private readonly StringAttribute _rowsAttr;

    /// <summary>
    /// The current frame to start on for the sprite sheet
    /// </summary>
    private readonly StringAttribute _frameAttr;

    /// <summary>
    /// Constructor to build out the texture node from the scope and attributes attached to this node
    /// </summary>
    /// <param name="scope">The node scope we are working with</param>
    /// <param name="attributes">The current attributes attached to this node</param>
    /// <exception cref="ArgumentNullException">Will return null exception if the 'AssetName' is not set as an attribute</exception>
    public SpriteNode(NodeScope scope, List<UIElementAttribute> attributes)
      : base(scope, attributes)
    {
      var assetNameAttr = attributes.FirstOrDefault(x => x.Name.Equals("AssetName", StringComparison.OrdinalIgnoreCase)) as StringAttribute;
      var frameCountAttr = attributes.FirstOrDefault(x => x.Name.Equals("FrameCount", StringComparison.OrdinalIgnoreCase)) as StringAttribute;
      var frameTickAmountAttr = attributes.FirstOrDefault(x => x.Name.Equals("FrameTickAmount", StringComparison.OrdinalIgnoreCase)) as StringAttribute;
      var colsAttr = attributes.FirstOrDefault(x => x.Name.Equals("Cols", StringComparison.OrdinalIgnoreCase)) as StringAttribute;
      var rowsAttr = attributes.FirstOrDefault(x => x.Name.Equals("Rows", StringComparison.OrdinalIgnoreCase)) as StringAttribute;
      var frameAttr = attributes.FirstOrDefault(x => x.Name.Equals("Frame", StringComparison.OrdinalIgnoreCase)) as StringAttribute;

      if (assetNameAttr == null)
        throw new ArgumentNullException("AssetName", "Does not exist as an attribute on the element");
      if (frameCountAttr == null)
        throw new ArgumentNullException("FrameCount", "Does not exist as an attribute on the element");
      if (frameTickAmountAttr == null)
        throw new ArgumentNullException("FrameTickAmount", "Does not exist as an attribute on the element");
      if (colsAttr == null)
        throw new ArgumentNullException("Cols", "Does not exist as an attribute on the element");

      _assetNameAttr = assetNameAttr;
      _frameCountAttr = frameCountAttr;
      _frameTickAmountAttr = frameTickAmountAttr;
      _colsAttr = colsAttr;
      _rowsAttr = rowsAttr ?? new StringAttribute("Frame", "1"); ;
      _frameAttr = frameAttr ?? new StringAttribute("Frame", "0");
    }

    /// <inheritdoc />
    protected override List<UIEntity>? AddEntity(Style style, UIEntity? parent, Dictionary<string, object>? scope)
    {
      if (
        int.TryParse(_frameCountAttr.Value, out var frameCount)
        && int.TryParse(_frameTickAmountAttr.Value, out var frameTickAmount)
        && int.TryParse(_colsAttr.Value, out var colsAttr)
        && int.TryParse(_rowsAttr.Value, out var rowsAttr)
        && int.TryParse(_frameAttr.Value, out var frameAttr)
      )
      {

        var entity = Scope.ServiceProvider.AddUISprite(_assetNameAttr.Value, frameCount, frameTickAmount, colsAttr, rowsAttr, frameAttr, style, parent?.Entity);
        return new List<UIEntity>() { CreateUIElement(entity) };
      }
      throw new Exception();
    }
  }
}
