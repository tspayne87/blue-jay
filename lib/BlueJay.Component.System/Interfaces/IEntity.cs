using System;
using System.Collections.Generic;
using System.Numerics;

namespace BlueJay.Component.System.Interfaces
{
  /// <summary>
  /// The basic building block for the component system that determines the objects in the game
  /// </summary>
  public interface IEntity : IDisposable
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
    /// The current layer that this is being bound too
    /// </summary>
    string Layer { get; set; }

    /// <summary>
    /// The weight that this entity should exist in based on the other entities in the layer
    /// </summary>
    int Weight { get; set; }

    /// <summary>
    /// Method is meant to add an addon when the object has already been generated
    /// </summary>
    /// <typeparam name="T">The type of addon</typeparam>
    /// <param name="addon">The addon to append to the list and update the addon trees</param>
    bool Add<T>(T addon) where T : struct, IAddon;

    /// <summary>
    /// Method is meant to remove and addon from the list
    /// </summary>
    /// <typeparam name="T">The type of addon</typeparam>
    /// <param name="addon">The current addon to be removed</param>
    bool Remove<T>(T addon) where T : struct, IAddon;

    /// <summary>
    /// Method is meant to remove an added based on the type
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    bool Remove<T>() where T : struct, IAddon;

    /// <summary>
    /// Method is meant to update an addon from the list
    /// </summary>
    /// <typeparam name="T">The type of addon</typeparam>
    /// <param name="addon">The current addon to be updated</param>
    bool Update<T>(T addon) where T : struct, IAddon;

    /// <summary>
    /// Method is meant to update or add the addon to this list
    /// </summary>
    /// <typeparam name="T">The type of addon</typeparam>
    /// <param name="addon">The addon we need to update or insert</param>
    bool Upsert<T>(T addon) where T : struct, IAddon;

    /// <summary>
    /// Helper method is meant to get a specific addon otherwise null will be given
    /// </summary>
    /// <typeparam name="TAddon">The addon that should be gotten by this entity</typeparam>
    /// <returns>The addon or null if not exist</returns>
    T GetAddon<T>() where T : struct, IAddon;

    /// <summary>
    /// Helper method meant to get an addon that may not exist on the entity
    /// </summary>
    /// <typeparam name="T">The type of addon that is needed</typeparam>
    /// <param name="addon">The addon found</param>
    /// <returns>Will return true if an addon of the type exists on the entity</returns>
    bool TryGetAddon<T>(out T addon) where T : struct, IAddon;

    /// <summary>
    /// Helper method to get a list of addons that represent the key given
    /// </summary>
    /// <param name="key">The key that determines the list of addons we are looking for</param>
    /// <returns>A list of addons based on the key given</returns>
    IEnumerable<IAddon> GetAddons(AddonKey key);

    /// <summary>
    /// Helper method is meant to match if the key given is able to be sloted into the key
    /// </summary>
    /// <param name="key">The key that is meant to be a bitwise flag to determine the possible addons that exist on this entity</param>
    /// <returns>Will return true if the key matches the list of addons in this entity</returns>
    bool MatchKey(AddonKey key);

    /// <summary>
    /// Helper method to determine if this entity has a specific addon
    /// </summary>
    /// <typeparam name="A1">The first addon</typeparam>
    /// <returns>Will return true if all the addons are found</returns>
    bool Contains<A1>() where A1 : struct, IAddon;

    /// <summary>
    /// Helper method to determine if this entity has a specific addon
    /// </summary>
    /// <typeparam name="A1">The first addon</typeparam>
    /// <typeparam name="A2">The second addon</typeparam>
    /// <returns>Will return true if all the addons are found</returns>
    bool Contains<A1, A2>() where A1 : struct, IAddon where A2 : struct, IAddon;

    /// <summary>
    /// Helper method to determine if this entity has a specific addon
    /// </summary>
    /// <typeparam name="A1">The first addon</typeparam>
    /// <typeparam name="A2">The second addon</typeparam>
    /// <typeparam name="A3">The third addon</typeparam>
    /// <returns>Will return true if all the addons are found</returns>
    bool Contains<A1, A2, A3>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon;

    /// <summary>
    /// Helper method to determine if this entity has a specific addon
    /// </summary>
    /// <typeparam name="A1">The first addon</typeparam>
    /// <typeparam name="A2">The second addon</typeparam>
    /// <typeparam name="A3">The third addon</typeparam>
    /// <typeparam name="A4">The forth addon</typeparam>
    /// <returns>Will return true if all the addons are found</returns>
    bool Contains<A1, A2, A3, A4>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon;

    /// <summary>
    /// Helper method to determine if this entity has a specific addon
    /// </summary>
    /// <typeparam name="A1">The first addon</typeparam>
    /// <typeparam name="A2">The second addon</typeparam>
    /// <typeparam name="A3">The third addon</typeparam>
    /// <typeparam name="A4">The forth addon</typeparam>
    /// <typeparam name="A5">The fifth addon</typeparam>
    /// <returns>Will return true if all the addons are found</returns>
    bool Contains<A1, A2, A3, A4, A5>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon where A5 : struct, IAddon;

    /// <summary>
    /// Helper method to determine if this entity has a specific addon
    /// </summary>
    /// <typeparam name="A1">The first addon</typeparam>
    /// <typeparam name="A2">The second addon</typeparam>
    /// <typeparam name="A3">The third addon</typeparam>
    /// <typeparam name="A4">The forth addon</typeparam>
    /// <typeparam name="A5">The fifth addon</typeparam>
    /// <typeparam name="A6">The sixth addon</typeparam>
    /// <returns>Will return true if all the addons are found</returns>
    bool Contains<A1, A2, A3, A4, A5, A6>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon where A5 : struct, IAddon where A6 : struct, IAddon;

    /// <summary>
    /// Helper method to determine if this entity has a specific addon
    /// </summary>
    /// <typeparam name="A1">The first addon</typeparam>
    /// <typeparam name="A2">The second addon</typeparam>
    /// <typeparam name="A3">The third addon</typeparam>
    /// <typeparam name="A4">The forth addon</typeparam>
    /// <typeparam name="A5">The fifth addon</typeparam>
    /// <typeparam name="A6">The sixth addon</typeparam>
    /// <typeparam name="A7">The seventh addon</typeparam>
    /// <returns>Will return true if all the addons are found</returns>
    bool Contains<A1, A2, A3, A4, A5, A6, A7>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon where A5 : struct, IAddon where A6 : struct, IAddon where A7 : struct, IAddon;

    /// <summary>
    /// Helper method to determine if this entity has a specific addon
    /// </summary>
    /// <typeparam name="A1">The first addon</typeparam>
    /// <typeparam name="A2">The second addon</typeparam>
    /// <typeparam name="A3">The third addon</typeparam>
    /// <typeparam name="A4">The forth addon</typeparam>
    /// <typeparam name="A5">The fifth addon</typeparam>
    /// <typeparam name="A6">The sixth addon</typeparam>
    /// <typeparam name="A7">The seventh addon</typeparam>
    /// <typeparam name="A8">The eighth addon</typeparam>
    /// <returns>Will return true if all the addons are found</returns>
    bool Contains<A1, A2, A3, A4, A5, A6, A7, A8>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon where A5 : struct, IAddon where A6 : struct, IAddon where A7 : struct, IAddon where A8 : struct, IAddon;

    /// <summary>
    /// Helper method to determine if this entity has a specific addon
    /// </summary>
    /// <typeparam name="A1">The first addon</typeparam>
    /// <typeparam name="A2">The second addon</typeparam>
    /// <typeparam name="A3">The third addon</typeparam>
    /// <typeparam name="A4">The forth addon</typeparam>
    /// <typeparam name="A5">The fifth addon</typeparam>
    /// <typeparam name="A6">The sixth addon</typeparam>
    /// <typeparam name="A7">The seventh addon</typeparam>
    /// <typeparam name="A8">The eighth addon</typeparam>
    /// <typeparam name="A9">The ninth addon</typeparam>
    /// <returns>Will return true if all the addons are found</returns>
    bool Contains<A1, A2, A3, A4, A5, A6, A7, A8, A9>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon where A5 : struct, IAddon where A6 : struct, IAddon where A7 : struct, IAddon where A8 : struct, IAddon where A9 : struct, IAddon;

    /// <summary>
    /// Helper method to determine if this entity has a specific addon
    /// </summary>
    /// <typeparam name="A1">The first addon</typeparam>
    /// <typeparam name="A2">The second addon</typeparam>
    /// <typeparam name="A3">The third addon</typeparam>
    /// <typeparam name="A4">The forth addon</typeparam>
    /// <typeparam name="A5">The fifth addon</typeparam>
    /// <typeparam name="A6">The sixth addon</typeparam>
    /// <typeparam name="A7">The seventh addon</typeparam>
    /// <typeparam name="A8">The eighth addon</typeparam>
    /// <typeparam name="A9">The ninth addon</typeparam>
    /// <typeparam name="A10">The tenth addon</typeparam>
    /// <returns>Will return true if all the addons are found</returns>
    bool Contains<A1, A2, A3, A4, A5, A6, A7, A8, A9, A10>() where A1 : struct, IAddon where A2 : struct, IAddon where A3 : struct, IAddon where A4 : struct, IAddon where A5 : struct, IAddon where A6 : struct, IAddon where A7 : struct, IAddon where A8 : struct, IAddon where A9 : struct, IAddon where A10 : struct, IAddon;
  }
}
