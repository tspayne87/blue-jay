using BlueJay.Component.System.Interfaces;

namespace BlueJay.UI
{
  /// <summary>
  /// Style update event that is meant to process a new texture based on style changes
  /// </summary>
  public class StyleUpdateEvent
  {
    /// <summary>
    /// The entity we are currently processing
    /// </summary>
    public IEntity Entity { get; private set; }

    /// <summary>
    /// Constructor to build out the entity
    /// </summary>
    /// <param name="entity">The entity we are processing</param>
    public StyleUpdateEvent(IEntity entity)
    {
      Entity = entity;
    }
  }
}
