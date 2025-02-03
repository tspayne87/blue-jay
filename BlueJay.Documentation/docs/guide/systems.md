# Introduction
Systems are meant to be the implementation of the game.  The following is an example system:

```csharp
  /// <summary>
  /// Basic rendering system to draw a texture at a position
  /// </summary>
  public class RenderingSystem : IDrawSystem
  {
    /// <summary>
    /// The sprite batch to draw to the screen
    /// </summary>
    private readonly SpriteBatch _batch;

    /// Store the query meant to be used to iterate over all entities that need to be rendered by
    /// this system
    private readonly IQuery<PositionAddon, TextureAddon> _renderQuery;

    /// <summary>
    /// Constructor method is meant to build out the renderer system
    /// and inject the renderer for drawing
    /// </summary>
    /// <param name="batch">The sprite batch to draw to the screen</param>
    /// <param name="renderQuery">The query of entities that exist in the game</param>
    public RenderingSystem(SpriteBatch batch, IQuery<PositionAddon, TextureAddon> renderQuery)
    {
      _batch = batch;
      _renderQuery = renderQuery;
    }

    /// This is the 
    public void OnDraw()
    {
      _batch.Begin();

      foreach (var entity in _renderQuery)
      {
        var pc = entity.GetAddon<PositionAddon>();
        var tc = entity.GetAddon<TextureAddon>();

        if (tc.Texture != null)
        {
          _batch.Draw(tc.Texture, pc.Position, Color.White);
        }
      }

      _batch.End();
    }
  }
```
## Types
There are 2 different types of systems and both of them are setup as interfaces *IUpdateSystem*, *IDrawSystem*.
Each handles a different lifecycle in each frame of your game, they will be called in the order above.

### IUpdateSystem
Will be called before the each draw system and is use for the main lifecycle of your game.

### IDrawSystem
Will be called after the update system and is meant to draw entities to the screen.