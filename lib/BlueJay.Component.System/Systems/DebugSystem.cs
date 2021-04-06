using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Core.Interfaces;
using Microsoft.Xna.Framework;

namespace BlueJay.Component.System.Systems
{
  /// <summary>
  /// Debug system is meant to print out debug information out on the screen based
  /// on the debug addon
  /// </summary>
  public class DebugSystem : ComponentSystem
  {
    /// <summary>
    /// The current renderer so we can render data to the screen
    /// </summary>
    private readonly IRenderer _renderer;

    /// <summary>
    /// The current y position so we can offset the debug information so it can be
    /// read
    /// </summary>
    private int _y;

    /// <summary>
    /// The key that should select the entities based on the addons
    /// </summary>
    public override long Key => DebugAddon.Identifier;

    /// <summary>
    /// Constructor method to build out the system and inject the renderer into
    /// the class
    /// </summary>
    /// <param name="renderer"></param>
    public DebugSystem(IRenderer renderer)
    {
      _renderer = renderer;
    }

    /// <summary>
    /// We need to reset the y position on draw so we always draw in the same place
    /// </summary>
    /// <param name="delta">The current delta for this frame</param>
    public override void OnDraw(int delta)
    {
      _y = 10;
    }

    /// <summary>
    /// The draw event where we are going to render the draw information to the screen
    /// </summary>
    /// <param name="delta">The current delta for this frame</param>
    /// <param name="entity">The current entity we are working with</param>
    public override void OnDraw(int delta, IEntity entity)
    {
      var dc = entity.GetAddon<DebugAddon>();
      var dAddons = entity.GetAddons(dc.KeyIdentifier);
      foreach (var addon in dAddons)
      {
        _renderer.DrawString(addon.ToString(), new Vector2(10, _y), Color.White);
        _y += 20;
      }
    }
  }
}
