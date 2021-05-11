using BlueJay.Component.System.Addons;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BlueJay.App.Games.Breakout.Addons
{
  public class BlockIndexAddon : Addon<BlockIndexAddon>
  {
    private Dictionary<int, Color> _colors = new Dictionary<int, Color>()
    {
      { 0, Color.Red },
      { 1, Color.OrangeRed },
      { 2, Color.Orange },
      { 3, Color.DarkGoldenrod },
      { 4, Color.Green }
    };

    private Dictionary<int, int> _scores = new Dictionary<int, int>()
    {
      { 0, 50 },
      { 1, 40 },
      { 2, 30 },
      { 3, 20 },
      { 4, 10 }
    };

    public int Index { get; set; }

    public Color Color => _colors[Index / BlockConsts.Amount];
    public int Score => _scores[Index / BlockConsts.Amount];

    public BlockIndexAddon(int index)
    {
      Index = index;
    }
  }
}
