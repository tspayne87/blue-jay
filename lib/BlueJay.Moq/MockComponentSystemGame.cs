using BlueJay.Core.Interfaces;
using BlueJay.Core;
using BlueJay.Utils;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using BlueJay.Events;
using BlueJay.Component.System;
using BlueJay.Events.Interfaces;

namespace BlueJay.Moq
{
  public class MockComponentSystemGame : IDisposable
  {
    private IServiceScope _scope;
    private ServiceProvider _root;
    public IServiceProvider Provider { get => _scope.ServiceProvider; }

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

    public void NextTick()
    {
      var processor = Provider.GetRequiredService<IEventProcessor>();
      processor.Update();
      processor.Draw();
    }

    public IScreenViewport MockScreen()
    {
      var screen = new Mock<IScreenViewport>();
      screen.Setup(x => x.Width).Returns(100);
      screen.Setup(x => x.Height).Returns(100);

      return screen.Object;
    }

    public IContentManagerContainer MockContentManagerContainer()
    {
      var content = new Mock<IContentManagerContainer>();
      content.Setup(x => x.Load<It.IsAnyType>(It.IsAny<string>()));

      return content.Object;
    }

    public void Dispose()
    {
      _scope.Dispose();
      _root.Dispose();
    }
  }
}
