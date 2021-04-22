using BlueJay.Component.System;
using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.UI.Addons;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.UI.Factories
{
  public static class NinePatchFactory
  {
    public static IEntity AddNinePatch(this IServiceProvider serviceProvider, int width, int height, Color color, IEntity parent = null)
    {
      var entity = serviceProvider.AddUIEntity<Entity>();
      var graphics = serviceProvider.GetRequiredService<GraphicsDevice>();
      var rectangle = new Texture2D(graphics, 3, 3);
      rectangle.SetData(new Color[] {
        Color.Black, Color.Black, Color.Black,
        Color.Black, color,       Color.Black,
        Color.Black, Color.Black, Color.Black,
      });

      entity.Add(new StyleAddon() { NinePatch = new NinePatch(rectangle) });
      entity.Add<TextureAddon>();
      entity.Add<PositionAddon>(Vector2.Zero);
      entity.Add<BoundsAddon>(new Rectangle(new Point(20, 20), new Point(width, height)));
      return entity;
    }

    public static IEntity AddNinePatch(this IServiceProvider serviceProvider, Texture2D texture, int width, int height, Color color, IEntity parent = null)
    {
      var entity = serviceProvider.AddUIEntity<Entity>();
      entity.Add(new StyleAddon() { NinePatch = new NinePatch(texture) });
      entity.Add<TextureAddon>();
      entity.Add<PositionAddon>(Vector2.Zero);
      entity.Add<BoundsAddon>(new Rectangle(new Point(20, 20), new Point(width, height)));
      return entity;
    }
  }
}
