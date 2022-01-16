# IServiceProvider Extensions Methods

## AddEntity
Set of methods meant to add entities to the [entity-collection](/api/component-system/entity-collection) on [layers](/api/component-system/layer)

```csharp
  /// @param provider: The service provider we will use to find the collection and build out the object with
  /// @param layer: The layer id that should be used when adding the entity
  /// @param weight: The current weight of the layer being added
  /// @returns Will return the entity that was created and added to the collection
  public static IEntity AddEntity(this IServiceProvider provider, string layer = "", int weight = 0);

  /// @param provider: The service provider we will use to find the collection and build out the object with
  /// @param entity: The entity we are currently adding to the system
  /// @param weight: The current weight of the layer being added
  /// @returns Will return the entity that was created and added to the collection
  public static IEntity AddEntity(this IServiceProvider provider, IEntity entity, string layer = "", int weight = 0);
```

## AddSpriteFont
Adds a sprite font to the global scope

```csharp
  /// @param provider: The view provider we will use to find the collection and build out the object with
  /// @param key: The key to use for this font lookup
  /// @param font: The font we are anting to save globally
  public static void AddSpriteFont(this IServiceProvider provider, string key, SpriteFont font);
```

## AddTextureFont
Add a [texture font](/api/core/texture-font) to the global scope

```csharp
  /// @param provider: The view provider we will use to find the collection and build out the object with
  /// @param key: The key to use for this font lookup
  /// @param font: The font we are wanting to save globally
  public static void AddTextureFont(this IServiceProvider provider, string key, TextureFont font);
```