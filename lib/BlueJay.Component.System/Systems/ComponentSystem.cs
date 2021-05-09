using BlueJay.Component.System.Interfaces;
using System.Collections.Generic;

namespace BlueJay.Component.System.Systems
{
  /// <summary>
  /// The basic component system that sets up overridable methods without having to
  /// implement all of them in the system class
  /// </summary>
  public abstract class ComponentSystem : IComponentSystem
  {
    /// <summary>
    /// The current addon key that is meant to act as a selector for the Draw/Update
    /// methods with entities
    /// </summary>
    public abstract long Key { get; }

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public abstract List<string> Layers { get; }

    /// <summary>
    /// The draw event that is called before all entitiy draw events for this system
    /// </summary>
    public virtual void OnDraw() { }

    /// <summary>
    /// The draw event that is called for each entity that was selected by the key
    /// for this system
    /// </summary>
    /// <param name="entity">The current entity that should be drawn</param>
    public virtual void OnDraw(IEntity entity) { }

    /// <summary>
    /// Initialization event that should be called once for this system to initialize
    /// things before moving forward
    /// </summary>
    public virtual void OnInitialize() { }

    /// <summary>
    /// The update event that is called before all entity update events for this system
    /// </summary>
    public virtual void OnUpdate() { }

    /// <summary>
    /// The update event that is called for eeach entity that was selected by the key
    /// for this system.
    /// </summary>
    /// <param name="entity">The current entity that should be updated</param>
    public virtual void OnUpdate(IEntity entity) { }
  }
}
