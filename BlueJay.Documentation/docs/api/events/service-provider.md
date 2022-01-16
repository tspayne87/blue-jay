# IServiceProvider Extensions Methods

## AddEventListener&lt;T, K&gt;
Will add an event listener to be called when the configured event is triggered.  Meant to handle objects
that inherit from the [EventListener&lt;T&gt;](/api/events/event-listener) abstract base class.

```csharp
  /// @typeparam T: The event listener implementation that should be used.
  /// @typeparam K: The event we are wanting to add the queue to.
  /// @param parameters: The constructor parameters that should be passed
  ///                    along to to object being created that does not
  ///                    exist in dependency injection.
  /// @returns Will return a disposable that can be disposed to remove
  ///          this event listener.
  public static IDisposable AddEventListener<T, K>(params object[] paramters);
```

## AddEventListener&lt;T&gt;
Will add an event listener that acts as a callback for a type of event and is meant to be used for quickly adding
event logic embeded into a view or other object.

```csharp
    /// @typeparam T: The event we are wanting to add the queue to.
    /// @param callback: The callback function that should be triggered for
    ///                  the event object
    /// @param target: The target object we want to process the listener onto
    /// @returns Will return a disposable that can be disposed to remove
  ///          this event listener
    public static IDisposable AddEventListener<T>(Func<T, bool> callback, object target = null);
```