using BlueJay.Shared.Games.Breakout;
using BlueJay.Shared.Views;
using BlueJay.Core;
using BlueJay.UI;
using BlueJay.UI.Component;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.Shared
{
  public class BlueJayAppGame : ComponentSystemGame
  {
    public BlueJayAppGame()
    {
      Content.RootDirectory = "Content";
      Window.AllowUserResizing = true;
      IsMouseVisible = true;
    }

    protected override void ConfigureServices(IServiceCollection serviceCollection)
    {
      // Add Framework Singletons
      serviceCollection.AddUI();

      // Add Custom scoped items
      serviceCollection.AddScoped<BreakoutGameService>();
    }

    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Load Content Manager
      var contentManager = serviceProvider.GetRequiredService<ContentManager>();

      // Add Fonts
      serviceProvider.AddSpriteFont("Default", contentManager.Load<SpriteFont>("TestFont"));
      var fontTexture = contentManager.Load<Texture2D>("Bitmap-Font");
      serviceProvider.AddTextureFont("Default", new TextureFont(fontTexture, 3, 24));

      // Add Views
      serviceProvider.AddView<TitleView>();
      serviceProvider.AddView<BreakOutView>();
    }
  }
}
