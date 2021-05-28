using BlueJay.Component.System.Interfaces;

namespace BlueJay.App.Games.Breakout
{
  /// <summary>
  /// Event is meant to start the game with the space is pressed
  /// </summary>
  public class StartBallEvent
  {
    /// <summary>
    /// The reference to the ball so we can add velocity to it
    /// </summary>
    public IEntity Ball { get; set; }
  }
}
