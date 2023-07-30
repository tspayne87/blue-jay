using BlueJay.Component.System.Interfaces;

namespace BlueJay.Common.Addons
{
  /// <summary>
  /// Addon to help with rendering sprite sheets to the screen
  /// </summary>
  public struct SpriteSheetAddon : IAddon
  {
    /// <summary>
    /// The number of rows that exist in the sprite sheet
    /// </summary>
    public int Rows { get; set; }

    /// <summary>
    /// The columns that exist in the sprite sheet
    /// </summary>
    public int Cols { get; set; }

    /// <summary>
    /// Constructor to build out a basic sprite sheet
    /// </summary>
    /// <param name="cols">The amount of columns that exist in the sprite sheet</param>
    /// <param name="rows">The amount of rows that exists in the sprite sheet</param>
    /// <exception cref="ArgumentOutOfRangeException">Will throw exception if rows or cols are not greater than 0</exception>
    public SpriteSheetAddon(int cols, int rows = 1)
    {
      if (rows <= 0)
        throw new ArgumentOutOfRangeException(nameof(rows), "Should be greater than 0");
      if (cols <= 0)
        throw new ArgumentOutOfRangeException(nameof(cols), "Should be greater than 0");

      Rows = rows;
      Cols = cols;
    }

    /// <summary>
    /// Overriden to string function is meant to give a quick and easy way to see
    /// how this object looks while debugging
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"SpriteSheet | Rows: {Rows}, Cols: {Cols}";
    }
  }
}
