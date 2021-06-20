using BlueJay.Component.System.Addons;

namespace BlueJay.Content.App.Games.Breakout.Addons
{
  /// <summary>
  /// Addon is meant to determine what type the entity is to deal with it in different ways
  /// </summary>
  public class TypeAddon : Addon<TypeAddon>
  {
    /// <summary>
    /// The type this entity should be
    /// </summary>
    public EntityType Type { get; private set; }

    /// <summary>
    /// Constructor to set the type of entity this addon should be
    /// </summary>
    /// <param name="type">The type this entity should be</param>
    public TypeAddon(EntityType type)
    {
      Type = type;
    }
  }
}
