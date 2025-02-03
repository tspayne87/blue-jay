using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Events.EventListeners.UIUpdate
{
  /// <summary>
  /// Template trigger is meant to update the height template details based on the type of height template we want to use
  /// </summary>
  internal class UITemplateUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The current layer of entities that we are working with
    /// </summary>
    private readonly IQuery _query;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="query">The current layer of entities that we are working with</param>
    public UITemplateUIUpdateEventListener(IQuery query)
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
        ProcessStretchEntity(entity, evt.Data);
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
          var pHeight = (psa?.CalculatedBounds.Height ?? evt.Size.Height) - (psa?.CurrentStyle.Padding?.TopBottom ?? 0);

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
