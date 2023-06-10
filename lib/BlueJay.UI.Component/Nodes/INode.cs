namespace BlueJay.UI.Component.Nodes
{
  /// <summary>
  /// The system is broken up into UIComponents, Nodes, and Entities that will be
  /// genreated by the UI ECS
  /// </summary>
  public interface INode
  {
    /// <summary>
    /// The UIComponent that is bound to this node
    /// </summary>
    internal UIComponent UIComponent { get; }

    /// <summary>
    /// Generates the node and all attached node for the component
    /// </summary>
    /// <param name="globalStyle">The global style for this node</param>
    public void GenerateUI(Style? globalStyle = null);

    internal void Initialize(UIComponent? parent = null);
  }
}
