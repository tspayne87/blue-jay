using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;

namespace BlueJay.UI.Events.EventListeners.UIUpdate
{
  internal class UICalculateHeightUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The current layer of entities that we are working with
    /// </summary>
    private readonly IQuery _query;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="query">The current layer of entities that we are working with</param>
    public UICalculateHeightUIUpdateEventListener(IQuery query)
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
        ProcessEntity(entity, evt.Data);
    }

    /// <summary>
    /// Process method is meant to process the height for each entity
    /// </summary>
    /// <param name="entity">The entity we are processing</param>
    /// <param name="evt">The event that triggered the change to the UI</param>
    private void ProcessEntity(IEntity entity, UIUpdateEvent evt)
    {
      /// Load addons
      var sa = entity.GetAddon<StyleAddon>();
      var la = entity.GetAddon<LineageAddon>();
      var psa = la.Parent?.GetAddon<StyleAddon>();

      /// Calculate the parents width/height
      var pHeight = (psa?.CalculatedBounds.Height ?? evt.Size.Height) - (psa?.CurrentStyle.Padding?.TopBottom ?? 0);

      // Process Height Properties
      if (sa.CurrentStyle.Height != null) sa.CalculatedBounds.Height = sa.CurrentStyle.Height.Value;
      else if (sa.CurrentStyle.HeightPercentage != null) sa.CalculatedBounds.Height = (int)Math.Floor(pHeight * sa.CurrentStyle.HeightPercentage.Value);

      entity.Update(sa);
    }
  }
}
