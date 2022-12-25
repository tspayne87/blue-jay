using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;

namespace BlueJay.UI.Events.EventListeners.UIUpdate
{
  internal class UICalculateHeightUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The layer collection that we need to iterate over to process each entity to determine what the height of each element is
    /// 
    /// NOTE: This event will get triggered twice during the event listener loop once to calculate the first iteration will handle basic
    ///       heights and the second to clean up any missed heights after other calculates may of updated to better heights based on
    ///       content or templating
    /// </summary>
    private readonly ILayerCollection _layers;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    public UICalculateHeightUIUpdateEventListener(ILayerCollection layers)
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
      var pHeight = (psa?.CalculatedBounds.Height ?? evt.Size.Height) - ((psa?.CurrentStyle.Padding ?? 0) * 2);

      // Process Height Properties
      if (sa.CurrentStyle.Height != null) sa.CalculatedBounds.Height = sa.CurrentStyle.Height.Value;
      else if (sa.CurrentStyle.HeightPercentage != null) sa.CalculatedBounds.Height = (int)Math.Floor(pHeight * sa.CurrentStyle.HeightPercentage.Value);

      entity.Update(sa);
    }
  }
}
