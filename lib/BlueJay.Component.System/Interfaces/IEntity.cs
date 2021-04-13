using System.Collections.Generic;

namespace BlueJay.Component.System.Interfaces
{
  /// <summary>
  /// The basic building block for the component system that determines the objects in the game
  /// </summary>
  public interface IEntity
  {
    /// <summary>
    /// The current unique identifier for this entity so it can be rebuilt through networking
    /// </summary>
    long Id { get; set; }

    /// <summary>
    /// Determines if this entity is currently active and should be interacted with
    /// </summary>
    bool Active { get; set; }

    /// <summary>
    /// Lifecycle hook is meant to load assets for this entity by passing them down to the addons
    /// </summary>
    void LoadContent();

    /// <summary>
    /// Method is meant to add an addon when the object has already been generated
    /// </summary>
    /// <param name="addon">The addon to append to the list and update the addon trees</param>
    void Add(IAddon addon);

    /// <summary>
    /// Method is meant to add an addon through DI
    /// </summary>
    /// <typeparam name="T">The addon that should be added</typeparam>
    /// <param name="parameters">The construction parameters that do not exist in the DI context</param>
    void Add<T>(params object[] parameters) where T : IAddon;

    /// <summary>
    /// Method is meant to remove and addon from the list
    /// </summary>
    /// <param name="addon">The current addon to be removed</param>
    void Remove(IAddon addon);

    /// <summary>
    /// Helper method to get a list of addons that represent the key given
    /// </summary>
    /// <param name="key">The key that determines the list of addons we are looking for</param>
    /// <returns>A list of addons based on the key given</returns>
    IEnumerable<IAddon> GetAddons(long key);

    /// <summary>
    /// Helper method is meant to get a specific addon otherwise null will be given
    /// </summary>
    /// <typeparam name="TAddon">The addon that should be gotten by this entity</typeparam>
    /// <returns>The addon or null if not exist</returns>
    TAddon GetAddon<TAddon>() where TAddon : IAddon;

    /// <summary>
    /// Helper method is meant to match if the key given is able to be sloted into the key
    /// </summary>
    /// <param name="key">The key that is meant to be a bitwise flag to determine the possible addons that exist on this entity</param>
    /// <returns>Will return true if the key matches the list of addons in this entity</returns>
    bool MatchKey(long key);
  }
}
