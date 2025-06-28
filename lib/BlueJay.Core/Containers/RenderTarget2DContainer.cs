using System;
using BlueJay.Core.Container;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Core.Containers;

public class RenderTarget2DContainer : IRenderTarget2DContainer
{
  /// <inheritdoc />
  public RenderTarget2D? Current { get; set; }

  /// <inheritdoc />
  public ITexture2DContainer AsTexture2DContainer()
  {
    return new Texture2DContainer()
    {
      Current = Current
    };
  }
}
