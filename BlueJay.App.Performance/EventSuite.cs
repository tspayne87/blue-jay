using BenchmarkDotNet.Attributes;
using BlueJay.Common.Addons;
using BlueJay.Component.System;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core;
using BlueJay.Core.Interfaces;
using BlueJay.Events;
using BlueJay.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.App.Performance
{
  [MemoryDiagnoser]
  public class EventSuite
  {
    private IServiceProvider? _serviceProvider;

    [Params(10, 100, 1_000, 100_000)]
    public int Iterations;

    [GlobalSetup]
    public void Setup()
    {
      var collection = new ServiceCollection()
        .AddSingleton<IDeltaService, DeltaService>()
        .AddBlueJayEvents()
        .AddBlueJaySystem()
        .AddBlueJay();

      _serviceProvider = collection.BuildServiceProvider();

      _serviceProvider
        .SetStartView<TestView>();

      for (int i = 0; i < Iterations / 2; i++)
        CreateEntity();
    }

    [Benchmark]
    public void OneTick()
    {
      for (int i = 0; i < Iterations; i++)
        _serviceProvider?.GetRequiredService<IViewCollection>()
          .Current?.Update();
    }

    private void CreateEntity()
    {
      if (_serviceProvider != null)
      {
        var entity = _serviceProvider.AddEntity();
        entity.Add(new PositionAddon());
        entity.Add(new VelocityAddon(new Vector2(5, 0)));
      }
    }
  }
}
