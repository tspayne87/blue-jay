# IEntityCollection Interface
The entity collection is where all the entities are housed in the system.

## Count
Gets the current count of how many entities exist in the collection

## this[int i]
Gets the entity at the position given

## GetByKey
Gets a set of entities by the key

```csharp
  /// @param key: The key we want to find entities on
  /// @returns Will return a list of entities matching the key given
  List<IEntity> GetByKey(long key);
```

## Add
Add an entity to the collection

```csharp
  /// @param item: The entity we are adding to the collection
  void Add(IEntity item);
```

## Remove
Remove an entity from the collection

```csharp
  /// @param item: The entity we are trying to remove
  /// @returns Will return a boolean determining if the entity was removed from the collection
  bool Remove(IEntity item);
```

## UpdateAddonTree
Updates the internal cache when an entity changes

```csharp
  /// @param item: The entity that has changed
  void UpdateAddonTree(IEntity item);
```

## Clear
Clear the collection competely

```csharp
  void Clear();
```

## ToArray
Method is meant generate an array from this entity so that we can remove entities without worring about indexes

```csharp
  /// @returns Will return the generate array
  IEntity[] ToArray();
```