using BlueJay.Component.System.Collections;

namespace BlueJay.Component.System.Interfaces
{
  public interface ILayer
  {
    EntityCollection Entities { get; }

    string Id { get; }

    int Weight { get; }
  }
}
