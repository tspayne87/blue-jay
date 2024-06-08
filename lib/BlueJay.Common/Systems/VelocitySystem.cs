using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using System.Collections.Generic;
using System.Numerics;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// Velocity system is meant to update the position based on the velocity
  /// </summary>
  public class VelocitySystem : IUpdateEntitySystem
  {
    /// <inheritdoc />
    public AddonKey Key => KeyHelper.Create<PositionAddon, VelocityAddon>();

    /// <inheritdoc />
    public List<string> Layers => new List<string>();

    /// <inheritdoc />
    public void OnUpdate(IEntity entity)
    {
      var pa = entity.GetAddon<PositionAddon>();
      var va = entity.GetAddon<VelocityAddon>();

      pa.Position += va.Velocity;
      entity.Update(pa);
    }
  }
}
