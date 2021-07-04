# Introduction
BlueJay is a framework that is meant to help with making games faster by building out common ideas on top of the MonoGame
Framework to get to building out the game faster.  Dependency Injection or DI is used in the project to decouple classes
from each other to allow for a greater range of customization.

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
      serviceCollection.AddSingleton<SpriteBatch>();
    }

    /// Configure singleton objects that will be used in the game
    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Add Fonts that will be used in the system
      serviceProvider.AddSpriteFont("Default", _contentManager.Load<SpriteFont>("TestFont"));
      var fontTexture = _contentManager.Load<Texture2D>("Bitmap-Font");
      serviceProvider.AddTextureFont("Default", new TextureFont(fontTexture, 3, 24));

      // Add Renderers that will be used in the system
      ServiceProvider.AddRenderer<Renderer>("Default");
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
  public class TitleView : View
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
      serviceProvider.AddComponentSystem<RenderingSystem>("Default");
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
  public class ColorAddon : Addon<ColorAddon>
  {
    /// <summary>
    /// The current color that should be used for the entity
    /// </summary>
    public Color Color;

    /// <summary>
    /// Basic contructor is meant to build out a default addon for color
    /// </summary>
    public ColorAddon()
      : this(Color.White) { }


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
  public class RenderingSystem : ComponentSystem
  {
    /// <summary>
    /// The renderer to draw textures to the screen
    /// </summary>
    private readonly IRenderer _renderer;

    /// <summary>
    /// The Selector to determine that Position and Texture addons are needed
    /// for this system
    /// </summary>
    public override long Key => PositionAddon.Identifier | TextureAddon.Identifier;

    /// <summary>
    /// The current layers that this system should be attached to
    /// </summary>
    public override List<string> Layers => new List<string>();

    /// <summary>
    /// Constructor method is meant to build out the renderer system and inject
    /// the renderer for drawing
    /// </summary>
    /// <param name="renderer">The renderer that should be used one draw</param>
    /// <param name="rendererCollection">The collection of renderers that exist in the system</param>
    public RenderingSystem(string renderer, RendererCollection rendererCollection)
    {
      _renderer = rendererCollection[renderer];
    }

    /// <summary>
    /// Draw method is meant to draw the entity to the screen based on the texture
    /// and position of the entity
    /// </summary>
    /// <param name="delta">The current delta for this frame</param>
    /// <param name="entity">The current entity that should be drawn</param>
    public override void OnDraw(IEntity entity)
    {
      var pc = entity.GetAddon<PositionAddon>();
      var tc = entity.GetAddon<TextureAddon>();

      _renderer.Draw(tc.Texture, pc.Position);
    }
  }
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
      entity.Add<BoundsAddon>(new Rectangle(Point.Zero, new Point(9, 9)));
      entity.Add<VelocityAddon>(Vector2.Zero);
      entity.Add<TypeAddon>(EntityType.Ball);
      entity.Add<TextureAddon>(texture);
      entity.Add<ColorAddon>(Color.Black);
      entity.Add<BallActiveAddon>();
      return entity;
    }
  }
```