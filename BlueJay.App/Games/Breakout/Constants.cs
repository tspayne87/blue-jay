namespace BlueJay.App.Games.Breakout
{
  /// <summary>
  /// The constant layer names so we can group the different entities to various layers
  /// </summary>
  public static class LayerNames
  {
    public const string BlockLayer = "BlockLayer";
    public const string PaddleLayer = "PaddleLayer";
    public const string BallLayer = "BallLayer";
  }

  /// <summary>
  /// The basic constants that are used for creating blocks on the top of the screen
  /// </summary>
  public static class BlockConsts
  {
    public const int Amount = 5;
    public const int Padding = 5;
    public const int TopOffset = 50;
  }
}
