namespace BlueJay.Component.System.Interfaces
{
  /// <summary>
  /// The building blocks for the entities to describe what they are and how they
  /// should interact with other entities
  /// </summary>
  public interface IAddon
  {
    /// <summary>
    /// Identifier is meant to determine the id of this addon that will be used
    /// in the system
    /// </summary>
    long Identifier { get; }

    /// <summary>
    /// Method is called when the addon is added to an entity
    /// </summary>
    void OnAdd();

    /// <summary>
    /// Method is called when the added needs to be loaded before being processed for the scene
    /// </summary>
    void OnLoad();

    /// <summary>
    /// Method is called when this addon is removed from the entity
    /// </summary>
    void OnRemove();
  }
}
