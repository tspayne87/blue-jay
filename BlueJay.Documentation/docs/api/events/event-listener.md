# EventListener&lt;T&gt;
The event listener abstract class is meant to be inherited from to allow for more control
over how the listener is configured by the event queue

## Process
Process method will be called when this event listener needs to handle an event that was
triggered

```csharp
    /// @param evt: The current event object that was triggered
    public abstract void Process(IEvent<T> evt);
```

## ShouldProcess
If this event handle should do extra processing to determine if we should be processing the
current event

```csharp
    /// @param evt: The event that is being processed
    /// @returns Will return a boolean determining if we should process the event listener
    public virtual bool ShouldProcess(IEvent evt);
```