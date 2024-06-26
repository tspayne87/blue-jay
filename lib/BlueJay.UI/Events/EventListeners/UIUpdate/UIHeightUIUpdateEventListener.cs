﻿using BlueJay.Component.System.Interfaces;
using BlueJay.Events;
using BlueJay.Events.Interfaces;
using BlueJay.UI.Addons;

namespace BlueJay.UI.Events.EventListeners.UIUpdate
{
  internal class UIHeightUIUpdateEventListener : EventListener<UIUpdateEvent>
  {
    /// <summary>
    /// The layer collection that we need to iterate over to process each entity to determine what the bounds will be set as
    /// </summary>
    private readonly ILayerCollection _layers;

    /// <summary>
    /// Constructor to injection the layer collection into the listener
    /// </summary>
    /// <param name="layers">The layer collection we are currently working with</param>
    public UIHeightUIUpdateEventListener(ILayerCollection layers)
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
        {
          var collection = layer.AsSpan();
          for (var i = collection.Length - 1; i >= 0; --i)
            ProcessEntity(collection[i]);
        }
      }
    }

    /// <summary>
    /// Process method is meant to process the width for each entity
    /// </summary>
    /// <param name="entity">The entity we are processing</param>
    /// <param name="evt">The event that triggered the change to the UI</param>
    private void ProcessEntity(IEntity entity)
    {
      var sa = entity.GetAddon<StyleAddon>();

      if (sa.CalculatedBounds.Height == 0)
      {
        var la = entity.GetAddon<LineageAddon>();
        var extraHeight = sa.CurrentStyle.Padding?.TopBottom ?? 0;

        if (la.Children.Count == 0)
        {
          sa.CalculatedBounds.Height = extraHeight;
          return;
        }

        var maxHeight = 0;
        var pos = 0;
        var height = 0;
        for (var i = 0; i < la.Children.Count; ++i)
        {
          var csa = la.Children[i].GetAddon<StyleAddon>();
          if (csa.CurrentStyle.Position != Position.Absolute && la.Children[i].Active && pos != csa.GridPosition.Y)
          {
            height += maxHeight;
            maxHeight = 0;
          }

          var csaHeight = csa.CurrentStyle.Position == Position.Absolute || !la.Children[i].Active ? 0 : csa.CalculatedBounds.Height;
          maxHeight = Math.Max(maxHeight, csaHeight);
          pos = csa.GridPosition.Y;
        }
        height += maxHeight;

        sa.CalculatedBounds.Height = height + (sa.CurrentStyle.Padding?.TopBottom ?? 0) + (pos * sa.CurrentStyle.ColumnGap.X);
        entity.Update(sa);
      }
    }
  }
}
