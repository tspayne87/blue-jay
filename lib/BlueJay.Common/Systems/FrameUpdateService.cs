using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System;
using BlueJay.Core.Interfaces;

namespace BlueJay.Common.Systems
{
  /// <summary>
  /// System is meant to update the frame data based on the delta value
  /// </summary>
  public class FrameUpdateSystem : IUpdateSystem
  {
    /// <summary>
    /// The delta service to update the frames to handle animations
    /// </summary>
    private readonly IDeltaService _delta;

    /// <summary>
    /// The current frame query meant
    /// </summary>
    private readonly IQuery<FrameAddon> _frameQuery;

    /// <summary>
    /// Constructor is meant to inject various service into the system
    /// </summary>
    /// <param name="delta">The delta service to update the frames to handle animations</param>
    public FrameUpdateSystem(IDeltaService delta, IQuery<FrameAddon> frameQuery)
    {
      _delta = delta;
      _frameQuery = frameQuery;
    }

    /// <summary>
    /// Update the entities frame data for the renderers
    /// </summary>
    /// <param name="entity">The entity we want to update the frame data for</param>
    public void OnUpdate()
    {
      foreach (var entity in _frameQuery)
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
              fa.Frame = fa.StartingFrame;
          }
          entity.Update(fa);
        }
      }
    }
  }
}
