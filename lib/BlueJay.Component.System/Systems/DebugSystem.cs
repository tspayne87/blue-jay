using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace BlueJay.Component.System.Systems
{
  public class DebugSystem : ComponentSystem
  {
    private readonly IRenderer _renderer;
    private int _y;
    public override long Key => DebugAddon.Identifier;

    public DebugSystem(IRenderer renderer)
    {
      _renderer = renderer;
    }

    public override void Draw(int delta)
    {
      _y = 10;
    }

    public override void Draw(int delta, IEntity entity)
    {
      var dc = entity.GetAddon<DebugAddon>();
      var dAddons = entity.GetAddons(dc.Key);
      foreach (var addon in dAddons)
      {
        _renderer.DrawString(addon.ToString(), new Vector2(10, _y), Color.White);
        _y += 20;
      }
      base.Draw(delta, entity);
    }
  }
}
