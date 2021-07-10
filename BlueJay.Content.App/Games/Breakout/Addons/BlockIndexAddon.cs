using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BlueJay.Content.App.Games.Breakout.Addons
{
  /// <summary>
  /// Addon is meant to determine the index where the block should be and what colors/scores this block has
  /// </summary>
  public struct BlockIndexAddon : IAddon
  {
    /// <summary>
    /// The colors based on where the block is on the screen
    /// </summary>
    private Dictionary<int, Color> _colors;

    /// <summary>
    /// The scores we are using when the block is destroyed
    /// </summary>
    private Dictionary<int, int> _scores;

    /// <summary>
    /// The current index of the block
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// The color this block should be
    /// </summary>
    public Color Color => _colors[Index / BlockConsts.Amount];

    /// <summary>
    /// The score this block should have
    /// </summary>
    public int Score => _scores[Index / BlockConsts.Amount];

    /// <summary>
    /// Constructor is meant to prime the index with whatever is given
    /// </summary>
    /// <param name="index">The current block index</param>
    public BlockIndexAddon(int index)
    {
      Index = index;

      _colors = new Dictionary<int, Color>()
      {
        { 0, Color.Red },
        { 1, Color.OrangeRed },
        { 2, Color.Orange },
        { 3, Color.DarkGoldenrod },
        { 4, Color.Green }
      };

      _scores = new Dictionary<int, int>()
      {
        { 0, 50 },
        { 1, 40 },
        { 2, 30 },
        { 3, 20 },
        { 4, 10 }
      };
    }
  }
}
