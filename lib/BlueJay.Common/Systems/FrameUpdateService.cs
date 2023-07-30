using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System;
using BlueJay.Core.Interfaces;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// System is meant to update the frame data based on the delta value
  /// </summary>
  public class FrameUpdateSystem : IUpdateEntitySystem
  {
    /// <summary>
    /// The delta service to update the frames to handle animations
    /// </summary>
    private readonly IDeltaService _delta;

    /// <inheritdoc />
    public long Key => KeyHelper.Create<FrameAddon>();

    /// <inheritdoc />
    public List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor is meant to inject various service into the system
    /// </summary>
    /// <param name="delta">The delta service to update the frames to handle animations</param>
    public FrameUpdateSystem(IDeltaService delta)
    {
      _delta = delta;
    }

    /// <summary>
    /// Update the entities frame data for the renderers
    /// </summary>
    /// <param name="entity">The entity we want to update the frame data for</param>
    public void OnUpdate(IEntity entity)
    {
      var fa = entity.GetAddon<FrameAddon>();
      if (fa.FrameTickAmount > 0)
      {
        fa.FrameTick -= _delta.Delta;
        if (fa.FrameTick <= 0)
        {
          fa.FrameTick += fa.FrameTickAmount;
          fa.Frame++;
          if (fa.Frame >= fa.FrameCount)
            fa.Frame = 0;
        }
        entity.Update(fa);
      }
    }
  }
}
