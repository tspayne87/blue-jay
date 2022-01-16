# ILayerCollection interface
The layer collection is a list of [layers](/api/component-system/layer) that exist in the system.  Each layer
is processed by their weight in this collection, and the lower the number the earlier it will be processed
by systems.  Each layer has an [entity collection](/api/component-system/entity-collection), representing all
entities in each layer.

## Count
The current number of layers that exist in the collection

## this[int i]
Getter to get a layer by its position in the collection

## this[string id]
Getter to get a layer by its name in the collection

## AddEntity
Will add an entity into a collection

```csharp
  /// @param entity: The entity we are currently adding
  /// @param layer: The layer we are working with
  /// @param weight: The weight of this layer so it is ordered correctly
  void AddEntity(IEntity entity, string layer = "", int weight = 0);
```

## RemoveEntity
Will remove an entity from a collection

```csharp
  /// @param entity: The entity we need to remove
  void RemoveEntity(IEntity entity);
```

## Add
Will add a layer into the collection

```csharp
  /// @param layer: The layer we are working with
  /// @param weight: The weight of this layer so it is ordered correctly
  void Add(string layer, int weight = 0);
```

## Contains
Determines if a layer exists in the collection

```csharp
  /// @param layer: The layer we are currently looking for
  /// @returns Will return true or false based on if the layer exists in the collection
  bool Contains(string layer);
```