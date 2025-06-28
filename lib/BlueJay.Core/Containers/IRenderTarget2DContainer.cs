using System;
using BlueJay.Core.Container;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Core.Containers;

public interface IRenderTarget2DContainer
{
  /// <summary>
  /// The containing texture
  /// </summary>
  RenderTarget2D? Current { get; set; }

  /// <summary>
  /// Converts the render target to a texture 2D container
  /// </summary>
  /// <returns>Will return the render target as a 2D</returns>
  ITexture2DContainer AsTexture2DContainer();
}
