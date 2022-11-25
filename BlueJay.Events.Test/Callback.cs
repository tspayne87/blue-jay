using BlueJay.Core.Interfaces;
using BlueJay.Events.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;
using System.Diagnostics;
using Xunit;
using static System.Formats.Asn1.AsnWriter;

namespace BlueJay.Events.Test
{
  public class Callback
  {
    public Mock<IDeltaService> DeltaService = new Mock<IDeltaService>();

    public IServiceProvider Provider => new ServiceCollection()
      .AddSingleton(DeltaService.Object)
      .AddBlueJayEvents()
      .BuildServiceProvider();


    [Fact]
    public void HandleData()
    {
      var scope = Provider.CreateScope();
      var processor = scope.ServiceProvider.GetRequiredService<IEventProcessor>();
      var queue = scope.ServiceProvider.GetRequiredService<IEventQueue>();

      var data = 0;
      scope.ServiceProvider.AddEventListener<int>(x => (data += x) >= 0);

      queue.DispatchEvent(1);
      processor.Update();
      Assert.Equal(1, data);

      queue.DispatchEvent(10);
      processor.Update();
      Assert.Equal(11, data);

      queue.DispatchEvent(5);
      queue.DispatchEvent(5);
      processor.Update();
      processor.Update();
      processor.Update();
      Assert.Equal(21, data);
    }

    [Fact]
    public void CallAmounts()
    {
      var scope = Provider.CreateScope();
      var processor = scope.ServiceProvider.GetRequiredService<IEventProcessor>();
      var queue = scope.ServiceProvider.GetRequiredService<IEventQueue>();

      var calls = 0;
      scope.ServiceProvider.AddEventListener<int>(x => ++calls >= 0);

      queue.DispatchEvent(1);
      processor.Update();
      Assert.Equal(1, calls);

      queue.DispatchEvent(2);
      processor.Update();
      processor.Update();
      Assert.Equal(2, calls);

      queue.DispatchEvent(3);
      queue.DispatchEvent(4);
      processor.Update();
      Assert.Equal(4, calls);
    }

    [Fact]
    public void DelayedHandleData()
    {
      var scope = Provider.CreateScope();
      var processor = scope.ServiceProvider.GetRequiredService<IEventProcessor>();
      var queue = scope.ServiceProvider.GetRequiredService<IEventQueue>();

      DeltaService.Setup(ms => ms.Delta).Returns(200);

      var data = 0;
      scope.ServiceProvider.AddEventListener<int>(x => (data += x) >= 0);

      queue.DispatchDelayedEvent(5, 500);
      processor.Update();
      Assert.Equal(0, data);

      queue.DispatchDelayedEvent(15, 200);
      processor.Update();
      Assert.Equal(15, data);

      processor.Update();
      Assert.Equal(20, data);
    }

    [Fact]
    public void DelayedCallAmounts()
    {
      var scope = Provider.CreateScope();
      var processor = scope.ServiceProvider.GetRequiredService<IEventProcessor>();
      var queue = scope.ServiceProvider.GetRequiredService<IEventQueue>();

      DeltaService.Setup(ms => ms.Delta).Returns(200);

      var times = 0;
      scope.ServiceProvider.AddEventListener<int>(x => ++times >= 0);

      queue.DispatchDelayedEvent(1, 500);
      processor.Update();
      Assert.Equal(0, times);

      queue.DispatchDelayedEvent(2, 200);
      processor.Update();
      Assert.Equal(1, times);

      processor.Update();
      Assert.Equal(2, times);
    }

    [Fact]
    public void Cancellable()
    {
      var scope = Provider.CreateScope();
      var processor = scope.ServiceProvider.GetRequiredService<IEventProcessor>();
      var queue = scope.ServiceProvider.GetRequiredService<IEventQueue>();

      DeltaService.Setup(ms => ms.Delta).Returns(200);

      var times = 0;
      scope.ServiceProvider.AddEventListener<int>(x => ++times >= 0);

      var dispose = queue.DispatchDelayedEvent(1, 500);
      processor.Update();

      queue.DispatchDelayedEvent(2, 300);
      processor.Update();
      Assert.Equal(0, times);

      dispose.Dispose();
      processor.Update();
      processor.Update();

      Assert.Equal(1, times);

      processor.Update();
      Assert.Equal(1, times);
    }

    [Fact]
    public void Weight()
    {
      var scope = Provider.CreateScope();
      var processor = scope.ServiceProvider.GetRequiredService<IEventProcessor>();
      var queue = scope.ServiceProvider.GetRequiredService<IEventQueue>();

      var data = 5;
      scope.ServiceProvider.AddEventListener<int>(x => (data += x) > 0);
      scope.ServiceProvider.AddEventListener<int>(x => (data *= x) > 0, -1);

      queue.DispatchEvent(2);
      processor.Update();
      Assert.Equal(12, data);

      queue.DispatchEvent(1);
      processor.Update();
      Assert.Equal(13, data);
    }

    [Fact]
    public void StopPropegation()
    {
      var scope = Provider.CreateScope();
      var processor = scope.ServiceProvider.GetRequiredService<IEventProcessor>();
      var queue = scope.ServiceProvider.GetRequiredService<IEventQueue>();

      var times = 0;
      scope.ServiceProvider.AddEventListener<int>(x => ++times < 3);
      scope.ServiceProvider.AddEventListener<int>(x => ++times >= 0);
      scope.ServiceProvider.AddEventListener<int>(x => ++times >= 0);
      scope.ServiceProvider.AddEventListener<int>(x => ++times >= 0);
      scope.ServiceProvider.AddEventListener<int>(x => ++times >= 0);

      queue.DispatchEvent(1);
      processor.Update();
      Assert.Equal(5, times);

      queue.DispatchEvent(1);
      processor.Update();
      Assert.Equal(6, times);

      queue.DispatchEvent(1);
      processor.Update();
      Assert.Equal(7, times);
    }

    [Fact]
    public void TriggerObjectEvent()
    {
      var scope = Provider.CreateScope();
      var processor = scope.ServiceProvider.GetRequiredService<IEventProcessor>();
      var queue = scope.ServiceProvider.GetRequiredService<IEventQueue>();

      var times = 0;
      scope.ServiceProvider.AddEventListener<int>(x => ++times > -1);

      queue.DispatchEvent((object)1);
      processor.Update();
      Assert.Equal(1, times);
    }

    [Fact]
    public void TriggerDelayedObjectEvent()
    {
      var scope = Provider.CreateScope();
      var processor = scope.ServiceProvider.GetRequiredService<IEventProcessor>();
      var queue = scope.ServiceProvider.GetRequiredService<IEventQueue>();

      DeltaService.Setup(ms => ms.Delta).Returns(200);

      var times = 0;
      scope.ServiceProvider.AddEventListener<int>(x => ++times >= 0);

      var dispose = queue.DispatchDelayedEvent((object)1, 500);
      processor.Update();

      queue.DispatchDelayedEvent((object)2, 300);
      processor.Update();
      Assert.Equal(0, times);

      dispose.Dispose();
      processor.Update();
      processor.Update();

      Assert.Equal(1, times);

      processor.Update();
      Assert.Equal(1, times);
    }
  }
}
