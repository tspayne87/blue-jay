using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using BlueJay.UI.Addons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BlueJay.UI.Systems
{
  /// <summary>
  /// The UI positioning system so that we can keep children in there parents space
  /// </summary>
  public class UIPositionSystem : ComponentSystem
  {
    /// <summary>
    /// The key to determine what entities this system should be handling
    /// </summary>
    public override long Key => LineageAddon.Identifier | PositionAddon.Identifier | BoundsAddon.Identifier;

    /// <summary>
    /// The layer this system should be working on
    /// </summary>
    public override List<string> Layers => new List<string>() { UIStatic.LayerName };

    /// <summary>
    /// Event method to update the entity based on the lineage of where it should be on the screen
    /// </summary>
    /// <param name="entity">The entity we need to update</param>
    public override void OnUpdate(IEntity entity)
    {
      var la = entity.GetAddon<LineageAddon>();
      var pa = entity.GetAddon<PositionAddon>();
      var ba = entity.GetAddon<BoundsAddon>();

      // Move position to the current bounds
      pa.Position = new Vector2(ba.Bounds.X, ba.Bounds.Y);

      // If we do not have a parent we leave the position where it currently is
      if (la.Parent == null) return;

      // If we have a parent we need to add the position so we are bound to the parent
      var ppa = la.Parent.GetAddon<PositionAddon>();
      pa.Position += ppa.Position;
    }
  }
}
