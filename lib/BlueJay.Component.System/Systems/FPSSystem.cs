using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.Component.System.Systems
{
  public class FPSSystem : ComponentSystem
  {
    private readonly IRenderer _renderer;
    private int _fps = 0;
    private int _updates = 0;
    private int _countdown = 1000;

    public override long Key => 0;

    public FPSSystem(IRenderer renderer)
    {
      _renderer = renderer;
    }

    public override void Update(int delta)
    {
      _updates++;
      _countdown -= delta;
      if (_countdown <= 0)
      {
        _fps = _updates;
        _updates = 0;
        _countdown += 1000;
      }
      base.Update(delta);
    }

    public override void Draw(int delta)
    {
      _renderer.DrawString($"fps: {_fps}", new Vector2(200, 10), Color.Black);
      base.Draw(delta);
    }
  }
}
