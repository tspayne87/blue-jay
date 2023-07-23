using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using BlueJay.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.UI.Factories
{
  public static class TextureFactory
  {
    public static IEntity AddUITexture(this IServiceProvider provider, string assetName, Style? style = null, IEntity? parent = null)
    {
      var content = provider.GetRequiredService<IContentManagerContainer>();
      var texture = content.Load<Texture2D>(assetName);
      style = style ?? new Style();
      style.Width = texture.Width;
      style.Height = texture.Height;
      style.NinePatch = null;
      style.BackgroundColor = null;

      var entity = provider.AddUIEntity(parent);
      entity.Active = parent?.Active ?? true;
      entity.Add(new StyleAddon(style));
      entity.Add(new TextureAddon(texture));
      entity.Add(new PositionAddon());
      entity.Add(new ColorAddon(Color.Black));
      entity.Add(new BoundsAddon());

      return entity;
    }
  }
}
