using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.UI.EventListeners.UIUpdate
{
  public class UIPositionUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The layer collection that we need to iterate over to process each entity to determine what the bounds will be set as
    /// </summary>
    private readonly LayerCollection _layers;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    public UIPositionUIUpdateEventListener(LayerCollection layers)
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
        ProcessEntity(_layers[UIStatic.LayerName].Entities[i], evt.Data);
      }
    }

    /// <summary>
    /// Process method is meant to process the width for each entity
    /// </summary>
    /// <param name="entity">The entity we are processing</param>
    /// <param name="evt">The event that triggered the change to the UI</param>
    private void ProcessEntity(IEntity entity, UIUpdateEvent evt)
    {
      var la = entity.GetAddon<LineageAddon>();
      var sa = entity.GetAddon<StyleAddon>();
      var psa = la.Parent?.GetAddon<StyleAddon>();
      var pla = la.Parent?.GetAddon<LineageAddon>();

      var pGridColumn = psa?.CurrentStyle.GridColumns ?? 1;
      var pGap = psa?.CurrentStyle.ColumnGap ?? Point.Zero;
      var offset = Math.Min(sa.CurrentStyle.ColumnOffset, pGridColumn);
      var span = Math.Min(sa.CurrentStyle.ColumnSpan, pGridColumn);

      var pWidth = (psa?.CalculatedBounds.Width ?? evt.Size.Width) - ((psa?.CurrentStyle.Padding ?? 0) * 2);
      var pHeight = (psa?.CalculatedBounds.Height ?? evt.Size.Height) - ((psa?.CurrentStyle.Padding ?? 0) * 2);

      var cWidth = (pWidth / pGridColumn) - ((pGridColumn - 1) * pGap.X);

      var fWidth = (cWidth * span) + ((span - 1) * pGap.X);

      // Calculate grid position
      var index = pla?.Children.FindIndex(x => x == entity) ?? -1;
      var maxHeight = 0;
      var y = 0;
      for (var i = 0; i <= index; ++i)
      {
        var sba = pla?.Children[i].GetAddon<StyleAddon>();

        if (sba != null)
        {
          if (y != sba.Value.GridPosition.Y)
          {
            sa.CalculatedBounds.Y += maxHeight + pGap.Y;
            maxHeight = 0;
            y = sba.Value.GridPosition.Y;
          }

          maxHeight = Math.Max(maxHeight, sba.Value.CalculatedBounds.Height);
        }
      }

      sa.CalculatedBounds.X += (cWidth * sa.GridPosition.X) + (sa.GridPosition.X * pGap.X);

      if (sa.CurrentStyle.TopOffset != null) sa.CalculatedBounds.Y = sa.CurrentStyle.TopOffset.Value;
      else
      {
        switch (sa.CurrentStyle.VerticalAlign)
        {
          case VerticalAlign.Center:
            sa.CalculatedBounds.Y += (pHeight - sa.CalculatedBounds.Height) / 2;
            break;
          case VerticalAlign.Bottom:
            sa.CalculatedBounds.Y += pHeight - sa.CalculatedBounds.Height;
            break;
        }
      }

      // Process Left Offset Properties
      if (sa.CurrentStyle.LeftOffset != null) sa.CalculatedBounds.X = sa.CurrentStyle.LeftOffset.Value;
      else
      {
        switch (sa.CurrentStyle.HorizontalAlign)
        {
          case HorizontalAlign.Center:
            sa.CalculatedBounds.X += (fWidth - sa.CalculatedBounds.Width) / 2;
            break;
          case HorizontalAlign.Right:
            sa.CalculatedBounds.X += fWidth - sa.CalculatedBounds.Width;
            break;
        }
      }

      entity.Update(sa);
    }
  }
}
