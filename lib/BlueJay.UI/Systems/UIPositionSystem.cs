using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BlueJay.UI.Systems
{
  /// <summary>
  /// The UI positioning system so that we can keep children in there parents space
  /// </summary>
  public class UIPositionSystem : IUpdateEntitySystem
  {
    /// <inheritdoc />
    public long Key => KeyHelper.Create<LineageAddon, PositionAddon, BoundsAddon>();

    /// <inheritdoc />
    public List<string> Layers => new List<string>() { UIStatic.LayerName };

    /// <inheritdoc />
    public void OnUpdate(IEntity entity)
    {
      var la = entity.GetAddon<LineageAddon>();
      var pa = entity.GetAddon<PositionAddon>();
      var ba = entity.GetAddon<BoundsAddon>();

      // Move position to the current bounds
      pa.Position = new Vector2(ba.Bounds.X, ba.Bounds.Y);

      // If we do not have a parent we do not want to worry about updating the position
      if (la.Parent != null)
      {
        // If we have a parent we need to add the position so we are bound to the parent
        var ppa = la.Parent.GetAddon<PositionAddon>();
        var psa = la.Parent.GetAddon<StyleAddon>();
        pa.Position += ppa.Position + new Vector2(psa.CurrentStyle.Padding ?? 0);
      }

      // Update entity with new position
      entity.Update(pa);
    }
  }
}
