using BlueJay.Component.System.Interfaces;
using System;
using System.Collections.Generic;

namespace BlueJay.Component.System
{
  /// <summary>
  /// Internal class is meant to create bit identifiers without having to lock up bits if the items are not used
  /// </summary>
  public sealed class AddonHelper
  {
    /// <summary>
    /// Creates an identifier based on the generics
    /// </summary>
    /// <typeparam name="T1">The first addon</typeparam>
    /// <returns>Will return a generated key based on the addon generics</returns>
    public static long Identifier<T1>() where T1 : IAddon => Identifier(typeof(T1));

    /// <summary>
    /// Creates an identifier based on the generics
    /// </summary>
    /// <typeparam name="T1">The first addon</typeparam>
    /// <typeparam name="T2">The second addon</typeparam>
    /// <returns>Will return a generated key based on the addon generics</returns>
    public static long Identifier<T1, T2>() where T1 : IAddon where T2 : IAddon => Identifier(typeof(T1), typeof(T2));

    /// <summary>
    /// Creates an identifier based on the generics
    /// </summary>
    /// <typeparam name="T1">The first addon</typeparam>
    /// <typeparam name="T2">The second addon</typeparam>
    /// <typeparam name="T3">The third addon</typeparam>
    /// <returns>Will return a generated key based on the addon generics</returns>
    public static long Identifier<T1, T2, T3>() where T1 : IAddon where T2 : IAddon where T3 : IAddon => Identifier(typeof(T1), typeof(T2), typeof(T3));

    /// <summary>
    /// Creates an identifier based on the generics
    /// </summary>
    /// <typeparam name="T1">The first addon</typeparam>
    /// <typeparam name="T2">The second addon</typeparam>
    /// <typeparam name="T3">The third addon</typeparam>
    /// <typeparam name="T4">The forth addon</typeparam>
    /// <returns>Will return a generated key based on the addon generics</returns>
    public static long Identifier<T1, T2, T3, T4>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon => Identifier(typeof(T1), typeof(T2), typeof(T3), typeof(T4));

    /// <summary>
    /// Creates an identifier based on the generics
    /// </summary>
    /// <typeparam name="T1">The first addon</typeparam>
    /// <typeparam name="T2">The second addon</typeparam>
    /// <typeparam name="T3">The third addon</typeparam>
    /// <typeparam name="T4">The forth addon</typeparam>
    /// <typeparam name="T5">The fifth addon</typeparam>
    /// <returns>Will return a generated key based on the addon generics</returns>
    public static long Identifier<T1, T2, T3, T4, T5>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon where T5 : IAddon => Identifier(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5));

    /// <summary>
    /// Creates an identifier based on the generics
    /// </summary>
    /// <typeparam name="T1">The first addon</typeparam>
    /// <typeparam name="T2">The second addon</typeparam>
    /// <typeparam name="T3">The third addon</typeparam>
    /// <typeparam name="T4">The forth addon</typeparam>
    /// <typeparam name="T5">The fifth addon</typeparam>
    /// <typeparam name="T6">The sixth addon</typeparam>
    /// <returns>Will return a generated key based on the addon generics</returns>
    public static long Identifier<T1, T2, T3, T4, T5, T6>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon where T5 : IAddon where T6 : IAddon => Identifier(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6));

    /// <summary>
    /// Creates an identifier based on the generics
    /// </summary>
    /// <typeparam name="T1">The first addon</typeparam>
    /// <typeparam name="T2">The second addon</typeparam>
    /// <typeparam name="T3">The third addon</typeparam>
    /// <typeparam name="T4">The forth addon</typeparam>
    /// <typeparam name="T5">The fifth addon</typeparam>
    /// <typeparam name="T6">The sixth addon</typeparam>
    /// <typeparam name="T7">The seventh addon</typeparam>
    /// <returns>Will return a generated key based on the addon generics</returns>
    public static long Identifier<T1, T2, T3, T4, T5, T6, T7>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon where T5 : IAddon where T6 : IAddon where T7 : IAddon => Identifier(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7));

    /// <summary>
    /// Creates an identifier based on the generics
    /// </summary>
    /// <typeparam name="T1">The first addon</typeparam>
    /// <typeparam name="T2">The second addon</typeparam>
    /// <typeparam name="T3">The third addon</typeparam>
    /// <typeparam name="T4">The forth addon</typeparam>
    /// <typeparam name="T5">The fifth addon</typeparam>
    /// <typeparam name="T6">The sixth addon</typeparam>
    /// <typeparam name="T7">The seventh addon</typeparam>
    /// <typeparam name="T8">The eighth addon</typeparam>
    /// <returns>Will return a generated key based on the addon generics</returns>
    public static long Identifier<T1, T2, T3, T4, T5, T6, T7, T8>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon where T5 : IAddon where T6 : IAddon where T7 : IAddon where T8 : IAddon => Identifier(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8));

    /// <summary>
    /// Creates an identifier based on the generics
    /// </summary>
    /// <typeparam name="T1">The first addon</typeparam>
    /// <typeparam name="T2">The second addon</typeparam>
    /// <typeparam name="T3">The third addon</typeparam>
    /// <typeparam name="T4">The forth addon</typeparam>
    /// <typeparam name="T5">The fifth addon</typeparam>
    /// <typeparam name="T6">The sixth addon</typeparam>
    /// <typeparam name="T7">The seventh addon</typeparam>
    /// <typeparam name="T8">The eighth addon</typeparam>
    /// <typeparam name="T9">the nineth addon</typeparam>
    /// <returns>Will return a generated key based on the addon generics</returns>
    public static long Identifier<T1, T2, T3, T4, T5, T6, T7, T8, T9>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon where T5 : IAddon where T6 : IAddon where T7 : IAddon where T8 : IAddon where T9 : IAddon => Identifier(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9));

    /// <summary>
    /// Creates an identifier based on the generics
    /// </summary>
    /// <typeparam name="T1">The first addon</typeparam>
    /// <typeparam name="T2">The second addon</typeparam>
    /// <typeparam name="T3">The third addon</typeparam>
    /// <typeparam name="T4">The forth addon</typeparam>
    /// <typeparam name="T5">The fifth addon</typeparam>
    /// <typeparam name="T6">The sixth addon</typeparam>
    /// <typeparam name="T7">The seventh addon</typeparam>
    /// <typeparam name="T8">The eighth addon</typeparam>
    /// <typeparam name="T9">the nineth addon</typeparam>
    /// <typeparam name="T10">The tenth addon</typeparam>
    /// <returns>Will return a generated key based on the addon generics</returns>
    public static long Identifier<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon where T5 : IAddon where T6 : IAddon where T7 : IAddon where T8 : IAddon where T9 : IAddon where T10 : IAddon => Identifier(typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6), typeof(T7), typeof(T8), typeof(T9), typeof(T10));

    /// <summary>
    /// Creates an identifier based on the types given, only counts IAddon items
    /// </summary>
    /// <param name="types">The addons we need to process</param>
    /// <returns>Will return a bit mask identifier for finding entities</returns>
    public static long Identifier(params Type[] types)
    {
      var id = 0L;
      for (var i = 0; i < types.Length; ++i)
        if (typeof(IAddon).IsAssignableFrom(types[i]))
          id |= GetIdentifier(types[i]);
      return id;
    }


    /// <summary>
    /// The current next key we should make for an addon
    /// </summary>
    private static long _nextKey = 1;

    /// <summary>
    /// The cache of keys based on the type of object given
    /// </summary>
    private static Dictionary<Type, long> _cache = new Dictionary<Type, long>();

    /// <summary>
    /// Helper method is meant to create an identifier if one does not exist and return either the cached or the created one
    /// </summary>
    /// <param name="key">The object we are working with when building out the identifier</param>
    /// <returns>Will return the identifier for the specific obj/type</returns>
    private static long GetIdentifier(Type key)
    {
      if (!_cache.ContainsKey(key))
        _cache[key] = NextKey();
      return _cache[key];
    }

    /// <summary>
    /// Helper method to move to the next bit for the type
    /// </summary>
    /// <param name="type">The type we are working with</param>
    /// <returns>Will return the next bit for the type</returns>
    private static long NextKey()
    {
      if (_nextKey == 0) throw new OverflowException("Cannot generate more than 64 keys in the system");
      _nextKey = _nextKey << 1;
      return _nextKey;
    }
  }
}
