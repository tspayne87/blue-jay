namespace BlueJay.Component.System.Interfaces
{
  /// <summary>
  /// Update system is meant to trigger an event update
  /// </summary>
  public interface IUpdateSystem : ISystem
  {
    /// <summary>
    /// The update event that is called before all entity update events for this system
    /// </summary>
    void OnUpdate();
  }

  /// <summary>
  /// Entity update system that is meant to trigger an update for each entity in the key
  /// </summary>
  public interface IUpdateEntitySystem : ISystem
  {
    /// <summary>
    /// The update event that is called for eeach entity that was selected by the key
    /// for this system.
    /// </summary>
    /// <param name="entity">The current entity that should be updated</param>
    void OnUpdate(IEntity entity);
  }

  /// <summary>
  /// The update interface that has the OnEnd Update method assigned to it
  /// </summary>
  public interface IUpdateEndSystem : ISystem
  {
    /// <summary>
    /// The update event that is called after all entity update events for this system
    /// </summary>
    void OnUpdateEnd();
  }
}
