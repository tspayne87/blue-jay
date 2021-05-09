using BlueJay.App.Games.Breakout.Addons;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlueJay.App.Games.Breakout.Systems
{
  public class BreakoutRenderingSystem : ComponentSystem
  {
    private readonly IRenderer _renderer;

    public override long Key => TypeAddon.Identifier | SizeAddon.Identifier | PositionAddon.Identifier;
    public override List<string> Layers => new List<string>();

    public BreakoutRenderingSystem(IRenderer renderer)
    {
      _renderer = renderer;
    }


    public override void OnDraw(IEntity entity)
    {
      var ta = entity.GetAddon<TypeAddon>();
      var sa = entity.GetAddon<SizeAddon>();
      var pa = entity.GetAddon<PositionAddon>();

      switch(ta.Type)
      {
        case EntityType.Paddle:
          _renderer.DrawRectangle(sa.Size.Width, sa.Size.Height, pa.Position, Color.Blue);
          break;
        case EntityType.Ball:
          break;
        case EntityType.Block:
          var ca = entity.GetAddon<ColorAddon>();
          _renderer.DrawRectangle(sa.Size.Width, sa.Size.Height, pa.Position, ca?.Color ?? Color.Black);
          break;
      }
    }
  }
}
