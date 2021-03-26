using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Extensions.DependencyInjection;
using BlueJay.Component.System.DependencyInjection;
using System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Interfaces;
using BlueJay.Core.Renderers;

namespace BlueJay.Component.System
{
  public abstract class ComponentSystemGame : Game
  {
    private IServiceCollection _serviceCollection;
    private IServiceProvider _serviceProvider;

    public ComponentSystemGame()
    {
      _serviceCollection = new ServiceCollection()
        .AddSingleton<IGraphicsDeviceService>(new GraphicsDeviceManager(this));
    }

    protected abstract void ConfigureServices(IServiceCollection serviceCollection);
    protected abstract void ConfigureProvider(IServiceProvider serviceProvider);

    protected override void LoadContent()
    {
      _serviceCollection.AddSingleton(this);
      _serviceCollection.AddSingleton(Content);
      _serviceCollection.AddSingleton(Window);
      _serviceCollection.AddSingleton(GraphicsDevice);
      _serviceCollection.AddSingleton<IViewCollection, ViewCollection>();

      ConfigureServices(_serviceCollection);

      _serviceProvider = _serviceCollection.BuildServiceProvider();
      ConfigureProvider(_serviceProvider);
    }

    protected override void Update(GameTime gameTime)
    {
      _serviceProvider.GetRequiredService<IViewCollection>()
        .Current?.Update(gameTime.ElapsedGameTime.Milliseconds);

      base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
      _serviceProvider.GetRequiredService<IViewCollection>()
        .Current?.Draw(gameTime.ElapsedGameTime.Milliseconds);

      base.Draw(gameTime);
    }
  }
}
