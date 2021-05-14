using BlueJay.Component.System.Collections;
using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using System;

namespace BlueJay.UI.EventListeners.Viewport
{
  /// <summary>
  /// Event listener will watch for changes in the viewport and do basic updates to the starting of the bounds of each
  /// UI entity
  /// </summary>
  public class UIBoundsViewportChangeListener : EventListener<ViewportChangeEvent>
  {
    /// <summary>
    /// The layer collection that we need to iterate over to process each entity to determine what the bounds will be set as
    /// </summary>
    private readonly LayerCollection _layers;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    public UIBoundsViewportChangeListener(LayerCollection layers)
    {
      _layers = layers;
    }

    /// <summary>
    /// The event that we should be processing when it is triggered
    /// </summary>
    /// <param name="evt">The current event object that was triggered</param>
    public override void Process(IEvent<ViewportChangeEvent> evt)
    {
      for (var i = 0; i < _layers[UIStatic.LayerName].Entities.Count; ++i)
      {
        ProcessEntity(_layers[UIStatic.LayerName].Entities[i], evt.Data);
      }
    }

    /// <summary>
    /// Process method is meant to update the bounds to a specific entity
    /// </summary>
    /// <param name="entity">The entity we are processing</param>
    /// <param name="evt">The event that triggered the change to the UI</param>
    private void ProcessEntity(IEntity entity, ViewportChangeEvent evt)
    {
      var la = entity.GetAddon<LineageAddon>();
      var sa = entity.GetAddon<StyleAddon>();
      var psa = la.Parent?.GetAddon<StyleAddon>();

      var pWidth = (psa?.CalculatedBounds.Width ?? evt.Current.Width) - ((psa?.CurrentStyle.Padding ?? 0) * 2);
      var pHeight = (psa?.CalculatedBounds.Height ?? evt.Current.Height) - ((psa?.CurrentStyle.Padding ?? 0) * 2);

      // Process Height Properties
      if (sa.CurrentStyle.Height != null) sa.CalculatedBounds.Height = sa.CurrentStyle.Height.Value;
      else if (sa.CurrentStyle.HeightPercentage != null) sa.CalculatedBounds.Height = (int)Math.Floor(pHeight * sa.CurrentStyle.HeightPercentage.Value);

      // Process Width Properties
      if (sa.CurrentStyle.Width != null) sa.CalculatedBounds.Width = sa.CurrentStyle.Width.Value;
      else if (sa.CurrentStyle.WidthPercentage != null) sa.CalculatedBounds.Width = (int)Math.Floor(pWidth * sa.CurrentStyle.WidthPercentage.Value);

      // Calculate against the nine patch
      if (sa.CurrentStyle.NinePatch != null)
      {
        // Add some pixels to fix the mod before rendering the texture so we have a seemless pattern without clipping
        var widthMod = sa.CalculatedBounds.Width % sa.CurrentStyle.NinePatch.Break.X;
        if (widthMod != 0)
          sa.CalculatedBounds.Width += sa.CurrentStyle.NinePatch.Break.X - widthMod;

        // Add some pixels to fix the mod before rendering the texture so we have a seemless patern without clipping
        var heightMod = sa.CalculatedBounds.Height % sa.CurrentStyle.NinePatch.Break.Y;
        if (heightMod != 0)
          sa.CalculatedBounds.Height += sa.CurrentStyle.NinePatch.Break.X - heightMod;
      }

      // Process Top Offset Properties
      if (sa.CurrentStyle.TopOffset != null) sa.CalculatedBounds.Y = sa.CurrentStyle.TopOffset.Value;
      else
      {
        switch (sa.CurrentStyle.VerticalAlign)
        {
          case VerticalAlign.Center:
            sa.CalculatedBounds.Y = (pHeight - sa.CalculatedBounds.Height) / 2;
            break;
          case VerticalAlign.Bottom:
            sa.CalculatedBounds.Y = pHeight - sa.CalculatedBounds.Height;
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
            sa.CalculatedBounds.X = (pWidth - sa.CalculatedBounds.Width) / 2;
            break;
          case HorizontalAlign.Right:
            sa.CalculatedBounds.X = pWidth - sa.CalculatedBounds.Width;
            break;
        }
      }
    }
  }
}
