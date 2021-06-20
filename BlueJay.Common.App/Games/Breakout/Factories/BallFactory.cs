using BlueJay.Common.App.Games.Breakout.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.Common.App.Games.Breakout.Factories
{
  public static class BallFactory
  {
    /// <summary>
    /// Factory method is meant to create an entity and add various addons to the entity that represents
    /// the ball in the game
    /// </summary>
    /// <param name="provider">The service provider we need to add the entities and systems to</param>
    /// <param name="texture">The ball texture that should be used</param>
    /// <returns>The entity that was created</returns>
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
