using BlueJay.Component.System.Addons;

namespace BlueJay.Content.App.Games.Breakout.Addons
{
  /// <summary>
  /// Addon to determine if the ball is active or not and should start handling velocity
  /// instead of being attached to the paddle
  /// </summary>
  public class BallActiveAddon : Addon<BallActiveAddon>
  {
    /// <summary>
    /// If the ball is currently active and not attached to the paddle
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// Constructor to prime the is active field
    /// </summary>
    public BallActiveAddon()
    {
      IsActive = false;
    }
  }
}
