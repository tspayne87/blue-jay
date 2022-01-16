# Introduction
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

    /// This is a scoped provider so all these will only be available in this
    /// scope and not others this will require developers to create base
    /// classes for common systems being added to views or they will need to
    /// be added for each one needed
    protected override void ConfigureProvider(IServiceProvider serviceProvider)
    {
      // Add Processor Systems
      serviceProvider.AddComponentSystem<ClearSystem>(Color.White);
      serviceProvider.AddComponentSystem<RenderingSystem>();
    }
  }
```