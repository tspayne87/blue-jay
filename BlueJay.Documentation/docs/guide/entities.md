## Entities
Entities will store the list of addons that define the entity in the game.  This could entail a player, enemy, sprite, background,
foreground, or pretty much anything that is handled by the systems and thus by the game.  When an addon needs to be updated the entity
has an exposed Update(IAddon addon) method that will allow for updating an addon in the entity.  If the addon is not known to be on the entity
or it is Upsert(IAddon addon) should be used.  Upsert(IAddon addon will try to update the addon, and if that fails, will add the addon to the entity.
Entities cannot store more than one type of an entity in their list.  Example usage is as follows:

```csharp
  var entity = provider.AddEntity(/* Layer Name */);
  entity.Add(new BoundsAddon(0, 0, 0, 0));

  // Gets a specific addon on the entity, this will also return a blank struct of the type which could be upserted
  // if changes were made
  var ba = entity.GetAddon<BoundsAddon>();
  ba.Bounds.X = 10;
  ba.Bounds.Y = 10;

  // Will update the addon on the entity, if this is not done whatever changes will be lost after the function scope
  entity.Update(ba);

  // Upsert example
  var upsertEntity = provider.AddEntity(/* Layer Name */);

  // Gets a specific addon on the entity
  var ba = upsertEntity.GetAddon<BoundsAddon>();
  ba.Bounds.X = 10;
  ba.Bounds.Y = 10;

  // Will update the addon on the entity, if this is not done whatever changes will be lost after the function scope
  upsertEntity.Upsert(ba); // This will add the addon to the entity without having to worry about if it has been added or not
```

## Factories
Factories are the suggested ways to create entities, adding extension methods onto the *IServiceProvider* interface so that entities
can be created and addons can be added to the entity created.  An example of creating a ball entity to a game is as follows:

```csharp
  public static class BallFactory
  {
    /// <summary>
    /// Factory method is meant to create an entity and add various addons to the entity that represents
    /// the ball in the game
    /// </summary>
    /// <param name="provider">The service provider we need to add the entities and systems to</param>
    /// <param name="texture">The ball texture that should be used</param>
    /// <returns>The entity that was created</returns>
    public static IEntity AddBall(this IServiceProvider provider, Texture2D texture)
    {
      var entity = provider.AddEntity(LayerNames.BallLayer);
      entity.Add(new BoundsAddon(0, 0, 9, 9));
      entity.Add(new VelocityAddon(Vector2.Zero));
      entity.Add(new TypeAddon(EntityType.Ball));
      entity.Add(new TextureAddon(texture));
      entity.Add(new ColorAddon(Color.Black));
      entity.Add(new BallActiveAddon());
      return entity;
    }
  }
```