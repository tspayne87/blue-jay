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
    /// <summary>
    /// Creates a UI Texture to be bound in the UI system
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="assetName">The current asset name needed to be loaded</param>
    /// <param name="style">The styles that will configure where the texture is</param>
    /// <param name="parent">The current parent this texture should be bound too</param>
    /// <returns>Will return the configured ui texture entity</returns>
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
