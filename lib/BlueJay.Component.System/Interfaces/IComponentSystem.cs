namespace BlueJay.Component.System.Interfaces
{
  public interface IComponentSystem
  {
    /// <summary>
    /// The current addon key that is meant to act as a selector for the Draw/Update
    /// methods with entities
    /// </summary>
    long Key { get; }

    /// <summary>
    /// The draw event that is called before all entitiy draw events for this system
    /// </summary>
    /// <param name="delta">The current delta for this frame</param>
    void OnDraw(int delta);

    /// <summary>
    /// The draw event that is called for each entity that was selected by the key
    /// for this system
    /// </summary>
    /// <param name="delta">The current delta for this frame</param>
    /// <param name="entity">The current entity that should be drawn</param>
    void OnDraw(int delta, IEntity entity);

    /// <summary>
    /// Initialization event that should be called once for this system to initialize
    /// things before moving forward
    /// </summary>
    void OnInitialize();

    /// <summary>
    /// The updat event that is called before all entity update events for this system
    /// </summary>
    /// <param name="delta">The current delta for this frame</param>
    void OnUpdate(int delta);

    /// <summary>
    /// The update event that is called for eeach entity that was selected by the key
    /// for this system.
    /// </summary>
    /// <param name="delta">The current delta for this frame</param>
    /// <param name="entity">The current entity that should be updated</param>
    void OnUpdate(int delta, IEntity entity);
  }
}
