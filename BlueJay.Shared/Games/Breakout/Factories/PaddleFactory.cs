using BlueJay.Shared.Games.Breakout.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using System;
using BlueJay.Common.Addons;

namespace BlueJay.Shared.Games.Breakout.Factories
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
      var entity = provider.AddEntity(LayerNames.PaddleLayer);
      entity.Add(new BoundsAddon(50, 250, 0, 20));
      entity.Add(new TypeAddon(EntityType.Paddle));
      return entity;
    }
  }
}
