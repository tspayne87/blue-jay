using BlueJay.App.Games.Breakout.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using Microsoft.Xna.Framework;
using System;

namespace BlueJay.App.Games.Breakout.Factories
{
  public static class BlockFactory
  {
    public static IEntity AddBlock(this IServiceProvider provider, int index)
    {
      var entity = provider.AddEntity<Entity>(LayerNames.BlockLayer);
      entity.Add<BoundsAddon>(Rectangle.Empty);
      entity.Add<TypeAddon>(EntityType.Block);
      entity.Add<BlockIndexAddon>(index);
      return entity;
    }
  }
}
