using BlueJay.App.Games.Breakout.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using Microsoft.Xna.Framework;
using System;

namespace BlueJay.App.Games.Breakout.Factories
{
  public static class PaddleFactory
  {
    public static IEntity AddPaddle(this IServiceProvider provider)
    {
      var entity = provider.AddEntity<Entity>(LayerNames.PaddleLayer);
      entity.Add<BoundsAddon>(new Rectangle(new Point(50, 250), new Point(0, 20)));
      entity.Add<TypeAddon>(EntityType.Paddle);
      return entity;
    }
  }
}
