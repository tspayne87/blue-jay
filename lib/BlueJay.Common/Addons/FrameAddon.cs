using BlueJay.Component.System.Interfaces;

namespace BlueJay.Common.Addons
{
  /// <summary>
  /// The frame addon meant to determine what frame the entity is currently on
  /// </summary>
  public struct FrameAddon : IAddon
  {
    /// <summary>
    /// The current frame in the addon exists on, will always start with 0 and go to the frame count
    /// </summary>
    public int Frame { get; set; }

    /// <summary>
    /// The countdown till the next frame tick
    /// </summary>
    public int FrameTick { get; set; }

    /// <summary>
    /// The amount of frames that exist
    /// </summary>
    public int FrameCount { get; set; }

    /// <summary>
    /// The amount of time that should elapse between frames
    /// </summary>
    public int FrameTickAmount { get; set; }

    /// <summary>
    /// Constructor to build out the frame addon
    /// </summary>
    /// <param name="frameCount">The amount of frames that exist for this entity</param>
    /// <param name="frameTickAmount">The amount of time in milliseconds to switch between frames</param>
    /// <param name="frame">The current frame to start on</param>
    public FrameAddon(int frameCount, int frameTickAmount, int frame = 0)
    {
      Frame = frame;
      FrameTick = frameTickAmount;
      FrameCount = frameCount;
      FrameTickAmount = frameTickAmount;
    }

    /// <summary>
    /// Overriden to string function is meant to give a quick and easy way to see
    /// how this object looks while debugging
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"FrameAddon | Frame: {Frame}, FrameCount: {FrameCount}, FrameTickAmount: {FrameTickAmount}, FrameTick: {FrameTick}";
    }
  }
}
