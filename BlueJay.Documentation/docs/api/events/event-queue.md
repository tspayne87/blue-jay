# IEventQueue Interface
The event queue is how you will interact with the queue to dispatch events and to add event listeners

## DispatchEvent&lt;T&gt;
Meant to dispatch events meant to be processed in the next frame.  No events are processed in the
same frame they are triggered.

```csharp
    /// @typeparam T: The type of event we are working with
    /// @param evt: The event that is being triggered
    void DispatchEvent<T>(T evt, object target = null);
```
## AddEventListener&lt;T&gt;
Will add event listeners that will watch on events being dispatched into the system to process on.  This
is overloaded many times to give different options.

```csharp
    /// @typeparam T: The type of event we are working with
    /// @param handler: The handler when the event is fired
    IDisposable AddEventListener<T>(IEventListener<T> handler);

    /// @typeparam T: The type of event we are working with
    /// @param callback: The callback that should be called when the event
    ///                  listener is processed
    IDisposable AddEventListener<T>(Func<T, bool> callback);

    /// @typeparam T: The type of event we are working with
    /// @param callback: The callback that should be called when the event
    ///                  listener is processed
    IDisposable AddEventListener<T>(Func<T, object, bool> callback);

    /// @typeparam T: The type of event we are working with
    /// @param callback: The callback that should be called when the event
    ///                  listener is processed
    /// @param target: The target this callback should be attached to
    IDisposable AddEventListener<T>(Func<T, bool> callback, object target);

    /// @typeparam T: The type of event we are working with
    /// @param callback: The callback that should be called when the event
    ///                  listener is processed
    /// @param target: The target this callback should be attached to
    IDisposable AddEventListener<T>(Func<T, object, bool> callback, object target);
```