# Introduction
Systems are meant to be the implementation of the game, and are required to have an addon key that will
determine which entities will be processed by the system.  The following is an example system:

```csharp
  /// <summary>
  /// Basic rendering system to draw a texture at a position
  /// </summary>
  public class RenderingSystem : IDrawSystem, IDrawEntitySystem, IDrawEndSystem
  {
    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly SpriteBatch _batch;

    /// The addon helper is used to create the bitmask for the combined
    /// addons needed by this system
    public long Key => KeyHelper.Identifier<PositionAddon, TextureAddon>();

    /// The layers that this system will work on, if nothing is given it
    /// will work on all layers
    public List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor method is meant to build out the renderer system
    /// and inject the renderer for drawing
    /// </summary>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    public RenderingSystem(SpriteBatch batch)
    {
      _batch = batch;
    }

    /// Method gets called before the entity draw methods
    public void OnDraw()
    {
      _batch.Begin();
    }

    /// Method will be called for every entity that matches the key
    public void OnDraw(IEntity entity)
    {
      var pc = entity.GetAddon<PositionAddon>();
      var tc = entity.GetAddon<TextureAddon>();

      if (tc.Texture != null)
      {
        _batch.Draw(tc.Texture, pc.Position, Color.White);
      }
    }

    /// Method gets called after the entity draw methods
    public void OnDrawEnd()
    {
      _batch.End();
    }
  }
```
## Key
The key is the main way the system knows what entities it should be processing, meaning it is a type of filter.  Using the
KeyHelper.Identifier<...> will help with merging addons together to create a key to filter out entities that should not
be processed by this system.

## Layers
The layers act as a way to filter the system out even further based on the layer this system should be processing on.

## Types
There are 6 different types of systems and all of them are setup as interfaces *IUpdateSystem*, *IUpdateEntitySystem*,
*IUpdateEndSystem*, *IDrawSystem*, *IDrawEntitySystem*, and *IDrawEndSystem*.  Each handle a different lifecycle in each frame
of your game, they will be called in the order above.

### IUpdateSystem
Will be called before any entities are processed by this system, this acts as a starting point before any of the entities
are processed by this system.  Can only be called once each frame.

### IUpdateEntitySystem
Will be called for each entity that meets the systems criteria and can be called multiple times in one frame.

### IUpdateEndSystem
Will be called after all the entities are processed and should handle the cleanup after each entity is processed.  Can only
be called once each frame


### IDrawSystem
Will be called before any entities are processed, in a draw system that is mainly reserved for starting a batch sprite batch so
you're not processing one entity at a time. Can only be called once each frame.

### IDrawEntitySystem
Will be called for each entity that meets the systems criteria and can be called multiple times in one frame.  This is where
drawing each entity to the screen would happen.

### IDrawEndSystem
Will be called after all entities are processed, this is where you could end the sprite batch and send the data along to be
renderered.  Can only be called once each frame.