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

    public override long Key => TypeAddon.Identifier | BoundsAddon.Identifier;
    public override List<string> Layers => new List<string>();

    public BreakoutRenderingSystem(IRenderer renderer)
    {
      _renderer = renderer;
    }

    public override void OnDraw(IEntity entity)
    {
      var ta = entity.GetAddon<TypeAddon>();
      var ba = entity.GetAddon<BoundsAddon>();

      switch(ta.Type)
      {
        case EntityType.Paddle:
          _renderer.DrawRectangle(ba.Bounds.Width, ba.Bounds.Height, new Vector2(ba.Bounds.X, ba.Bounds.Y), Color.Blue);
          break;
        case EntityType.Ball:
          {
            var txa = entity.GetAddon<TextureAddon>();
            var ca = entity.GetAddon<ColorAddon>();
            _renderer.Draw(txa.Texture, new Vector2(ba.Bounds.X, ba.Bounds.Y), ca?.Color ?? Color.Black);
          }
          break;
        case EntityType.Block:
          {
            var bia = entity.GetAddon<BlockIndexAddon>();
            _renderer.DrawRectangle(ba.Bounds.Width, ba.Bounds.Height, new Vector2(ba.Bounds.X, ba.Bounds.Y), bia?.Color ?? Color.Black);
          }
          break;
      }
    }
  }
}
