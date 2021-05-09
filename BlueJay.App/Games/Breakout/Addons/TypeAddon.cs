using BlueJay.Component.System.Addons;

namespace BlueJay.App.Games.Breakout.Addons
{
  public class TypeAddon : Addon<TypeAddon>
  {
    public EntityType Type { get; set; }

    public TypeAddon(EntityType type)
    {
      Type = type;
    }
  }
}
