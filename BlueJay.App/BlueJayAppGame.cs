using BlueJay.App.Games.Breakout;
using BlueJay.App.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.App
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
      serviceCollection.AddSingleton(Content.Load<SpriteFont>("TestFont"));
      serviceCollection.AddSingleton<SpriteBatch>();

      serviceCollection.AddScoped<BreakoutGameService>();
    }

    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      serviceProvider.AddView<TitleView>();
      serviceProvider.AddView<BreakOutView>();
    }
  }
}
