using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Extensions.DependencyInjection;
using System;
using BlueJay.Core;
using BlueJay.Core.Interfaces;
using BlueJay.Events.Interfaces;
using BlueJay.Events;
using BlueJay.Interfaces;
using BlueJay.Views;
using BlueJay.Component.System.Collections;

namespace BlueJay
{
  /// <summary>
  /// Extension to the game object that builds out all the services and DI to get the component system working
  /// properly
  /// </summary>
  public abstract class ComponentSystemGame : Game
  {
    /// <summary>
    /// The current service collection we are using for DI
    /// </summary>
    private IServiceCollection _serviceCollection;

    /// <summary>
    /// The service provider for the DI implementation
    /// </summary>
    private IServiceProvider _serviceProvider;

    /// <summary>
    /// The delta service that can be injected by things in the system
    /// </summary>
    private DeltaService _deltaService;

    /// <summary>
    /// Constructore for the game to build out the graphice device manager as wellas start the service collection and delta
    /// services
    /// </summary>
    public ComponentSystemGame()
    {
      _deltaService = new DeltaService();
      _serviceCollection = new ServiceCollection()
        .AddSingleton<IGraphicsDeviceService>(new GraphicsDeviceManager(this))
        .AddSingleton<IDeltaService>(_deltaService);
    }

    /// <summary>
    /// Abstract method is meant to configure singletons into ehte service collection
    /// </summary>
    /// <param name="serviceCollection">The current service collection</param>
    protected abstract void ConfigureServices(IServiceCollection serviceCollection);

    /// <summary>
    /// Abstract method is meant to add a hook once the provider has been created
    /// </summary>
    /// <param name="serviceProvider">The service provider that has been created from the collection</param>
    protected abstract void ConfigureProvider(IServiceProvider serviceProvider);

    /// <summary>
    /// Lifecycle method is meant to add all meaningfull stuff into DI so it can be injected without knowing where it is
    /// </summary>
    protected override void LoadContent()
    {
      // All singletons that will never really change
      _serviceCollection.AddSingleton(this);
      _serviceCollection.AddSingleton(Content);
      _serviceCollection.AddSingleton(Window);
      _serviceCollection.AddSingleton(GraphicsDevice);
      _serviceCollection.AddSingleton<IViewCollection, ViewCollection>();

      // Scopped collections that will be used in each view when processing
      _serviceCollection.AddScoped<IEventProcessor, EventProcessor>();
      _serviceCollection.AddScoped<EventQueue>();
      _serviceCollection.AddScoped<LayerCollection>();
      _serviceCollection.AddScoped<SystemCollection>();
      _serviceCollection.AddScoped<RendererCollection>();
      _serviceCollection.AddScoped<FontCollection>();

      ConfigureServices(_serviceCollection);

      _serviceProvider = _serviceCollection.BuildServiceProvider();
      ConfigureProvider(_serviceProvider);
    }

    /// <summary>
    /// Lifecycle method is meant to update the frame
    /// </summary>
    /// <param name="gameTime">The current elapsed game time</param>
    protected override void Update(GameTime gameTime)
    {
      _deltaService.Delta = gameTime.ElapsedGameTime.Milliseconds;
      _serviceProvider.GetRequiredService<IViewCollection>()
        .Current?.Update();

      base.Update(gameTime);
    }

    /// <summary>
    /// Lifecycle method is meant to draw items to the screen
    /// </summary>
    /// <param name="gameTime">The current elapsed game time</param>
    protected override void Draw(GameTime gameTime)
    {
      _serviceProvider.GetRequiredService<IViewCollection>()
        .Current?.Draw();

      base.Draw(gameTime);
    }
  }
}
