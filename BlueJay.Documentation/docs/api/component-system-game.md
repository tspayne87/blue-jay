# ComponentSystemGame
Extension class on the game object from monogame to create the container for DI and setup the view
architecture for the game to make transitions between views that need to be rendered.

## ConfigureServices
Method is meant to give the developer a chance to configure any singletons in the system that was not
already configured by factories or by the provider itself.

```csharp
  /// @param serviceCollection: The current service collection
  protected abstract void ConfigureServices(IServiceCollection serviceCollection);
```

## ConfigureProvider
Method is meant to add extra configurations for the DI container

```csharp
  /// @param serviceProvider: The service provider that has been created from the collection
  protected abstract void ConfigureProvider(IServiceProvider serviceProvider);
```