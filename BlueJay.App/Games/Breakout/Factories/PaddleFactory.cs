using BlueJay.App.Games.Breakout.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace BlueJay.App.Games.Breakout.Factories
{
  public static class PaddleFactory
  {
    /// <summary>
    /// Factory is meant to create a paddle entity and add addons to it
    /// </summary>
    /// <param name="provider">The service provider we need to add the entities and systems to</param>
    /// <returns>The entity that was created</returns>
    public static IEntity AddPaddle(this IServiceProvider provider)
    {
      var entity = provider.AddEntity<Entity>(LayerNames.PaddleLayer);
      entity.Add<BoundsAddon>(new Rectangle(new Point(50, 250), new Point(0, 20)));
      entity.Add<TypeAddon>(EntityType.Paddle);
      return entity;
    }
  }
}
