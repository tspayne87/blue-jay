using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// Velocity system is meant to update the position based on the velocity
  /// </summary>
  public class VelocitySystem : IUpdateSystem
  {
    /// <summary>
    /// The entities that we want to update the position based on the velocity
    /// </summary>
    private readonly IQuery<PositionAddon, VelocityAddon> _entities;

    /// <summary>
    /// Constructor method to build out the velocity system
    /// </summary>
    /// <param name="entities">The entities that we want to update the position based on the velocity</param>
    public VelocitySystem(IQuery<PositionAddon, VelocityAddon> entities)
    {
      _entities = entities;
    }

    /// <inheritdoc />
    public void OnUpdate()
    {
      foreach (var entity in _entities)
      {
        var pa = entity.GetAddon<PositionAddon>();
        var va = entity.GetAddon<VelocityAddon>();

        pa.Position += va.Velocity;
        entity.Update(pa);
      }
    }
  }
}
