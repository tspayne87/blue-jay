# IViewCollection interface
The collection of views that exist in the system, these are loaded in dynamically based on when they are
set as current during implmentation.

## Current
The current view that should be processed by the game

## SetCurrent&lt;T&gt;
Will set the current view that should be processing by the system

```csharp
  /// @typeparam T: The view we need to switch too
  /// @returns Will return the object based on the type given
  T SetCurrent<T>();
```