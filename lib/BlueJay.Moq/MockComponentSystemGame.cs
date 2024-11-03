using BlueJay.Core.Interfaces;
using BlueJay.Core;
using BlueJay.Utils;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using BlueJay.Events;
using BlueJay.Component.System;
using BlueJay.Events.Interfaces;

namespace BlueJay.Moq
{
  /// <summary>
  /// Helper class is meant to mimic what the component game system does and
  /// adds in logic to progress frame by frame to make sure data is processed properly
  /// </summary>
  public class MockComponentSystemGame : IDisposable
  {
    /// <summary>
    /// The current service scope
    /// </summary>
    private IServiceScope _scope;

    /// <summary>
    /// The service provider meant to allow for dependency injection
    /// </summary>
    private ServiceProvider _root;

    /// <summary>
    /// The current provider for dependency injection
    /// </summary>
    public IServiceProvider Provider { get => _scope.ServiceProvider; }

    /// <summary>
    /// Constructor is meant to include various mocks and get the game setup and ready for testing
    /// the codebase
    /// </summary>
    public MockComponentSystemGame()
    {
      var serviceCollection = new ServiceCollection()
        .AddSingleton(MockContentManagerContainer())
        .AddSingleton(MockScreen())
        .AddSingleton<IDeltaService, DeltaService>()
        .AddBlueJayEvents()
        .AddBlueJaySystem();

      _root = serviceCollection.BuildServiceProvider();
      _scope = _root.CreateScope();
    }

    /// <summary>
    /// Trigger the next tick to occur, or the next frame in the game
    /// </summary>
    public void NextTick()
    {
      var processor = Provider.GetRequiredService<IEventProcessor>();
      processor.Update();
      processor.Draw();
    }

    /// <summary>
    /// Dispose method meant to clear resources when this object is destoryed
    /// </summary>
    public void Dispose()
    {
      _scope.Dispose();
      _root.Dispose();
    }

    /// <summary>
    /// Helper method is meant to mock a screen to use from when doing various calculations
    /// </summary>
    /// <returns>Will return the mocked screen viewport</returns>
    private IScreenViewport MockScreen()
    {
      var screen = new Mock<IScreenViewport>();
      screen.Setup(x => x.Width).Returns(100);
      screen.Setup(x => x.Height).Returns(100);

      return screen.Object;
    }

    /// <summary>
    /// Helper method that is meant to mock the content manager so that content based tests
    /// can be created
    /// </summary>
    /// <returns>Will return the mocked content manager</returns>
    private IContentManagerContainer MockContentManagerContainer()
    {
      var content = new Mock<IContentManagerContainer>();
      content.Setup(x => x.Load<It.IsAnyType>(It.IsAny<string>()));

      return content.Object;
    }
  }
}
