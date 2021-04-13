﻿using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace BlueJay.Core.Renderers
{
  /// <summary>
  /// The camera renderer that will setup a transform matrix for beginning
  /// </summary>
  public class CameraRenderer : Renderer
  {
    /// <summary>
    /// The current camera that should be used to get the transform matrix
    /// </summary>
    private readonly ICamera _camera;

    /// <summary>
    /// Constructor to build out the camera renderer
    /// </summary>
    /// <param name="graphics">The graphics device to create a rectangle from</param>
    /// <param name="batch">The sprite batch that will be used to render stuff to the screen</param>
    /// <param name="font">The global font</param>
    /// <param name="camera">The current camera that should be used when starting the process</param>
    public CameraRenderer(GraphicsDevice graphics, SpriteBatch batch, SpriteFont font, ICamera camera)
      : base(graphics, batch, font)
    {
      _camera = camera;
    }

    /// <summary>
    /// The begin set to start the batch that should be drawn
    /// </summary>
    public override void Begin()
    {
      Batch.Begin(transformMatrix: _camera.GetTransformMatrix);
    }
  }
}
