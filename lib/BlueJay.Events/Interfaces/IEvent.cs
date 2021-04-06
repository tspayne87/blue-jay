using System;
namespace BlueJay.Events.Interfaces
{
  public interface IEvent<TData>
  {
    object Target { get; }
    TData Data { get; }
  }
}
