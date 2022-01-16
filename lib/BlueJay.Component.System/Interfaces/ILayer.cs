using BlueJay.Component.System.Collections;

namespace BlueJay.Component.System.Interfaces
{
  /// <summary>
  /// The layer that an set of entities are on
  /// </summary>
  public interface ILayer
  {
    /// <summary>
    /// The collection of entities that exist on the layer
    /// </summary>
    IEntityCollection Entities { get; }

    /// <summary>
    /// The current id of the layer
    /// </summary>
    string Id { get; }

    /// <summary>
    /// The current weight of the layer to determine when it will be processed by the system
    /// </summary>
    int Weight { get; }
  }
}
