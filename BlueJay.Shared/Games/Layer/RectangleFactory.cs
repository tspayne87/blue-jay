using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Shared.Games.Layer.Addons;
using Microsoft.Xna.Framework;
using System;

namespace BlueJay.Shared.Games.Layer
{
  /// <summary>
  /// Factory class to help build entities
  /// </summary>
  internal static class RectangleFactory
  {
    /// <summary>
    /// Creates a top entity
    /// </summary>
    /// <param name="provider">The provider to add an entity to</param>
    /// <param name="size">The size of the entity</param>
    /// <param name="position">The position of the entity</param>
    /// <param name="color">The color of the entity</param>
    /// <returns>Will return the generated entity</returns>
    public static IEntity AddTopEntity(this IServiceProvider provider, Size size, Vector2 position, Color color)
    {
      var entity = provider.AddEntity("top", 10);
      entity.Add(new SizeAddon(size));
      entity.Add(new PositionAddon(position));
      entity.Add(new ColorAddon(color));
      entity.Add(new TopAddon());

      return entity;
    }

    /// <summary>
    /// Creates a bottom entity
    /// </summary>
    /// <param name="provider">The provider to add an entity to</param>
    /// <param name="size">The size of the entity</param>
    /// <param name="position">The position of the entity</param>
    /// <param name="color">The color of the entity</param>
    /// <returns>Will return the generated entity</returns>
    public static IEntity AddBottomEntity(this IServiceProvider provider, Size size, Vector2 position, Color color)
    {
      var entity = provider.AddEntity("bottom", 0);
      entity.Add(new SizeAddon(size));
      entity.Add(new PositionAddon(position));
      entity.Add(new ColorAddon(color));
      entity.Add(new BottomAddon());

      return entity;
    }
  }
}
