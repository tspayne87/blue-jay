using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using BlueJay.Events;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BlueJay.UI.Systems
{
  /// <summary>
  /// UI style bounds trigger system is meant to trigger a change if the calculated ui systems change the bounds
  /// </summary>
  public class UIStyleBoundsTriggerSystem : ComponentSystem
  {
    /// <summary>
    /// The current event queue that is being processed
    /// </summary>
    private readonly EventQueue _eventQueue;

    /// <summary>
    /// The key that we should filter on for this system to work with
    /// </summary>
    public override long Key => BoundsAddon.Identifier | StyleAddon.Identifier;

    /// <summary>
    /// The layers this system should be working on
    /// </summary>
    public override List<string> Layers => new List<string>() { UIStatic.LayerName };

    /// <summary>
    /// Constructor to build out the UI style bounds trigger system
    /// </summary>
    /// <param name="eventQueue">The event queue since this system will update send out events to the system</param>
    public UIStyleBoundsTriggerSystem(EventQueue eventQueue)
    {
      _eventQueue = eventQueue;
    }

    /// <summary>
    /// Update event that will process the bounds object based on the styles given
    /// </summary>
    /// <param name="entity">The entity we are processing</param>
    public override void OnUpdate(IEntity entity)
    {
      var ba = entity.GetAddon<BoundsAddon>();
      var sa = entity.GetAddon<StyleAddon>();

      if (ba.Bounds != sa.CalculatedBounds)
      {
        ba.Bounds = sa.CalculatedBounds;
        _eventQueue.DispatchEvent(new StyleUpdateEvent(entity));
      }
      sa.CalculatedBounds = Rectangle.Empty;
    }
  }
}
