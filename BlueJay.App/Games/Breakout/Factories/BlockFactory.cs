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
    public static IEntity AddBlock(this IServiceProvider provider, Color color, int pos)
    {
      var size = new Size(100, 20);
      var padding = 5;
      var position = new Vector2((pos % 5) * (size.Width + padding), (pos / 5) * (size.Height + padding));

      var entity = provider.AddEntity<Entity>(LayerNames.BlockLayer);
      entity.Add<SizeAddon>(size);
      entity.Add<PositionAddon>(position);
      entity.Add<TypeAddon>(EntityType.Block);
      entity.Add<ColorAddon>(color);
      return entity;
    }
  }
}
