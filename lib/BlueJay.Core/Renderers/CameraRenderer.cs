using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Core.Renderers
{
  public class CameraRenderer : Renderer
  {
    private readonly ICamera _camera;

    public CameraRenderer(GraphicsDevice graphics, SpriteBatch batch, ContentManager content, SpriteFont font, ICamera camera)
      : base(graphics, batch, content, font)
    {
      _camera = camera;
    }

    public override void Begin()
    {
      Batch.Begin(transformMatrix: _camera.GetTransformMatrix);
    }
  }
}
