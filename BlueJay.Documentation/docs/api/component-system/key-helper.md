# KeyHelper
The key helper is meant to create bitwise keys based on addons so that matching entities are faster
per lookup

## Create
Will create a key based on the addons attached during the creation process

```csharp
  /// @typeparam T1: The first addon
  /// @returns Will return a generated key based on the addon generics
  public static long Create<T1>() where T1 : IAddon;

  /// @typeparam T1: The first addon
  /// @typeparam T2: The second addon
  /// @returns Will return a generated key based on the addon generics
  public static long Create<T1, T2>() where T1 : IAddon where T2 : IAddon;

  /// @typeparam T1: The first addon
  /// @typeparam T2: The second addon
  /// @typeparam T3: The third addon
  /// @returns Will return a generated key based on the addon generics
  public static long Create<T1, T2, T3>() where T1 : IAddon where T2 : IAddon where T3 : IAddon;

  /// @typeparam T1: The first addon
  /// @typeparam T2: The second addon
  /// @typeparam T3: The third addon
  /// @typeparam T4: The forth addon
  /// @returns Will return a generated key based on the addon generics
  public static long Create<T1, T2, T3, T4>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon;

  /// @typeparam T1: The first addon
  /// @typeparam T2: The second addon
  /// @typeparam T3: The third addon
  /// @typeparam T4: The forth addon
  /// @typeparam T5: The fifth addon
  /// @returns Will return a generated key based on the addon generics
  public static long Create<T1, T2, T3, T4, T5>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon where T5 : IAddon;

  /// @typeparam T1: The first addon
  /// @typeparam T2: The second addon
  /// @typeparam T3: The third addon
  /// @typeparam T4: The forth addon
  /// @typeparam T5: The fifth addon
  /// @typeparam T6: The sixth addon
  /// @returns Will return a generated key based on the addon generics
  public static long Create<T1, T2, T3, T4, T5, T6>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon where T5 : IAddon where T6 : IAddon;

  /// @typeparam T1: The first addon
  /// @typeparam T2: The second addon
  /// @typeparam T3: The third addon
  /// @typeparam T4: The forth addon
  /// @typeparam T5: The fifth addon
  /// @typeparam T6: The sixth addon
  /// @typeparam T7: The seventh addon
  /// @returns Will return a generated key based on the addon generics
  public static long Create<T1, T2, T3, T4, T5, T6, T7>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon where T5 : IAddon where T6 : IAddon where T7 : IAddon;

  /// @typeparam T1: The first addon
  /// @typeparam T2: The second addon
  /// @typeparam T3: The third addon
  /// @typeparam T4: The forth addon
  /// @typeparam T5: The fifth addon
  /// @typeparam T6: The sixth addon
  /// @typeparam T7: The seventh addon
  /// @typeparam T8: The eighth addon
  /// @returns Will return a generated key based on the addon generics
  public static long Create<T1, T2, T3, T4, T5, T6, T7, T8>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon where T5 : IAddon where T6 : IAddon where T7 : IAddon where T8 : IAddon;

  /// @typeparam T1: The first addon
  /// @typeparam T2: The second addon
  /// @typeparam T3: The third addon
  /// @typeparam T4: The forth addon
  /// @typeparam T5: The fifth addon
  /// @typeparam T6: The sixth addon
  /// @typeparam T7: The seventh addon
  /// @typeparam T8: The eighth addon
  /// @typeparam T9: the nineth addon
  /// @returns Will return a generated key based on the addon generics
  public static long Create<T1, T2, T3, T4, T5, T6, T7, T8, T9>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon where T5 : IAddon where T6 : IAddon where T7 : IAddon where T8 : IAddon where T9 : IAddon;

  /// @typeparam T1: The first addon
  /// @typeparam T2: The second addon
  /// @typeparam T3: The third addon
  /// @typeparam T4: The forth addon
  /// @typeparam T5: The fifth addon
  /// @typeparam T6: The sixth addon
  /// @typeparam T7: The seventh addon
  /// @typeparam T8: The eighth addon
  /// @typeparam T9: the nineth addon
  /// @typeparam T10: The tenth addon
  /// @returns Will return a generated key based on the addon generics
  public static long Create<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>() where T1 : IAddon where T2 : IAddon where T3 : IAddon where T4 : IAddon where T5 : IAddon where T6 : IAddon where T7 : IAddon where T8 : IAddon where T9 : IAddon where T10 : IAddon;

  /// @param types: The addons we need to process
  /// @returns Will return a bit mask identifier for finding entities
  public static long Create(params Type[] types);
```