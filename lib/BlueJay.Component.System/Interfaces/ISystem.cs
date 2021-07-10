using System.Collections.Generic;

namespace BlueJay.Component.System.Interfaces
{
  /// <summary>
  /// The basic system meant to figure out what type of key is bound to this system as well as the layers
  /// </summary>
  public interface ISystem
  {
    /// <summary>
    /// The current addon key that is meant to act as a selector for the Draw/Update
    /// methods with entities
    /// </summary>
    long Key { get; }

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    List<string> Layers { get; }
  }
}
