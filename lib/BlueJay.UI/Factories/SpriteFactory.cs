using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.UI.Addons;
using BlueJay.Utils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace BlueJay.UI.Factories
{
  public static class SpriteFactory
  {
    /// <summary>
    /// Creates a UI Sprite to be bounded to the UI System
    /// </summary>
    /// <param name="provider">The service provider we will use to find the collection and build out the object with</param>
    /// <param name="assetName">The current asset name needed to be loaded</param>
    /// <param name="frameCount">The current frame count for the sprite</param>
    /// <param name="frameTickAmount">The amount of time where the next frame should trigger</param>
    /// <param name="cols">The amount of columns in the sprite sheet</param>
    /// <param name="rows">The amount of rows in the sprite sheet</param>
    /// <param name="frame">The current starting frame that should be used for the sprite</param>
    /// <param name="style">The styles that will configure where the texture is</param>
    /// <param name="parent">The current parent this texture should be bound too</param>
    /// <returns>Will return the configured ui texture entity</returns>
    public static IEntity AddUISprite(this IServiceProvider provider, string assetName, int frameCount, int frameTickAmount, int cols, int rows = 1, int frame = 0, Style? style = null, IEntity? parent = null)
    {
      var content = provider.GetRequiredService<IContentManagerContainer>();
      var texture = content.Load<Texture2D>(assetName);
      style = style ?? new Style();
      style.Width = texture.Width / cols;
      style.Height = texture.Height / rows;
      style.NinePatch = null;
      style.BackgroundColor = null;

      var entity = provider.AddUIEntity(parent);
      entity.Active = parent?.Active ?? true;
      entity.Add(new StyleAddon(style));
      entity.Add(new TextureAddon(texture));
      entity.Add(new PositionAddon());
      entity.Add(new ColorAddon(Color.White));
      entity.Add(new BoundsAddon());
      entity.Add(new FrameAddon(frameCount, frameTickAmount, frame));
      entity.Add(new SpriteSheetAddon(cols, rows));

      return entity;
    }
  }
}
