# Introduction
BlueJay is a framework that is meant to help with making games faster by building out common ideas on top of the MonoGame
Framework to get to building out the game faster.  Dependency Injection or DI is used in the project to decouple classes
from each other to allow for a greater range of customization. To get your MonoGame Environment setup you can see their
[Getting Started](https://docs.monogame.net/articles/getting_started/0_getting_started.html) guide.

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

## Setup
After creating the MonoGame project in Visual Studios you should have a soltion that looks like the following:

![Solution Example](/Solution_Example.png)

Replace the *Game1.cs* contents with the following:
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
      serviceProvider.AddTextureFont("Default", new TextureFont(fontTexture, 3, 24));

      // Set the view to load when starting the game
      serviceProvider.SetStartView<ExampleView>();
    }
  }
```

The *ComponentSystemGame* is an abstract game class meant to bootstrap all the basic contents of the game class
and act like a *StartUp* file in a basic mvc app.  It also set up DI internaly and start the event queue for each
frame of the game.  It breaks up into to main functions *ConfigureServices* and *ConfigureProvider*.  *ConfigureServices*
allows the developer to configure the *Singleton*, *Scoped*, and *Transient* objects for DI. However, the *ConfigureProvider* is meant
to configure various global objects before the game bootstraps into starting up.  This would also be the place where you
would setup your starting view.