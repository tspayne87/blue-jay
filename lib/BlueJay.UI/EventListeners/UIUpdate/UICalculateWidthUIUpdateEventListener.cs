using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using System;

namespace BlueJay.UI.EventListeners.UIUpdate
{
  /// <summary>
  /// Event listener that will calculate the width for each UI entity
  /// </summary>
  public class UICalculateWidthUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The layer collection that we need to iterate over to process each entity to determine what the bounds will be set as
    /// </summary>
    private readonly ILayerCollection _layers;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    public UICalculateWidthUIUpdateEventListener(ILayerCollection layers)
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
      var sa = entity.GetAddon<StyleAddon>();

      if (sa.CalculatedBounds.Width == 0)
      {
        /// Load addons
        var la = entity.GetAddon<LineageAddon>();
        var psa = la.Parent?.GetAddon<StyleAddon>();

        /// Get the parents grid columns
        var pGridColumn = psa?.CurrentStyle.GridColumns ?? 1;

        /// Get the parents grid column gap
        var pGap = psa?.CurrentStyle.ColumnGap ?? Point.Zero;

        /// Get the amount of columns this element should span
        var span = Math.Min(sa.CurrentStyle.ColumnSpan, pGridColumn);

        /// Calculate the parents width
        var pWidth = (psa?.CalculatedBounds.Width ?? evt.Size.Width) - ((psa?.CurrentStyle.Padding ?? 0) * 2);

        /// Take the parent width and grid columns to determine the width we should start with
        /// Next we want to get the gaps between the spaces so we have even grid columns between each other
        var cWidth = (pWidth - ((pGridColumn - 1) * pGap.X)) / pGridColumn;

        /// Calculate the field width based on the span of the column and the width of each column
        var fWidth = (cWidth * span) + ((span - 1) * pGap.X);

        // Process Width Properties
        if (sa.CurrentStyle.Width != null) sa.CalculatedBounds.Width = sa.CurrentStyle.Width.Value;
        else if (sa.CurrentStyle.WidthPercentage != null) sa.CalculatedBounds.Width = (int)Math.Floor(fWidth * sa.CurrentStyle.WidthPercentage.Value);
        else sa.CalculatedBounds.Width = fWidth;

        entity.Update(sa);
      }
    }
  }
}
