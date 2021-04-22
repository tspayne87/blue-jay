using BlueJay.Component.System;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using Microsoft.Xna.Framework;
using System;

namespace BlueJay.UI.Factories
{
  public static class ButtonFactory
  {
    public static IEntity AddButton(this IServiceProvider serviceProvider, string text, int width, int height, Color color, IEntity parent = null)
    {
      var ninePatch = serviceProvider.AddNinePatch(width, height, color, parent);
      var entity = serviceProvider.AddUIEntity<Entity>(ninePatch);

      entity.Add<PositionAddon>(Vector2.Zero);
      return entity;
    }
  }
}
