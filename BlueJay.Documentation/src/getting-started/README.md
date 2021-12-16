# Introduction
BlueJay is a framework that is meant to help with making games faster by building out common ideas on top of the MonoGame
Framework to get to building out the game faster.  Dependency Injection or DI is used in the project to decouple classes
from each other to allow for a greater range of customization.

## Features
- Addon/Entity System
- Event Queue
- Common Addon/Systems
- UI Framework

## Installation
### Package Manager
```bash
  Install-Package BlueJay
```

### .NET CLI
```bash
  dotnet add BlueJay
```

### Paket CLI
```bash
  paket add BlueJay
```

## Component System Game
The *ComponentSystemGame* is an abstract game class meant to bootstrap all the basic contents of the game class
and act like a *StartUp* file in a basic mvc app.  It also set up DI internaly and start the event queue for each
frame of the game.  It breaks up into to main functions *ConfigureServices* and *ConfigureProvider*.  *ConfigureServices*
allows the developer to add in *Singleton*, *Scoped*, and *Transient* objects into DI, where *ConfigureProvider* is meant
to allow for the *Singleton* objects to be manipulated on a global scale in the game.  Basic example is as follows:

```csharp
  public class Game1 : ComponentSystemGame
  {
    /// Bootstrap the basic game objects and where the content is located before ContentManger
    /// is initialized
    public Game1()
    {
      Content.RootDirectory = "Content";
    }

    /// Add custom items as Singleton, Scoped, and Transient
    protected override void ConfigureServices(IServiceCollection serviceCollection)
    {
      // Add singletons that should be in the system
      serviceCollection.AddSingleton<GlobalConfigurations>();

      // Adds the scoped services needed by the different views
      serviceCollection.AddScoped<GameService>();
    }

    /// Configure singleton objects that will be used in the game
    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Add Fonts that will be used in the system
      serviceProvider.AddSpriteFont("Default", _contentManager.Load<SpriteFont>("TestFont"));
      var fontTexture = _contentManager.Load<Texture2D>("Bitmap-Font");
      serviceProvider.AddTextureFont("Default", new TextureFont(fontTexture, 3, 24));

      // Set the view to load when starting the game
      serviceProvider.SetStartView<ExampleView>();
    }
  }
```

## Views
Views are meant to switch between different screens of the game for example the *Title Screen* and *Game Screen*, they
are not meant for overlays on the screen that should be accomplished inside the view itself.  The view is mainly a place
to store all the enities that will be used for the scene being rendered at the moment.  It can also be used as a way to handle
screen transitions.  Also, when a view is created a new DI scope is generated and all the *Scoped* objects will get a different
version generated at this time for them.  A basic example of using a view is as follows:

```csharp
  public class ExampleView : View
  {
    /// Inject the Content Manager since it is stored a singleton in DI
    public readonly ContentManager _contentManager;

    /// DI is used to create this title view with the following injected singleton
    public TitleView(ContentManager contentManager)
    {
      _contentManager = contentManager;
    }

    /// This is a scoped provider so all these will only be available in this scope and not others
    /// this will require developers to create base classes for common systems being added to views
    /// or they will need to be added for each one needed
    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Add Processor Systems
      serviceProvider.AddComponentSystem<ClearSystem>(Color.White);
      serviceProvider.AddComponentSystem<RenderingSystem>();
    }
  }
```

## Addons
Addons are data points that can be added to entities so that systems can filter on those types of addons
to break down all entities in the system to their basic level.  Addons should be small data points that handle
a piece the data for a piece of the game.  An example is the common *ColorAddon* as follows:

```csharp
  /// <summary>
  /// The color addon is for attaching color to an entity
  /// </summary>
  public struct ColorAddon : IAddon
  {
    /// <summary>
    /// The current color that should be used for the entity
    /// </summary>
    public Color Color;

    /// <summary>
    /// Constructor to build out the color addon
    /// </summary>
    /// <param name="color">The current color that should be assigned</param>
    public ColorAddon(Color color)
    {
      Color = color;
    }

    /// <summary>
    /// Overridden to string method is meant to print out a nice version of the
    /// addon for debugging purposes
    /// </summary>
    /// <returns>Will return a debug string</returns>
    public override string ToString()
    {
      return $"Color | R: {Color.R}, G: {Color.G}, B: {Color.B}, A: {Color.A}";
    }
  }
```

_*Note:*_ Only 64 different addons can be used in a game

## Systems
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

    /// The addon helper is used to create the bitmask for the combined addons needed by this system
    public long Key => AddonHelper.Identifier<PositionAddon, TextureAddon>();

    /// The layers that this system will work on, if nothing is given it will work on all layers
    public List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor method is meant to build out the renderer system and inject
    /// the renderer for drawing
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

## Entities
Entities will store the list of addons that define the entity in the game.  This could entail a player, enemy, sprite, background, or
foreground, pretty much anything that is handled by the systems and thus by the game.  When an addon needs to be updated the entity
has exposed Update(IAddon addon) method that will allow for updating an addon in the entity.  If the addon is not known to be on the entity
or it is Upsert(IAddon addon)  should be used which will try to update the addon and if that fails will add the addon to the entity.  Also,
Entities cannot store more than one type of an entity in their list.  Example useage is as follows:

```csharp
  var entity = provider.AddEntity<Entity>(/* Layer Name */);
  entity.Add(new BoundsAddon(0, 0, 0, 0));

  // Gets a specific addon on the entity, this will also return a blank struct of the type which could be upserted
  // if changes were made
  var ba = entity.GetAddon<BoundsAddon>();
  ba.Bounds.X = 10;
  ba.Bounds.Y = 10;

  // Will update the addon on the entity, if this is not done whatever changes will be lost after the function scope
  entity.Update(ba);

  // Upsert example
  var upsertEntity = provider.AddEntity<Entity>(/* Layer Name */);

  // Gets a specific addon on the entity
  var ba = upsertEntity.GetAddon<BoundsAddon>();
  ba.Bounds.X = 10;
  ba.Bounds.Y = 10;

  // Will update the addon on the entity, if this is not done whatever changes will be lost after the function scope
  upsertEntity.Upsert(ba); // This will add the addon to the entity without having to worry about if it has been added or not
```

## Factories
Factories are the suggested ways to create entities, adding extension methods onto *IServiceProvider* interface so that entities
can be created and addons be added to the entity created.  An example of creating a ball entity to a game is as follows:

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
      var entity = provider.AddEntity<Entity>(LayerNames.BallLayer);
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