using BlueJay.App.Games.Breakout.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.App.Games.Breakout.Factories
{
  public static class BallFactory
  {
    public static IEntity AddBall(this IServiceProvider provider, Texture2D texture)
    {
      var entity = provider.AddEntity<Entity>(LayerNames.BallLayer);
      entity.Add<BoundsAddon>(new Rectangle(Point.Zero, new Point(9, 9)));
      entity.Add<VelocityAddon>(Vector2.Zero);
      entity.Add<TypeAddon>(EntityType.Ball);
      entity.Add<TextureAddon>(texture);
      entity.Add<ColorAddon>(Color.Black);
      entity.Add<BallActiveAddon>();
      return entity;
    }
  }
}
