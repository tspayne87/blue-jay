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
}
