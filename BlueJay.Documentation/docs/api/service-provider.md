# IServiceProvider Extensions Methods

## AddSystem&lt;T, K&gt
Will add a system to be used for processing entities that exist in the system

```csharp
  /// @typeparam T: The current object we are adding to the service collection
  /// @param parameters: The constructor parameters that do not exists in D
  /// @returns Will return the system that was created and added to the collection
  public static T AddSystem<T>(params object[] parameters);
```

## SetStartView&lt;T, K&gt
Will set the starting view for the game itself and what the player will first see when booting
up the game itself.

```csharp
  /// @typeparam T: The current object we are adding to the view collection
  /// @returns Will return the system that was created and added to the collection
  public static T SetStartView<T>();
```