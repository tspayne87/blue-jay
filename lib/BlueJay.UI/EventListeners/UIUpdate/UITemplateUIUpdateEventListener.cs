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
  /// <summary>
  /// Template trigger is meant to update the height template details based on the type of height template we want to use
  /// </summary>
  public class UITemplateUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The layer collection that we need to iterate over to process each entity to determine what the bounds will be set as
    /// </summary>
    private readonly LayerCollection _layers;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    public UITemplateUIUpdateEventListener(LayerCollection layers)
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
        ProcessStretchEntity(_layers[UIStatic.LayerName].Entities[i], evt.Data);
      }
    }

    /// <summary>
    /// Helper method is meant to handle stretch entities on the vertical axis
    /// </summary>
    /// <param name="entity">The current entity being processed</param>
    /// <param name="evt">The event to process the current width/height we are working with in the global scope</param>
    private void ProcessStretchEntity(IEntity entity, UIUpdateEvent evt)
    {
      var sa = entity.GetAddon<StyleAddon>();
      var la = entity.GetAddon<LineageAddon>();

      if (sa.CurrentStyle.HeightTemplate == HeightTemplate.Stretch)
      {
        var psa = la.Parent?.GetAddon<StyleAddon>();
        var pla = la.Parent?.GetAddon<LineageAddon>();
        if (psa != null && pla != null)
        {
          var pHeight = (psa?.CalculatedBounds.Height ?? evt.Size.Height) - ((psa?.CurrentStyle.Padding ?? 0) * 2);

          var diviser = 0;
          var maxHeight = 0;
          var height = 0;
          var pos = 0;
          for (var i = 0; i < pla.Value.Children.Count; ++i)
          {
            var ssa = pla.Value.Children[i].GetAddon<StyleAddon>();
            if (ssa.CurrentStyle.Position == Position.Absolute || !pla.Value.Children[i].Active) continue;

            if (ssa.CurrentStyle.HeightTemplate == HeightTemplate.Stretch)
            {
              diviser++;
            }
            else
            {
              if (pos != ssa.GridPosition.Y)
              {
                height += maxHeight;
                maxHeight = 0;
              }

              maxHeight = Math.Max(maxHeight, ssa.CalculatedBounds.Height);
              pos = ssa.GridPosition.Y;
            }
          }
          height += maxHeight;

          if (diviser != 0)
          {
            var pGap = psa?.CurrentStyle.ColumnGap ?? Point.Zero;

            sa.CalculatedBounds.Height = (pHeight - height - (pos * pGap.X)) / diviser;
            entity.Update(sa);
          }
        }
      }
    }
  }
}
