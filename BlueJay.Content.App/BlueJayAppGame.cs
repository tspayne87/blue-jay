﻿using BlueJay.Content.App.Games.Breakout;
using BlueJay.Content.App.Views;
using BlueJay.Core;
using BlueJay.Core.Interfaces;
using BlueJay.Core.Renderers;
using BlueJay.UI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.Content.App
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
      serviceCollection.AddSingleton(Content.Load<SpriteFont>("TestFont"));
      serviceCollection.AddSingleton<SpriteBatch>();

      // Add Custom scoped items
      serviceCollection.AddScoped<BreakoutGameService>();
    }

    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Add framework configurations
      serviceProvider.UseUI();

      // Load Content Manager
      var contentManager = serviceProvider.GetRequiredService<ContentManager>();

      // Add Fonts
      serviceProvider.AddSpriteFont("Default", contentManager.Load<SpriteFont>("TestFont"));
      var fontTexture = contentManager.Load<Texture2D>("Bitmap-Font");
      serviceProvider.AddTextureFont("Default", new TextureFont(fontTexture, 3, 24));

      // Add Renderers
      serviceProvider.AddRenderer<Renderer>("Default");

      // Add Views
      serviceProvider.AddView<TitleView>();
      serviceProvider.AddView<BreakOutView>();
    }
  }
}
