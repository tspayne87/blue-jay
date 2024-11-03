using BlueJay.Shared.Games.Breakout;
using BlueJay.Shared.Views;
using BlueJay.Core;
using BlueJay.UI;
using BlueJay.UI.Component;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using BlueJay.Component.System;
using System;
using BlueJay.Core.Container;
using BlueJay.Core.Containers;
using BlueJay.Utils;

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
      serviceCollection.AddComponentUI();

      // Add Custom scoped items
      serviceCollection.AddScoped<BreakoutGameService>();
    }

    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Load Content Manager
      var contentManager = serviceProvider.GetRequiredService<IContentManagerContainer>();

      // Add Fonts
      serviceProvider.AddSpriteFont("Default", contentManager.Load<ISpriteFontContainer>("TestFont"));
      var fontTexture = contentManager.Load<ITexture2DContainer>("Bitmap-Font");
      serviceProvider.AddTextureFont("Default", new TextureFont(fontTexture, 3, 24));

      // Add Views
      serviceProvider.SetStartView<TitleView>();
    }
  }
}
