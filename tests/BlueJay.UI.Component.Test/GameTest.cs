using BlueJay.Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.UI.Component.Test
{
  public class GameTest : IDisposable
  {
    protected MockComponentSystemGame _game;

    public GameTest()
    {
      _game = new MockComponentSystemGame();
    }

    public virtual void Dispose()
    {
      _game.Dispose();
    }
  }
}
