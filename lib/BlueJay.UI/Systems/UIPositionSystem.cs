using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Systems
{
  /// <summary>
  /// The UI positioning system so that we can keep children in there parents space
  /// </summary>
  internal class UIPositionSystem : IUpdateSystem
  {
    /// <summary>
    /// The entities that we need to update the position of
    /// </summary>
    private readonly IQuery _entities;

    /// <summary>
    /// Constructor to build out the UI position system
    /// </summary>
    /// <param name="entities">The entities that we need to update the position of</param>
    public UIPositionSystem(IQuery<LineageAddon, PositionAddon, BoundsAddon> entities)
    {
      _entities = entities.WhereLayer(UIStatic.LayerName);
    }

    /// <inheritdoc />
    public void OnUpdate()
    {
      foreach (var entity in _entities)
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
          pa.Position += ppa.Position + new Vector2(psa.CurrentStyle.Padding?.Left ?? 0, psa.CurrentStyle.Padding?.Top ?? 0);
        }

        // Update entity with new position
        entity.Update(pa);
      }
    }
  }
}
