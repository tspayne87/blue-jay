using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System.Systems
{
  public class RenderingSystem : ComponentSystem
  {
    private readonly IRenderer _renderer;
    public override long Key => PositionAddon.Identifier | TextureAddon.Identifier;

    public RenderingSystem(IRenderer renderer)
    {
      _renderer = renderer;
    }

    public override void Draw(int delta, IEntity entity)
    {
      var pc = entity.GetAddon<PositionAddon>();
      var tc = entity.GetAddon<TextureAddon>();

      _renderer.Draw(tc.Texture, pc.Position);
    }
  }
}
