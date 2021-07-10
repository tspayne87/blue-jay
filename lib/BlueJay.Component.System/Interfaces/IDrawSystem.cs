namespace BlueJay.Component.System.Interfaces
{
  /// <summary>
  /// The draw system that is triggered on the draw event
  /// </summary>
  public interface IDrawSystem : ISystem
  {
    /// <summary>
    /// The draw event that is called before all entitiy draw events for this system
    /// </summary>
    void OnDraw();
  }

  /// <summary>
  /// Entity draw system is meant to draw all the entities that match the key of the system
  /// </summary>
  public interface IDrawEntitySystem : ISystem
  {
    /// <summary>
    /// The draw event that is called for each entity that was selected by the key
    /// for this system
    /// </summary>
    /// <param name="entity">The current entity that should be drawn</param>
    void OnDraw(IEntity entity);
  }

  /// <summary>
  /// The update interface that has the OnEnd Draw method assigned to it
  /// </summary>
  public interface IDrawEndSystem : ISystem
  {
    /// <summary>
    /// The draw event that is called after the other draw calls
    /// </summary>
    void OnDrawEnd();
  }
}
