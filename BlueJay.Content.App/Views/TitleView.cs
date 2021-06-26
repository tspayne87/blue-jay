using BlueJay.Component.System.Systems;
using BlueJay.Views;
using Microsoft.Xna.Framework;
using System;
using BlueJay.UI;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BlueJay.Core;
using Microsoft.Extensions.DependencyInjection;
using BlueJay.Component.System.Collections;
using BlueJay.Component.System;
using BlueJay.Content.App.Components;

namespace BlueJay.Content.App.Views
{
  /// <summary>
  /// The title view to switch between sample games for BlueJay
  /// </summary>
  public class TitleView : View
  {
    /// <summary>
    /// The content manger meant to load the one texture used by breakout
    /// </summary>
    public readonly ContentManager _contentManager;

    /// <summary>
    /// Constructor is meant to inject the global content manger into the system
    /// </summary>
    public TitleView(ContentManager contentManager)
    {
      _contentManager = contentManager;
    }

    /// <summary>
    /// Configuration method is meant to add in all the systems that this game will use and bootstrap the game with entities
    /// that will be used by the game and its systems
    /// </summary>
    /// <param name="serviceProvider">The service provider we need to add the entities and systems to</param>
    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Add Fonts
      serviceProvider.AddSpriteFont("Default", _contentManager.Load<SpriteFont>("TestFont"));
      var fontTexture = _contentManager.Load<Texture2D>("Bitmap-Font");
      serviceProvider.AddTextureFont("Default", new TextureFont(fontTexture, 3, 24));

      // Add Processor Systems
      serviceProvider.AddComponentSystem<ClearSystem>(Color.White);
      serviceProvider.AddUISystems();
      serviceProvider.AddUIMouseSupport();
      serviceProvider.AddUITouchSupport();

      serviceProvider.AddComponentSystem<RenderingSystem>(serviceProvider.GetRequiredService<RendererCollection>()[RendererName.Default]);
      serviceProvider.AddComponentSystem<FPSSystem>("Default");

      serviceProvider.AddUIComponent<TitleViewComponent>();
    }
  }
}
