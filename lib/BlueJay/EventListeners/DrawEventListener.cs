using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.Events.Lifecycle;
using System.Runtime.InteropServices;

namespace BlueJay.EventListeners
{
  /// <summary>
  /// Main lifecycle listener to hook into the draw event
  /// </summary>
  internal class DrawEventListener : EventListener<DrawEvent>
  {
    /// <summary>
    /// The list of systems that have been set to be drawable
    /// </summary>
    private readonly DrawableSystemCollection _systems;

    /// <summary>
    /// Constructor to build out the update event
    /// </summary>
    /// <param name="systems">The list of systems that have been set to be drawable</param>
    public DrawEventListener(DrawableSystemCollection systems)
    {
      _systems = systems;
    }

    /// <summary>
    /// Process method is meant to handle the update event and send that update to all systems in the system
    /// </summary>
    /// <param name="evt">The current update event we are working with</param>
    public override void Process(IEvent<DrawEvent> evt)
    {
      foreach (var system in CollectionsMarshal.AsSpan(_systems))
        if (system is IDrawSystem)
          ((IDrawSystem)system).OnDraw();
    }
  }
}
