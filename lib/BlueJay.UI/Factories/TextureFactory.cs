using BlueJay.Common.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Container;
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
    public static IEntity AddUITexture(this IServiceProvider provider, UITextureOptions options, Style? style = null, IEntity? parent = null)
    {
      var content = provider.GetRequiredService<IContentManagerContainer>();
      var texture = content.Load<ITexture2DContainer>(options.AssetName);
      style = style ?? new Style();
      style.Width = texture.Width;
      style.Height = texture.Height;
      style.NinePatch = null;
      style.BackgroundColor = null;

      var entity = provider.AddUIEntity(parent);
      entity.Active = parent?.Active ?? true;
      entity.Add(new StyleAddon(style));
      entity.Add(new TextureAddon(texture));
      entity.Add(new FrameAddon(options.FrameCount ?? 1, options.FrameTickAmount ?? 0, options.Frame ?? 0, options.StartingFrame));
      entity.Add(new SpriteSheetAddon(options.Columns ?? 1, options.Rows ?? 1));
      entity.Add(new PositionAddon());
      entity.Add(new ColorAddon(Color.White));
      entity.Add(new BoundsAddon());

      return entity;
    }
  }

  public class UITextureOptions
  {
    /// <summary>
    /// The asset name of the texture being loaded
    /// </summary>
    public string AssetName { get; }

    /// <summary>
    /// The current frame count of the animation
    /// </summary>
    public int? FrameCount { get; set; }

    /// <summary>
    /// The amount of time between each frame in milliseconds
    /// </summary>
    public int? FrameTickAmount { get; set; }

    /// <summary>
    /// The current frame for the sprite
    /// </summary>
    public int? Frame { get; set; }

    /// <summary>
    /// The starting frame for the sprite
    /// </summary>
    public int? StartingFrame { get; set; }

    /// <summary>
    /// The rows of the spritesheet being used
    /// </summary>
    public int? Rows { get; set; }

    /// <summary>
    /// The columns of the spritesheet being used
    /// </summary>
    public int? Columns { get; set; }

    /// <summary>
    /// Constructor meant to setup force an asset name to the options
    /// </summary>
    /// <param name="assetName">The asset name of the texture that should be loaded</param>
    public UITextureOptions(string assetName)
    {
      AssetName = assetName;
    }
  }
}
