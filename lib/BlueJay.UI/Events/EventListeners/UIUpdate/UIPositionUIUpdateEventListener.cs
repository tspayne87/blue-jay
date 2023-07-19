using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Events.EventListeners.UIUpdate
{
  internal class UIPositionUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The layer collection that we need to iterate over to process each entity to determine what the bounds will be set as
    /// </summary>
    private readonly ILayerCollection _layers;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    public UIPositionUIUpdateEventListener(ILayerCollection layers)
    {
      _layers = layers;
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<UIUpdateEvent> evt)
    {
      if (_layers.Contains(UIStatic.LayerName))
      {
        var layer = _layers[UIStatic.LayerName];
        if (layer != null)
          foreach (var entity in layer.AsSpan())
            ProcessEntity(entity, evt.Data);
      }
    }

    /// <summary>
    /// Process method is meant to process the width for each entity
    /// </summary>
    /// <param name="entity">The entity we are processing</param>
    /// <param name="evt">The event that triggered the change to the UI</param>
    private void ProcessEntity(IEntity entity, UIUpdateEvent evt)
    {
      /// Load addons needed to process different style and linage
      var la = entity.GetAddon<LineageAddon>();
      var sa = entity.GetAddon<StyleAddon>();
      var psa = la.Parent?.GetAddon<StyleAddon>();
      var pla = la.Parent?.GetAddon<LineageAddon>();

      /// The parent grid column style
      var pGridColumn = psa?.CurrentStyle.GridColumns ?? 1;

      /// The parent column gap style
      var pGap = psa?.CurrentStyle.ColumnGap ?? Point.Zero;

      /// The column offset for this item
      var offset = Math.Min(sa.CurrentStyle.ColumnOffset, pGridColumn);

      /// The current span of this
      var span = Math.Min(sa.CurrentStyle.ColumnSpan, pGridColumn);

      /// The width of the current box this item is bound to
      var pWidth = (psa?.CalculatedBounds.Width ?? evt.Size.Width) - ((psa?.CurrentStyle.Padding ?? 0) * 2);

      /// The height of the current box this item is bound to
      var pHeight = (psa?.CalculatedBounds.Height ?? evt.Size.Height) - ((psa?.CurrentStyle.Padding ?? 0) * 2);

      /// The current width of each box based on each column in the parents grid
      var cWidth = (pWidth - ((pGridColumn - 1) * pGap.X)) / pGridColumn;

      /// The full width of this element based on the width of each box in the grid and the gaps that it should span
      var fWidth = (cWidth * span) + ((span - 1) * pGap.X);

      if (sa.CurrentStyle.Position != Position.Absolute && sa.CurrentStyle.TopOffset == null)
      {
        /// Calculate grid position of the element we are working with so we know where to put the element
        var index = pla?.Children.FindIndex(x => x == entity) ?? -1;

        /// The maxium height that exists for each element in the grid row
        var maxHeight = 0;

        /// The current y position of each element
        var y = 0;
        for (var i = 0; i <= index; ++i)
        {
          /// Get the siblings style component to get details about it
          var sba = pla?.Children[i].GetAddon<StyleAddon>();

          if (sba != null)
          {
            if (y != sba.Value.GridPosition.Y)
            { /// If we are jumping into a new row on the grid we want to calculate the next y position based on the highest column in this row
              sa.CalculatedBounds.Y += maxHeight + pGap.Y;

              /// Reset the maxium height
              maxHeight = 0;

              /// Set the new y position
              y = sba.Value.GridPosition.Y;
            }

            /// Set the max height so we know where to go for the next y position
            maxHeight = Math.Max(maxHeight, sba.Value.CalculatedBounds.Height);
          }
        }

        /// Determine the x coordinate based on its pre-calculated grid position <see cref="UIGridCalculationUIUpdateEventListener" />
        sa.CalculatedBounds.X += (cWidth * sa.GridPosition.X) + (sa.GridPosition.X * pGap.X);
      }

      /// If a top offset is used we want to ignore all other calculates since this is where the item should be in the
      /// parents scope
      if (sa.CurrentStyle.TopOffset != null) sa.CalculatedBounds.Y = sa.CurrentStyle.TopOffset.Value;
      else
      { /// We want to do extra processing if we need to process the y position of this element to the center of its bounds height
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

      /// If a left offset is used we want to ignore all other calculates since this is where the item should be in the
      /// parents scope
      if (sa.CurrentStyle.LeftOffset != null) sa.CalculatedBounds.X = sa.CurrentStyle.LeftOffset.Value;
      else
      {/// We want to do extra processing if we need to process the x position of this element to the center of its bounds width
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
