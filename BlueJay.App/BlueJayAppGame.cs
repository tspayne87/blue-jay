using BlueJay.App.Games.Breakout;
using BlueJay.App.Views;
using BlueJay.Core.Interfaces;
using BlueJay.Core.Renderers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BlueJay.App
{
  [Obsolete]
  public class BlueJayAppGameOb : ComponentSystemGame
  {
    public BlueJayAppGameOb()
    {
      Content.RootDirectory = "Content";
      Window.AllowUserResizing = true;
      IsMouseVisible = true;
    }

    protected override void ConfigureServices(IServiceCollection serviceCollection)
    {
      serviceCollection.AddSingleton(Content.Load<SpriteFont>("TestFont"));
      serviceCollection.AddSingleton<SpriteBatch>();
      serviceCollection.AddSingleton<IRenderer, Renderer>();

      serviceCollection.AddScoped<BreakoutGameService>();
    }

    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      serviceProvider.AddView<TitleView>();
      serviceProvider.AddView<BreakOutView>();
    }
  }
}
