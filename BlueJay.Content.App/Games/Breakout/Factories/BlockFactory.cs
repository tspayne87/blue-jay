using BlueJay.Content.App.Games.Breakout.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using Microsoft.Xna.Framework;
using System;
using BlueJay.Common.Addons;

namespace BlueJay.Content.App.Games.Breakout.Factories
{
  public static class BlockFactory
  {
    /// <summary>
    /// Factory method is meant to create a block entity and add addons to that
    /// entity for processing in the game
    /// </summary>
    /// <param name="provider">The service provider we need to add the entities and systems to</param>
    /// <param name="index">The index this block should be set as</param>
    /// <returns>The entity that was created</returns>
    public static IEntity AddBlock(this IServiceProvider provider, int index)
    {
      var entity = provider.AddEntity<Entity>(LayerNames.BlockLayer);
      entity.Add(new BoundsAddon(Rectangle.Empty));
      entity.Add(new TypeAddon(EntityType.Block));
      entity.Add(new BlockIndexAddon(index));
      return entity;
    }
  }
}
