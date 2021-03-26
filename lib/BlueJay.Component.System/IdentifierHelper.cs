using BlueJay.Component.System.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueJay.Component.System
{
  /// <summary>
  /// The type of identifiers that are meant to only have a set number of items
  /// </summary>
  internal enum IdentifierType
  {
    Addon
  }

  /// <summary>
  /// Internal class is meant to create bit identifiers without having to lock up bits if the items are not used
  /// </summary>
  internal static class IdentifierHelper
  {
    /// <summary>
    /// The current ditionary for all the next keys
    /// </summary>
    private static Dictionary<IdentifierType, long> _nextKey = new Dictionary<IdentifierType, long>() {
      { IdentifierType.Addon, 1 }
    };

    /// <summary>
    /// The cache of keys based on the type of object given
    /// </summary>
    private static Dictionary<IdentifierType, Dictionary<Type, long>> _cache = new Dictionary<IdentifierType, Dictionary<Type, long>>()
    {
      { IdentifierType.Addon, new Dictionary<Type, long>() }
    };

    /// <summary>
    /// Method is meant to get the identifer for an addon in the system
    /// </summary>
    /// <typeparam name="TComponent">The current type of addon we are looking for</typeparam>
    /// <returns>Will return the key for this particular addon</returns>
    internal static long Addon<TComponent>() where TComponent : IAddon => GetIdentifier(typeof(TComponent), IdentifierType.Addon);

    /// <summary>
    /// Method is meant to get the identifer for an addon in the system
    /// </summary>
    /// <param name="type">The current type of addon we are looking for</param>
    /// <returns>Will return the key for this particular addon</returns>
    internal static long Addon(Type type) => GetIdentifier(type, IdentifierType.Addon);

    /// <summary>
    /// Helper method is meant to create an identifier if one does not exist and return either the cached or the created one
    /// </summary>
    /// <param name="key">The object we are working with when building out the identifier</param>
    /// <param name="type">The type of identifier we are working with</param>
    /// <returns>Will return the identifier for the specific obj/type</returns>
    private static long GetIdentifier(Type key, IdentifierType type)
    {
      if (_cache[type].ContainsKey(key)) return _cache[type][key];
      _cache[type][key] = NextKey(type);
      return _cache[type][key];
    }

    /// <summary>
    /// Helper method to move to the next bit for the type
    /// </summary>
    /// <param name="type">The type we are working with</param>
    /// <returns>Will return the next bit for the type</returns>
    private static long NextKey(IdentifierType type)
    {
      if (_nextKey.ContainsKey(type))
      {
        if (_nextKey[type] == 0) throw new OverflowException("Cannot generate more than 64 keys in the system");
        _nextKey[type] = _nextKey[type] << 1;
      }
      else
      {
        _nextKey[type] = 1;
      }
      return _nextKey[type];
    }
  }
}
