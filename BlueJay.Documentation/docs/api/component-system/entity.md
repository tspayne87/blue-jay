# IEntity interface
Entities are a collection of addons that will be processed by systems to handle the game itself.  These
are the basic building blocks for the component system that make the bridge between addon to system.

## Id
The unique idetifier for each entity in the system

## Active
If this entity is active and should be processed by systems

## Layer
The current layer this entity is located on

## Add
Adds an addon to the entity

```csharp
  /// @typeparam T: The type of addon
  /// @param addon: The addon to append to the list and update the addon trees
  bool Add<T>(T addon) where T : struct, IAddon;
```

## Remove
Removes an addon from the entity

```csharp
  /// @typeparam T: The type of addon
  /// @param addon: The current addon to be removed
  bool Remove<T>(T addon) where T : struct, IAddon;
```

## Update
Updates an addon to the entity

```csharp
  /// @typeparam T: The type of addon
  /// @param addon: The current addon to be updated
  bool Update<T>(T addon) where T : struct, IAddon;
```

## Upsert
Updates or Adds an addon to the entity

```csharp
  /// @typeparam T: The type of addon
  /// @param addon: The addon we need to update or insert
  bool Upsert<T>(T addon) where T : struct, IAddon;
```

## GetAddon
Will get an addon from the entity

```csharp
  /// @typeparam TAddon: The addon that should be gotten by this entity
  /// @returns The addon or null if not exist
  T GetAddon<T>() where T : struct, IAddon;
```

## MatchKey
Will determine if the bitwise long has all the bits set for this entity based on the addons
attached

```csharp
  /// @param key: The key that is meant to be a bitwise flag to determine the possible addons that exist on this entity
  /// @returns Will return true if the key matches the list of addons in this entity
  bool MatchKey(long key);
```