using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using System.Collections.Generic;

namespace BlueJay.Component.System.Systems
{
  /// <summary>
  /// Velocity system is meant to update the position based on the velocity
  /// </summary>
  public class VelocitySystem : ComponentSystem
  {
    /// <summary>
    /// The Selector to determine that Position and Texture addons are needed
    /// for this system
    /// </summary>
    public override long Key => PositionAddon.Identifier | VelocityAddon.Identifier;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>();

    /// <summary>
    /// The update event that is called for eeach entity that was selected by the key
    /// for this system.
    /// </summary>
    /// <param name="entity">The current entity that should be updated</param>
    public override void OnUpdate(IEntity entity)
    {
      var pa = entity.GetAddon<PositionAddon>();
      var va = entity.GetAddon<VelocityAddon>();

      pa.Position += va.Velocity;
    }
  }
}
