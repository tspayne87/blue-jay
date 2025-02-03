using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Events.Lifecycle;

namespace BlueJay.EventListeners
{
  /// <summary>
  /// Main lifecycle listener to hook in the component system into the event system
  /// </summary>
  internal class UpdateEventListener : EventListener<UpdateEvent>
  {
    /// <summary>
    /// The current system being processed
    /// </summary>
    private readonly ISystem _system;

    /// <summary>
    /// Constructor to build out the update event
    /// </summary>
    /// <param name="system">The current system being processed</param>
    public UpdateEventListener(ISystem system)
    {
      _system = system;
    }

    /// <summary>
    /// Process method is meant to handle the update event and send that update to all systems in the system
    /// </summary>
    /// <param name="evt">The current update event we are working with</param>
    public override void Process(IEvent<UpdateEvent> evt)
    {
      // Call On update if system has an on update event
      if (_system is IUpdateSystem)
        ((IUpdateSystem)_system).OnUpdate();
    }
  }
}
