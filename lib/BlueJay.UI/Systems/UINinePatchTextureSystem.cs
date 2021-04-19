using BlueJay.Component.System.Addons;
using BlueJay.Component.System.Interfaces;
using BlueJay.Component.System.Systems;
using BlueJay.UI.Addons;
using System.Collections.Generic;

namespace BlueJay.UI.Systems
{
  public class UINinePatchTextureSystem : ComponentSystem
  {
    public override long Key => TextureAddon.Identifier | StyleAddon.Identifier | BoundsAddon.Identifier;

    public override List<string> Layers => new List<string>() { UIStatic.LayerName };

    public override void OnUpdate(IEntity entity)
    {
      var ta = entity.GetAddon<TextureAddon>();
      var sa = entity.GetAddon<StyleAddon>();
      var ba = entity.GetAddon<BoundsAddon>();


    }
  }
}
