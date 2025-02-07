﻿using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Events.EventListeners.UIUpdate
{
  internal class UIGridCalculationUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The current layer of entities that we are working with
    /// </summary>
    private readonly IQuery _query;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="query">The current layer of entities that we are working with</param>
    public UIGridCalculationUIUpdateEventListener(IQuery query)
    {
      _query = query.WhereLayer(UIStatic.LayerName);
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<UIUpdateEvent> evt)
    {
      foreach (var entity in _query)
        ProcessEntity(entity);
    }

    /// <summary>
    /// Process method is meant to update the bounds to a specific entity
    /// </summary>
    /// <param name="entity">The entity we are processing</param>
    private void ProcessEntity(IEntity entity)
    {
      var la = entity.GetAddon<LineageAddon>();
      var sa = entity.GetAddon<StyleAddon>();
      var pla = la.Parent?.GetAddon<LineageAddon>();
      var psa = la.Parent?.GetAddon<StyleAddon>();

      var pos = Point.Zero;
      if (pla != null && psa != null)
      {
        var index = pla?.Children.IndexOf(entity) ?? -1;
        for (var i = 0; i <= index; ++i)
        {
          var sba = pla?.Children[i].GetAddon<StyleAddon>();
          if (sba == null || sba.Value.CurrentStyle.Position == Position.Absolute) continue;

          pos.X += Math.Min(sba.Value.CurrentStyle.ColumnOffset, psa.Value.CurrentStyle.GridColumns);
          if (pos.X >= psa.Value.CurrentStyle.GridColumns)
          {
            pos.X -= psa.Value.CurrentStyle.GridColumns;
            pos.Y++;
          }

          var span = Math.Min(sba.Value.CurrentStyle.ColumnSpan, psa.Value.CurrentStyle.GridColumns);
          if (i != index)
          {
            pos.X += span;
            if (pos.X > psa.Value.CurrentStyle.GridColumns)
            {
              pos.X = span;
              pos.Y++;
            }
          }
          else
          {
            if (pos.X + span > psa.Value.CurrentStyle.GridColumns)
            {
              pos.X = 0;
              pos.Y++;
            }
          }
        }
      }

      sa.GridPosition = pos;
      entity.Update(sa);
    }
  }
}
