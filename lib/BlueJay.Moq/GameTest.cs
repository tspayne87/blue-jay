namespace BlueJay.Moq
{
  /// <summary>
  /// Basic game test for testing systems based on the game itself
  /// </summary>
  public class GameTest : IDisposable
  {
    /// <summary>
    /// The current game that can be interacted with
    /// </summary>
    protected MockComponentSystemGame _game;

    /// <summary>
    /// Constructor to build out the mock game for testing purposes
    /// </summary>
    public GameTest()
    {
      _game = new MockComponentSystemGame();
    }

    /// <summary>
    /// Disposable meant to clean up the mocked game system
    /// </summary>
    public virtual void Dispose()
    {
      _game.Dispose();
    }
  }
}
