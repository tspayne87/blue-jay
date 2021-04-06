using BlueJay.Component.System.Interfaces;

namespace BlueJay.Component.System.Addons
{
  /// <summary>
  /// Basic addon object for the entities to be attached to
  /// </summary>
  /// <typeparam name="TAddon">The addon type that will be used for this addon</typeparam>
  public abstract class Addon<TAddon> : IAddon
    where TAddon: Addon<TAddon>
  {
    /// <summary>
    /// Static method is meant to trigger the identifier and create the bitwise long
    /// for finding addons attached to an entity
    /// </summary>
    public static readonly long Identifier = IdentifierHelper.Addon<TAddon>();

    /// <summary>
    /// The local instance of this identifier to statisfy the interface
    /// </summary>
    long IAddon.Identifier => Identifier;

    /// <summary>
    /// Method is called when the addon is added to an entity
    /// </summary>
    public virtual void OnAdd() { }

    /// <summary>
    /// Method is called when the added needs to be loaded before being processed for the scene
    /// </summary>
    public virtual void OnLoad() { }

    /// <summary>
    /// Method is called when this addon is removed from the entity
    /// </summary>
    public virtual void OnRemove() { }
  }
}
