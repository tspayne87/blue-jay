using BlueJay.Component.System.Interfaces;

namespace BlueJay.Content.App.Games.Breakout.Addons
{
  /// <summary>
  /// Addon to determine if the ball is active or not and should start handling velocity
  /// instead of being attached to the paddle
  /// </summary>
  public struct BallActiveAddon : IAddon
  {
    /// <summary>
    /// If the ball is currently active and not attached to the paddle
    /// </summary>
    public bool IsActive { get; set; }
  }
}
