﻿using BlueJay.Shared.Games.Breakout.Addons;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI;
using Microsoft.Xna.Framework;
using BlueJay.Common.Addons;

namespace BlueJay.Shared.Games.Breakout.EventListeners
{
  /// <summary>
  /// The stretch bounds system is meant to move and stretch bounds in the system to make it look
  /// like the screen is the whole game
  /// </summary>
  public class UpdateBoundsEventListener : EventListener<UpdateBoundsEvent>
  {
    /// <summary>
    /// All the entities that exist in the game
    /// </summary>
    private readonly IQuery _query;

    /// <summary>
    /// The current event queue that is being processed
    /// </summary>
    private readonly IEventQueue _eventQueue;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    public UpdateBoundsEventListener(IEventQueue eventQueue, IQuery query)
    {
      _eventQueue = eventQueue;
      _query = query.ExcludeLayer(UIStatic.LayerName);
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<UpdateBoundsEvent> evt)
    {
      foreach (var entity in _query)
        ProcessEntity(entity, evt.Data);
    }

    /// <summary>
    /// The update event that is called for eeach entity that was selected by the key
    /// for this system.
    /// </summary>
    /// <param name="entity">The current entity that should be updated</param>
    public void ProcessEntity(IEntity entity, UpdateBoundsEvent evt)
    {
      var ta = entity.GetAddon<TypeAddon>();
      var ba = entity.GetAddon<BoundsAddon>();

      switch (ta.Type)
      {
        case EntityType.Block:
          { // We want to reshape the blocks to fit the screen
            var bia = entity.GetAddon<BlockIndexAddon>();
            var size = new Size((evt.Size.Width - (BlockConsts.Padding * (BlockConsts.Amount + 1))) / BlockConsts.Amount, evt.Size.Height / 15);
            var position = new Point((bia.Index % BlockConsts.Amount) * (size.Width + BlockConsts.Padding) + BlockConsts.Padding, (bia.Index / BlockConsts.Amount) * (size.Height + BlockConsts.Padding) + BlockConsts.TopOffset);
            ba.Bounds = new Rectangle(position, size.ToPoint());
            entity.Update(ba);
          }
          break;
        case EntityType.Paddle:
          { // We want to reshape the paddle to fit the screen
            var size = new Size(evt.Size.Width / 7, 20);
            var position = new Point(ba.Bounds.X, evt.Size.Height - (evt.Size.Height / 10));
            ba.Bounds = new Rectangle(position, size.ToPoint());
            entity.Update(ba);
          }
          break;
      }
    }
  }
}
