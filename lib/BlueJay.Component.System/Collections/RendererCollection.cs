using System.Collections.Generic;
using BlueJay.Core.Interfaces;

namespace BlueJay.Component.System.Collections
{
  /// <summary>
  /// Renderer collection that will take care of the different types of renderers
  /// that could exist in the game
  /// </summary>
  public class RendererCollection : Dictionary<string, IRenderer> { }
}
