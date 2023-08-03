using BlueJay.UI.Component.Nodes;

namespace BlueJay.UI.Component.Events
{
  /// <summary>
  /// Internal event meant to handle updating the UIEntity weight when new entity
  /// is added to the internal tree structure
  /// </summary>
  internal class UpdateNodeWeight
  {
    /// <summary>
    /// The node that exists in the shadow UI tree, that contains all the UI Entities
    /// </summary>
    public Node Node { get; }

    /// <summary>
    /// Constructor to build out the event that should be triggered
    /// </summary>
    /// <param name="node">The node that exists in the shadow UI tree, that contains all the UI Entities</param>
    public UpdateNodeWeight(Node node)
    {
      Node = node;
    }
  }
}
