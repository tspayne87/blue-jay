using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using System;

namespace BlueJay.UI.EventListeners.UIUpdate
{
  public class UIGridCalculationUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The layer collection that we need to iterate over to process each entity to determine what the bounds will be set as
    /// </summary>
    private readonly LayerCollection _layers;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    public UIGridCalculationUIUpdateEventListener(LayerCollection layers)
    {
      _layers = layers;
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<UIUpdateEvent> evt)
    {
      for (var i = 0; i < _layers[UIStatic.LayerName].Entities.Count; ++i)
      {
        ProcessEntity(_layers[UIStatic.LayerName].Entities[i]);
      }
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
          if (sba.Value.CurrentStyle.Position == Position.Absolute) continue;

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
