using BlueJay.Shared.Games.Breakout.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using BlueJay.Common.Addons;

namespace BlueJay.Shared.Games.Breakout.Factories
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
      var entity = provider.AddEntity(LayerNames.BallLayer);
      entity.Add(new BoundsAddon(0, 0, 9, 9));
      entity.Add(new VelocityAddon(Vector2.Zero));
      entity.Add(new TypeAddon(EntityType.Ball));
      entity.Add(new TextureAddon(texture));
      entity.Add(new ColorAddon(Color.Black));
      entity.Add(new BallActiveAddon());
      return entity;
    }
  }
}
