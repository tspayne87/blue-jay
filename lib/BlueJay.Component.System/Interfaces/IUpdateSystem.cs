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
}
