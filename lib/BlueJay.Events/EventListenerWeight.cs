using BlueJay.Events.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Events
{
  /// <summary>
  /// Private object meant to handle the different weights for listeners
  /// </summary>
  internal class EventListenerWeight
  {
    /// <summary>
    /// The event listener that should be used
    /// </summary>
    public IEventListener EventListener { get; private set; }

    /// <summary>
    /// The order event listeners will be handled in from lowest to highest
    /// </summary>
    public int Weight { get; private set; }

    /// <summary>
    /// Constructor meant to be a builder for the object
    /// </summary>
    /// <param name="eventListener">The event listener that should be used</param>
    /// <param name="weight">The order event listeners will be handled in from lowest to highest</param>
    public EventListenerWeight(IEventListener eventListener, int weight)
    {
      EventListener = eventListener;
      Weight = weight;
    }
  }
}
